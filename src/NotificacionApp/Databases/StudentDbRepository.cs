using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificacionApp.Common;
using NotificacionApp.Domain;
using NotificacionApp.Repositories;

namespace NotificacionApp.Databases
{
    /// <summary>
    /// Database implementation of Student Repository.
    /// </summary>
    public class StudentDbRepository : IStudentRepository
    {
        private readonly NotificacionDbContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Db context</param>
        public StudentDbRepository(NotificacionDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<IList<Student>> FindAsync(Func<Student, bool>? criteria)
        {
            if (criteria == null)
            {
                return await _context.Students
                    .ToListAsync();
            }

            return _context.Students
                .Where(criteria)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<Student> GetByIdAsync(Guid id)
        {
            var student = await _context.Students
                .SingleAsync(s => s.Id == id)
                .ConfigureAwait(false);

            return student;
        }

        /// <inheritdoc/>
        public async Task SaveAsync(Student student)
        {
            if (student.Id == Guid.Empty)
            {
                await _context.Students
                    .AddAsync(student)
                    .ConfigureAwait(false);
            }
            else
            {
                student = await _context.Students
                    .SingleAsync(s => s.Id == student.Id)
                    .ConfigureAwait(false);
                student.Name = student.Name;
                student.Username = student.Username;
            }

            await _context
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task RemoveASync(Guid id)
        {
            var student = await _context.Students
                .SingleAsync(s => s.Id == id)
                .ConfigureAwait(false);

            _context.Students
                .Remove(student);
        }

        /// <inheritdoc/>
        public async Task SetPasswordAsync(Guid id, string password)
        {
            var student = await _context.Students
                 .SingleAsync(s => s.Id == id)
                 .ConfigureAwait(false);

            student.Password = EncryptionHelper.Hash(password);

            await _context.SaveChangesAsync()
                .ConfigureAwait(false);
        }
    }

}
