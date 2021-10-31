using System;

namespace NotificacionApp.Common
{
    /// <summary>
    /// Session user.
    /// </summary>
    public record SessionUser(Guid Id, string Name, UserRole UserRole);
}
