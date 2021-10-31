namespace NotificacionApp.Common
{
    /// <summary>
    /// Session manager.
    /// </summary>
    public class SessionManager : ISessionManager
    {
        /// <summary>
        /// Session user.
        /// </summary>
        public SessionUser? SessionUser { get; set; }
    }
}
