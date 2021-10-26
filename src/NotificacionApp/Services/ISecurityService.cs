using NotificacionApp.Common;
using System.Threading.Tasks;

namespace NotificacionApp.Services
{
    /// <summary>
    /// Security service.
    /// </summary>
    public interface ISecurityService
    {
        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="userRole">User role</param>
        /// <returns><see cref="SecurityToken"/></returns>
        Task<SecurityToken?> LoginAsync(string username, string password, UserRole userRole);
    }
}
