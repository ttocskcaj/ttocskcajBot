using System.Linq;
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
            Thing thing = Game.Instance.FindThing(command.Entity);

            if (!thing.CanTake)
                throw new CommandException($"Don't be silly! {thing.Name} is too heavy to carry!");
            if (!thing.Discovered)
                throw new EntityNotFoundException($"{thing.Name} wasn't found. Have a look around for it first.");
            Game.Instance.Inventory.AddThing(thing);
            Game.RemoveFromRoom(thing);
            return new CommandResponse($"Added {thing.Name} to inventory");
        }

        public static CommandResponse Action(Command command)
        {

            // Get the subject to take the action on.
            Thing subject = Game.Instance.FindThing(command.Entity);

            if (!subject.ActionsReceivable.ContainsKey(command.Verb))
                throw new EntityNotSupportedException($"Don't be silly! You can't {command.Verb} {command.Entity}!");

            // Get the action object from the subject.
            Action action = subject.ActionsReceivable[command.Verb];

            if (action.NeedsDiscovering && !subject.Discovered)
            {
                throw new EntityNotFoundException($"{command.Entity} wasn't found!");
            }

            if (action.NeedsEquippedThing)
            {
                // Check each equipped item to see if it can do this action.
                Thing thing;
                if (Game.Instance.Inventory.EquippedTool != null &&
                    Game.Instance.Inventory.EquippedTool.ActionsAvailable.Contains(command.Verb))
                {
                    thing = Game.Instance.Inventory.EquippedTool;
                }
                else if (Game.Instance.Inventory.EquippedArmour != null &&
                         Game.Instance.Inventory.EquippedArmour.ActionsAvailable.Contains(command.Verb))
                {
                    thing = Game.Instance.Inventory.EquippedArmour;
                }
                else if (Game.Instance.Inventory.EquippedWeapon != null &&
                         Game.Instance.Inventory.EquippedWeapon.ActionsAvailable.Contains(command.Verb))
                {
                    thing = Game.Instance.Inventory.EquippedWeapon;
                }
                else
                {
                    throw new EntityNotFoundException($"You don't have an item equipped to {command.Verb} with!");
                }
                // Where applicable, check if the thing has uses remaining, and deduct them.
                if (thing.LimitedUses)
                {
                    if (thing.Uses < 1)
                    {
                        throw new EntityOutOfUsesException($"{thing.Name} is out of uses!");
                    }
                    thing.Uses--;
                }
            }

            return new CommandResponse(subject.ExecuteAction(action));


            /* Old action code
                switch (command.Verb)
                {
                    case "light":
                        subject.State = 1;
                        subject.LightLevel += 0.7;
                        return new CommandResponse($"Lit {subject.Name}");
                    default:
                        throw new CommandException($"Potato!");
                }
                */
        }
    }
}