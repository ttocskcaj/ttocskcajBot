using System;
using System.Collections.Generic;

namespace ttocskcajBot.Entities
{
    class Room
    {
        public List<Portal> Portals {get; set; }
        public List<Area> Areas { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
    }
}
