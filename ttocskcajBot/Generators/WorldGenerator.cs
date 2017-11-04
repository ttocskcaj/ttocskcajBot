using System.Collections.Generic;
using ttocskcajBot.Entities;
using ttocskcajBot.Generators.Things;
using ttocskcajBot.Models;

namespace ttocskcajBot.Generators
{
    public class WorldGenerator: IGenerator
    {
        /// <summary>
        /// Collection of all ThingModels.
        /// Gets loaded with the gamedata and is used to generate Things.
        /// </summary>
        internal static List<ThingGenerator> ThingModels { get; set; }

        /// <summary>
        /// Collection of all RoomModels.
        /// Gets loaded with the gamedata and is used to generate Rooms.
        /// </summary>
        public static List<RoomGenerator> RoomModels { get; set; }

        public IEntity New()
        {
            return new World();
        }
    }
}