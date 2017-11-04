using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace ttocskcajBot.Generators.Things
{
    internal class ThingModelReferenceResolver : IReferenceResolver
    {
        private readonly List<ThingGenerator> _thingModels;

        internal ThingModelReferenceResolver(List<ThingGenerator> things)
        {
            _thingModels = things;
        }
        public object ResolveReference(object context, string reference)
        {
            return _thingModels.First(x => x.ID.Equals(reference));
        }

        public string GetReference(object context, object value)
        {
            ThingGenerator thing = (ThingGenerator)value;
            return thing.ID;
        }

        public bool IsReferenced(object context, object value)
        {
            ThingGenerator thing = (ThingGenerator)value;

            return _thingModels.Contains(thing);
        }

        public void AddReference(object context, string reference, object value)
        {
            _thingModels.Add((ThingGenerator)value);
        }
    }
}
