using System;
using System.Collections.Generic;
using ttocskcajBot.Commands.Middleware;
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
            if (!Instance.Routes.ContainsKey(command.Verb))
                throw new CommandException("Command doesn't exist! Check out ```.help```");

            // Run each middleware before method.
            if (Instance.Routes[command.Verb].Middleware != null)
            {
                foreach (IMiddleware mw in Instance.Routes[command.Verb].Middleware)
                {
                    mw.Before(command);
                }
            }

            // Exeucte the commands action.
            CommandResponse response = Instance.Routes[command.Verb].Action(command);

            // Run each middleware after method.
            if (Instance.Routes[command.Verb].Middleware != null)
            {
                foreach (IMiddleware mw in Instance.Routes[command.Verb].Middleware)
                {
                    mw.After(command);
                }
            }

            return response;
        }

        internal static void AddRoute(string commandVerb, RouteAction routeAction)
        {
            Instance.Routes.Add(commandVerb, routeAction);
        }
    }
}