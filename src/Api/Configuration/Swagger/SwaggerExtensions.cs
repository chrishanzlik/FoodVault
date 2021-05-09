using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FoodVault.Api.Configuration.Swagger
{
    internal static class SwaggerExtensions
    {
        /// <summary>
        /// Adds a preconfigured swagger instance to the service collection.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        internal static IServiceCollection AddSwagger(this IServiceCollection self)
        {
            self.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "foodvault API", Version = "v1" });

                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:44305/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:44305/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"foodvault.api", "FoodVault API"}
                            }
                        }
                    },
                    In = ParameterLocation.Header,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,

                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[] {}
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });

            return self;
        }

        /// <summary>
        /// Configures swagger within the request pipeline.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        internal static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder self)
        {
            self.UseSwagger();
            self.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "foodvault API v1");
                c.OAuthConfigObject = new OAuthConfigObject()
                {
                    ClientId = "ro.client",
                    ClientSecret = "dummy",
                    Scopes = new[] { "foodvault.api" }
                };
            });

            return self;
        }
    }
}
