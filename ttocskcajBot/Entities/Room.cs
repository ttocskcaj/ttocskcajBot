using System.Collections.Generic;
using System.Linq;

namespace ttocskcajBot.Entities
{
    internal class Room
    {
        public List<Portal> Portals { get; set; }
        public List<Area> Areas { get; set; }
        public List<Description> Descriptions { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }

        public double GetLightLevel()
        {
            return Areas.SelectMany(area => area.Things, (area, thing) => new {area, thing})
                .Where(x => x.thing.IsLightSource())
                .Select(x => x.thing.LightLevel).Sum();
        }

        internal string GetCurrentDescription()
        {
            double lightLevel = GetLightLevel();
            return Descriptions.Where(x => x.MinLightLevel <= lightLevel).OrderByDescending(x => x.MinLightLevel).First().Message;
        }
    }
}
