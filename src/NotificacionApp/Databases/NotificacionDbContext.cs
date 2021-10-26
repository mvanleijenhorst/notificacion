using Microsoft.EntityFrameworkCore;
using NotificacionApp.Database.Configurations;
using NotificacionApp.Domain;
using System;

namespace NotificacionApp.Databases
{
    /// <summary>
    /// NotificacionContext.
    /// </summary>
    public class NotificacionDbContext : DbContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions"/></param>
        public NotificacionDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Teachers.
        /// </summary>
        public DbSet<Teacher> Teachers => Set<Teacher>();

        /// <summary>
        /// Students.
        /// </summary>
        public DbSet<Student> Students => Set<Student>();

        /// <summary>
        /// Questions.
        /// </summary>
        public DbSet<Question> Questions => Set<Question>();


        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ApplyConfiguration(new TeacherEntityTypeConfiguration());
            builder.ApplyConfiguration(new StudentEntityTypeConfiguration());
        }
    }
}
