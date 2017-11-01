using System;
using System.Collections.Generic;
using System.Linq;
using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Entities;
using ttocskcajBot.Exceptions;
using static ttocskcajBot.Commands.Command;

namespace ttocskcajBot.Commands
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
                    response += String.Format("{0}:\t{1}", thing.Value, thing.Key.Name);
                    response += "\n";
                }
                // Add any equipped items to the response string.
                response += "```\n";
                response += "__**Equipped Items**__\n";
                response += "```\n";
                if (Game.Instance.Inventory.EquippedArmour != null)
                {
                    response += String.Format("Armour:\t{0}\n", Game.Instance.Inventory.EquippedArmour.Name);
                }
                else
                {
                    response += "Armour:\t- \n";
                }

                if (Game.Instance.Inventory.EquippedTool != null)
                {
                    response += String.Format("Tool:\t{0}\n", Game.Instance.Inventory.EquippedTool.Name);
                }
                else
                {
                    response += "Tool:\t- \n";
                }

                if (Game.Instance.Inventory.EquippedWeapon != null)
                {
                    response += String.Format("Weapon:\t{0}\n", Game.Instance.Inventory.EquippedWeapon.Name);
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
            if (Game.Instance.Inventory.ContainsThing(thingID))
            {
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
                return new CommandResponse(String.Format("Dropped {0}", thing.Name));
            }
            throw new EntityNotFoundException(String.Format("You don't have {0}", command.Entity));
        }
        public static CommandResponse Equip(Command command)
        {
            string thingID = command.Entity.ToLower().Replace(' ', '_');
            return new CommandResponse(Game.Instance.Inventory.EquipThing(thingID));
        }
    }
}
