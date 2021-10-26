using System;
using System.Runtime.Serialization;

namespace NotificacionApp.Databases
{
    [Serializable]
    internal class NotFoundException : Exception
    {
        public NotFoundException()
        { }

        public NotFoundException(string? message) 
            : base(message)
        { }

        public NotFoundException(Type type, Guid id)
            : base($"Entity '{type.Name}' not found with id '{id}'")
        { }
         
        public NotFoundException(string? message, Exception? innerException) 
            : base(message, innerException)
        { }

        protected NotFoundException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
        { }
    }
}
