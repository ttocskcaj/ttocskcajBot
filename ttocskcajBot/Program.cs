using System;
using DSharpPlus;
using System.Threading.Tasks;
using System.Collections.Generic;
using ttocskcajBot.Commands;

namespace ttocskcajBot
{
    class Program
    {
        static DiscordClient discord;
        static List<Player> players;
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

            
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.StartsWith(">")){
                    string response = CommandRunner.Exec(Command.ParseMessage(e.Message));
                    await e.Message.RespondAsync(response);
                }
            };

            await discord.ConnectAsync();
            await Task.Delay(-1);

        }

    }
}