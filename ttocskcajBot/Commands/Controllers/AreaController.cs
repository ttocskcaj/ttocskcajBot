using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Entities;
using ttocskcajBot.Exceptions;
using static ttocskcajBot.Commands.Command;

namespace ttocskcajBot.Commands
{
    /// <summary>
    /// Runs commands that have to do with Areas
    /// </summary>
    internal class AreaController : IController
    {
        public string RunCommand(Command command)
        {

            if (command.Verb.Equals("inspect"))
            {
                // Try and get an Area entity from the command.
                if (command.Entity == null) throw new EntityNotFoundException("Please enter an entity to inspect!");
                Area area = (Area)Game.Instance.FindEntity(command.Entity, "Area");

                foreach (Thing thing in area.Things)
                {
                    thing.Discovered = true;
                }
                return area.Description;
            }
            // That command doesn't exist on this controller.
            throw new CommandException(Properties.Resources.ResourceManager.GetString("commandNotFound"));


        }

    }
}