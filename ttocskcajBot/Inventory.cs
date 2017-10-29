using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ttocskcajBot.Entities;
using ttocskcajBot.Entities.Things;
using ttocskcajBot.Exceptions;
using static ttocskcajBot.Commands.Command;

namespace ttocskcajBot
{
    class Inventory
    {
        // Inventory items {thing, qty}
        internal Dictionary<Thing, int> Items { get; set; }

        // Weapon that's currently equipped.
        internal Weapon EquippedWeapon { get; set; }
        // Weapon that's currently equipped.
        internal Tool EquippedTool { get; set; }
        // Weapon that's currently equipped.
        internal Armour EquippedArmour { get; set; }

        public Inventory()
        {
            Items = new Dictionary<Thing, int>();
        }

        internal void AddThing(Thing thing)
        {
            if (Items.ContainsKey(thing))
            {
                Items[thing]++;
            }
            else
            {
                Items.Add(thing, 1);
            }
        }

        internal bool ContainsThing(String thingID)
        {
            return (Items.Where(item => item.Key.ID.Equals(thingID)).ToDictionary(x => x.Key, y => y.Value).Count > 0);
        }

        internal void RemoveThing(String thingID)
        {
            if (ContainsThing(thingID))
            {
                KeyValuePair<Thing, int> invEntry = Items.Where(item => item.Key.ID.Equals(thingID)).ToDictionary(x => x.Key, y => y.Value).First();
                if (invEntry.Value > 1)
                {
                    Items[invEntry.Key]--;
                }
                else
                {
                    Items.Remove(invEntry.Key);
                }

            }
            else
            {
                throw new EntityNotFoundException(String.Format("{0} wasn't found in your inventory!", thingID));

            }
        }

        internal Thing GetThing(String thingID)
        {
            if (ContainsThing(thingID))
            {
                KeyValuePair<Thing, int> invEntry = Items.Where(item => item.Key.ID.Equals(thingID)).ToDictionary(x => x.Key, y => y.Value).First();
                return invEntry.Key;

            }
            throw new EntityNotFoundException(String.Format("{0} wasn't found in your inventory!", thingID));
        }

        internal string EquipThing(String thingID)
        {
            Thing thing = GetThing(thingID);
            Console.WriteLine(thing.GetType().Name);
            switch (thing.GetType().Name)
            {
                case "Weapon":
                    EquippedWeapon = (Weapon)thing;
                    break;
                case "Tool":
                    EquippedTool = (Tool)thing;
                    break;
                case "Armour":
                    EquippedArmour = (Armour)thing;
                    break;
                default:
                    throw new CommandException("Can't equip that!");
            }
            return thing.EquippedMessage;

        }
        internal void Clear()
        {
            Items.Clear();
            EquippedWeapon = null;
            EquippedArmour = null;
            EquippedTool = null;
        }
    }
}
