using System;
using Microsoft.AspNetCore.Mvc;
using NotificacionApp.Common;
using NotificacionApp.Controllers.Common;
using NotificacionApp.Domain;

namespace NotificacionApp.Controllers.Students.Models
{
    /// <summary>
    /// Update question request.
    /// </summary>
    public record UpdateQuestionRequest([FromQuery] Guid Id, string Room, string Table, string Topic, Guid TeacherId, Guid StudentId, string Response)
    {
        /// <summary>
        /// Validate request.
        /// </summary>
        /// <returns><see cref="ValidationResult"/></returns>
        internal ValidationResult Validate()
        {
            var result = new ValidationResult();
            result.IsRequired(nameof(Id), Id);
            result.IsRequired(nameof(TeacherId), TeacherId);
            result.IsRequired(nameof(StudentId), StudentId);
            result.IsRequired(nameof(Room), Room);
            result.IsRequired(nameof(Table), Table);
            result.IsRequired(nameof(Topic), Topic);
            result.HasMaxLenght(nameof(Room), Room, ValidationConstants.NormalFieldLength);
            result.HasMaxLenght(nameof(Table), Table, ValidationConstants.NormalFieldLength);
            result.HasMaxLenght(nameof(Topic), Topic, ValidationConstants.NormalFieldLength);
            if (!string.IsNullOrWhiteSpace(Response))
            {
                result.HasValidEnum(nameof(Response), Response, typeof(QuestionResponse));
            }
            return result;
        }

        /// <summary>
        /// Convert request to domain object.
        /// </summary>
        /// <returns>Domain object</returns>
        internal Question ToDomain(Teacher teacher, Student student)
        {
            QuestionResponse? response = null;
            if (!string.IsNullOrWhiteSpace(Response))
            {
                response = Enum.Parse<QuestionResponse>(Response);
            }
            return new(Id, Room, Table, Topic, student, teacher, response);
        }
    };
}
