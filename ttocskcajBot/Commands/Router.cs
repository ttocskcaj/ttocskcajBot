using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Commands.Middleware;
using ttocskcajBot.Exceptions;
using static ttocskcajBot.Commands.Command;

namespace ttocskcajBot.Commands
{
    internal class Router
    {
        private Dictionary<String, RouteAction> Routes { get; set; }

        private static readonly Lazy<Router> lazy = new Lazy<Router>(() => new Router());
        public static Router Instance { get { return lazy.Value; } }
        public Router()
        {
            Routes = new Dictionary<string, RouteAction>();
        }

        internal static CommandResponse Route(Command command)
        {
            if (Instance.Routes.ContainsKey(command.Verb))
            {
                return Instance.Routes[command.Verb].Action(command);
            }
            throw new CommandException("Command doesn't exist! Check out ```.help```");
        }

        internal static void AddRoute(String commandVerb, RouteAction routeAction)
        {
            Instance.Routes.Add(commandVerb, routeAction);
        }
    }
}