using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttocskcajBot
{
    internal class Action
    {
        public Dictionary<string, object> Result { get; set; }
        public string Message { get; set; }
        public bool NeedsEquippedThing { get; set; }
        public bool NeedsDiscovering { get; set; }
    }
}
