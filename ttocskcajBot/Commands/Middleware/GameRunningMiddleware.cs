using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ttocskcajBot.Exceptions;

namespace ttocskcajBot.Commands.Middleware
{
    class GameRunningMiddleware : IMiddleware
    {
        public bool After(Command command)
        {
            return true;
        }

        public bool Before(Command command)
        {
            // Ensures the game is running before executing a command.
            if (Game.Instance.IsRunning()) return true;
            else throw new GameNotRunningException(Properties.Resources.ResourceManager.GetString("gameNotRunning"));
        }
    }
}
