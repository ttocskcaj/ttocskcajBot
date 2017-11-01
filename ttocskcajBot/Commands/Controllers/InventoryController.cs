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
            if (Game.Instance.Inventory.Items.Count > 0)
            {
                // Add each item to the response string.
                foreach (KeyValuePair<Thing, int> thing in Game.Instance.Inventory.Items)
                {
                    response += $"{thing.Value}:\t{thing.Key.Name}";
                    response += "\n";
                }
                // Add any equipped items to the response string.
                response += "```\n";
                response += "__**Equipped Items**__\n";
                response += "```\n";
                if (Game.Instance.Inventory.EquippedArmour != null)
                {
                    response += $"Armour:\t{Game.Instance.Inventory.EquippedArmour.Name}\n";
                }
                else
                {
                    response += "Armour:\t- \n";
                }

                if (Game.Instance.Inventory.EquippedTool != null)
                {
                    response += $"Tool:\t{Game.Instance.Inventory.EquippedTool.Name}\n";
                }
                else
                {
                    response += "Tool:\t- \n";
                }

                if (Game.Instance.Inventory.EquippedWeapon != null)
                {
                    response += $"Weapon:\t{Game.Instance.Inventory.EquippedWeapon.Name}\n";
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
            if (!Game.Instance.Inventory.ContainsThing(thingID))
                throw new EntityNotFoundException($"You don't have {command.Entity}");
            Thing thing = Game.Instance.Inventory.GetThing(thingID);
            Game.Instance.CurrentRoom.Areas.First().Things.Add(thing);

            if (Game.Instance.Inventory.EquippedArmour == thing)
            {
                Game.Instance.Inventory.EquippedArmour = null;
            }
            if (Game.Instance.Inventory.EquippedTool == thing)
            {
                Game.Instance.Inventory.EquippedTool = null;
            }
            if (Game.Instance.Inventory.EquippedWeapon == thing)
            {
                Game.Instance.Inventory.EquippedWeapon = null;
            }

            Game.Instance.Inventory.RemoveThing(thingID);
            return new CommandResponse($"Dropped {thing.Name}");
        }
        public static CommandResponse Equip(Command command)
        {
            string thingID = command.Entity.ToLower().Replace(' ', '_');
            return new CommandResponse(Game.Instance.Inventory.EquipThing(thingID));
        }
    }
}
