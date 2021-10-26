using System;

namespace NotificacionApp.Common
{
    public record SessionUser(Guid Id, string Name, UserRole UserRole);
}
