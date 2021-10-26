namespace NotificacionApp.Common
{
    /// <summary>
    /// System user.
    /// </summary>
    public interface ISystemUser
    {
        /// <summary>
        /// Username.
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        string Password { get; set; } 

        /// <summary>
        /// User role.
        /// </summary>
        UserRole UserRole { get; }
    }
}
