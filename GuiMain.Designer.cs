namespace LeftyBotGui
{
    partial class GuiMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.consoleControl1 = new ConsoleControl.ConsoleControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startBotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopBotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopAndQuitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.setBotTokenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPrimaryChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editPronounsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editBirthdaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editValidationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Console Output";
            // 
            // consoleControl1
            // 
            this.consoleControl1.AutoSize = true;
            this.consoleControl1.IsInputEnabled = true;
            this.consoleControl1.Location = new System.Drawing.Point(12, 49);
            this.consoleControl1.Name = "consoleControl1";
            this.consoleControl1.SendKeyboardCommandsToProcess = false;
            this.consoleControl1.ShowDiagnostics = false;
            this.consoleControl1.Size = new System.Drawing.Size(636, 383);
            this.consoleControl1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(12, 438);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(523, 20);
            this.textBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(541, 436);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Send Message";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(660, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startBotToolStripMenuItem,
            this.stopBotToolStripMenuItem,
            this.stopAndQuitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // startBotToolStripMenuItem
            // 
            this.startBotToolStripMenuItem.Name = "startBotToolStripMenuItem";
            this.startBotToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.startBotToolStripMenuItem.Text = "Start Bot";
            // 
            // stopBotToolStripMenuItem
            // 
            this.stopBotToolStripMenuItem.Name = "stopBotToolStripMenuItem";
            this.stopBotToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stopBotToolStripMenuItem.Text = "Stop Bot";
            // 
            // stopAndQuitToolStripMenuItem
            // 
            this.stopAndQuitToolStripMenuItem.Name = "stopAndQuitToolStripMenuItem";
            this.stopAndQuitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stopAndQuitToolStripMenuItem.Text = "Quit";
            this.stopAndQuitToolStripMenuItem.Click += new System.EventHandler(this.stopAndQuitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.setBotTokenToolStripMenuItem,
            this.setPrimaryChannelToolStripMenuItem,
            this.editPronounsToolStripMenuItem,
            this.editBirthdaysToolStripMenuItem,
            this.editValidationsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.githubToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // githubToolStripMenuItem
            // 
            this.githubToolStripMenuItem.Name = "githubToolStripMenuItem";
            this.githubToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.githubToolStripMenuItem.Text = "Github";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.aboutToolStripMenuItem.Text = "About This Bot";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.aboutToolStripMenuItem1.Text = "About Discord.NET";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(190, 22);
            this.toolStripMenuItem1.Text = "Autostart";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // setBotTokenToolStripMenuItem
            // 
            this.setBotTokenToolStripMenuItem.Name = "setBotTokenToolStripMenuItem";
            this.setBotTokenToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.setBotTokenToolStripMenuItem.Text = "Set Bot Token...";
            // 
            // setPrimaryChannelToolStripMenuItem
            // 
            this.setPrimaryChannelToolStripMenuItem.Name = "setPrimaryChannelToolStripMenuItem";
            this.setPrimaryChannelToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.setPrimaryChannelToolStripMenuItem.Text = "Set Primary Channel...";
            // 
            // editPronounsToolStripMenuItem
            // 
            this.editPronounsToolStripMenuItem.Name = "editPronounsToolStripMenuItem";
            this.editPronounsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.editPronounsToolStripMenuItem.Text = "Edit Pronouns...";
            // 
            // editBirthdaysToolStripMenuItem
            // 
            this.editBirthdaysToolStripMenuItem.Name = "editBirthdaysToolStripMenuItem";
            this.editBirthdaysToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.editBirthdaysToolStripMenuItem.Text = "Edit Birthdays...";
            // 
            // editValidationsToolStripMenuItem
            // 
            this.editValidationsToolStripMenuItem.Name = "editValidationsToolStripMenuItem";
            this.editValidationsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.editValidationsToolStripMenuItem.Text = "Edit Validations...";
            // 
            // GuiMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 470);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.consoleControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GuiMain";
            this.Text = "Lefty Discord Bot";
            this.Load += new System.EventHandler(this.GuiMain_Load);
            this.Shown += new System.EventHandler(this.GuiMain_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private ConsoleControl.ConsoleControl consoleControl1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem githubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem startBotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopBotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopAndQuitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem setBotTokenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setPrimaryChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editPronounsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editBirthdaysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editValidationsToolStripMenuItem;
    }
}

