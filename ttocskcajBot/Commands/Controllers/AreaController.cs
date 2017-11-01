using System;
using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Entities;
using ttocskcajBot.Exceptions;
using static ttocskcajBot.Commands.Command;

namespace ttocskcajBot.Commands
{
    /// <summary>
    /// Runs commands that have to do with Areas
    /// </summary>;
    internal class AreaController
    {
        internal static CommandResponse Inspect(Command command)
        {
            // Try and get an Area entity from the command.
            if (command.Entity == null) throw new EntityNotFoundException("Please enter an entity to inspect!");
            Area area = (Area)Game.Instance.FindEntity(command.Entity, "Area");

            // Set each Thing in the area to discovered.
            foreach (Thing thing in area.Things)
            {
                thing.Discovered = true;
            }
            return new CommandResponse(area.Description);
        }
    }
}
