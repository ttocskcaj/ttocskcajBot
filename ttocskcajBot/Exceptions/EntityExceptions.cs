using System;

namespace ttocskcajBot.Exceptions
{
    internal class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
    internal class EntityOutOfUsesException : Exception
    {
        public EntityOutOfUsesException(string message) : base(message)
        {
        }
    }
    internal class EntityNotSupportedException : Exception
    {
        public EntityNotSupportedException(string message) : base(message)
        {
        }
    }
}
