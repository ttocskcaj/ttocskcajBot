using System;

namespace ttocskcajBot.Exceptions
{
    internal class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
