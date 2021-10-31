using System;
using Microsoft.AspNetCore.Mvc;
using NotificacionApp.Controllers.Common;

namespace NotificacionApp.Controllers.Students.Models
{
    /// <summary>
    /// Update password of student request.
    /// </summary>
    public record UpdateStudentPasswordRequest([FromQuery] Guid Id, string Password)
    {
        /// <summary>
        /// Validate request.
        /// </summary>
        /// <returns><see cref="ValidationResult"/></returns>
        internal ValidationResult Validate()
        {
            var result = new ValidationResult();
            result.IsRequired(nameof(Id), Id);
            result.IsRequired(nameof(Password), Password);
            return result;
        }
    }
}
