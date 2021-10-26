using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using NotificacionApp.Controllers.Security;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NotificacionApp.Common
{
    /// <summary>
    /// Security operation filter.
    /// </summary>
    public class SecurityTokenOperationFilter : IOperationFilter
    {
        /// <inheritdoc/>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context == null || context.MethodInfo == null || context.MethodInfo.DeclaringType == null)
            {
                return;
            }

            operation.Parameters ??= new List<OpenApiParameter>();
            var attributes = context.MethodInfo.DeclaringType.GetCustomAttributes(typeof(AuthorizeAttribute), true);

            if (attributes.Any())
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = SecurityConstant.SecurityTokenHeader,
                    In = ParameterLocation.Header,
                    Description = "SecurityToken",
                    Required = true
                });
            }
        }
    }
}
