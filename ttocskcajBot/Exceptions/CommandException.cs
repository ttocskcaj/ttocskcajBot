using System;

namespace ttocskcajBot.Exceptions
{
    internal class CommandException : Exception
    {
        public CommandException(string message) : base(message)
        {
        }
    }
}
