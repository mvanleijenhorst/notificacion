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
    /// Implementation of student manager service.
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository">Repository</param>
        public StudentService(IStudentRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc/>
        public Task<IList<Student>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _repository.FindAsync(null);
        }

        /// <inheritdoc/>
        public Task<Student> GetByIdAsync(Guid id)
        {
            return _repository.GetByIdAsync(id);
        }

        /// <inheritdoc/>
        public async Task<ValidationResult> AddAsync(Student student)
        {
            var validation = new ValidationResult();

            var users = await _repository.FindAsync(t => t.Username.Equals(student.Username, StringComparison.OrdinalIgnoreCase));
            validation.IsUnique(nameof(Student.Username), !users.Any());

            if (validation.IsValid())
            {
                await _repository.SaveAsync(student);
            }

            return validation;
        }

        /// <inheritdoc/>
        public async Task<ValidationResult> UpdateAsync(Student student)
        {
            var validation = new ValidationResult();

            try
            {
                await _repository.SaveAsync(student);
            }
            catch
            {
                validation.UpdateFailed(nameof(Student), student.Id);
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
                validation.RemoveFailed(nameof(Student), id);
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
                validation.UpdateFailed(nameof(Student), id);
            }

            return validation;
        }
    }
}
