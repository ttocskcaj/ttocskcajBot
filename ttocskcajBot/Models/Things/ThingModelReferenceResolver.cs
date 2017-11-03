using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace ttocskcajBot.Models.Things
{
    internal class ThingModelReferenceResolver : IReferenceResolver
    {
        private readonly List<ThingModel> _thingModels;

        internal ThingModelReferenceResolver(List<ThingModel> things)
        {
            _thingModels = things;
        }
        public object ResolveReference(object context, string reference)
        {
            return _thingModels.First(x => x.ID.Equals(reference));
        }

        public string GetReference(object context, object value)
        {
            ThingModel thing = (ThingModel)value;
            return thing.ID;
        }

        public bool IsReferenced(object context, object value)
        {
            ThingModel thing = (ThingModel)value;

            return _thingModels.Contains(thing);
        }

        public void AddReference(object context, string reference, object value)
        {
            _thingModels.Add((ThingModel)value);
        }
    }
}
