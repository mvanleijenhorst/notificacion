using System;
using System.Linq;
using System.Threading.Tasks;
using NotificacionApp.Common;
using NotificacionApp.Repositories;

namespace NotificacionApp.Services
{
    /// <summary>
    /// Security service implementation.
    /// </summary>
    public class SecurityService : ISecurityService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="teacherRepository">teacher repository</param>
        public SecurityService(ITeacherRepository teacherRepository, IStudentRepository studentRepository)
        {
            _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
        }

        /// <inheritdoc/>
        public async Task<SecurityToken?> LoginAsync(string username, string password, UserRole userRole)
        {
            switch (userRole)
            {
                case UserRole.Teacher:
                    var teachers = await _teacherRepository.FindAsync(t => t.Username.Equals(username, StringComparison.OrdinalIgnoreCase)).ConfigureAwait(false);
                    var teacher = teachers.SingleOrDefault();
                    if (teacher != null && EncryptionHelper.Verify(password, teacher.Password))
                    {
                        return new SecurityToken(teacher.Id, teacher.Name, teacher.UserRole);
                    }
                    break;
                case UserRole.Student:
                    var students = await _studentRepository.FindAsync(t => t.Username.Equals(username, StringComparison.OrdinalIgnoreCase)).ConfigureAwait(false);
                    var student = students.SingleOrDefault();
                    if (student != null && EncryptionHelper.Verify(password, student.Password))
                    {
                        return new SecurityToken(student.Id, student.Name, student.UserRole);
                    }
                    break;
            }

            return null;
        }

    }
}
