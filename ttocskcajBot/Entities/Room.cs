using System.Collections.Generic;
using System.Linq;
using AvsAnLib;
using ttocskcajBot.Entities.Things;

namespace ttocskcajBot.Entities
{
    public class Room : IEntity
    {
        public List<Portal> Portals { get; set; }
        public List<Thing> Things { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public string WallMaterial { get; set; }
        public string FloorMaterial { get; set; }

        public Room()
        {
            Portals = new List<Portal>();
            Things = new List<Thing>();
        }
        public double GetLightLevel()
        {
            return Things.Where(x => x.IsLightSource()).Select(x => x.LightLevel).Sum();
        }

        internal string Description
        {
            get
            {
                string description = $"A {Name.ToLower()} with floors of {FloorMaterial} and walls of {WallMaterial}\n";
                for (int index = 0; index < Things.Count; index++)
                {
                    Thing thing = Things[index];
                    if (thing.IsLightSource() || this.GetLightLevel() > 0)
                    {
                        if (index == Things.Count - 1)
                        {
                            description += "and ";
                        }
                        description += $"{AvsAn.Query(thing.Name.Split(' ').First()).Article} {thing.Name.ToLower()}, ";
                    }
                }
                return description.TrimEnd(',', ' ');
            }
        }

    }
}
