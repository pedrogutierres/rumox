using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Rumox.API.Configurations
{
    public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var declaringAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true);
            var methodAttributes = context.MethodInfo.GetCustomAttributes(true);

            var isAuthorized = declaringAttributes.OfType<AuthorizeAttribute>().Any() || methodAttributes.OfType<AuthorizeAttribute>().Any();
            var allowAnonymous = declaringAttributes.OfType<AllowAnonymousAttribute>().Any() || methodAttributes.OfType<AllowAnonymousAttribute>().Any();

            if (!isAuthorized || allowAnonymous)
                return;

            operation.Security.Add(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
        }
    }
}