using System.IO;
using System.Linq;
using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Exceptions;
using static ttocskcajBot.Commands.Command;

namespace ttocskcajBot.Commands
{
    internal class GameController : IController
    {
        public static CommandResponse New(Command command)
        {
            Game.Instance.NewGame();
            //return Game.Instance.CurrentRoom.GetCurrentDescription();
            return new CommandResponse(Game.Instance.CurrentRoom.GetCurrentDescription());
        }
        public static CommandResponse Help(Command command)
        {
            return new CommandResponse(File.ReadAllText("GameData/Help.txt"));
        }

    }
}