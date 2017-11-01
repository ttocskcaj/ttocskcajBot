using System.IO;

namespace ttocskcajBot.Commands.Controllers
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