using System.Collections.Generic;
using System.Runtime.InteropServices;
using ttocskcajBot.Entities;
using ttocskcajBot.Entities.Things;
using ttocskcajBot.Models;
using ttocskcajBot.Tools;

namespace ttocskcajBot.Generators
{
    public class RoomGenerator : IGenerator
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Dictionary<string, List<string>> Materials { get; set; }
        public Dictionary<string,double> Things { get; set; }


        public Room New()
        {
            Room room = new Room
            {
                ID = ID,
                Name = Name,
                FloorMaterial = Materials["Floor"].PickRandom(),
                WallMaterial = Materials["Walls"].PickRandom()
            };

            foreach (KeyValuePair<string, double> thingChance in Things)
            {
                Chance.DoByChance(thingChance.Value, () =>
                {
                    Thing thing = Game.WorldGenerator.GetNewThing(thingChance.Key);
                    room.Things.Add(thing);
                });
            }
            return room;
        }

        IEntity IGenerator.New()
        {
            return New();
        }
        
    }
}
