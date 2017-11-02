using ttocskcajBot.Exceptions;
using ttocskcajBot.Properties;

namespace ttocskcajBot.Commands.Controllers
{
    internal class RoomController : IController
    {
        public string RunCommand(Command command)
        {
            if (command.Verb.Equals("move"))
            {
                return "What direction?";
            }
            throw new CommandException(Resources.ResourceManager.GetString("commandNotFound"));

        }

        public static CommandResponse Room(Command command)
        {
            return new CommandResponse(Game.Instance.CurrentRoom.GetCurrentDescription());
        }
    }
}