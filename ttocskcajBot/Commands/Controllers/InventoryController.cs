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
        public string RunCommand(Command command)
        {
            if (command.Verb.Equals("inventory"))
            {
                if (command.Entity == null)
                {
                    string response = "__**Inventory**__\n";
                    response += "```\n";
                    if (Game.Instance.Inventory.Items.Count > 0)
                    {
                        foreach (KeyValuePair<Thing, int> thing in Game.Instance.Inventory.Items)
                        {
                            response += String.Format("{0}:\t{1}", thing.Value, thing.Key.Name);
                            response += "\n";
                        }
                        response += "```\n";
                        response += "__**Equipped Items**__\n";
                        response += "```\n";
                        if(Game.Instance.Inventory.EquippedArmour != null)
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

                    return response;
                }
            }

            if (command.Verb.Equals("drop"))
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
                    return String.Format("Dropped {0}", thing.Name);
                }
            }

            if (command.Verb.Equals("equip"))
            {
                string thingID = command.Entity.ToLower().Replace(' ', '_');
                //Game.Instance.Inventory.Equipped = thing;
                return Game.Instance.Inventory.EquipThing(thingID);
            }


            // That command doesn't exist on this controller.
            throw new CommandException(Properties.Resources.ResourceManager.GetString("commandNotFound"));
        }
    }
}
