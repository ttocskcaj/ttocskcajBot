using System.Collections.Generic;
using ttocskcajBot.Entities;
using ttocskcajBot.Entities.Things;
using ttocskcajBot.Models;
using ttocskcajBot.Tools;

namespace ttocskcajBot.Generators.Things
{
    public abstract class ThingGenerator : IGenerator
    {
        /// <summary>
        /// The ID.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// A collection of default properties for the generated thing.
        /// </summary>
        public Dictionary<string, object> Defaults { get; set; }

        /// <summary>
        /// A collection of variations/additions to properties for the generated thing.
        /// </summary>
        public List<Dictionary<string, object>> Variations { get; set; }

        /// <summary>
        /// Actions that this thing is capable of.
        /// </summary>
        public string[] ActionsAvailable { get; set; }

        /// <summary>
        /// The Actions that can be performed on this Thing.
        /// </summary>
        public Dictionary<string, Action> ActionsReceivable { get; set; }

        /// <summary>
        /// A constructor that initializes some properties to default values.
        /// </summary>
        protected ThingGenerator()
        {
            // Initialize some default property values.
            Defaults = new Dictionary<string, object>();
            Variations = new List<Dictionary<string, object>>();
            ActionsAvailable = new string[0];
            ActionsReceivable = new Dictionary<string, Action>();
        }
         
        public Thing New()
        {
            return New(true, -1);
        }

        public Thing New(bool useVariations)
        {
            return New(useVariations, -1);
        }

        public Thing New(bool useVariations, int variationKey)
        {
            // Create a new thing object.
            Thing thing = new Thing
            {
                ID = ID,
                ActionsAvailable = ActionsAvailable,
                ActionsReceivable = ActionsReceivable
            };

            // Assign each default property to the thing.
            thing.SetProperties(Defaults);


            if (Variations.Count > 0 && useVariations)
            {
                // 50% chance of picking a variation.
                Chance.DoByChance(0.5, () =>
                {
                    if (variationKey < 0)
                    {
                        // Pick a random variation to use.
                        variationKey = Chance.RandomInt(0, Variations.Count);
                    }
                    thing.SetProperties(Variations[variationKey]);
                });
            }
            return thing;
        }
        IEntity IGenerator.New()
        {
            return New();
        }

    }
}
