﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ttocskcajBot.Entities;
using ttocskcajBot.Entities.Things;
using ttocskcajBot.Exceptions;

namespace ttocskcajBot
{
    internal class Game
    {

        private static readonly Lazy<Game> lazy = new Lazy<Game>(() => new Game());
        public static Game Instance { get { return lazy.Value; } }

        internal Room CurrentRoom { get; set; }

        internal Inventory Inventory { get; set; }
        internal List<Room> Rooms { get; set; }
        internal List<Thing> Things { get; set; }

        public Game()
        {
            Rooms = new List<Room>();
            Things = new List<Thing>();
            Inventory = new Inventory();
            LoadGameData();
        }

        private void LoadGameData()
        {
            // Load thing data.
            Things.AddRange(JsonConvert.DeserializeObject<List<Furniture>>(File.ReadAllText("GameData/Things/Furniture.json")));
            Things.AddRange(JsonConvert.DeserializeObject<List<Tool>>(File.ReadAllText("GameData/Things/Tools.json")));
            Things.AddRange(JsonConvert.DeserializeObject<List<Armour>>(File.ReadAllText("GameData/Things/Armours.json")));
            Things.AddRange(JsonConvert.DeserializeObject<List<Weapon>>(File.ReadAllText("GameData/Things/Weapons.json")));

            // Load room data.
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                ReferenceResolverProvider = () => new ThingReferenceResolver(Things)
            };
            foreach (string file in Directory.EnumerateFiles("GameData/Rooms", "*.json", SearchOption.AllDirectories))
            {
                Room room = JsonConvert.DeserializeObject<Room>(File.ReadAllText(file), jsonSerializerSettings);
                Rooms.Add(room);
            }
        }

        internal static void RemoveFromRoom(Thing thing)
        {
            foreach (Area area in Instance.CurrentRoom.Areas)
            {
                if (area.Things.Contains(thing))
                {
                    area.Things.Remove(thing);
                    break;
                }
            }
        }

        internal void NewGame()
        {
            if (Rooms.Count < 1)
            {
                LoadGameData();
            }
            CurrentRoom = Rooms.Where(x => x.ID.Equals("dark_room")).First();
            Inventory.Clear();
        }
        internal bool IsRunning()
        {
            return (CurrentRoom != null);

        }

        internal IEntity FindEntity(string entityName, string type)
        {
            entityName = entityName.ToLower().Replace(' ', '_');
            // Check each area in the room for the entity
            foreach (Area area in CurrentRoom.Areas)
            {
                foreach (Thing thing in area.Things)
                {
                    if (thing.ID.Equals(entityName) && type == "Thing")
                    {
                        return thing;
                    }
                }
                if (area.ID.Equals(entityName) && type == "Area")
                {
                    return area;
                }
            }
            throw new EntityNotFoundException(String.Format("{0} was not found!", entityName));
        }

    }

}