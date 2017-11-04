using System.Collections.Generic;
using System.Linq;
using ttocskcajBot.Entities.Things;
using ttocskcajBot.Exceptions;

namespace ttocskcajBot.Commands.Controllers
{
    /// <summary>
    /// Runs commands that have to do with Areas
    /// </summary>
    internal class InventoryController : IController
    {
        public static CommandResponse Inventory(Command command)
        {

            string response = "__**Inventory**__\n ```\n";
            // If the inventory contains any items.
            if (Game.Inventory.Items.Count > 0)
            {
                // Add each item to the response string.
                foreach (KeyValuePair<Thing, int> thing in Game.Inventory.Items)
                {
                    response += $"{thing.Value}:\t{thing.Key.Name}";
                    response += "\n";
                }
                // Add any equipped items to the response string.
                response += "```\n";
                response += "__**Equipped Items**__\n";
                response += "```\n";
                if (Game.Inventory.EquippedArmour != null)
                {
                    response += $"Armour:\t{Game.Inventory.EquippedArmour.Name}\n";
                }
                else
                {
                    response += "Armour:\t- \n";
                }

                if (Game.Inventory.EquippedTool != null)
                {
                    response += $"Tool:\t{Game.Inventory.EquippedTool.Name}\n";
                }
                else
                {
                    response += "Tool:\t- \n";
                }

                if (Game.Inventory.EquippedWeapon != null)
                {
                    response += $"Weapon:\t{Game.Inventory.EquippedWeapon.Name}\n";
                }
                else
                {
                    response += "Weapon:\t- \n";
                }
                response += "```\n";

            }
            else
            {
                response += "Nothing to see here. Go explore!";
                response += "```\n\n";
            }

            return new CommandResponse(response);

        }

        public static CommandResponse Drop(Command command)
        {
            string thingID = command.Entity.ToLower().Replace(' ', '_');
            if (!Game.Inventory.ContainsThing(thingID))
                throw new EntityNotFoundException($"You don't have {command.Entity}");
            Thing thing = Game.Inventory.GetThing(thingID);
            Game.CurrentRoom.Things.Add(thing);

            if (Game.Inventory.EquippedArmour == thing)
            {
                Game.Inventory.EquippedArmour = null;
            }
            if (Game.Inventory.EquippedTool == thing)
            {
                Game.Inventory.EquippedTool = null;
            }
            if (Game.Inventory.EquippedWeapon == thing)
            {
                Game.Inventory.EquippedWeapon = null;
            }

            Game.Inventory.RemoveThing(thingID);
            return new CommandResponse($"Dropped {thing.Name}");
        }
        public static CommandResponse Equip(Command command)
        {
            string thingID = command.Entity.ToLower().Replace(' ', '_');
            return new CommandResponse(Game.Inventory.EquipThing(thingID));
        }
    }
}
