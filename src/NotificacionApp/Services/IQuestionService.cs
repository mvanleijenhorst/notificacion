using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotificacionApp.Common;
using NotificacionApp.Controllers.Common;
using NotificacionApp.Domain;

namespace NotificacionApp.Services
{
    /// <summary>
    /// Question service.
    /// </summary>
    public interface IQuestionService
    {
        /// <summary>
        /// Get all questions.
        /// </summary>
        /// <param name="id">Id of the user that ask the questions</param>
        /// <param name="userRole">Role of the user.</param>
        /// <returns>List of questions</returns>
        Task<IList<Question>> GetAllAsync(Guid id, UserRole userRole);

        /// <summary>
        /// Get question by id.
        /// </summary>
        /// <param name="id">Id the question</param>
        /// <returns></returns>
        Task<Question> GetByIdAsync(Guid id);

        /// <summary>
        /// Add a question.
        /// </summary>
        /// <param name="question">Question</param>
        /// <returns><see cref="ValidationResult"/></returns>
        Task<ValidationResult> AddAsync(Question question);

        /// <summary>
        /// Remove a question.
        /// </summary>
        /// <param name="id">Id of the question</param>
        /// <returns><see cref="ValidationResult"/></returns>
        Task<ValidationResult> RemoveAsync(Guid id);

        /// <summary>
        /// Update a question.
        /// </summary>
        /// <param name="question">Question</param>
        /// <returns><see cref="ValidationResult"/></returns>
        Task<ValidationResult> UpdateAsync(Question question);
    }
}
