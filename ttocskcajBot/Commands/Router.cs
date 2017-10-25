using System;
using System.Collections.Generic;
using System.Linq;
using ttocskcajBot.Commands.Controllers;
using static ttocskcajBot.Commands.Command;

namespace ttocskcajBot.Commands
{
    internal class Router
    {
        private Dictionary<List<string>, IController> routes;

        private static readonly Lazy<Router> lazy = new Lazy<Router>(() => new Router());
        public static Router Instance { get { return lazy.Value; } }

        public Router()
        {
            routes = new Dictionary<List<string>, IController>() {
                {  new List<string>(){ "move" }, new RoomController() }
            };
        }
        internal IController GetCommandController(Command command)
        {
            try
            {
                return routes.Where(x => x.Key.Contains(command.Verb)).First().Value;
            }
            catch (InvalidOperationException)
            {
                throw new CommandException("Command doesn't exist!");
            }

        }
    }
}