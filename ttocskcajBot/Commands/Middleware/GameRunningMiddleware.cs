using ttocskcajBot.Exceptions;
using ttocskcajBot.Properties;

namespace ttocskcajBot.Commands.Middleware
{
    internal class GameRunningMiddleware : IMiddleware
    {
        public bool After(Command command)
        {
            return true;
        }

        public bool Before(Command command)
        {
            // Ensures the game is running before executing a command.
            if (Game.Instance.IsRunning()) return true;
            throw new GameNotRunningException(Resources.ResourceManager.GetString("gameNotRunning"));
        }
    }
}
