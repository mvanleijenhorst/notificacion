﻿using System;
using NotificacionApp.Controllers.Common;

namespace NotificacionApp.Controllers.Teachers.Models
{
    /// <summary>
    /// Get teacher request.
    /// </summary>
    public record GetStudentRequest(Guid Id)
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
    }
}
