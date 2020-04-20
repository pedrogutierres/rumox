using Rumox.API.ResponseType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Rumox.API.Configurations
{
    public static class ControllersConfiguration
    {
        public static void AddConrollersConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory =
                        (context => new BadRequestObjectResult(new ResponseError
                        {
                            Errors = context.ModelState.Values.SelectMany(p => p.Errors).Select(erro => erro.Exception?.Message ?? erro.ErrorMessage)
                        }));
                });
        }
    }
}