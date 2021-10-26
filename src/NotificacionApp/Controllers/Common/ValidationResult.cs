using System;
using System.Collections.Generic;
using System.Linq;

namespace NotificacionApp.Controllers.Common
{
    /// <summary>
    /// Validation result.
    /// </summary>
    public class ValidationResult
    {
        private const string GeneralMessage = "general";

        /// <summary>
        /// Constructor.
        /// </summary>
        public ValidationResult()
        {
            Messages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Validation messages.
        /// </summary>
        public IList<ValidationMessage> Messages { get; init; }
        
        /// <summary>
        /// Is valid.
        /// </summary>
        /// <returns></returns>
        public bool IsValid() => !Messages.Any();

        /// <summary>
        /// Check if field is required.
        /// </summary>
        /// <param name="field">Name of the field</param>
        /// <param name="value">Value</param>
        public void IsRequired(string field, string value) => Validate(() => !string.IsNullOrWhiteSpace(value), ValidationMessage.Required(field));

        /// <summary>
        /// Check if field is of valid enum value.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="response"></param>
        /// <param name="type"></param>
        internal void HasValidEnum(string field, string value, Type type) => Validate(() => 
            Enum.TryParse(type, value, out _), ValidationMessage.InvalidValue(field));
        

        /// <summary>
        /// Check if field is required.
        /// </summary>
        /// <param name="field">Name of the field</param>
        /// <param name="value">Value</param>
        public void IsRequired(string field, Guid? value) => Validate(() => (value != null && !Guid.Empty.Equals(value)), ValidationMessage.Required(field));

        /// <summary>
        /// Check for max length.
        /// </summary>
        /// <param name="field">Name of the field</param>
        /// <param name="value">Value</param>
        /// <param name="length">Maximum length</param>
        public void HasMaxLenght(string field, string value, int length) => Validate(() => value.Length <= length, ValidationMessage.MaxLength(field, length));


        /// <summary>
        /// Check if the field is unique.
        /// </summary>
        /// <param name="field">Name of the field</param>
        /// <param name="isUnique">True is value is unique</param>
        public void IsUnique(string field, bool isUnique) => Validate(() => isUnique, ValidationMessage.Unique(field));


        /// <summary>
        /// Element couldn't be found.
        /// </summary>
        /// <param name="element">Element</param>
        /// <param name="id">Id fo the element</param>
        internal void NotFound(string element, Guid id) => Messages.Add(ValidationMessage.NotFound(element, id));

        /// <summary>
        /// Element couldn't be removed.
        /// </summary>
        /// <param name="element">Element</param>
        /// <param name="id">Id fo the element</param>
        internal void RemoveFailed (string element, Guid id) => Messages.Add(ValidationMessage.RemoveFailed(element, id));

        /// <summary>
        /// Element couldn't be updated.
        /// </summary>
        /// <param name="element">Element</param>
        /// <param name="id">Id fo the element</param>
        internal void UpdateFailed(string element, Guid id) => Messages.Add(ValidationMessage.UpdateFailed(element, id));

        /// <summary>
        /// Element couldn't be created.
        /// </summary>
        /// <param name="element">Element</param>
        /// <param name="id">Id fo the element</param>
        internal void CreateFailed(string element) => Messages.Add(ValidationMessage.CreateFailed(element));


        /// <summary>
        /// Validate.
        /// </summary>
        /// <param name="validation">Validation</param>
        /// <param name="message">Message when validation failed</param>
        private void Validate(Func<bool> validation, ValidationMessage message)
        {
            if (!validation.Invoke())
            {
                Messages.Add(message);
            }
        }




    }
}
