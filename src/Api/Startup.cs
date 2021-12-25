using Autofac;
using Autofac.Extensions.DependencyInjection;
using FoodVault.Api.Configuration.Authorization;
using FoodVault.Api.Configuration.ExecutionContext;
using FoodVault.Api.Configuration.Swagger;
using FoodVault.Api.Configuration.Validation;
using FoodVault.Api.IdentityServer;
using FoodVault.Api.Modules.Storages;
using FoodVault.Api.Modules.UserAccess;
using FoodVault.Framework.Application;
using FoodVault.Framework.Application.Emails;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Domain;
using FoodVault.Framework.Infrastructure.Emails;
using FoodVault.Modules.Storage.Infrastructure;
using FoodVault.Modules.UserAccess.Infrastructure;
using Hellang.Middleware.ProblemDetails;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FoodVault.Api
{
    /// <summary>
    /// API startup.
    /// </summary>
    internal class Startup
    {
        private IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureIdentityServer(services);

            services.AddControllers();

            services.AddHttpContextAccessor();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

            services.AddProblemDetails(x =>
            {
                x.IncludeExceptionDetails = (ctx, ex) => Environment.IsDevelopment();
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<DomainRuleValidationException>(ex => new DomainRuleValidationExceptionProblemDetails(ex));
            });

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(HasPermissionAttribute.HasPermissionPolicyName, policyBuilder =>
                {
                    policyBuilder.Requirements.Add(new HasPermissionAuthorizationRequirement());
                    policyBuilder.AddAuthenticationSchemes(IdentityServerAuthenticationDefaults.AuthenticationScheme);
                });
            });

            services.AddScoped<IAuthorizationHandler, HasPermissionAuthorizationHandler>();

            services.AddSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var autofacContainer = app.ApplicationServices.GetAutofacRoot();

            ConfigureModules(autofacContainer);

            app.UseProblemDetails();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.ConfigureSwagger();
            }

            app.UseIdentityServer();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new StorageAutofacModule());
            containerBuilder.RegisterModule(new UserAccessAutofacModule());
        }

        private void ConfigureIdentityServer(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApis())
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddInMemoryApiScopes(IdentityServerConfiguration.GetApiScopes())
                .AddInMemoryPersistedGrants()
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, (IdentityServerAuthenticationOptions x) =>
                {
                    x.Authority = "https://localhost:44305";
                    x.ApiName = "foodvault.api";
                    x.RequireHttpsMetadata = false;
                });
        }

        private void ConfigureModules(ILifetimeScope container)
        {
            var httpContextAccessor = container.Resolve<IHttpContextAccessor>();
            var executionContextAccessor = container.Resolve<IExecutionContextAccessor>();
            var linkGenerator = container.Resolve<LinkGenerator>();

            var fileUploadSettings = Configuration.GetSection(nameof(FileUploadSettings)).Get<FileUploadSettings>();
            var mailerSettings = Configuration.GetSection(nameof(MailerSettings)).Get<MailerSettings>();
            var mailer = new SmtpEmailSender(mailerSettings);

            var storageModuleUrlBuilder = new StorageModuleUrlBuilder(httpContextAccessor, linkGenerator);
            var userAccessModuleUrlBuilder = new UserAccessModuleUrlBuilder(httpContextAccessor, linkGenerator);

            StorageModule.Initialize(
                Configuration["ConnectionString"],
                executionContextAccessor,
                fileUploadSettings,
                storageModuleUrlBuilder,
                container.Resolve<ILogger<StorageModule>>(),
                null);

            UserAccessModule.Initialize(
                Configuration["ConnectionString"],
                executionContextAccessor,
                userAccessModuleUrlBuilder,
                mailer,
                container.Resolve<ILogger<UserAccessModule>>(),
                null);
        }
    }
}
