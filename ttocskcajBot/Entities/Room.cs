using System;
using System.Collections.Generic;
using System.Linq;

namespace ttocskcajBot.Entities
{
    class Room
    {
        public List<Portal> Portals { get; set; }
        public List<Area> Areas { get; set; }
        public List<Description> Descriptions { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }

        public double GetLightLevel()
        {
            double lightLevel = 0;
            foreach (Area area in Areas)
            {
                foreach (Thing thing in area.Things)
                {
                    if (thing.IsLightSource())
                    {
                        lightLevel += thing.LightLevel;
                    }
                }
            }
            return lightLevel;
        }

        internal string GetCurrentDescription()
        {
            double lightLevel = GetLightLevel();
            return Descriptions.Where(x => x.MinLightLevel <= lightLevel).OrderBy(x => x.MinLightLevel).First().Message;
        }
    }
}
