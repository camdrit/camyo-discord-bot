using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace LeftyBotGui.Modules
{
    class Clean : ModuleBase<SocketCommandContext>
    {
        [Command("Clean")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task CleanCommand([Remainder] int Delete = 0)
        {
            IGuildUser Bot = Context.Guild.GetUser(Context.Client.CurrentUser.Id);
            await Context.Message.DeleteAsync();
            if (Delete == 0)
            {
                await ReplyAsync("I'm happy to clean up the chat for you but I need a specified number of messages to delete. :c");
                return;
            }
            int Amount = 0;
            if (Delete <= 100)
            {
                foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
                {
                    Amount++;
                    var enumerator = Item.MentionedUserIds.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        object curID = enumerator.Current;
                        if (curID.ToString() == Bot.Id.ToString() && Item.Attachments.Count > 1)
                            await Item.DeleteAsync();
                    }
                    if ((Item.Author.Id == Bot.Id || Item.Content.StartsWith(Helpers.Prefix.ToString())) && Item.Attachments.Count > 1)
                        await Item.DeleteAsync();

                }
                var m = await ReplyAsync("Cleaned up " + Amount + " messages.");
                await Task.Delay(5000);
                await m.DeleteAsync();

            }
            else
            {
                await ReplyAsync("I'm happy to clean up the chat for you but I can't delete more than 100 messages :c");
            }

        }
    }
}
