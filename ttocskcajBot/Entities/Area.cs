﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttocskcajBot.Entities
{
    class Area : IEntity
    {
        public List<Thing> Things { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
    }
}
