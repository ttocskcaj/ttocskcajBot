using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using ttocskcajBot.Entities;

namespace ttocskcajBot.Commands
{
    class Command
    {
        public string Verb { get; set; }
        public IEntity Entity { get; set; }

        internal static Command ParseMessage(DiscordMessage message)
        {
            string commandString = message.Content.TrimStart('>');
            string[] parts = commandString.Split(' ');

            Command command = new Command();
            switch (parts.Length)
            {
                case 0:
                    throw new CommandException("Command was empty");
                case 1:
                    command.Verb = parts[0];
                    break;
                default:
                    throw new CommandException("Command was too long");
            }

            return command;
        }

        [Serializable]
        private class CommandException : Exception
        {
            public CommandException()
            {
            }

            public CommandException(string message) : base(message)
            {
            }

            public CommandException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }
    }
}
}
