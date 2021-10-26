using System;

namespace NotificacionApp.Controllers.Students.Models
{
    /// <summary>
    /// View student response.
    /// </summary>
    public record ViewStudentResponse(Guid Id, string Name, string Username);
}
