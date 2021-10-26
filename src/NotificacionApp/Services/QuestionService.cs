using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificacionApp.Common;
using NotificacionApp.Controllers.Common;
using NotificacionApp.Domain;
using NotificacionApp.Repositories;

namespace NotificacionApp.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repository;

        public QuestionService(IQuestionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<Question> GetByIdAsync(Guid id)
        {
            return _repository.GetByIdAsync(id);
        }

        public async Task<IList<Question>> GetAllAsync(Guid id, UserRole userRole)
        {
            var response = userRole switch
            {
                UserRole.Teacher => await _repository.FindAsync(q => q.Teacher.Id == id).ConfigureAwait(false),
                UserRole.Student => await _repository.FindAsync(q => q.Student.Id == id).ConfigureAwait(false),
                _ => new List<Question>(),
            };
            return response.OrderBy(q => q.CreateDateTime).ToList();
        }

        public async Task<ValidationResult> AddAsync(Question question)
        {
            var result = new ValidationResult();

            try
            {
                await _repository.SaveAsync(question);
            }
            catch
            {
                result.CreateFailed(nameof(Question));
            }

            return result;
        }

        public async Task<ValidationResult> UpdateAsync(Question question)
        {
            var result = new ValidationResult();

            var found = await _repository.FindAsync(q => question.Id == q.Id);
            if (!found.Any())
            {
                result.NotFound(nameof(Question), question.Id);
                return result;
            }

            try
            {
                await _repository.SaveAsync(question);
            }
            catch
            {
                result.CreateFailed(nameof(Question));
            }

            return result;
        }

        public async Task<ValidationResult> RemoveAsync(Guid id)
        {
            var result = new ValidationResult();

            var found = await _repository.FindAsync(q => q.Id == id);
            if (!found.Any())
            {
                result.NotFound(nameof(Question), id);
                return result;
            }

            try
            {
                await _repository.RemoveASync(id);
            }
            catch
            {
                result.RemoveFailed(nameof(Question), id);
            }

            return result;
        }

    }
}
