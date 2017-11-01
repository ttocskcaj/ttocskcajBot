using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Commands.Middleware;

namespace ttocskcajBot.Commands
{
    class RouteAction
    {
        public delegate CommandResponse ActionDelegate(Command command);

        public IMiddleware[] Middleware { get; set; }
        public ActionDelegate Action { get; set; }

        public RouteAction(ActionDelegate action)
        {
            Action = action;
        }
        public RouteAction(IMiddleware[] middleware, ActionDelegate action)
        {
            Action = action;
            Middleware = middleware;
        }

    }
}
