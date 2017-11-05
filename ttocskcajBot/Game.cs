using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ttocskcajBot.Entities;
using ttocskcajBot.Entities.Things;
using ttocskcajBot.Exceptions;
using ttocskcajBot.Generators;
using ttocskcajBot.Generators.Things;
using ttocskcajBot.Models;

namespace ttocskcajBot
{
    internal class Game
    {
        private static  Game _instance;
        private Inventory _inventory;
        private List<Room> _rooms;
        private WorldGenerator _worldGenerator;
        private World _world;

        /// <summary>
        /// The singleton instance.
        /// </summary>
        public static Game Instance => _instance ?? (_instance = new Game());

        /// <summary>
        /// The players Inventory.
        /// </summary>
        internal static Inventory Inventory
        {
            get => Instance._inventory;
            set => Instance._inventory = value;
        }

        /// <summary>
        /// Collection of all rooms.
        /// </summary>
        internal static List<Room> Rooms
        {
            get => Instance._rooms;
            set => Instance._rooms = value;
        }

        public static World World
        {
            get => Instance._world;
            set => Instance._world = value;
        }


        /// <summary>
        /// The World Generator.
        /// </summary>
        public static WorldGenerator WorldGenerator
        {
            get => Instance._worldGenerator;
            set => Instance._worldGenerator = value;
        }


        public Game()
        {
            _rooms = new List<Room>();
            _inventory = new Inventory();
            _worldGenerator = new WorldGenerator();
            
        }

        public static void LoadGameData()
        {
            // Load ThingGenerator data.
            Console.Write("Loading game data... ");
            WorldGenerator.ThingGenerators.AddRange(JsonConvert.DeserializeObject<List<FurnitureGenerator>>(File.ReadAllText("GameData/Things/Furniture.json")));  // Furniture 
            WorldGenerator.ThingGenerators.AddRange(JsonConvert.DeserializeObject<List<ToolGenerator>>(File.ReadAllText("GameData/Things/Tools.json")));           // Tools
            WorldGenerator.ThingGenerators.AddRange(JsonConvert.DeserializeObject<List<ArmourGenerator>>(File.ReadAllText("GameData/Things/Armours.json")));       // Armour
            WorldGenerator.ThingGenerators.AddRange(JsonConvert.DeserializeObject<List<WeaponGenerator>>(File.ReadAllText("GameData/Things/Weapons.json")));       // Weapons

            WorldGenerator.RoomGenerators.AddRange(
                JsonConvert.DeserializeObject<List<RoomGenerator>>(File.ReadAllText("GameData/Rooms/Rooms.json")));                             // Rooms

            Console.WriteLine("Done!");
        }

        internal static void RemoveThingFromRoom(Thing thing)
        {
            World.CurrentRoom.Things.Remove(thing);
        }

        internal static void NewGame()
        {
            World = WorldGenerator.New();
            // Clear the inventory.
            Inventory.Clear();
        }


        internal bool IsRunning()
        {
            return World.CurrentRoom != null;
        }

        internal Thing FindThing(string thingID)
        {
            thingID = thingID.ToLower().Replace(' ', '_');
            // Check each area in the room for the entity
            foreach (Thing thing in World.CurrentRoom.Things)
            {
                if (thing.MatchesName(thingID))
                {
                    return thing;
                }
            }
            throw new EntityNotFoundException($"{thingID} was not found!");
        }
    }
}