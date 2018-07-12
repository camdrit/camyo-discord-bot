using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

using Discord;
using Discord.WebSocket;
using FluentScheduler;
using System.IO;
using System.Configuration;

namespace LeftyBotGui
{
    public partial class GuiMain : Form
    {
        private DiscordSocketClient _client;

        private CommandHandler _handler;

        public async Task StartAsync()
        {
            _client = new DiscordSocketClient();

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


            var channel = _client.GetChannel(ulong.Parse(ConfigurationManager.AppSettings["primaryChannel"])) as SocketTextChannel;
            Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Bot is connected to: " + channel.Name + "\n", System.Drawing.Color.White);
            textBox1.Invoke(new Action(() => textBox1.Enabled = true));
            button1.Invoke(new Action(() => button1.Enabled = true));
            JobManager.AddJob(() => BirthdayJob(channel), s => s.ToRunNow().AndEvery(1).Days().At(12, 0));
            JobManager.AddJob(() => ImageJob(channel), s => s.ToRunNow().AndEvery(1).Days().At(12, 0));

        }

        public Task Log(LogMessage msg)
        {
            Helpers.ConsoleControl.WriteOutput(msg.ToString(), System.Drawing.Color.White);
            return Task.CompletedTask;
        }

        public GuiMain()
        {
            InitializeComponent();
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox1.Text != null && textBox1.Text != String.Empty)
            {
                var channel = _client.GetChannel(ulong.Parse(ConfigurationManager.AppSettings["primaryChannel"])) as SocketTextChannel;
                channel.SendMessageAsync(textBox1.Text);
                Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - (Bot Message) " + textBox1.Text, System.Drawing.Color.White);
                textBox1.Text = String.Empty;
            } else
            {
                MessageBox.Show("You must enter a message first!");
            }
        }

        private void GuiMain_Shown(object sender, EventArgs e)
        {
            StartAsync();
        }

        private void GuiMain_Load(object sender, EventArgs e)
        {
            textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnterKeyPress);
        }

        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                button1.PerformClick();
            }
        }

        private void BirthdayJob(SocketTextChannel channel)
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
                channel.SendMessageAsync(singleMessage);
            }
            else if (totalPeeps > 1)
            {
                bulkMessage += "\n Let's all remember to wish these gamers a happy birthday **tomorrow!** :confetti_ball:";
                channel.SendMessageAsync(bulkMessage);
                Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Birthdays Today: " + totalPeeps + "\n", System.Drawing.Color.White);
                Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Birthday Message: " + bulkMessage + "\n", System.Drawing.Color.White);
            }
            else if (totalPeeps < 1)
            {
                Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Birthdays Today: " + totalPeeps + "\n", System.Drawing.Color.White);
            }
        }

        private void ImageJob(SocketTextChannel channel)
        {
            Random rand = new Random();
            int fCount = Directory.GetFiles("C:\\LeftyImages", "*", SearchOption.TopDirectoryOnly).Length;
            int img = rand.Next(0, fCount);
            Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Current Image Count: " + fCount.ToString() + "\n", System.Drawing.Color.White);
            Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - Sending Lefty Image of the Day. Image # " + img.ToString() + "\n", System.Drawing.Color.White);
            channel.SendFileAsync("C:\\LeftyImages\\" + img + ".jpg", "meooww!!! (Take a look at the Daily Lefty image. That's me!!!)");
        }

    }

}
