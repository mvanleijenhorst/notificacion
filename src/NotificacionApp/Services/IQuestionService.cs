using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotificacionApp.Common;
using NotificacionApp.Controllers.Common;
using NotificacionApp.Domain;

namespace NotificacionApp.Services
{
    public interface IQuestionService
    {
        Task<IList<Question>> GetAllAsync(Guid id, UserRole userRole);
        Task<Question> GetByIdAsync(Guid id);
        Task<ValidationResult> AddAsync(Question question);
        Task<ValidationResult> RemoveAsync(Guid id);
        Task<ValidationResult> UpdateAsync(Question question);
    }
}
