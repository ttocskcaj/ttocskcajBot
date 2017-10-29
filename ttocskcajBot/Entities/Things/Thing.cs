﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttocskcajBot.Entities
{
    class Thing : IEntity, ICloneable
    {
        public string ID {get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool CanTake { get; set; }
        public string EquippedMessage { get; set; }
        public bool Discovered { get; set; }
        public double LightLevel { get; set; }
        public int State { get; set; }

        internal bool IsLightSource()
        {
            return (State > 0 && LightLevel > 0);
        }

        public Thing()
        {
            Discovered = false;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
