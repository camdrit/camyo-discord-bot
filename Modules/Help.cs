using Discord.Commands;
using System.Threading.Tasks;

namespace LeftyBotGui.Modules
{

    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("Help")]
        public async Task HelpCommand()
        {
            await Context.Channel.SendMessageAsync("All commands can be executed either by using the prefix **" + Helpers.Prefix + "** or by mentioning me!\n\n***" + Helpers.Prefix + "help***: Displays this dialogue.\n***" + Helpers.Prefix + "birthday m/d/yy***: allows you to set your birthday. I'll remember the date and give a reminder the day prior at 12:00:00 UTC.\n***" + Helpers.Prefix + "validate me/username***: Gives a random bit of love to either yourself or a gamer of your specification.\n***" + Helpers.Prefix + "pronouns me/he/she/they***: allows you to set or check your pronouns. I'll remember them and only ever refer to you using those pronouns.");
        }
    }
}
