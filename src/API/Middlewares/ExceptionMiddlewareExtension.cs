using Core.Domain.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rumox.API.ResponseType;
using System.Net;
using System.Text.Json;

namespace Rumox.API.Middlewares
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    ResponseError error;

                    var logger = loggerFactory.CreateLogger("ExceptionHandler");

                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        logger.LogCritical($"Erro interno do servidor: {exceptionHandlerFeature.Error.AgruparTodasAsMensagens()}");

                        error = new ResponseError(
                                   "Erro interno no servidor",
                                   null,
                                   StatusCodes.Status500InternalServerError,
                                   context.Request.HttpContext.Request.Path,
                                   "Erro interno no servidor, contate o suporte");
                    }
                    else
                    {
                        logger.LogCritical($"Erro inesperado não encontrou a exception");

                        error = new ResponseError(
                                   "Erro inesperado no servidor",
                                   null,
                                   StatusCodes.Status500InternalServerError,
                                   context.Request.HttpContext.Request.Path,
                                   "Erro interno no servidor, contate o suporte");
                    }

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(JsonSerializer.Serialize(error, 
                        new JsonSerializerOptions { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, DictionaryKeyPolicy = JsonNamingPolicy.CamelCase }));
                });
            });
        }
    }
}
