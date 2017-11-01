using System.Collections.Generic;
using ttocskcajBot.Entities.Things;

namespace ttocskcajBot.Entities
{
    internal class Area : IEntity
    {
        public List<Thing> Things { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
    }
}
