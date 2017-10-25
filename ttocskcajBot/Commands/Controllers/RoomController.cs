using ttocskcajBot.Commands.Controllers;
using static ttocskcajBot.Commands.Command;

namespace ttocskcajBot.Commands
{
    internal class RoomController : IController
    {
        public string RunCommand(Command command)
        {
            if (command.Verb.Equals("move"))
            {
                return "What direction?";
            }
            throw new CommandException("Command doesn't exist!");
        }

    }
}