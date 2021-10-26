using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NotificacionApp.Controllers.Common;
using NotificacionApp.Databases;
using NotificacionApp.Domain;
using NotificacionApp.Repositories;

namespace NotificacionApp.Services
{
    /// <summary>
    /// Implementation of teacher manager service.
    /// </summary>
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository">Repository</param>
        public TeacherService(ITeacherRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc/>
        public Task<IList<Teacher>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _repository.FindAsync(null);
        }

        /// <inheritdoc/>
        public Task<Teacher> GetByIdAsync(Guid id)
        {
            return _repository.GetByIdAsync(id);
        }

        /// <inheritdoc/>
        public async Task<ValidationResult> AddAsync(Teacher teacher)
        {
            var validation = new ValidationResult();

            var users = await _repository.FindAsync(t => t.Username.Equals(teacher.Username, StringComparison.OrdinalIgnoreCase));
            validation.IsUnique(nameof(Teacher.Username), !users.Any());

            if (validation.IsValid())
            {
                await _repository.SaveAsync(teacher);
            }

            return validation;
        }

        /// <inheritdoc/>
        public async Task<ValidationResult> UpdateAsync(Teacher teacher)
        {
            var validation = new ValidationResult();

            try
            {
                await _repository.SaveAsync(teacher);
            }
            catch
            {
                validation.UpdateFailed(nameof(Teacher), teacher.Id);
            }

            return validation;
        }

        /// <inheritdoc/>
        public async Task<ValidationResult> RemoveAsync(Guid id)
        {
            var validation = new ValidationResult();

            try
            {
                await _repository.RemoveASync(id).ConfigureAwait(false);
            }
            catch
            {
                validation.RemoveFailed(nameof(Teacher), id);
            }

            return validation;
        }

        /// <inheritdoc/>
        public async Task<ValidationResult> SetPasswordAsync(Guid id, string password)
        {
            var validation = new ValidationResult();

            try
            {
                await _repository.SetPasswordAsync(id, password);
            }
            catch
            {
                validation.UpdateFailed(nameof(Teacher), id);
            }

            return validation;
        }
    }
}
