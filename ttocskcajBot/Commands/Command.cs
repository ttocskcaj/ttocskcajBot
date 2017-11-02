using System;
using System.Diagnostics;
using DSharpPlus.Entities;
using ttocskcajBot.Exceptions;

namespace ttocskcajBot.Commands
{
    internal class Command
    {
        public string Verb { get; set; }
        public string Entity { get; set; }
        public DiscordMessage DiscordMessage { get; set; }

        internal static Command ParseMessage(DiscordMessage discordMessage)
        {
            Debug.WriteLine($"Player issued command: <{discordMessage.Author.Username}> {discordMessage.Content}");

            string commandString = discordMessage.Content.TrimStart('.');
            string[] parts = commandString.Split(new[] { ' ' }, 2);

            Command command = new Command {DiscordMessage = discordMessage};
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

    }

    internal class CommandEventArgs
    {
        public Command Command { get; set; }
    }
}

