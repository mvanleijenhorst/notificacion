using System;
using NotificacionApp.Common;
using NotificacionApp.Controllers.Common;
using NotificacionApp.Domain;

namespace NotificacionApp.Controllers.Students.Models
{
    /// <summary>
    /// Add question request.
    /// </summary>
    public record AddQuestionRequest(string Room, string Table, string Topic, Guid TeacherId)
    {
        /// <summary>
        /// Validate request.
        /// </summary>
        /// <returns><see cref="ValidationResult"/></returns>
        internal ValidationResult Validate()
        {
            var result = new ValidationResult();
            result.IsRequired(nameof(TeacherId), TeacherId);
            result.IsRequired(nameof(Room), Room);
            result.IsRequired(nameof(Table), Table);
            result.IsRequired(nameof(Topic), Topic);
            result.HasMaxLenght(nameof(Room), Room, ValidationConstants.NormalFieldLength);
            result.HasMaxLenght(nameof(Table), Table, ValidationConstants.NormalFieldLength);
            result.HasMaxLenght(nameof(Topic), Topic, ValidationConstants.NormalFieldLength);
            return result;
        }

        /// <summary>
        /// Convert request to domain object.
        /// </summary>
        /// <returns>Domain object</returns>
        internal Question ToDomain(Teacher teacher, Student student)
        {
            return new(Room, Table, Topic, student, teacher);
        }
    }
}
