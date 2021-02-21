using Autofac;
using Autofac.Extensions.DependencyInjection;
using FoodVault.Api.Configuration.ExecutionContext;
using FoodVault.Api.Modules.Storages;
using FoodVault.Api.Modules.UserAccess;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Modules.Storage.Infrastructure;
using FoodVault.Modules.Storage.Infrastructure.Configuration;
using FoodVault.Modules.UserAccess.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "foodvault storage", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var autofacContainer = app.ApplicationServices.GetAutofacRoot();

            InitializeModules(autofacContainer);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "foodvault storage v1"));
            }

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

        private void InitializeModules(ILifetimeScope container)
        {
            var httpContextAccessor = container.Resolve<IHttpContextAccessor>();
            var linkGenerator = container.Resolve<LinkGenerator>();

            var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);
            var fileUploadSettings = Configuration.GetSection(nameof(FileUploadSettings)).Get<FileUploadSettings>();

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
                container.Resolve<ILogger<UserAccessModule>>(),
                null);
        }
    }
}
