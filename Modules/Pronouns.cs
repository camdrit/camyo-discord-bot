using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace LeftyBotGui.Modules
{

    public class Pronouns : ModuleBase<SocketCommandContext>
    {
        [Command("Pronouns")]
        public async Task PronounsCommand(IGuildUser user)
        {
            if (Helpers.Pronouns.pronounsList.ContainsKey(user.Id.ToString()))
            {
                int thisUserPronouns = Convert.ToInt32(Helpers.Pronouns.pronounsList[user.Id.ToString()]);
                await ReplyAsync(user.Username.ToString() + " uses " + Helpers.Pronouns.pronounTypes[thisUserPronouns][0] + "/" + Helpers.Pronouns.pronounTypes[thisUserPronouns][1] + " pronouns!");
            }
            else
                await ReplyAsync("I don't know " + user.Username.ToString() + "'s pronouns! Ask them to set their pronouns with the ***" + Helpers.Prefix + "pronouns*** command.");
        }



        [Command("Pronouns")]
        public async Task PronounsCommand(string me)
        {
            if (me == "me")
            { 
                if (Helpers.Pronouns.pronounsList.ContainsKey(Context.Message.Author.Id.ToString()))
                {
                    int thisUserPronouns = Convert.ToInt32(Helpers.Pronouns.pronounsList[Context.Message.Author.Id.ToString()]);
                    await ReplyAsync("You currently use " + Helpers.Pronouns.pronounTypes[thisUserPronouns][0] + "/" + Helpers.Pronouns.pronounTypes[thisUserPronouns][1] + " pronouns!");
                }
                else
                    await ReplyAsync("I don't know your pronouns! You can set them by running this command again. Run this command with no extra parameters and I'll show you how. :sparkling_heart:");
            }
            else if (me == "he" || me == "she" || me == "they")
            {
                switch (me)
                {
                    case "he":
                        Helpers.Pronouns.pronounsList[Context.Message.Author.Id.ToString()] = "0";
                        Helpers.Pronouns.pronounsByName[Context.Message.Author.Username.ToString().ToLower()] = "0";
                        await ReplyAsync("Alright, I'll only refer to you using he/him pronouns! :sparkling_heart:");
                        break;
                    case "she":
                        Helpers.Pronouns.pronounsList[Context.Message.Author.Id.ToString()] = "1";
                        Helpers.Pronouns.pronounsByName[Context.Message.Author.Username.ToString().ToLower()] = "1";
                        await ReplyAsync("Alright, I'll only refer to you using she/her pronouns! :sparkling_heart:");
                        break;
                    case "they":
                        Helpers.Pronouns.pronounsList[Context.Message.Author.Id.ToString()] = "2";
                        Helpers.Pronouns.pronounsByName[Context.Message.Author.Username.ToString().ToLower()] = "2";
                        await ReplyAsync("Alright, I'll only refer to you using they/them pronouns! :sparkling_heart:");
                        break;
                    case "default":
                        await ReplyAsync("I'm sorry, I don't think I know that pronoun set. Maybe try asking if it can be added!");
                        break;
                }
                
                Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Setting pronouns for User " + Context.Message.Author.Username + " (" + Context.Message.Author.Id.ToString() + ") to " + Helpers.Pronouns.pronounTypes[int.Parse(Helpers.Pronouns.pronounsList[Context.Message.Author.Id.ToString()])][0] + "/" + Helpers.Pronouns.pronounTypes[int.Parse(Helpers.Pronouns.pronounsList[Context.Message.Author.Id.ToString()])][1] + "\n", System.Drawing.Color.White);

            }
            else
            {
                if (Helpers.Pronouns.pronounsByName.ContainsKey(me.ToLower()))
                {
                    int thisUserPronouns = Convert.ToInt32(Helpers.Pronouns.pronounsByName[me.ToLower()]);
                    await ReplyAsync(me + " uses " + Helpers.Pronouns.pronounTypes[thisUserPronouns][0] + "/" + Helpers.Pronouns.pronounTypes[thisUserPronouns][1] + " pronouns!");
                }
                else
                    await ReplyAsync("I don't know " + me + "'s pronouns! Ask them to set their pronouns with the ***" + Helpers.Prefix + "pronouns*** command.");
            }


        }

        [Command("Pronouns")]
        public async Task PronounsCommand()
        {
            await ReplyAsync("You can set your own pronouns by typing: ***" + Helpers.Prefix + "pronouns type***\n\nAvailable types are:\n**he** - he/him\n**she** - she/her\n**they** - they/them\n\nYou can check your current pronouns by typing: ***" + Helpers.Prefix + "pronouns me***\n\nYou can check someone else's pronouns by typing: ***" + Helpers.Prefix + "pronouns @name***");
        }

    }
}
