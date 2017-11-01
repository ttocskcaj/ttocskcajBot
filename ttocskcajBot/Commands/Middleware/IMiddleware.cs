namespace ttocskcajBot.Commands.Middleware
{
    internal interface IMiddleware
    {
        bool Before(Command command);

        bool After(Command command);
    }
}
