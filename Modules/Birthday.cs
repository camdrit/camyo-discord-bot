using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LeftyBotGui.Modules
{
    public class Birthday : ModuleBase<SocketCommandContext>
    {
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
                await ReplyAsync("Ok, so your birthday is **" + FormattedDate(date) + "**? I'll remember that! Wait...whoa! That means your birthday is today! Hey @here it's " + Context.User.Mention + "'s birthday today! " + Helpers.FirstLetterToUpper(pronouns.pronounTypes[int.Parse(myPronouns)][0] as string) + " is " + (Helpers.GetAge(DateTime.Parse(date)) + 1) + " years old today! :birthday:");
            else if (dateParsed == tomorrow)
                await ReplyAsync("Ok, so your birthday is **" + FormattedDate(date) + "**? I'll remember that! Wait...whoa! That means your birthday is tomorrow! Hey @here it's " + Context.User.Mention + "'s birthday tomorrow! " + Helpers.FirstLetterToUpper(pronouns.pronounTypes[int.Parse(myPronouns)][0] as string) + " will be " + (Helpers.GetAge(DateTime.Parse(date)) + 1) + " years old! :birthday: Let's all wish " + pronouns.pronounTypes[int.Parse(myPronouns)][1] + " a happy birthday when the time comes!");
            else await ReplyAsync("Ok, so your birthday is **" + FormattedDate(date) + "**? I'll remember that!");

            Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Setting birthday for User " + Context.Message.Author.Username + " (" + Context.Message.Author.Id.ToString() + ") to " + FormattedDate(date) + "\n", System.Drawing.Color.White);
        }


        [Command("Birthday")]
        public async Task BirthdayCommand()
        {
            await ReplyAsync("You can tell me what your birthday is and I'll remind everyone a day before! Try typing: \n\n***" + Helpers.Prefix + "birthday m/d/yy***\n\nPlease explicitly specify your birthdate with the **m/d/yy** format!");
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
    }
}
