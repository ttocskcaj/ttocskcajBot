﻿using System.IO;

namespace ttocskcajBot.Commands.Controllers
{
    internal class GameController : IController
    {
        public static CommandResponse New(Command command)
        {
            Game.NewGame();
            return new CommandResponse(Game.World.CurrentRoom.Description);
        }
        public static CommandResponse Help(Command command)
        {
            return new CommandResponse(File.ReadAllText("GameData/Help.txt"));
        }

    }
}