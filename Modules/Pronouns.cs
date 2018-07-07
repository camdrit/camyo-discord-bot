using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeftyBotGui.Modules
{

    public class Pronouns : ModuleBase<SocketCommandContext>
    {
        [Command("Pronouns")]
        public async Task PronounsCommand(IGuildUser user)
        {
            using (StreamReader file = File.OpenText("pronouns.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                PronounList pronouns = (PronounList)serializer.Deserialize(file, typeof(PronounList));
                if (pronouns.pronounsList.ContainsKey(user.Id.ToString()))
                {
                    int thisUserPronouns = Convert.ToInt32(pronouns.pronounsList[user.Id.ToString()]);
                    await ReplyAsync(user.Username.ToString() + " uses " + pronouns.pronounTypes[thisUserPronouns][0] + "/" + pronouns.pronounTypes[thisUserPronouns][1] + " pronouns!");
                }
                else
                    await ReplyAsync("I don't know " + user.Username.ToString() + "'s pronouns! Ask them to set their pronouns with the ***~pronouns*** command.");
            }

        }



        [Command("Pronouns")]
        public async Task PronounsCommand(string me)
        {
            if (me == "me")
            {
                using (StreamReader file = File.OpenText("pronouns.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    PronounList pronouns = (PronounList)serializer.Deserialize(file, typeof(PronounList));
                    Helpers.ConsoleControl.WriteOutput(Context.Message.Author.Id.ToString() + "\n", System.Drawing.Color.White);
                    if (pronouns.pronounsList.ContainsKey(Context.Message.Author.Id.ToString()))
                    {
                        int thisUserPronouns = Convert.ToInt32(pronouns.pronounsList[Context.Message.Author.Id.ToString()]);
                        await ReplyAsync("You currently use " + pronouns.pronounTypes[thisUserPronouns][0] + "/" + pronouns.pronounTypes[thisUserPronouns][1] + " pronouns!");
                    }
                    else
                        await ReplyAsync("I don't know your pronouns! You can set them by running this command again. Run this command with no extra parameters and I'll show you how. :sparkling_heart:");
                    file.Close();
                }
            }
            else if (me == "he" || me == "she" || me == "they")
            {
                using (StreamReader file = File.OpenText("pronouns.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    PronounList pronouns = (PronounList)serializer.Deserialize(file, typeof(PronounList));
                    switch (me)
                    {
                        case "he":
                            pronouns.pronounsList[Context.Message.Author.Id.ToString()] = "0";
                            pronouns.pronounsByName[Context.Message.Author.Username.ToString().ToLower()] = "0";
                            await ReplyAsync("Alright, I'll only refer to you using he/him pronouns! :sparkling_heart:");
                            break;
                        case "she":
                            pronouns.pronounsList[Context.Message.Author.Id.ToString()] = "1";
                            pronouns.pronounsByName[Context.Message.Author.Username.ToString().ToLower()] = "1";
                            await ReplyAsync("Alright, I'll only refer to you using she/her pronouns! :sparkling_heart:");
                            break;
                        case "they":
                            pronouns.pronounsList[Context.Message.Author.Id.ToString()] = "2";
                            pronouns.pronounsByName[Context.Message.Author.Username.ToString().ToLower()] = "2";
                            await ReplyAsync("Alright, I'll only refer to you using they/them pronouns! :sparkling_heart:");
                            break;
                        case "default":
                            await ReplyAsync("I'm sorry, I don't think I know that pronoun set. Maybe try asking if it can be added!");
                            break;
                    }
                    file.Close();
                    using (StreamWriter sw = new StreamWriter("pronouns.json"))
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, pronouns);
                        Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Setting pronouns for User " + Context.Message.Author.Username + " (" + Context.Message.Author.Id.ToString() + ") to " + pronouns.pronounTypes[int.Parse(pronouns.pronounsList[Context.Message.Author.Id.ToString()])][0] + "/" + pronouns.pronounTypes[int.Parse(pronouns.pronounsList[Context.Message.Author.Id.ToString()])][1] + "\n", System.Drawing.Color.White);
                        sw.Close();
                    }

                }

            }
            else
            {
                using (StreamReader file = File.OpenText("pronouns.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    PronounList pronouns = (PronounList)serializer.Deserialize(file, typeof(PronounList));
                    if (pronouns.pronounsByName.ContainsKey(me.ToLower()))
                    {
                        int thisUserPronouns = Convert.ToInt32(pronouns.pronounsByName[me.ToLower()]);
                        await ReplyAsync(me + " uses " + pronouns.pronounTypes[thisUserPronouns][0] + "/" + pronouns.pronounTypes[thisUserPronouns][1] + " pronouns!");
                    }
                    else
                        await ReplyAsync("I don't know " + me + "'s pronouns! Ask them to set their pronouns with the ***~pronouns*** command.");
                }
            }


        }

        [Command("Pronouns")]
        public async Task PronounsCommand()
        {
            await ReplyAsync("You can set your own pronouns by typing: ***~pronouns type***\n\nAvailable types are:\n**he** - he/him\n**she** - she/her\n**they** - they/them\n\nYou can check your current pronouns by typing: ***~pronouns me***\n\nYou can check someone else's pronouns by typing: ***~pronouns @name***");
        }

    }
}
