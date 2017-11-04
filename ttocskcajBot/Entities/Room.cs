using System.Collections.Generic;
using System.Linq;
using ttocskcajBot.Entities.Things;

namespace ttocskcajBot.Entities
{
    internal class Room : IEntity
    {
        public List<Portal> Portals { get; set; }
        public List<Description> Descriptions { get; set; }
        public List<Thing> Things { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }

        public double GetLightLevel()
        {
            return Things.Where(x => x.IsLightSource()).Select(x => x.LightLevel).Sum();
        }

        internal string GetCurrentDescription()
        {
            double lightLevel = GetLightLevel();
            return Descriptions.Where(x => x.MinLightLevel <= lightLevel).OrderByDescending(x => x.MinLightLevel).First().Message;
        }
    }
}
