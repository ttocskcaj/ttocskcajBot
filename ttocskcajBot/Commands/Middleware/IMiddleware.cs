using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttocskcajBot.Commands.Middleware
{
    interface IMiddleware
    {
        bool Before(Command command);

        bool After(Command command);
    }
}
