using System;
using System.Collections.Generic;
using ttocskcajBot.Exceptions;

namespace ttocskcajBot.Commands
{
    internal class Router
    {
        private Dictionary<string, RouteAction> Routes { get; }

        private static readonly Lazy<Router> Lazy = new Lazy<Router>(() => new Router());
        public static Router Instance => Lazy.Value;

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

        internal static void AddRoute(string commandVerb, RouteAction routeAction)
        {
            Instance.Routes.Add(commandVerb, routeAction);
        }
    }
}