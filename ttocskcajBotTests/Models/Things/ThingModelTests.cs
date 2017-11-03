using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ttocskcajBot;
using ttocskcajBot.Entities.Things;
using ttocskcajBot.Models.Things;

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
            ThingModel thingModel = new FurnitureModel();
            thingModel.ID = "test_thing";
            thingModel.Defaults = new Dictionary<string, object>()
            {
                {"Name", "Test Thing" }
            };

            Thing thing = thingModel.CreateThing();
            Assert.AreEqual(thing.Name, "Test Thing");
        }
        [TestMethod()]
        public void CreateThingTest2()
        {
            ThingModel thingModel = Game.Instance.ThingModels.First(x => x.ID.Equals("fireplace"));

            Thing thing = thingModel.CreateThing();
            Assert.IsTrue(new string[]{"Fire Pit", "Brick Fireplace", "Fireplace", "Marble Fireplace", "Stone Fireplace"}.Contains(thing.Name));
        }
    }
}