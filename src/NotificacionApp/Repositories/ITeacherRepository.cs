using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotificacionApp.Domain;

namespace NotificacionApp.Repositories
{
    /// <summary>
    /// Student repository.
    /// </summary>
    public interface ITeacherRepository
    {
        /// <summary>
        /// Find async.
        /// </summary>
        /// <param name="criteria">Search criteria</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>IList with teachers</returns>
        Task<IList<Teacher>> FindAsync(Func<Teacher, bool>? criteria);

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Teacher</returns>
        Task<Teacher> GetByIdAsync(Guid id);

        /// <summary>
        /// Remove teacher.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task RemoveASync(Guid id);

        /// <summary>
        /// Save teacher.
        /// </summary>
        /// <param name="teacher">Teacher</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task SaveAsync(Teacher teacher);

        /// <summary>
        /// Set password.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="password">Password</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task SetPasswordAsync(Guid id, string password);
    }
}
