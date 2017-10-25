using System;

namespace ttocskcajBot.Commands.Controllers
{
    internal interface IController
    {
        string RunCommand(Command command);
    }
}