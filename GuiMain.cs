using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using Discord;

namespace LeftyBotGui
{
    public partial class GuiMain : Form
    {
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
                BotLogic.Channel.SendMessageAsync(textBox1.Text);
                Helpers.ConsoleControl.WriteOutput(DateTime.Now.ToString() + " - (Bot Message) " + textBox1.Text, System.Drawing.Color.White);
                textBox1.Text = String.Empty;
            } else
            {
                MessageBox.Show("You must enter a message first!");
            }
        }

        private void GuiMain_Shown(object sender, EventArgs e)
        {
            BotLogic.StartAsync();
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

        public static void EnableInputArea(GuiMain instance)
        {
            instance.textBox1.Invoke(new Action(() => instance.textBox1.Enabled = true));
            instance.button1.Invoke(new Action(() => instance.button1.Enabled = true));
        }
    }

}
