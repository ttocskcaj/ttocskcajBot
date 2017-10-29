using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttocskcajBot.Entities
{
    internal class ThingReferenceResolver : IReferenceResolver
    {
        private List<Thing> things;

        internal ThingReferenceResolver(List<Thing> things)
        {
            this.things = things;
        }
        public object ResolveReference(object context, string reference)
        {
            Thing thing = things.Where(x => x.ID.Equals(reference)).First();
            if (thing != null)
            {
                return thing.Clone();
            }

            return null;
        }

        public string GetReference(object context, object value)
        {
            Thing thing = (Thing)value;
            return thing.ID;
        }

        public bool IsReferenced(object context, object value)
        {
            Thing thing = (Thing)value;

            return things.Contains(thing);
        }

        public void AddReference(object context, string reference, object value)
        {
            things.Add((Thing)value);
        }
    }
}
