using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DSharpPlus;
using ttocskcajBot.Commands;
using ttocskcajBot.Commands.Controllers;
using ttocskcajBot.Commands.Middleware;

namespace ttocskcajBot
{
    internal class Program
    {
        private static DiscordClient _discord;

        internal Router Router { get; set; }

        private static void Main()
        {
            MainAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            _discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "MzY2ODcwMjYxNzk2MTc1ODc1.DLzKLQ.oq9i7LxSWhAFMufTBiqZlWS_oUQ",
                TokenType = TokenType.Bot
            });

            SetupRoutes();

            _discord.MessageCreated += async e =>
            {
                if (e.Message.Content.StartsWith("."))
                {
                    try
                    {
                        Command command = Command.ParseMessage(e.Message);
                        CommandResponse response = Router.Route(command);
                        await e.Message.RespondAsync(response.MessageResponse);

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        Debug.WriteLine(ex.StackTrace);
                        await e.Message.RespondAsync(ex.Message);
                    }
                }
            };

            await _discord.ConnectAsync();
            await Task.Delay(-1);

        }

        private static void SetupRoutes()
        {
            // Game control routes
            Router.AddRoute("new", new RouteAction(GameController.New));
            Router.AddRoute("help", new RouteAction(GameController.Help));

            // Room/Portal routes
            Router.AddRoute("room", new RouteAction(new IMiddleware[] { new GameRunningMiddleware() }, RoomController.Room));
            Router.AddRoute("enter", new RouteAction(new IMiddleware[] { new GameRunningMiddleware() }, PortalController.Enter));


            // Area routes
            Router.AddRoute("inspect", new RouteAction(new IMiddleware[] { new GameRunningMiddleware() }, AreaController.Inspect));

            // Thing interaction routes
            Router.AddRoute("take", new RouteAction(new IMiddleware[] { new GameRunningMiddleware() }, ThingController.Take));
            Router.AddRoute("light", new RouteAction(new IMiddleware[] { new GameRunningMiddleware() }, ThingController.Action));
            Router.AddRoute("open", new RouteAction(new IMiddleware[] { new GameRunningMiddleware() }, ThingController.Action));


            // Inventory routes.
            Router.AddRoute("inventory", new RouteAction(new IMiddleware[] { new GameRunningMiddleware() }, InventoryController.Inventory));
            Router.AddRoute("drop", new RouteAction(new IMiddleware[] { new GameRunningMiddleware() }, InventoryController.Drop));
            Router.AddRoute("equip", new RouteAction(new IMiddleware[] { new GameRunningMiddleware() }, InventoryController.Equip));
        }
    }
}