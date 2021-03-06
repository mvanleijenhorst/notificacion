using System;

namespace NotificacionApp.Controllers.Teachers.Models
{
    /// <summary>
    /// View teacher response.
    /// </summary>
    public record ViewTeacherResponse(Guid Id, string Name, string Username);
}
