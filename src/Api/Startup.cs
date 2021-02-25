using Autofac;
using Autofac.Extensions.DependencyInjection;
using FoodVault.Api.Configuration.ExecutionContext;
using FoodVault.Api.IdentityServer;
using FoodVault.Api.Modules.Storages;
using FoodVault.Api.Modules.UserAccess;
using FoodVault.Framework.Application.Emails;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Infrastructure.Emails;
using FoodVault.Modules.Storage.Infrastructure;
using FoodVault.Modules.UserAccess.Infrastructure;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace FoodVault.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureIdentityServer(services);

            services.AddControllers();

            services.AddHttpContextAccessor();

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "foodvault API", Version = "v1" });
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var autofacContainer = app.ApplicationServices.GetAutofacRoot();

            ConfigureModules(autofacContainer);

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "foodvault API v1");
                });
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
                .AddInMemoryPersistedGrants()
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, x =>
                {
                    x.Authority = "https://localhost:44305";
                    x.ApiName = "foodvault.api";
                    x.RequireHttpsMetadata = false;
                });
        }

        private void ConfigureModules(ILifetimeScope container)
        {
            var httpContextAccessor = container.Resolve<IHttpContextAccessor>();
            var linkGenerator = container.Resolve<LinkGenerator>();

            var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);
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
