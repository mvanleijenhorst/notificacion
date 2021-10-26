using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotificacionApp.Domain;

namespace NotificacionApp.Repositories
{
    /// <summary>
    /// Student repository.
    /// </summary>
    public interface IStudentRepository
    {
        /// <summary>
        /// Find async.
        /// </summary>
        /// <param name="criteria">Search criteria</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>IList with students</returns>
        Task<IList<Student>> FindAsync(Func<Student, bool>? criteria);

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Student</returns>
        Task<Student> GetByIdAsync(Guid id);

        /// <summary>
        /// Remove student.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task RemoveASync(Guid id);

        /// <summary>
        /// Save student.
        /// </summary>
        /// <param name="student">Student</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task SaveAsync(Student student);

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
