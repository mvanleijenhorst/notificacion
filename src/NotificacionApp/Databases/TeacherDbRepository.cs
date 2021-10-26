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
    /// Database implementation of Teacher Repository.
    /// </summary>
    public class TeacherDbRepository : ITeacherRepository
    {
        private readonly NotificacionDbContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Db context</param>
        public TeacherDbRepository(NotificacionDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<IList<Teacher>> FindAsync(Func<Teacher, bool>? criteria)
        {
            if (criteria == null)
            {
                return await _context.Teachers
                    .ToListAsync();
            }

            return _context.Teachers
                .Where(criteria)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<Teacher> GetByIdAsync(Guid id)
        {
            var teacher = await _context.Teachers
                .SingleAsync(s => s.Id == id)
                .ConfigureAwait(false);

            return teacher;
        }

        /// <inheritdoc/>
        public async Task SaveAsync(Teacher teacher)
        {
            if (teacher.Id == Guid.Empty)
            {
                await _context.Teachers
                    .AddAsync(teacher)
                    .ConfigureAwait(false);
            }
            else
            {
                teacher = await _context.Teachers
                    .SingleAsync(s => s.Id == teacher.Id)
                    .ConfigureAwait(false);
                teacher.Name = teacher.Name;
                teacher.Username = teacher.Username;
            }

            await _context
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task RemoveASync(Guid id)
        {
            var teacher = await _context.Teachers
                .SingleAsync(s => s.Id == id)
                .ConfigureAwait(false);

            _context.Teachers
                .Remove(teacher);
        }

        /// <inheritdoc/>
        public async Task SetPasswordAsync(Guid id, string password)
        {
            var teacher = await _context.Teachers
                 .SingleAsync(s => s.Id == id)
                 .ConfigureAwait(false);

            teacher.Password = EncryptionHelper.Hash(password);

            await _context.SaveChangesAsync()
                .ConfigureAwait(false);
        }
    }
}
