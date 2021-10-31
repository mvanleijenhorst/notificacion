using System;

namespace NotificacionApp.Controllers.Common
{
    /// <summary>
    /// Validation message.
    /// </summary>
    public record ValidationMessage(string Field, string Message)
    {
        internal static ValidationMessage Required(string field) => new(field, $"{field} is required");
        internal static ValidationMessage MaxLength(string field, int length) => new(field, $"{field} has a maximum lenght of {length} characters");
        internal static ValidationMessage Unique(string field) => new(field, $"{field} should be unique");
        internal static ValidationMessage InvalidValue(string field) => new(field, $"{field} has an invalid value");

        internal static ValidationMessage NotFound(string element, Guid id) => new(element, $"{element} with Id '{id}' is not found");
        internal static ValidationMessage RemoveFailed(string element, Guid id) => new(element, $"{element} with Id '{id}' couldn't be removed");
        internal static ValidationMessage UpdateFailed(string element, Guid id) => new(element, $"{element} with Id '{id}' couldn't be updated");
        internal static ValidationMessage CreateFailed(string element) => new(element, $"{element} couldn't be created");
    }
}
