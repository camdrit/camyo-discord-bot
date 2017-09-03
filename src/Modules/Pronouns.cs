using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Camyo.Modules
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
                    Console.WriteLine(Context.Message.Author.Id.ToString());
                    if (pronouns.pronounsList.ContainsKey(Context.Message.Author.Id.ToString()))
                    {
                        int thisUserPronouns = Convert.ToInt32(pronouns.pronounsList[Context.Message.Author.Id.ToString()]);
                        await ReplyAsync("You currently use " + pronouns.pronounTypes[thisUserPronouns][0] + "/" + pronouns.pronounTypes[thisUserPronouns][1] + " pronouns!");
                    }
                    else
                        await ReplyAsync("I don't know your pronouns! You can set them by running this command again. Run this command with no extra parameters and I'll show you how. :sparkling_heart:");
                    file.Close();
                }
            } else if (me == "he" || me == "she" || me == "they")
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
                        Console.WriteLine(DateTime.Now.ToString() + " - Setting pronouns for User " + Context.Message.Author.Username + " (" + Context.Message.Author.Id.ToString() + ") to " + pronouns.pronounTypes[int.Parse(pronouns.pronounsList[Context.Message.Author.Id.ToString()])][0]+"/"+ pronouns.pronounTypes[int.Parse(pronouns.pronounsList[Context.Message.Author.Id.ToString()])][1]);
                        sw.Close();
                    }
                    
                }

            } else
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

        [Command("Birthday")]
        public async Task BirthdayCommand(string date)
        {
            var tomorrow = DateTime.Today.AddDays(1).ToString("M/d");
            var today = DateTime.Today.ToString("M/d");
            var dateParsed = DateTime.Parse(date).ToString("M/d");
            PronounList pronouns;
            using (StreamReader file = File.OpenText("pronouns.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                pronouns = (PronounList)serializer.Deserialize(file, typeof(PronounList));
                file.Close();
            }
            pronouns.pronounsList.TryGetValue(Context.User.Id.ToString(), out string myPronouns);
            if (myPronouns == null)
                myPronouns = "2";

            using (StreamReader file = File.OpenText("birthdays.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                BirthdayList birthdays = (BirthdayList)serializer.Deserialize(file, typeof(BirthdayList));
                birthdays.birthdaysList[Context.Message.Author.Id.ToString()] = date;
                file.Close();
                using (StreamWriter sw = new StreamWriter("birthdays.json"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, birthdays);
                    sw.Close();
                }
            }

            if (dateParsed == today)
                await ReplyAsync("Ok, so your birthday is **" + FormattedDate(date) + "**? I'll remember that! Wait...whoa! That means your birthday is today! Hey @here it's " + Context.User.Mention + "'s birthday today! " + FirstLetterToUpper(pronouns.pronounTypes[int.Parse(myPronouns)][0] as string) + " is " + (GetAge(DateTime.Parse(date)) + 1) + " years old today! :birthday:");
            else if (dateParsed == tomorrow)
                await ReplyAsync("Ok, so your birthday is **" + FormattedDate(date) + "**? I'll remember that! Wait...whoa! That means your birthday is tomorrow! Hey @here it's " + Context.User.Mention + "'s birthday tomorrow! " + FirstLetterToUpper(pronouns.pronounTypes[int.Parse(myPronouns)][0] as string) + " will be " + (GetAge(DateTime.Parse(date)) + 1) + " years old! :birthday: Let's all wish " + pronouns.pronounTypes[int.Parse(myPronouns)][1] + " a happy birthday when the time comes!");
            else await ReplyAsync("Ok, so your birthday is **" + FormattedDate(date) + "**? I'll remember that!");

            Console.WriteLine(DateTime.Now.ToString() + " - Setting birthday for User " + Context.Message.Author.Username + " (" + Context.Message.Author.Id.ToString() + ") to " + FormattedDate(date));
        }


        [Command("Birthday")]
        public async Task BirthdayCommand()
        {
            await ReplyAsync("You can tell me what your birthday is and I'll remind everyone a day before! Try typing: \n\n***~birthday m/d/yy***\n\nPlease explicitly specify your birthdate with the **m/d/yy** format!");
        }

        private string FormattedDate(string date)
        {
            DateTime parsed = DateTime.Parse(date);
            string suffix;

            if (parsed.Day % 10 == 1 && parsed.Day != 11)
                suffix = "st";
            else if (parsed.Day % 10 == 2 && parsed.Day != 12)
                suffix = "nd";
            else if (parsed.Day % 10 == 3 && parsed.Day != 13)
                suffix = "rd";
            else
                suffix = "th";
            return string.Format("{0:MMMM d}{1}, {0:yyyy}", parsed, suffix);
        }

        public int GetAge(DateTime dateOfBirth)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - dateOfBirth.Year;
            return age;
        }

        public string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}
