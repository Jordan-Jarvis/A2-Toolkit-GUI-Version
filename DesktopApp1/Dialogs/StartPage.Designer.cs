namespace DesktopApp1
{
    partial class StartPage
    {

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
        public void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartPage));
            this.reboot = new System.Windows.Forms.Button();
            this.rebootEDL = new System.Windows.Forms.Button();
            this.enterFastboot = new System.Windows.Forms.Button();
            this.exitFastboot = new System.Windows.Forms.Button();
            this.unlockOEM = new System.Windows.Forms.Button();
            this.unlockFlashing = new System.Windows.Forms.Button();
            this.unlockCritical = new System.Windows.Forms.Button();
            this.browse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.browseTwrp = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button7 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.settings2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settings3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reboot
            // 
            this.reboot.Location = new System.Drawing.Point(15, 19);
            this.reboot.Name = "reboot";
            this.reboot.Size = new System.Drawing.Size(103, 23);
            this.reboot.TabIndex = 1;
            this.reboot.Text = "Reboot ";
            this.reboot.UseVisualStyleBackColor = true;
            this.reboot.Click += new System.EventHandler(this.button2_Click);
            // 
            // rebootEDL
            // 
            this.rebootEDL.Location = new System.Drawing.Point(15, 52);
            this.rebootEDL.Name = "rebootEDL";
            this.rebootEDL.Size = new System.Drawing.Size(103, 23);
            this.rebootEDL.TabIndex = 2;
            this.rebootEDL.Text = "EDL Mode";
            this.rebootEDL.UseVisualStyleBackColor = true;
            this.rebootEDL.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // enterFastboot
            // 
            this.enterFastboot.Location = new System.Drawing.Point(15, 81);
            this.enterFastboot.Name = "enterFastboot";
            this.enterFastboot.Size = new System.Drawing.Size(103, 23);
            this.enterFastboot.TabIndex = 3;
            this.enterFastboot.Text = "Enter Fastboot";
            this.enterFastboot.UseVisualStyleBackColor = true;
            this.enterFastboot.Click += new System.EventHandler(this.button3_Click);
            // 
            // exitFastboot
            // 
            this.exitFastboot.Location = new System.Drawing.Point(15, 110);
            this.exitFastboot.Name = "exitFastboot";
            this.exitFastboot.Size = new System.Drawing.Size(103, 23);
            this.exitFastboot.TabIndex = 4;
            this.exitFastboot.Text = "Exit Fastboot";
            this.exitFastboot.UseVisualStyleBackColor = true;
            this.exitFastboot.Click += new System.EventHandler(this.button4_Click);
            // 
            // unlockOEM
            // 
            this.unlockOEM.Location = new System.Drawing.Point(12, 19);
            this.unlockOEM.Name = "unlockOEM";
            this.unlockOEM.Size = new System.Drawing.Size(103, 23);
            this.unlockOEM.TabIndex = 7;
            this.unlockOEM.Text = "OEM";
            this.unlockOEM.UseVisualStyleBackColor = true;
            this.unlockOEM.Click += new System.EventHandler(this.unlockOEM_Click);
            // 
            // unlockFlashing
            // 
            this.unlockFlashing.Location = new System.Drawing.Point(12, 48);
            this.unlockFlashing.Name = "unlockFlashing";
            this.unlockFlashing.Size = new System.Drawing.Size(103, 23);
            this.unlockFlashing.TabIndex = 8;
            this.unlockFlashing.Text = "Flashing";
            this.unlockFlashing.UseVisualStyleBackColor = true;
            this.unlockFlashing.Click += new System.EventHandler(this.unlockFlashing_Click);
            // 
            // unlockCritical
            // 
            this.unlockCritical.Location = new System.Drawing.Point(12, 77);
            this.unlockCritical.Name = "unlockCritical";
            this.unlockCritical.Size = new System.Drawing.Size(103, 23);
            this.unlockCritical.TabIndex = 9;
            this.unlockCritical.Text = "Flashing Critical";
            this.unlockCritical.UseVisualStyleBackColor = true;
            this.unlockCritical.Click += new System.EventHandler(this.unlockCritical_Click);
            // 
            // browse
            // 
            this.browse.Location = new System.Drawing.Point(172, 80);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(75, 23);
            this.browse.TabIndex = 10;
            this.browse.Text = "Browse...";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 134);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(256, 20);
            this.textBox1.TabIndex = 11;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(617, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Welcome, This toolkit is designed for the Mi A2. I am not responsible for any dam" +
    "age caused to your device by using this program ";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Red;
            this.groupBox1.Controls.Add(this.unlockFlashing);
            this.groupBox1.Controls.Add(this.unlockCritical);
            this.groupBox1.Controls.Add(this.unlockOEM);
            this.groupBox1.Location = new System.Drawing.Point(484, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(125, 222);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unlock Bootloader";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FloralWhite;
            this.groupBox2.Controls.Add(this.exitFastboot);
            this.groupBox2.Controls.Add(this.reboot);
            this.groupBox2.Controls.Add(this.rebootEDL);
            this.groupBox2.Controls.Add(this.enterFastboot);
            this.groupBox2.Location = new System.Drawing.Point(328, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(134, 222);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reboot Options";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.checkBox5);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.browse);
            this.groupBox3.Controls.Add(this.checkedListBox1);
            this.groupBox3.Location = new System.Drawing.Point(327, 256);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(280, 168);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Flash Img";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(172, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "FLASH!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "system_a",
            "system_b",
            "vendor_a",
            "vendor_b",
            "boot_a",
            "boot_b",
            "abl"});
            this.checkedListBox1.Location = new System.Drawing.Point(16, 19);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(118, 109);
            this.checkedListBox1.TabIndex = 17;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox4.Controls.Add(this.checkBox2);
            this.groupBox4.Controls.Add(this.comboBox2);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.browseTwrp);
            this.groupBox4.Location = new System.Drawing.Point(15, 256);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(307, 168);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Start TWRP image";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(226, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "LAUNCH!";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1Async);
            // 
            // browseTwrp
            // 
            this.browseTwrp.Location = new System.Drawing.Point(226, 105);
            this.browseTwrp.Name = "browseTwrp";
            this.browseTwrp.Size = new System.Drawing.Size(75, 23);
            this.browseTwrp.TabIndex = 0;
            this.browseTwrp.Text = "Browse...";
            this.browseTwrp.UseVisualStyleBackColor = true;
            this.browseTwrp.Click += new System.EventHandler(this.browseTwrp_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox5.Controls.Add(this.checkBox4);
            this.groupBox5.Controls.Add(this.checkBox3);
            this.groupBox5.Controls.Add(this.checkBox1);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.button5);
            this.groupBox5.Controls.Add(this.textBox4);
            this.groupBox5.Controls.Add(this.textBox3);
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Controls.Add(this.button4);
            this.groupBox5.Location = new System.Drawing.Point(12, 28);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(310, 222);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Install Zip File";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 23);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(170, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "I already have TWRP installed";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Select a file to sideload";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Select a TWRP img";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(191, 48);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "Browse...";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(15, 102);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(254, 20);
            this.textBox4.TabIndex = 4;
            this.textBox4.Text = "TWRP File Here";
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(15, 180);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(254, 20);
            this.textBox3.TabIndex = 3;
            this.textBox3.Text = "Zip File Here";
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(191, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "INSTALL!";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(191, 128);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "Browse...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(343, 435);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 20;
            this.button6.Text = "Download";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(343, 464);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(217, 21);
            this.comboBox1.TabIndex = 21;
            this.comboBox1.Text = "Select Version";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(495, 435);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 22;
            this.button7.Text = "Flash";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(665, 25);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settings2ToolStripMenuItem,
            this.settings3ToolStripMenuItem,
            this.donateToolStripMenuItem});
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(38, 22);
            this.toolStripLabel1.Text = "File";
            // 
            // settings2ToolStripMenuItem
            // 
            this.settings2ToolStripMenuItem.Name = "settings2ToolStripMenuItem";
            this.settings2ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.settings2ToolStripMenuItem.Text = "Settings";
            this.settings2ToolStripMenuItem.Click += new System.EventHandler(this.settings2ToolStripMenuItem_Click);
            // 
            // settings3ToolStripMenuItem
            // 
            this.settings3ToolStripMenuItem.Name = "settings3ToolStripMenuItem";
            this.settings3ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.settings3ToolStripMenuItem.Text = "About";
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.donateToolStripMenuItem.Text = "Donate";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(56, 134);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(245, 21);
            this.comboBox2.TabIndex = 22;
            this.comboBox2.Text = "Choose Twrp Image";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(125, 109);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(95, 17);
            this.checkBox2.TabIndex = 23;
            this.checkBox2.Text = "Save Location";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(171, 157);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(95, 17);
            this.checkBox3.TabIndex = 24;
            this.checkBox3.Text = "Save Location";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(171, 75);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(95, 17);
            this.checkBox4.TabIndex = 25;
            this.checkBox4.Text = "Save Location";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(168, 109);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(95, 17);
            this.checkBox5.TabIndex = 24;
            this.checkBox5.Text = "Save Location";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(665, 506);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartPage";
            this.Text = "Jarvinator Mi A2 Toolkit";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button reboot;
        public System.Windows.Forms.Button rebootEDL;
        public System.Windows.Forms.Button enterFastboot;
        public System.Windows.Forms.Button exitFastboot;
        public System.Windows.Forms.Button unlockOEM;
        public System.Windows.Forms.Button unlockFlashing;
        public System.Windows.Forms.Button unlockCritical;
        public System.Windows.Forms.Button browse;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.CheckedListBox checkedListBox1;
        public System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.Button browseTwrp;
        public System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button4;
        public System.Windows.Forms.TextBox textBox4;
        public System.Windows.Forms.Button button5;
        public System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button button6;
        public System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.Button button7;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripLabel1;
        private System.Windows.Forms.ToolStripMenuItem settings2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settings3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
        public System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
    }
}

