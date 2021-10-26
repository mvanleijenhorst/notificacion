using NotificacionApp.Common;
using NotificacionApp.Controllers.Common;

namespace NotificacionApp.Controllers.Security.Models
{
    /// <summary>
    /// Login request.
    /// </summary>
    public record LoginRequest(string Username, string Password, UserRole Role)
    {
        /// <summary>
        /// Validate request.
        /// </summary>
        /// <returns></returns>
        public ValidationResult Validate()
        {
            ValidationResult result = new ValidationResult();
            result.IsRequired(nameof(Username), Username);
            result.IsRequired(nameof(Password), Password);
            return result;
        }
    }
}
