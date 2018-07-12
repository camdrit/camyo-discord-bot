using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace LeftyBotGui.Modules
{

    public class Hi : ModuleBase<SocketCommandContext>
    {

        [Command("Hi")]
        public async Task HewwoCommand()
        {
            await ReplyAsync(MentionUtils.MentionUser(Context.Message.Author.Id) + " mrrrreeeoooww!");
        }
    }
}
