﻿using System;
using NotificacionApp.Controllers.Common;

namespace NotificacionApp.Controllers.Teachers.Models
{
    /// <summary>
    /// Update password of teacher request.
    /// </summary>
    public record UpdateStudentPasswordRequest(Guid Id, string Password)
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
