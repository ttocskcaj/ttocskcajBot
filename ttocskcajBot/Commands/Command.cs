using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Entities;

namespace ttocskcajBot.Commands
{
    class Command
    {
        public string Verb { get; set; }
        public string Entity { get; set; }

        public Command()
        {
        }

        internal static Command ParseMessage(DiscordMessage message)
        {
            Console.WriteLine(String.Format("Player command: <{0}> {1}", message.Author.Username, message.Content));

            string commandString = message.Content.TrimStart('>');
            string[] parts = commandString.Split(new[] { ' ' }, 2);

            Command command = new Command();
            switch (parts.Length)
            {
                case 0:
                    throw new CommandException("Commands can't be empty!");
                case 1:
                    command.Verb = parts[0];
                    break;
                case 2:
                    command.Verb = parts[0];
                    command.Entity = parts[1];
                    break;
                default:
                    throw new CommandException("That command is too long!");
            }

            return command;
        }
        internal string Exec()
        {
            Route route = Router.GetRoute(this);

            if (route.MiddlewareBefore(this))
            {
                string response = route.Controller.RunCommand(this);
                route.MiddlewareAfter(this);
                return response;
            }
            else throw new CommandException("Tomato!");
        }

        [Serializable]
        internal class CommandException : Exception
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

