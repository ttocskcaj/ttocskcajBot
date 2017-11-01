using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttocskcajBot.Exceptions
{
    class CommandException : Exception
    {
        public CommandException(string message) : base(message)
        {
        }
    }
}
