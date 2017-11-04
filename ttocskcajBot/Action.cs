using System.Collections.Generic;

namespace ttocskcajBot
{
    public class Action
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
        /// A Dictionary of ThingIDs that this action may provide/create when executed and the chance of each happening.
        /// </summary>
        public Dictionary<string, double> ProvidesThings { get; set; }

        /// <summary>
        /// Whether or not the Thing is destroyed when this action happens.
        /// </summary>
        public bool DestroysThing { get; set; }
    }
}
