using ttocskcajBot.Exceptions;
using ttocskcajBot.Properties;

namespace ttocskcajBot.Commands.Controllers
{
    internal class PortalController : IController
    {
        public static CommandResponse Enter(Command command)
        {
            throw new CommandException("Cabbages");
        }
    }
}