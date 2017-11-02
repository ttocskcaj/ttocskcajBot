using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ttocskcajBot.Entities.Things
{
    internal class Thing : IEntity, ICloneable
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool CanTake { get; set; }
        public string EquippedMessage { get; set; }
        public bool Discovered { get; set; }
        public double LightLevel { get; set; }
        public int State { get; set; }
        public string[] AlternativeTerms { get; set; }
        public string[] ActionsAvailable { get; set; }
        public bool LimitedUses { get; set; }
        public int Uses { get; set; }
        public Dictionary<string, Action> ActionsReceivable { get; set; }

        public object this[string propertyName]
        {
            get => GetType().GetProperty(propertyName)?.GetValue(this, null);
            set => GetType().GetProperty(propertyName)?.SetValue(this, value, null);
        }

        internal bool IsLightSource()
        {
            return State > 0 && LightLevel > 0;
        }

        public Thing()
        {
            // Initialize some default property values.
            Discovered = false;
            AlternativeTerms = new string[0];
            ActionsAvailable = new string[0];
            ActionsReceivable = new Dictionary<string, Action>();
            LimitedUses = false;

        }



        public bool MatchesName(string entityName)
        {
            // Check if this things ID or AlternativeTerms matches entityName
            return ID.Equals(entityName) || AlternativeTerms.Contains(entityName);
        }

        public string ExecuteAction(Action action)
        {
            // Change each property listed in the action results. Uses reflection and is probably slow :(
            foreach (KeyValuePair<string, object> resultItem in action.Result)
            {
                string propertyName = resultItem.Key;
                propertyName = char.ToUpper(propertyName[0]) + propertyName.Substring(1);

                // Hack to convert int64 to int32
                object propertyValue = resultItem.Value;
                if (propertyValue is long)
                {
                    propertyValue = Convert.ToInt32(propertyValue);
                }
                this[propertyName] = propertyValue;
            }
            string response = action.Message;


            // Return the message from the action to display to player.
            return response;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
