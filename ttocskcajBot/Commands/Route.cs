using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Commands.Middleware;

namespace ttocskcajBot.Commands
{
    class Route
    {
        public string[] AcceptedCommands { get; set; }
        public IMiddleware[] Middleware { get; set; }
        public IController Controller { get; set; }

        public Route(string[] acceptedCommands, IController controller)
        {
            AcceptedCommands = acceptedCommands;
            Controller = controller;
        }
        public Route(string[] acceptedCommands, IMiddleware[] middleware, IController controller)
        {
            AcceptedCommands = acceptedCommands;
            Controller = controller;
            Middleware = middleware;
        }

        public bool MiddlewareBefore(Command command)
        {
            if (Middleware != null)
            {
                foreach (IMiddleware mw in Middleware)
                {
                    // If any middleware fail, return false;
                    if (!mw.Before(command)) return false;
                }
            }
            return true;
        }
        public bool MiddlewareAfter(Command command)
        {
            if (Middleware != null)
            {
                foreach (IMiddleware mw in Middleware)
                {
                    mw.After(command);
                }
            }
            return true;
        }
    }
}
