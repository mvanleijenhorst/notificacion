using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NotificacionApp.Controllers.Common;
using NotificacionApp.Domain;

namespace NotificacionApp.Services
{
    /// <summary>
    /// Student manager interface.
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Add student.
        /// </summary>
        /// <param name="student">Student</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="ValidationResult"/></returns>
        Task<ValidationResult> AddAsync(Student student);

        /// <summary>
        /// Get all students.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="Student"/></returns>
        Task<IList<Student>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get student by id.
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="Student"/></returns>
        /// <exception cref="InvalidOperationException">When student cannot be found</exception>
        Task<Student> GetByIdAsync(Guid id);

        /// <summary>
        /// Add student.
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
        /// Update student.
        /// </summary>
        /// <param name="student">Student</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="ValidationResult"/></returns>
        Task<ValidationResult> UpdateAsync(Student student);
    }
}
