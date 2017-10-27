using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Exceptions;
using static ttocskcajBot.Commands.Command;

namespace ttocskcajBot.Commands
{
    internal class RoomController : IController
    {
        public string RunCommand(Command command)
        {
            if (Game.Instance.IsRunning())
            {
                if (command.Verb.Equals("move"))
                {
                    return "What direction?";
                }
                throw new CommandException(Properties.Resources.ResourceManager.GetString("commandNotFound"));
            }
            throw new GameNotRunningException(Properties.Resources.ResourceManager.GetString("gameNotRunning"));
        }

    }
}