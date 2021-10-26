﻿using System;
using Microsoft.AspNetCore.Mvc;
using NotificacionApp.Controllers.Common;

namespace NotificacionApp.Controllers.Students.Models
{
    /// <summary>
    /// Remove student request.
    /// </summary>
    public record RemoveStudentRequest([FromQuery] Guid Id)
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
