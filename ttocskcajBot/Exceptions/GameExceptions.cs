using System;

namespace ttocskcajBot.Exceptions
{
    internal class GameNotRunningException : Exception
    {
        public GameNotRunningException(string message) : base(message)
        {
        }
    }
}
