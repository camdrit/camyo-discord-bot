using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeftyBotGui.Modules
{

    public class Validate : ModuleBase<SocketCommandContext>
    {
        private List<List<string>> _validations;

        [Command("Validate")]
        public async Task ValidateCommand(IGuildUser user)
        {
            _validations = new List<List<string>>() { Helpers.MaleValidations, Helpers.GirlValidations, Helpers.TheyValidations };

            if (!Helpers.Pronouns.pronounsList.ContainsKey(user.Id.ToString()) || (Helpers.Pronouns.pronounsList.ContainsKey(user.Id.ToString()) && Helpers.Pronouns.pronounsList[user.Id.ToString()] == "2"))
            {
                Random rand = new Random();
                int r = rand.Next(_validations[2].Count);
                await ReplyAsync(String.Format(_validations[2][r], user.Username.ToString()));

            }
            else
            {
                Random rand = new Random();
                int thisUserPronouns = Convert.ToInt32(Helpers.Pronouns.pronounsList[user.Id.ToString()]);
                int r = rand.Next(_validations[thisUserPronouns].Count);
                await ReplyAsync(String.Format(_validations[thisUserPronouns][r], user.Username.ToString()));
            }
        }
    }
}