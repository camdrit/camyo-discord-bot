using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using System.Threading.Tasks;
using System;

namespace Camyo
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;

        private CommandService _service;

        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client;

            _service = new CommandService();

            await _service.AddModulesAsync(Assembly.GetEntryAssembly());

            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            var context = new SocketCommandContext(_client, msg);

            int argPos = 0;
            if (msg.HasCharPrefix('~', ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                Console.WriteLine(DateTime.Now.ToString() + " - User " + context.Message.Author.Username + " (" + context.Message.Author.Id.ToString() + ") sent command: " + context.Message.Content);
                var result = await _service.ExecuteAsync(context, argPos);

                if (!result.IsSuccess)
                {
                    if (result.Error == CommandError.UnknownCommand)
                        await context.Channel.SendMessageAsync("I didn't recognize that commmand but I still love you.");
                    else if (result.ErrorReason == "User not found.")
                        await context.Channel.SendMessageAsync("I don't know what user you were referring to, but I'm sure they're great. :sparkling_heart:");
                    else
                        await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}
