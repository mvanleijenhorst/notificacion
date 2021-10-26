using System;

namespace NotificacionApp.Controllers.Teachers.Models
{
    /// <summary>
    /// View teacher response.
    /// </summary>
    public record ViewStudentResponse(Guid Id, string Name, string Username);
}
