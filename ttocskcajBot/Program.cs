using System;
using System.Threading.Tasks;
using DSharpPlus;
using ttocskcajBot.Commands;
using ttocskcajBot.Commands.Controllers;

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
                        Console.Write(ex.StackTrace);
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

            // Area routes
            Router.AddRoute("inspect", new RouteAction(AreaController.Inspect));

            // Thing interaction routes
            Router.AddRoute("take", new RouteAction(ThingController.Take));

            // Inventory routes.
            Router.AddRoute("inventory", new RouteAction(InventoryController.Inventory));
            Router.AddRoute("drop", new RouteAction(InventoryController.Drop));
            Router.AddRoute("equip", new RouteAction(InventoryController.Equip));
        }
    }
}