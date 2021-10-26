using NotificacionApp.Controllers.Common;
using NotificacionApp.Domain;

namespace NotificacionApp.Controllers.Students.Models
{
    /// <summary>
    /// Add student request.
    /// </summary>
    public record AddStudentRequest(string Name, string Username)
    {
        /// <summary>
        /// Validate request.
        /// </summary>
        /// <returns><see cref="ValidationResult"/></returns>
        internal ValidationResult Validate()
        {
            var result = new ValidationResult();
            result.IsRequired(nameof(Name), Name);
            result.IsRequired(nameof(Username), Username);
            result.HasMaxLenght(nameof(Name), Name, ValidationConstants.NormalFieldLength);
            result.HasMaxLenght(nameof(Username), Name, ValidationConstants.NormalFieldLength);
            return result;
        }

        /// <summary>
        /// Convert request to domain object.
        /// </summary>
        /// <returns>Domain object</returns>
        internal Student ToDomain()
        {
            return new(Name, Username);
        }
    }
}
