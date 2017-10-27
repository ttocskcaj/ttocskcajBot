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
            if (Game.Instance.IsRunning())
            {
                if (command.Verb.Equals("inspect"))
                {
                    try
                    {
                        if (command.Entity == null) throw new EntityNotFoundException("Please enter an entity to inspect!");
                        Area area = (Area)Game.Instance.FindEntity(command.Entity);
                        return area.Description;
                    }
                    catch (EntityNotFoundException ex)
                    {
                        return ex.Message;
                    }
                }
                throw new CommandException(Properties.Resources.ResourceManager.GetString("commandNotFound"));
            }
            throw new GameNotRunningException(Properties.Resources.ResourceManager.GetString("gameNotRunning"));

        }

    }
}