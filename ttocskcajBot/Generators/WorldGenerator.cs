using System;
using System.Collections.Generic;
using System.Linq;
using ttocskcajBot.Entities;
using ttocskcajBot.Entities.Things;
using ttocskcajBot.Generators.Things;
using ttocskcajBot.Models;
using ttocskcajBot.Tools;

namespace ttocskcajBot.Generators
{
    public class WorldGenerator : IGenerator
    {
        /// <summary>
        /// Collection of all ThingGenerators.
        /// Gets loaded with the gamedata and is used to generate Things.
        /// </summary>
        public  List<ThingGenerator> ThingGenerators { get; set; }

        /// <summary>
        /// Collection of all RoomGenerators.
        /// Gets loaded with the gamedata and is used to generate Rooms.
        /// </summary>
        public  List<RoomGenerator> RoomGenerators { get; set; }

        public WorldGenerator()
        {
            ThingGenerators = new List<ThingGenerator>();
            RoomGenerators = new List<RoomGenerator>();
        }

        public World New()
        {
            Console.WriteLine("Creating a new world!");
            World world = new World();
            Room startRoom = RoomGenerators[Chance.RandomInt(0, RoomGenerators.Count)].New();
            Console.WriteLine("Starting room: \n" + startRoom.Description);
            world.CurrentRoom = startRoom;
            return world;
        }

        IEntity IGenerator.New()
        {
            return New();
        }

        public Thing GetNewThing(string thingID)
        {
            return ThingGenerators.First(x => x.ID == thingID).New();
        }
    }
}