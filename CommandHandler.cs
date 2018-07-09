using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;

namespace LeftyBotGui
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
            if (!(s is SocketUserMessage msg)) return;

            var context = new SocketCommandContext(_client, msg);

            int argPos = 0;
            if (msg.HasCharPrefix(Helpers.Prefix, ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                
                Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - User " + context.Message.Author.Username + " (" + context.Message.Author.Id.ToString() + ") sent command: " + context.Message.Content + "\n", System.Drawing.Color.White);
                var result = await _service.ExecuteAsync(context, argPos);

                if (!result.IsSuccess)
                {
                    if (result.Error == CommandError.UnknownCommand)
                        await context.Channel.SendMessageAsync("mrrrp?.");
                    else if (result.ErrorReason == "User not found.")
                        await context.Channel.SendMessageAsync("I don't know what user you were referring to, but I'm sure they're great. :sparkling_heart:");
                    else
                        await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}
