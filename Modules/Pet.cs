using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace LeftyBotGui.Modules
{
    public class Pet : ModuleBase<SocketCommandContext>
    {
        [Command("Pet")]
        public async Task PetCommand()
        {
            Random rand = new Random();
            int r = rand.Next(Helpers.PetResponses.Count);
            await ReplyAsync(Helpers.PetResponses[r]);
        }
    }
}
