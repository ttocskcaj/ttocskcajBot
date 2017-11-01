using System;
using DSharpPlus;
using System.Threading.Tasks;
using System.Collections.Generic;
using ttocskcajBot.Commands;
using static ttocskcajBot.Commands.Command;
using static ttocskcajBot.Commands.RouteAction;

namespace ttocskcajBot
{
    class Program
    {
        static DiscordClient discord;
        private static Game game;

        internal Router Router { get; set; }
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        static async Task MainAsync(string[] args)
        {
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "MzY2ODcwMjYxNzk2MTc1ODc1.DLzKLQ.oq9i7LxSWhAFMufTBiqZlWS_oUQ",
                TokenType = TokenType.Bot
            });

            game = Game.Instance;
            SetupRoutes();

            discord.MessageCreated += async e =>
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

            await discord.ConnectAsync();
            await Task.Delay(-1);

        }
        static void SetupRoutes()
        {
            // Game control routes
            Router.AddRoute("new", new RouteAction(new ActionDelegate(GameController.New)));
            Router.AddRoute("help", new RouteAction(new ActionDelegate(GameController.Help)));

            // Area routes
            Router.AddRoute("inspect", new RouteAction(new ActionDelegate(AreaController.Inspect)));

            // Thing interaction routes
            Router.AddRoute("take", new RouteAction(new ActionDelegate(ThingController.Take)));

            // Inventory routes.
            Router.AddRoute("inventory", new RouteAction(new ActionDelegate(InventoryController.Inventory)));
            Router.AddRoute("drop", new RouteAction(new ActionDelegate(InventoryController.Drop)));
            Router.AddRoute("equip", new RouteAction(new ActionDelegate(InventoryController.Equip)));
        }
    }
}