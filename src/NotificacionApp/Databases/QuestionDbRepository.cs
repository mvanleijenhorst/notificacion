using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificacionApp.Domain;
using NotificacionApp.Repositories;

namespace NotificacionApp.Databases
{
    /// <summary>
    /// Database implementation of Question Repository.
    /// </summary>
    public class QuestionDbRepository : IQuestionRepository
    {
        private readonly NotificacionDbContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Db context</param>
        public QuestionDbRepository(NotificacionDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<IList<Question>> FindAsync(Func<Question, bool>? criteria)
        {
            if (criteria == null)
            {
                return await _context
                    .Questions
                    .Include(p => p.Student)
                    .Include(p => p.Teacher)
                    .ToListAsync();
            }

            return _context.Questions
                .Include(p => p.Student)
                .Include(p => p.Teacher)
                .Where(criteria)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<Question> GetByIdAsync(Guid id)
        {
            var question = await _context.Questions
                .Include(p => p.Student)
                .Include(p => p.Teacher)
                .SingleAsync(s => s.Id == id)
                .ConfigureAwait(false);

            return question;
        }

        /// <inheritdoc/>
        public async Task SaveAsync(Question question)
        {
            if (question.Id == Guid.Empty)
            {
                await _context.Questions
                    .AddAsync(question)
                    .ConfigureAwait(false);
            }
            else
            {
                question = await _context.Questions
                    .Include(p => p.Student)
                    .Include(p => p.Teacher)
                    .SingleAsync(s => s.Id == question.Id)
                    .ConfigureAwait(false);
                question.Room = question.Room;
                question.Table = question.Table;
                question.Response = question.Response;
                question.Teacher = question.Teacher;
                question.Student = question.Student;
                question.Topic = question.Topic;
                question.ResponseDateTime = question.ResponseDateTime;
                question.Response = question.Response;
            }

            await _context
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task RemoveASync(Guid id)
        {
            var question = await _context.Questions
                .SingleAsync(s => s.Id == id)
                .ConfigureAwait(false);

            _context.Questions
                .Remove(question);
        }
    }
}
