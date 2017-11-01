using System;
using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Entities;
using ttocskcajBot.Exceptions;
using static ttocskcajBot.Commands.Command;

namespace ttocskcajBot.Commands
{
    /// <summary>
    /// Runs commands that have to do with Areas
    /// </summary>
    internal class ThingController : IController
    {
        public static CommandResponse Take(Command command)
        {

            // Try and get an Thing entity from the command.
            if (command.Entity == null) throw new EntityNotFoundException("Please enter an entity to take!");
            Thing thing = (Thing)Game.Instance.FindEntity(command.Entity, "Thing");

            if (thing.CanTake)
            {
                if (thing.Discovered)
                {
                    Game.Instance.Inventory.AddThing(thing);
                    Game.RemoveFromRoom(thing);
                    return  new CommandResponse(String.Format("Added {0} to inventory", thing.Name));
                }
                throw new EntityNotFoundException(String.Format("{0} wasn't found. Have a look around for it first.", thing.Name));
            }
            throw new CommandException(String.Format("Don't be silly! {0} is too heavy to carry!", thing.Name));
        }

    }
}