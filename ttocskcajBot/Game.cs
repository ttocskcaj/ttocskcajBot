using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ttocskcajBot.Entities;
using ttocskcajBot.Exceptions;

namespace ttocskcajBot
{
    internal class Game
    {

        private static readonly Lazy<Game> lazy = new Lazy<Game>(() => new Game());
        public static Game Instance { get { return lazy.Value; } }

        internal Room CurrentRoom { get; set; }



        internal Dictionary<Thing, int> Inventory { get; set; }
        internal List<Room> Rooms { get; set; }

        public Game()
        {
            Rooms = new List<Room>();
            Inventory = new Dictionary<Thing, int>();
            LoadGameData();
        }

        private void LoadGameData()
        {
            string sourceDirectory = "GameData/Rooms";
            foreach (string file in Directory.EnumerateFiles(sourceDirectory, "*.json", SearchOption.AllDirectories))
            {
                string contents = File.ReadAllText(file);
                Rooms.Add(JsonConvert.DeserializeObject<Room>(contents));
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

        internal IEntity FindEntity(string entityName)
        {
            entityName = entityName.ToLower().Replace(' ', '_');
            // Check each area in the room for the entity
            foreach (Area area in CurrentRoom.Areas)
            {
                if (area.ID.Equals(entityName))
                {
                    return area;
                }

            }
            throw new EntityNotFoundException(String.Format("{0} was not found!", entityName));
        }
    }

}