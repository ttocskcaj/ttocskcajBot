using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ttocskcajBot;
using ttocskcajBot.Entities.Things;
using ttocskcajBot.Generators.Things;

namespace ttocskcajBotTests.Models.Things
{
    [TestClass()]
    public class ThingModelTests
    {
        [TestInitialize]
        public void Initialize()
        {
            // Creates a new game and loads the GameData.
            Game.NewGame();
        }

        [TestMethod()]
        public void CreateThingTest1()
        {
            ThingGenerator thingGenerator = new FurnitureGenerator();
            thingGenerator.ID = "test_thing";
            thingGenerator.Defaults = new Dictionary<string, object>()
            {
                {"Name", "Test Thing" }
            };

            Thing thing = (Thing) thingGenerator.New();
            Assert.AreEqual(thing.Name, "Test Thing");
        }
        [TestMethod()]
        public void CreateThingTest2()
        {
            ThingGenerator thingGenerator = Game.WorldGenerator.ThingModels.First(x => x.ID.Equals("fireplace"));

            Thing thing = (Thing)thingGenerator.New();
            Assert.IsTrue(new string[]{"Fire Pit", "Brick Fireplace", "Fireplace", "Marble Fireplace", "Stone Fireplace"}.Contains(thing.Name));
        }
    }
}