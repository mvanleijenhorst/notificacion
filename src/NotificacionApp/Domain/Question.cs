using NotificacionApp.Common;
using System;

namespace NotificacionApp.Domain
{
    /// <summary>
    /// Question.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        private Question()
        { 
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="room">Room</param>
        /// <param name="table">Table</param>
        /// <param name="topic">Topic</param>
        /// <param name="student">Student</param>
        /// <param name="teacher">Teacher</param>
        public Question(string room, string table, string topic, Student student, Teacher teacher)
        {
            if (string.IsNullOrWhiteSpace(room))
            {
                throw new ArgumentException($"'{nameof(room)}' cannot be null or whitespace", nameof(room));
            }

            if (string.IsNullOrWhiteSpace(table))
            {
                throw new ArgumentException($"'{nameof(table)}' cannot be null or whitespace", nameof(table));
            }

            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException($"'{nameof(topic)}' cannot be null or whitespace", nameof(topic));
            }

            Room = room;
            Table = table;
            Topic = topic;
            CreateDateTime = DateTime.Now;
            ResponseDateTime = null;
            Response = null;
            
            Student = student ?? throw new ArgumentNullException(nameof(student));
            Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="room">Room</param>
        /// <param name="table">Table</param>
        /// <param name="topic">Topic</param>
        /// <param name="student">Student</param>
        /// <param name="teacher">Teacher</param>
        public Question(Guid id, string room, string table, string topic, Student student, Teacher teacher, QuestionResponse? response)
        {
            if (string.IsNullOrWhiteSpace(room))
            {
                throw new ArgumentException($"'{nameof(room)}' cannot be null or whitespace", nameof(room));
            }

            if (string.IsNullOrWhiteSpace(table))
            {
                throw new ArgumentException($"'{nameof(table)}' cannot be null or whitespace", nameof(table));
            }

            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException($"'{nameof(topic)}' cannot be null or whitespace", nameof(topic));
            }

            Id = id;
            Room = room;
            Table = table;
            Topic = topic;
            Response = response;
            CreateDateTime = DateTime.MinValue;
            ResponseDateTime = DateTime.Now;
            Response = response;

            Student = student ?? throw new ArgumentNullException(nameof(student));
            Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
        }


        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Room.
        /// </summary>
        public string Room { get; set; } = null!;

        /// <summary>
        /// Table.
        /// </summary>
        public string Table { get; set; } = null!;

        /// <summary>
        /// Topic.
        /// </summary>
        public string Topic { get; set; } = null!;

        /// <summary>
        /// Date and time when the question is asked.
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// Date and time when the teacher response on the question.
        /// </summary>
        public DateTime? ResponseDateTime { get; set; }

        /// <summary>
        /// Question response.
        /// </summary>
        public QuestionResponse? Response { get; set; }

        /// <summary>
        /// Teacher.
        /// </summary>
        public Teacher Teacher { get; set; } = null!;

        /// <summary>
        /// Student.s
        /// </summary>
        public Student Student { get; set; } = null!;
    }
}
