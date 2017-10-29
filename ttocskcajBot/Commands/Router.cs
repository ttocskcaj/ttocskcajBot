using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Commands.Middleware;
using static ttocskcajBot.Commands.Command;

namespace ttocskcajBot.Commands
{
    internal class Router
    {
        private List<Route> Routes { get; set; }

        private static readonly Lazy<Router> lazy = new Lazy<Router>(() => new Router());
        public static Router Instance { get { return lazy.Value; } }

        public Router()
        {
            Routes = new List<Route>() {
                new Route(new string[]{ "new", "help" }, new GameController()),
                new Route(new string[]{ "inspect" }, new IMiddleware[] {new GameRunningMiddleware() }, new AreaController()),
                new Route(new string[]{ "take" }, new IMiddleware[] {new GameRunningMiddleware() }, new ThingController()),
                new Route(new string[]{ "inventory", "drop", "equip" }, new IMiddleware[] {new GameRunningMiddleware() }, new InventoryController())
            };
        }
        internal static Route GetRoute(Command command)
        {
            try
            {
                return Instance.Routes.Where(x => x.AcceptedCommands.Contains(command.Verb)).First();
            }
            catch (InvalidOperationException)
            {
                throw new CommandException(Properties.Resources.ResourceManager.GetString("commandNotFound"));
            }

        }
    }
}