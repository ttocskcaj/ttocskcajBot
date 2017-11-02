using System;
using System.Collections.Generic;
using System.Linq;
using ttocskcajBot.Entities.Things;
using ttocskcajBot.Exceptions;

namespace ttocskcajBot
{
    internal class Inventory
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

        internal bool ContainsThing(string thingID)
        {
            return Items.Where(item => item.Key.MatchesName(thingID)).ToDictionary(x => x.Key, y => y.Value).Count > 0;
        }

        internal void RemoveThing(string thingID)
        {
            if (ContainsThing(thingID))
            {
                KeyValuePair<Thing, int> invEntry = Items.Where(item => item.Key.MatchesName(thingID)).ToDictionary(x => x.Key, y => y.Value).First();
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
                throw new EntityNotFoundException($"{thingID} wasn't found in your inventory!");

            }
        }

        internal Thing GetThing(string thingID)
        {
            if (!ContainsThing(thingID))
                throw new EntityNotFoundException($"{thingID} wasn't found in your inventory!");

            KeyValuePair<Thing, int> invEntry = Items.Where(item => item.Key.MatchesName(thingID)).ToDictionary(x => x.Key, y => y.Value).First();
            return invEntry.Key;
        }

        internal string EquipThing(string thingID)
        {
            Thing thing = GetThing(thingID);
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
