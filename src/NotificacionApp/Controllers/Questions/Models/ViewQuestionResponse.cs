using System;

namespace NotificacionApp.Controllers.Students.Models
{
    /// <summary>
    /// View question response.
    /// </summary>
    public record ViewQuestionResponse(Guid Id, string Room, string Table, string Topic
        , Guid TeacherId, string TeacherName
        , Guid StudentId, string StudentName
        , DateTime CreateDateTime
        , DateTime? ResponseDateTime
        , string? response);
}
