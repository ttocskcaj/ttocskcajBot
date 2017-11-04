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
        private Room _currentRoom;
        private Inventory _inventory;
        private List<Room> _rooms;
        private WorldGenerator _worldGenerator;
        private World _world;

        /// <summary>
        /// The singleton instance.
        /// </summary>
        public static Game Instance => _instance ?? (_instance = new Game());


        /// <summary>
        /// Reference to the room the player is currently in.
        /// </summary>
        internal static Room CurrentRoom
        {
            get => Instance._currentRoom;
            set => Instance._currentRoom = value;
        }

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
        }

        public static void LoadGameData()
        {
            // Load ThingGenerator data.
            Console.Write("Loading game data... ");
            WorldGenerator.ThingModels.AddRange(JsonConvert.DeserializeObject<List<FurnitureGenerator>>(File.ReadAllText("GameData/Things/Furniture.json")));  // Furniture 
            WorldGenerator.ThingModels.AddRange(JsonConvert.DeserializeObject<List<ToolGenerator>>(File.ReadAllText("GameData/Things/Tools.json")));           // Tools
            WorldGenerator.ThingModels.AddRange(JsonConvert.DeserializeObject<List<ArmourGenerator>>(File.ReadAllText("GameData/Things/Armours.json")));       // Armour
            WorldGenerator.ThingModels.AddRange(JsonConvert.DeserializeObject<List<WeaponGenerator>>(File.ReadAllText("GameData/Things/Weapons.json")));       // Weapons

            WorldGenerator.RoomModels.AddRange(
                JsonConvert.DeserializeObject<List<RoomGenerator>>(File.ReadAllText("GameData/Rooms/Rooms.json")));                             // Rooms

            Console.WriteLine("Done!");
        }

        internal static void RemoveThingFromRoom(Thing thing)
        {
            CurrentRoom.Things.Remove(thing);
        }

        internal static void NewGame()
        {
            World = (World) WorldGenerator.New();
            // Clear the inventory.
            Inventory.Clear();
        }


        internal bool IsRunning()
        {
            return CurrentRoom != null;
        }

        internal Thing FindThing(string thingID)
        {
            thingID = thingID.ToLower().Replace(' ', '_');
            // Check each area in the room for the entity
            foreach (Thing thing in CurrentRoom.Things)
            {
                if (thing.MatchesName(thingID))
                {
                    return thing;
                }
            }
            throw new EntityNotFoundException($"{thingID} was not found!");
        }



        public static Thing CreateThing(string thingID)
        {
            return (Thing) ThingModels.First(x => x.ID == thingID).CreateEntity();
        }
    }


}