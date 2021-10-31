using System;
using Microsoft.AspNetCore.Mvc;
using NotificacionApp.Controllers.Common;
using NotificacionApp.Domain;

namespace NotificacionApp.Controllers.Teachers.Models
{
    /// <summary>
    /// Edit teacher request.
    /// </summary>
    public record UpdateTeacherRequest([FromQuery] Guid Id, string Name, string Username)
    {
        /// <summary>
        /// Validate request.
        /// </summary>
        /// <returns><see cref="ValidationResult"/></returns>
        internal ValidationResult Validate()
        {
            var result = new ValidationResult();
            result.IsRequired(nameof(Id), Id);
            result.IsRequired(nameof(Name), Name);
            result.IsRequired(nameof(Username), Username);

            result.HasMaxLenght(nameof(Name), Name, ValidationConstants.NormalFieldLength);
            result.HasMaxLenght(nameof(Username), Username, ValidationConstants.NormalFieldLength);
            return result;
        }

        /// <summary>
        /// Convert request to domain object.
        /// </summary>
        /// <returns>Domain object</returns>
        internal Teacher ToDomain()
        {
            return new Teacher(Id, Name, Username);
        }
    };
}
