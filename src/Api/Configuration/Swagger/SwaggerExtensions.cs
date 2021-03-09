using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodVault.Api.Configuration.Swagger
{
    internal static class SwaggerExtensions
    {
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
                            //Scopes = new Dictionary<string, string>
                            //{
                            //    {"foodvault.api", "foodvault.api"}
                            //}
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
            });

            return self;
        }

        internal static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder self)
        {
            self.UseSwagger();
            self.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "foodvault API v1");
                c.OAuthConfigObject = new OAuthConfigObject()
                {
                    ClientId = "ro.client",
                    ClientSecret = "dummy",
                };
            });

            return self;
        }
    }
}
