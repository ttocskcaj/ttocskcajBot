using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Entities;
using ttocskcajBot.Exceptions;

namespace ttocskcajBot.Commands
{
    class Command
    {
        public string Verb { get; set; }
        public string Entity { get; set; }
        public DiscordMessage DiscordMessage { get; set; }

        public EventHandler<CommandEventArgs> CommandIssued { get; set; }

        public Command()
        {
        }

        internal static Command ParseMessage(DiscordMessage discordMessage)
        {
            Console.WriteLine(String.Format("Player command: <{0}> {1}", discordMessage.Author.Username, discordMessage.Content));

            string commandString = discordMessage.Content.TrimStart('.');
            string[] parts = commandString.Split(new[] { ' ' }, 2);

            Command command = new Command();
            command.DiscordMessage = discordMessage;
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
        internal void Exec()
        {
            OnCommandIssued(this);
        }
        protected virtual void OnCommandIssued(Command command)
        {
            EventHandler<CommandEventArgs> handler = CommandIssued;
            if (handler != null)
            {
                handler(this, new CommandEventArgs() { Command = command });
            }
        }
    }

    internal class CommandEventArgs
    {
        public Command Command { get; set; }
    }
}

