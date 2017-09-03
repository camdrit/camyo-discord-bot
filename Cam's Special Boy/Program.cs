using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FluentScheduler;
using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;

namespace Camyo
{
    public class Program
    {
        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        private CommandHandler _handler;

        public async Task StartAsync()
        {

            Console.Title = "Camyo Discord Bot 0.0.9a";

            _client = new DiscordSocketClient();

            new CommandHandler();

            await _client.LoginAsync(TokenType.Bot, ConfigurationManager.AppSettings["botToken"]);
            
            
            await _client.SetGameAsync("type ~help");
            await _client.StartAsync();

            _handler = new CommandHandler();

            await _handler.InitializeAsync(_client);

            Console.WriteLine(DateTime.Now.ToString() + " - Bot initialized.");


            JobManager.AddJob(() => {
                if (_client.ConnectionState == ConnectionState.Connected)
                {
                    var channel = _client.GetChannel(ulong.Parse(ConfigurationManager.AppSettings["primaryChannel"])) as SocketTextChannel;
                    Console.WriteLine(DateTime.Now.ToString() + " - Bot is connected to: " + channel.Name);
                JobManager.AddJob(() => BirthdayJob(channel), s => s.ToRunNow().AndEvery(1).Days().At(12, 0));
                }
            }, s => s.ToRunOnceIn(20).Seconds());

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private void BirthdayJob(SocketTextChannel channel)
        {
            Console.WriteLine(DateTime.Now.ToString() + " - Checking Birthdays...");
            int totalPeeps = 0;
            string singleMessage = "";
            string bulkMessage = "@here :birthday: Birthday role call! These lovely gamers have birthdays **tomorrow**:\n\n";

            PronounList pronouns;
            using (StreamReader file = File.OpenText("pronouns.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                pronouns = (PronounList)serializer.Deserialize(file, typeof(PronounList));
                file.Close();
            }
            using (StreamReader file = File.OpenText("birthdays.json"))
            {
                var tomorrow = DateTime.Today.AddDays(1).ToString("M/d");
                JsonSerializer serializer = new JsonSerializer();
                BirthdayList birthdays = (BirthdayList)serializer.Deserialize(file, typeof(BirthdayList));
                foreach (KeyValuePair<string, string> entry in birthdays.birthdaysList)
                {
                    var day = DateTime.Parse(entry.Value).ToString("M/d");
                    pronouns.pronounsList.TryGetValue(entry.Key, out string myPronouns);
                    if (myPronouns == null)
                        myPronouns = "2";
                    if (day == tomorrow)
                    {
                        singleMessage = "@here " + _client.GetUser(ulong.Parse(entry.Key)).Mention + "'s birthday is **tomorrow!** :birthday: " + FirstLetterToUpper(pronouns.pronounTypes[int.Parse(myPronouns)][0] as string) + " will be " + (GetAge(DateTime.Parse(entry.Value)) + 1) + " years old! Be sure to wish " + pronouns.pronounTypes[int.Parse(myPronouns)][1] + " a happy birthday when the time comes!";
                        bulkMessage += _client.GetUser(ulong.Parse(entry.Key)).Mention + " will be " + (GetAge(DateTime.Parse(entry.Value)) + 1) + " years old!\n";
                        totalPeeps++;
                    }

                }
                file.Close();
                if (totalPeeps == 1)
                {
                    Console.WriteLine(DateTime.Now.ToString() + " - Birthdays Today: " + totalPeeps);
                    Console.WriteLine(DateTime.Now.ToString() + " - Birthday Message: " + singleMessage);
                    channel.SendMessageAsync(singleMessage);
                }
                else if (totalPeeps > 1)
                {
                    bulkMessage += "\n Let's all remember to wish these gamers a happy birthday **tomorrow!** :confetti_ball:";
                    channel.SendMessageAsync(bulkMessage);
                    Console.WriteLine(DateTime.Now.ToString() + " - Birthdays Today: " + totalPeeps);
                    Console.WriteLine(DateTime.Now.ToString() + " - Birthday Message: " + bulkMessage);
                }
                else if (totalPeeps < 1)
                {
                    Console.WriteLine(DateTime.Now.ToString() + " - Birthdays Today: " + totalPeeps);
                }
            }
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
