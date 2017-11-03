using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ttocskcajBot.Entities;
using ttocskcajBot.Entities.Things;
using ttocskcajBot.Exceptions;
using ttocskcajBot.Models.Things;

namespace ttocskcajBot
{
    internal class Game
    {
        /// <summary>
        /// Lazy initialization for singleton.
        /// </summary>
        private static readonly Lazy<Game> Lazy = new Lazy<Game>(() => new Game());

        /// <summary>
        /// The singleton instance.
        /// </summary>
        public static Game Instance => Lazy.Value;

        /// <summary>
        /// Reference to the room the player is currently in.
        /// </summary>
        internal Room CurrentRoom { get; set; }

        /// <summary>
        /// The players Inventory.
        /// </summary>
        internal Inventory Inventory { get; set; }

        /// <summary>
        /// Collection of all rooms.
        /// </summary>
        internal List<Room> Rooms { get; set; }

        /// <summary>
        /// Collection of all ThingModels.
        /// Gets loaded with the gamedata and is used to generate Things.
        /// </summary>
        internal List<ThingModel> ThingModels { get; set; }

        public Game()
        {
            Rooms = new List<Room>();
            ThingModels = new List<ThingModel>();
            Inventory = new Inventory();
            LoadGameData();
        }

        private void LoadGameData()
        {
            // Load ThingModel data.
            Debug.Write("Loading game data... ");
            ThingModels.AddRange(JsonConvert.DeserializeObject<List<FurnitureModel>>(File.ReadAllText("GameData/Things/Furniture.json")));  // Furniture 
            ThingModels.AddRange(JsonConvert.DeserializeObject<List<ToolModel>>(File.ReadAllText("GameData/Things/Tools.json")));           // Tools
            ThingModels.AddRange(JsonConvert.DeserializeObject<List<ArmourModel>>(File.ReadAllText("GameData/Things/Armours.json")));       // Armour
            ThingModels.AddRange(JsonConvert.DeserializeObject<List<WeaponModel>>(File.ReadAllText("GameData/Things/Weapons.json")));       // Weapons
            Debug.WriteLine("Done!");
            /* Old room loading data. Rooms are now random.
            // Load room data.
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceResolverProvider = () => new ThingModelReferenceResolver(ThingModels)
            };
            */
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

        internal static void NewGame()
        {
            if (Instance.Rooms.Count < 1)
            {
                Instance.LoadGameData();
            }
            //Instance.CurrentRoom = Instance.Rooms.First(x => x.ID.Equals("dark_room"));
            Instance.Inventory.Clear();
        }
        internal bool IsRunning()
        {
            return CurrentRoom != null;

        }

        internal IEntity FindEntity(string entityName, string type)
        {
            entityName = entityName.ToLower().Replace(' ', '_');
            // Check each area in the room for the entity
            foreach (Area area in CurrentRoom.Areas)
            {
                foreach (Thing thing in area.Things)
                {
                    if (type.Equals("Thing"))
                    {
                        if (thing.MatchesName(entityName))
                        {
                            return thing;
                        }
                    }
                }
                if (area.ID.Equals(entityName) && type == "Area")
                {
                    return area;
                }
            }
            throw new EntityNotFoundException($"{entityName} was not found!");
        }

        public Thing FindThing(string thingID)
        {
            return (Thing)FindEntity(thingID, "Thing");
        }
    }

}