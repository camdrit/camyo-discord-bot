using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LeftyBotGui.Modules
{

    public class Commands : ModuleBase<SocketCommandContext>
    {
        private readonly List<string> _malevalidations = new List<string>() { "I love {0}. He's cute. So handsome.", "{0} is a strong boy! Amazing boy!", "{0} :clap: is :clap: valid! :clap:", "{0} is an amazing, hard-working boy who deserves your love and support. Some days may be hard for him, but he's trying and that's all that matters.", "I once heard that {0} saved the world from evil one time. I don't know if that's true but he's really cool anyway so it doesn't matter." };
        private readonly List<string> _girlvalidations = new List<string>() { "I love {0}. She's cute. So beautiful.", "{0} is a strong girl! Amazing girl!", "{0} :clap: is :clap: valid! :clap:", "{0} is an amazing, hard-working girl who deserves your love and support. Some days may be hard for her, but she's trying and that's all that matters.", "I once heard that {0} saved the world from evil one time. I don't know if that's true but she's really cool anyway so it doesn't matter." };
        private readonly List<string> _theyvalidations = new List<string>() { "I love {0}. They're cute. So wonderful.", "{0} is a strong gamer! Amazing gamer!", "{0} :clap: is :clap: valid! :clap:", "{0} is an amazing, hard-working gamer who deserves your love and support. Some days may be hard for them, but they're trying and that's all that matters.", "I once heard that {0} saved the world from evil one time. I don't know if that's true but they're really cool anyway so it doesn't matter." };
        private List<string> _petresponses = new List<string>() { "mrrrp.", "mmrrrow!", "*purrs*", "*bites you*", "*sits on your hand*", "mrrreow?", "mrrreoow!", "*chirps*", "*rolls over*", "*ignores you*", "*glares*", "meow...", "*rubs cheek on hand*" };
        private List<string> _goodpetresonses = new List<string>() { "mrrrp.", "mmrrrow!", "*purrs*", "*sits on your hand*", "mrrreow?", "mrrreoow!", "*chirps*", "*rolls over*", "meow...", "*rubs cheek on hand*" };
        private List<List<string>> _validations;

        [Command("Help")]
        public async Task HelpCommand()
        {
            await Context.Channel.SendMessageAsync("All commands can be executed either by using the prefix **" + Helpers.Prefix + "** or by mentioning me!\n\n***" + Helpers.Prefix + "help***: Displays this dialogue.\n***" + Helpers.Prefix + "birthday m/d/yy***: allows you to set your birthday. I'll remember the date and give a reminder the day prior at 12:00:00 UTC.\n***" + Helpers.Prefix + "validate me/username***: Gives a random bit of love to either yourself or a gamer of your specification.\n***" + Helpers.Prefix + "pronouns me/he/she/they***: allows you to set or check your pronouns. I'll remember them and only ever refer to you using those pronouns.");
        }

        [Command("Pet")]
        public async Task PetCommand()
        {
            if (Context.Message.Author.Id == (ulong)195049510743965696)
            {
                await ReplyAsync("*bites you*");
            } else if (Context.Message.Author.Id == (ulong)198573127897317376)
            {
                Random rand = new Random();
                int r = rand.Next(_goodpetresonses.Count);
                await ReplyAsync(_petresponses[r]);
                await ReplyAsync("I love my dad!");
            } else
            {
                Random rand = new Random();
                int r = rand.Next(_petresponses.Count);
                await ReplyAsync(_petresponses[r]);
            }
        }

        [Command("Validate")]
        public async Task ValidateCommand(IGuildUser user)
        {
            _validations = new List<List<string>>() { _malevalidations, _girlvalidations, _theyvalidations };
            using (StreamReader file = File.OpenText("pronouns.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                PronounList pronouns = (PronounList)serializer.Deserialize(file, typeof(PronounList));
                if (!pronouns.pronounsList.ContainsKey(user.Id.ToString()) || (pronouns.pronounsList.ContainsKey(user.Id.ToString()) && pronouns.pronounsList[user.Id.ToString()] == "2"))
                {
                    Random rand = new Random();
                    int r = rand.Next(_validations[2].Count);
                    await ReplyAsync(String.Format(_validations[2][r], user.Username.ToString()));

                }
                else
                {
                    Random rand = new Random();
                    int thisUserPronouns = Convert.ToInt32(pronouns.pronounsList[user.Id.ToString()]);
                    int r = rand.Next(_validations[thisUserPronouns].Count);
                    await ReplyAsync(String.Format(_validations[thisUserPronouns][r], user.Username.ToString()));
                }
                file.Close();
            }
        }

        [Command("Hi")]
        public async Task HewwoCommand()
        {
            await ReplyAsync(MentionUtils.MentionUser(Context.Message.Author.Id) + " mrrrreeeoooww!");
        }

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
                    IEnumerator enumerator = Item.MentionedUserIds.GetEnumerator();
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
