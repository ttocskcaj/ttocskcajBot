
using System;

namespace ttocskcajBot.Tools
{
    public class Chance
    {
        private readonly Random _random;

        /// <summary>
        /// Lazy initialization for singleton.
        /// </summary>
        private static readonly Lazy<Chance> Lazy = new Lazy<Chance>(() => new Chance());

        /// <summary>
        /// The singleton instance.
        /// </summary>
        public static Chance Instance => Lazy.Value;

        public Chance()
        {
            _random = new Random(295874942);
        }

        public static void DoByChance(double d, System.Action action)
        {
            if (Instance._random.NextDouble() <= d)
            {
                action();
            }
        }

        public static int RandomInt(int min, int max)
        {
            return Instance._random.Next(min, max);
        }
    }
}
