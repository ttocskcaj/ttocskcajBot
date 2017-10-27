using System;
using DSharpPlus;
using System.Threading.Tasks;
using System.Collections.Generic;
using ttocskcajBot.Commands;
using static ttocskcajBot.Commands.Command;

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

            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.StartsWith(">"))
                {
                    try
                    {
                        string response = Command.ParseMessage(e.Message).Exec();
                        await e.Message.RespondAsync(response);
                    }
                    catch (Exception ex)
                    {
                        await e.Message.RespondAsync(ex.Message);
                    }
                }
            };

            await discord.ConnectAsync();
            await Task.Delay(-1);

        }

    }
}