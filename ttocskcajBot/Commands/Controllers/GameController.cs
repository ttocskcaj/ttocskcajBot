using System.IO;
using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Exceptions;
using static ttocskcajBot.Commands.Command;

namespace ttocskcajBot.Commands
{
    internal class GameController : IController
    {
        public string RunCommand(Command command)
        {

            if (command.Verb.Equals("new"))
            {
                Game.Instance.NewGame();
                return Game.Instance.CurrentRoom.Description;
            }
            if (command.Verb.Equals("help"))
            {
                return File.ReadAllText("GameData/Help.txt");
            }
            throw new CommandException(Properties.Resources.ResourceManager.GetString("commandNotFound"));
        }

    }
}