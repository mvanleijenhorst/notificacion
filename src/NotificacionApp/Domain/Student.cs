using NotificacionApp.Common;
using System;

namespace NotificacionApp.Domain
{
    /// <summary>
    /// Student entity.
    /// </summary>
    public class Student : ISystemUser
    {

        /// <summary>
        /// Private constructor.
        /// </summary>
        private Student()
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name of the student</param>
        /// <param name="username">Username for login of the student</param>
        public Student(string name, string username)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"'{nameof(username)}' cannot be null or whitespace", nameof(username));
            }

            Id = Guid.Empty;
            Name = name;
            Username = username;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">Name of the student</param>
        /// <param name="username">Username for login of the student</param>
        /// <param name="password">Password for login of the student</param>
        public Student(Guid id, string name, string username)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"'{nameof(username)}' cannot be null or whitespace", nameof(username));
            }

            Id = id;
            Name = name;
            Username = username;
        }

        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <inheritdoc/>
        public string Username { get; set; } = null!;

        /// <inheritdoc/>
        public string Password { get; set; } = "<not set>";

        /// <summary>
        /// User role.
        /// </summary>
        public UserRole UserRole { get { return UserRole.Student; } }

    }
}
