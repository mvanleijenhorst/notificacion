using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NotificacionApp.Controllers.Common;
using NotificacionApp.Domain;

namespace NotificacionApp.Services
{
    /// <summary>
    /// Teacher manager interface.
    /// </summary>
    public interface ITeacherService
    {
        /// <summary>
        /// Add teacher.
        /// </summary>
        /// <param name="teacher">Teacher</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="ValidationResult"/></returns>
        Task<ValidationResult> AddAsync(Teacher teacher);

        /// <summary>
        /// Get all teachers.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="Teacher"/></returns>
        Task<IList<Teacher>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get teacher by id.
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="Teacher"/></returns>
        /// <exception cref="InvalidOperationException">When teacher cannot be found</exception>
        Task<Teacher> GetByIdAsync(Guid id);

        /// <summary>
        /// Add teacher.
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="ValidationResult"/></returns>
        Task<ValidationResult> RemoveAsync(Guid id);

        /// <summary>
        /// Set password.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="password">Password</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="ValidationResult"/></returns>
        Task<ValidationResult> SetPasswordAsync(Guid id, string password);

        /// <summary>
        /// Update teacher.
        /// </summary>
        /// <param name="teacher">Teacher</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="ValidationResult"/></returns>
        Task<ValidationResult> UpdateAsync(Teacher teacher);
    }
}
