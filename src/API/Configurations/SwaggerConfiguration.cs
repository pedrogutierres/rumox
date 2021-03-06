﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Rumox.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("doc", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Rumox API",
                    Description = "API do projeto Rumox",
                    TermsOfService = new Uri("https://rumoh.io/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Rumoh.IO",
                        Email = "contato@rumoh.io",
                        Url = new Uri("https://rumoh.io/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "CC BY-NC-ND",
                        Url = new Uri("https://creativecommons.org/licenses/by-nc-nd/4.0/legalcode")
                    }
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor inserir o token JWT no campo",
                    Scheme = "Bearer",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.ApiKey
                });

                // Para adicionar JWT em todas as rotas
                //s.AddSecurityRequirement(new OpenApiSecurityRequirement {
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            }
                //        },
                //        new string[] { }
                //    }
                //});

                s.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            app.UseSwagger();

            if (!env.IsProduction())
            {
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/doc/swagger.json", "Rumox API");
                });
            }

            return app;
        }
    }
}