using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ttocskcajBot.Entities.Things
{
    internal class Thing : IEntity, ICloneable
    {
        /// <summary>
        /// The unique ID.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// The Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Whether or not the Thing can be taken by a player
        /// </summary>
        public bool CanTake { get; set; }

        /// <summary>
        /// A message to display to the player when the Thing is equipped.
        /// </summary>
        public string EquippedMessage { get; set; }

        /// <summary>
        /// Whether or not the Thing has been discovered.
        /// </summary>
        public bool Discovered { get; set; }

        /// <summary>
        /// The amount of light the Thing produces.
        /// </summary>
        public double LightLevel { get; set; }

        /// <summary>
        /// The state of the Thing. E.G. is it on or off?
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Alternative terms that the user can find this Thing with.
        /// </summary>
        public string[] AlternativeTerms { get; set; }

        /// <summary>
        /// Actions that this thing is capable of.
        /// </summary>
        public string[] ActionsAvailable { get; set; }

        /// <summary>
        /// Whether or not this Thing has limited uses.
        /// </summary>
        public bool LimitedUses { get; set; }

        /// <summary>
        /// How many uses this Thing has remaining.
        /// </summary>
        public int Uses { get; set; }

        /// <summary>
        /// The Actions that can be performed on this Thing.
        /// </summary>
        public Dictionary<string, Action> ActionsReceivable { get; set; }

        /// <summary>
        /// Allows setting/getting of properties by their name using reflection.
        /// </summary>
        /// <param name="propertyName">The name of the property to set/get</param>
        /// <returns>The value of the specified property.</returns>
        public object this[string propertyName]
        {
            get => GetType().GetProperty(propertyName)?.GetValue(this, null);
            set => GetType().GetProperty(propertyName)?.SetValue(this, value, null);
        }

        /// <summary>
        /// Whether or not this Thing is currently a light source. I.E. Is it on, and does it provide light?
        /// </summary>
        /// <returns>True if the Thing is producing light.</returns>
        internal bool IsLightSource()
        {
            return State > 0 && LightLevel > 0;
        }

        /// <summary>
        /// A constructor that initializes some properties to default values.
        /// </summary>
        public Thing()
        {
            // Initialize some default property values.
            Discovered = false;
            AlternativeTerms = new string[0];
            ActionsAvailable = new string[0];
            ActionsReceivable = new Dictionary<string, Action>();
            LimitedUses = false;

        }

        /// <summary>
        /// Checks if this Thing matches a name/id provided or the provided name matches an alternativeTerm.
        /// </summary>
        /// <param name="entityName">The name to check.</param>
        /// <returns>True if the Thing matches that name.</returns>
        public bool MatchesName(string entityName)
        {
            // Check if this things ID or AlternativeTerms matches entityName
            return ID.Equals(entityName) || AlternativeTerms.Contains(entityName);
        }

        /// <summary>
        /// Executes the provided action on this Thing.
        /// </summary>
        /// <param name="action">The Action to perform.</param>
        /// <returns>A response to show the user.</returns>
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

        /// <inheritdoc />
        /// <summary>
        /// Creates a memberwise clone of this Thing.
        /// </summary>
        /// <returns>A new Thing object that is a clone of this one.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
