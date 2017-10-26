using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Entities;
using ttocskcajBot.Exceptions;
using static ttocskcajBot.Commands.Command;

namespace ttocskcajBot.Commands
{
    internal class AreaController : IController
    {
        public string RunCommand(Command command)
        {
            if (command.Verb.Equals("inspect"))
            {
                try
                {
                Area area = (Area) Game.Instance.FindEntity(command.Entity);
                return area.Description;
                } catch (EntityNotFoundException ex)
                {
                    return ex.Message;
                }
            }
            throw new CommandException("Command doesn't exist!");
        }

    }
}