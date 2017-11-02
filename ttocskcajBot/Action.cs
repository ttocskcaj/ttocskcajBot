using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ttocskcajBot.Entities.Things;

namespace ttocskcajBot
{
    internal class Action
    {
        /// <summary>
        /// A dictionary of properties that get changed on the Thing as a result to this action happening.
        /// </summary>
        public Dictionary<string, object> Result { get; set; }

        /// <summary>
        /// The message to display to the user when this action happens.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Whether or not this action needs a Thing equipped to complete.
        /// </summary>
        public bool NeedsEquippedThing { get; set; }

        /// <summary>
        /// Whether or not the Thing needs to be discovered before this action can happen.
        /// </summary>
        public bool NeedsDiscovering { get; set; }

        /// <summary>
        /// A List of new Things that this action provides/creates when it happens. Dropped on the ground.
        /// </summary>
        public List<Thing> ProvidesThings { get; set; }

        /// <summary>
        /// Whether or not the Thing is destroyed when this action happens.
        /// </summary>
        public bool DestroysThing { get; set; }
    }
}
