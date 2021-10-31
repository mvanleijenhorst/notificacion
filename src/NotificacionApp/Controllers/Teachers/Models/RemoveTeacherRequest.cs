using System;
using Microsoft.AspNetCore.Mvc;
using NotificacionApp.Controllers.Common;

namespace NotificacionApp.Controllers.Teachers.Models
{
    /// <summary>
    /// Remove teacher request.
    /// </summary>
    public record RemoveTeacherRequest([FromQuery] Guid Id)
    {
        /// <summary>
        /// Validate request.
        /// </summary>
        /// <returns><see cref="ValidationResult"/></returns>
        internal ValidationResult Validate()
        {
            var result = new ValidationResult();
            result.IsRequired(nameof(Id), Id);
            return result;
        }
    };
}
