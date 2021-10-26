using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotificacionApp.Domain;

namespace NotificacionApp.Repositories
{
    /// <summary>
    /// Question repository.
    /// </summary>
    public interface IQuestionRepository
    {
        /// <summary>
        /// Find async.
        /// </summary>
        /// <param name="criteria">Search criteria</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>IList with questions</returns>
        Task<IList<Question>> FindAsync(Func<Question, bool>? criteria);

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Question</returns>
        Task<Question> GetByIdAsync(Guid id);

        /// <summary>
        /// Remove question.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task RemoveASync(Guid id);

        /// <summary>
        /// Save question.
        /// </summary>
        /// <param name="question">Question</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task SaveAsync(Question question);
    }
}
