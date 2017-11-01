using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace ttocskcajBot.Entities.Things
{
    internal class ThingReferenceResolver : IReferenceResolver
    {
        private readonly List<Thing> _things;

        internal ThingReferenceResolver(List<Thing> things)
        {
            _things = things;
        }
        public object ResolveReference(object context, string reference)
        {
            Thing thing = _things.First(x => x.ID.Equals(reference));
            return thing?.Clone();
        }

        public string GetReference(object context, object value)
        {
            Thing thing = (Thing)value;
            return thing.ID;
        }

        public bool IsReferenced(object context, object value)
        {
            Thing thing = (Thing)value;

            return _things.Contains(thing);
        }

        public void AddReference(object context, string reference, object value)
        {
            _things.Add((Thing)value);
        }
    }
}
