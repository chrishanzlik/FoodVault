using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dapper;
using FoodVault.Api.Configuration.ExecutionContext;
using FoodVault.Api.Modules.Storages;
using FoodVault.Framework.Application.FileUploads;
using FoodVault.Framework.Infrastructure.Database;
using FoodVault.Modules.Storage.Infrastructure;
using FoodVault.Modules.Storage.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
            ConfigureDapperTypeHandlers();

            services.AddControllers();

            services.Configure<FileUploadSettings>(Configuration.GetSection(nameof(FileUploadSettings)));
            services.AddSingleton<IFileUploadSettings>(x => x.GetService<IOptions<FileUploadSettings>>().Value);

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
        }

        private void InitializeModules(ILifetimeScope container)
        {
            var httpContextAccessor = container.Resolve<IHttpContextAccessor>();
            var fileUploadSettings = container.Resolve<IFileUploadSettings>();
            var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);

            StorageStartup.Initialize(
                Configuration["ConnectionString"],
                executionContextAccessor,
                fileUploadSettings,
                container.Resolve<ILogger<StorageModule>>(),
                null);
        }

        private void ConfigureDapperTypeHandlers()
        {
            //TODO: Move to module
            SqlMapper.AddTypeHandler(new NullableDateTimeUtcDapperHandler());
            SqlMapper.AddTypeHandler(new DateTimeUtcDapperHandler());
        }
    }
}
