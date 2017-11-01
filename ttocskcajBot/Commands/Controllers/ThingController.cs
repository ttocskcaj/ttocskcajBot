using ttocskcajBot.Entities.Things;
using ttocskcajBot.Exceptions;

namespace ttocskcajBot.Commands.Controllers
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

            if (!thing.CanTake)
                throw new CommandException($"Don't be silly! {thing.Name} is too heavy to carry!");
            if (!thing.Discovered)
                throw new EntityNotFoundException($"{thing.Name} wasn't found. Have a look around for it first.");
            Game.Instance.Inventory.AddThing(thing);
            Game.RemoveFromRoom(thing);
            return  new CommandResponse($"Added {thing.Name} to inventory");
        }

    }
}