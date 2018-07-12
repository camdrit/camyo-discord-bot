using Discord;
using Discord.WebSocket;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeftyBotGui
{
    public static class BotLogic
    {
        private static DiscordSocketClient _client = new DiscordSocketClient();

        public static Discord.ConnectionState BotConnectionState { get { return _client.ConnectionState; } }
        public static SocketTextChannel Channel { get { return _client.GetChannel(ulong.Parse(ConfigurationManager.AppSettings["primaryChannel"])) as SocketTextChannel; } }

        private static CommandHandler _handler;

        private static bool BotReady { get; set; } = false;

        public static async Task StartAsync()
        {
            new CommandHandler();

            if ((ConfigurationManager.AppSettings["botToken"] == null || ConfigurationManager.AppSettings["botToken"] == string.Empty ||
                ConfigurationManager.AppSettings["botToken"] == "bot_token_here") ||
                (ConfigurationManager.AppSettings["primaryChannel"] == null || ConfigurationManager.AppSettings["primaryChannel"] == string.Empty ||
                ConfigurationManager.AppSettings["primaryChannel"] == "channel_id_here"))
            {
                Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Either the bot token or the channel ID is not configured. Please configure this setting before using the bot.\n", System.Drawing.Color.White);
                return;
            }

            await _client.LoginAsync(TokenType.Bot, ConfigurationManager.AppSettings["botToken"]);

            await _client.SetGameAsync("type" + Helpers.Prefix + "help");
            await _client.StartAsync();

            _handler = new CommandHandler();

            await _handler.InitializeAsync(_client);

            Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Bot initialized.\n", System.Drawing.Color.White);

            while (_client.ConnectionState != Discord.ConnectionState.Connected)
            {
                System.Threading.Thread.Sleep(500);
            }

             
            Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Bot is connected to: " + Channel.Name + "\n", System.Drawing.Color.White);

            GuiMain.EnableInputArea((GuiMain)Application.OpenForms[0]);

            JobManager.AddJob(() => BirthdayJob(), s => s.ToRunNow().AndEvery(1).Days().At(12, 0));
            JobManager.AddJob(() => ImageJob(), s => s.ToRunNow().AndEvery(1).Days().At(12, 0));

        }

        public static Task Log(LogMessage msg)
        {
            Helpers.ConsoleControl.WriteOutput(msg.ToString(), System.Drawing.Color.White);
            return Task.CompletedTask;
        }

        private static void BirthdayJob()
        {
            PronounList pnouns = Helpers.Pronouns;
            Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Checking Birthdays...\n", System.Drawing.Color.White);
            int totalPeeps = 0;
            string singleMessage = "";
            string bulkMessage = "@everyone :birthday: Birthday role call! These lovely gamers have birthdays **tomorrow**:\n\n";

            var tomorrow = DateTime.Today.AddDays(1).ToString("M/d");

            foreach (KeyValuePair<string, string> entry in Helpers.Birthdays.birthdaysList)
            {
                var day = DateTime.Parse(entry.Value).ToString("M/d");
                Helpers.Pronouns.pronounsList.TryGetValue(entry.Key, out string myPronouns);
                if (myPronouns == null)
                    myPronouns = "2";
                if (day == tomorrow)
                {
                    singleMessage = "@everyone " + _client.GetUser(ulong.Parse(entry.Key)).Mention + "'s birthday is **tomorrow!** :birthday: " + Helpers.FirstLetterToUpper(Helpers.Pronouns.pronounTypes[int.Parse(myPronouns)][0] as string) + " will be " + Helpers.GetAge(DateTime.Parse(entry.Value)) + " years old! Be sure to wish " + Helpers.Pronouns.pronounTypes[int.Parse(myPronouns)][1] + " a happy birthday when the time comes!";
                    bulkMessage += _client.GetUser(ulong.Parse(entry.Key)).Mention + " will be " + Helpers.GetAge(DateTime.Parse(entry.Value)) + " years old!\n";
                    totalPeeps++;
                }

            }
            if (totalPeeps == 1)
            {
                Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Birthdays Today: " + totalPeeps + "\n", System.Drawing.Color.White);
                Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Birthday Message: " + singleMessage + "\n", System.Drawing.Color.White);
                Channel.SendMessageAsync(singleMessage);
            }
            else if (totalPeeps > 1)
            {
                bulkMessage += "\n Let's all remember to wish these gamers a happy birthday **tomorrow!** :confetti_ball:";
                Channel.SendMessageAsync(bulkMessage);
                Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Birthdays Today: " + totalPeeps + "\n", System.Drawing.Color.White);
                Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Birthday Message: " + bulkMessage + "\n", System.Drawing.Color.White);
            }
            else if (totalPeeps < 1)
            {
                Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Birthdays Today: " + totalPeeps + "\n", System.Drawing.Color.White);
            }
        }

        private static void ImageJob()
        {
            Random rand = new Random();
            int fCount = Directory.GetFiles("C:\\LeftyImages", "*", SearchOption.TopDirectoryOnly).Length;
            int img = rand.Next(0, fCount);
            Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Current Image Count: " + fCount.ToString() + "\n", System.Drawing.Color.White);
            Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Sending Lefty Image of the Day. Image # " + img.ToString() + "\n", System.Drawing.Color.White);
            Channel.SendFileAsync("C:\\LeftyImages\\" + img + ".jpg", "meooww!!! (Take a look at the Daily Lefty image. That's me!!!)");
        }
    }
}
