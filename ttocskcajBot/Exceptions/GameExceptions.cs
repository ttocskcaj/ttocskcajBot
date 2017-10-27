using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttocskcajBot.Exceptions
{
    class GameNotRunningException : Exception
    {
        public GameNotRunningException(string message) : base(message)
        {
        }
    }
}
