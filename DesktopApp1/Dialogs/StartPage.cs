using DesktopApp1.Dialogs;
using DesktopApp1.subroutines.adb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DesktopApp1
{
    public partial class StartPage : Form
    {
        public ConsoleDisplay c;
        int hasSelectedSoftwareVersion = 0;
        public string downloadVersion;
        private runExternalProcesses run;
        public DownloadDialog f2;
        internal runExternalProcesses Run { get => run; set => run = value; }
        



        public StartPage()
        {
            
            c = new ConsoleDisplay();
            
            if (!Properties.Settings.Default.ShowConsole)
                c.Hide();

            InitializeComponent();
            if (Properties.Settings.Default.SaveFlashImg)
            {
                textBox1.Text = Properties.Settings.Default.SaveFlashImgLocation;
                checkBox5.Checked = true;
            }
            if (Properties.Settings.Default.SaveLocationZipFileTwrp)
            {
                checkBox4.Checked = true;
                textBox4.Text = Properties.Settings.Default.SaveLocationZipFileTwrpLocation;
            }
            if (Properties.Settings.Default.SaveLocationSideLoadZip)
            {
                checkBox3.Checked = true;
                textBox3.Text = Properties.Settings.Default.SaveLocationSideLoadZipLocation;
            }
            if (Properties.Settings.Default.SaveLocationTwrp)
            {
                checkBox2.Checked = true;
                comboBox2.Text = Properties.Settings.Default.SaveLocationTwrpLocation;
            }
            checkBox1.Checked = Properties.Settings.Default.TwrpInstalled;

            


            
            StickyWindow.RegisterExternalReferenceForm(this);
            Run = new runExternalProcesses(this);
            XMLReader xmlList = new XMLReader("./A2Versions.xml");
            List<VersionInfo> versions = xmlList.getList();
            int i = 0;

            foreach (VersionInfo versionInfo in versions)
            {
                comboBox1.DisplayMember = "version";
                comboBox1.ValueMember = "link";
                var item1 = new MyType { version = versionInfo.getVersion(), link = versionInfo.getLink() };
                comboBox1.Items.Add(item1);
                comboBox1.SelectedIndex = i;
                i++;
            }
            comboBox1.SelectedIndex = 0;
            comboBox1.Sorted = true;
            comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            
            new Thread(() => jarvisBlink()).Start();
        }


        private void jarvisBlink()
        {
            while (!this.IsHandleCreated)
            {
                System.Threading.Thread.Sleep(100);
            }
            Random getrandom = new Random();
            while (!IsDisposed)
            {
                if (pictureBox2 != null)
                {
                    pictureBox2.Invoke((Action)(() => pictureBox2.Visible = false));
                }
                
                System.Threading.Thread.Sleep(1000 + getrandom.Next(500, 6000));
                if (IsDisposed)
                    break;
                if (pictureBox2 != null)
                {
                    pictureBox2.Invoke((Action)(() => pictureBox2.Visible = true));
                }
                System.Threading.Thread.Sleep(150);

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (hasSelectedSoftwareVersion == 1)
            {
                string downloadLink = downloadVersion;
                
            }
            else
            {
                MessageBox.Show("Please select a software version to download.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            new Thread(() =>
            {
                string temp;
                string isDevice = Run.fastboot("devices");
                if (!string.IsNullOrEmpty(isDevice) || string.IsNullOrWhiteSpace(isDevice))
                {
                    temp = Run.adb("reboot");
                    if ( temp.Contains("no devices")) 
                    {
                        MessageBox.Show("An error occured, the device was not detected. Please ensure it is plugged in with debug enabled and the drivers are up to date.");
                    }
                    else if (temp.Contains("error"))
                    {
                        MessageBox.Show("An unknown error occured, please check the console output for more info.");
                    }
                }
                else
                {
                    temp = Run.fastboot("reboot");
                }

                if (temp.Contains("error"))
                {
                    return;
                }
                else
                {
                    MessageBox.Show("Command sent successfully.");
                }
                
            }).Start();

            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                if (Run.autoFastboot())
                {
                    Run.EDL();
                };
                
            }).Start();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                string temp;
                temp = Run.adb("reboot bootloader");
                if (temp.Contains("error"))
                    {
                    if (temp.Contains("no devices"))
                    {
                        MessageBox.Show("An error occured, the device was not detected. Please ensure it is plugged in with debug enabled and the drivers are up to date.");
                    }
                    else
                    {
                        MessageBox.Show("An unknown error occured, please check the console output for more info.");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Command sent successfully.");
                }
            
            }).Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Run.fastboot("reboot");
            }).Start();
            MessageBox.Show("Command sent!");
        }

        


        private void unlockFlashing_Click(object sender, EventArgs e)
        {
            string message = "UNLOCKING YOUR BOOTLOADER WILL FACTORY RESET YOUR DEVICE! PLEASE BACK UP EVERYTHING! press ok to continue.";
            string caption = "Warning";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            

            DialogResult results = MessageBox.Show(message, caption, buttons);


            if (results == DialogResult.Cancel)
            {
                return;
            }
            MessageBox.Show("Please ensure the \"oem unlocking\" and \"usb debugging\" is enabled in Developer Settings. (Once unlocked, DO NOT TURN OFF OEM UNLOCKING! IT COULD BRICK YOUR DEVICE!) Your phone may ask to authorize the computer, if it does, select yes.");
            new Thread(() =>
            {
                if (Run.autoFastboot())
                {
                    Run.fastboot("flashing unlock");
                    MessageBox.Show("Please select unlock using the volume buttons, power button is select. Once you do that, you will be done!");
                }
            }).Start();
        }

        private void unlockOEM_Click(object sender, EventArgs e)
        {
            string message = "UNLOCKING YOUR BOOTLOADER WILL FACTORY RESET YOUR DEVICE! PLEASE BACK UP EVERYTHING! press ok to continue.";
            string caption = "Warning";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;


            DialogResult results = MessageBox.Show(message, caption, buttons);


            if (results == DialogResult.Cancel)
            {
                return;
            }
            MessageBox.Show("Please ensure the \"oem unlocking\" and \"usb debugging\" is enabled in Developer Settings. (Once unlocked, DO NOT TURN OFF OEM UNLOCKING! IT COULD BRICK YOUR DEVICE!) Your phone may ask to authorize the computer, if it does, select yes.");
            new Thread(() =>
            {
                if (Run.autoFastboot())
                {
                    Run.fastboot("oem unlock");
                    MessageBox.Show("Please select unlock using the volume buttons, power button is select. Once you do that, you will be done!");
                }
            }).Start();
        }

        private void unlockCritical_Click(object sender, EventArgs e)
        {
            string message = "UNLOCKING YOUR BOOTLOADER WILL FACTORY RESET YOUR DEVICE! PLEASE BACK UP EVERYTHING! press ok to continue.";
            string caption = "Warning";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;


            DialogResult results = MessageBox.Show(message, caption, buttons);


            if (results == DialogResult.Cancel)
            {
                return;
            }
            MessageBox.Show("Please ensure the \"oem unlocking\" and \"usb debugging\" is enabled in Developer Settings. (Once unlocked, DO NOT TURN OFF OEM UNLOCKING! IT COULD BRICK YOUR DEVICE!) Your phone may ask to authorize the computer, if it does, select yes.");
            new Thread(() =>
            {
                if (Run.autoFastboot())
                {
                    Run.fastboot("flashing unlock_critical");
                    MessageBox.Show("Please select unlock using the volume buttons, power button is select. Once you do that, you will be done!");
                }
            }).Start();
        }

        private void browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = "herro";
            MessageBox.Show("output");
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private bool flashImg(string currentPartition, string filePath)
        {
            string output;
            MessageBox.Show($"flash {currentPartition} {filePath}");
                output = Run.fastboot("flash {currentPartition} {filePath}");
            if (output.Contains("error"))
            {
                return false;
            }
            return true;

        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            // THIS IS THE FLASH FUNCTION!!!
            List<string> partitions = new List<String>();
            if (checkedListBox1.CheckedItems.Count != 0)
            {
                // If so, loop through all checked items and print results.  
               // string s = "";
                for (int x = 0; x < checkedListBox1.CheckedItems.Count; x++)
                {
                    partitions.Add(checkedListBox1.CheckedItems[x].ToString());
                }
                
            }
            else
            {
                MessageBox.Show("Error, Please select at least one partition to flash!");
                return;
            }

            if (Run.checkExist(textBox1.Text, ".img") == false)
            {
                return;
            }
            Run.autoFastboot();
            //string currentPartition;
            for (int i = 0; i < partitions.Count; i++)
            {
                if (flashImg(partitions[i], textBox1.Text) == true)
                {

                }
                else
                {
                    
                    string message = $"flashing the {partitions[i]} failed.";
                    //string caption = "Error!";
                   // MessageBoxButtons buttons = MessageBoxButtons.AbortRetryIgnore;
                    DialogResult result = new DialogResult();

                    if (result == DialogResult.Retry)
                    {
                        i--;
                    }
                    else if(result == DialogResult.Abort)
                    {
                        return;
                    }
                }
            }
            


            //partitions.Add(checkedListBox1.)

        }

        
        private void groupBox4_Enter(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void installTwrp_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void browseTwrp_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            comboBox2.Text = openFileDialog1.FileName;
            
            
        }

        public void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            textBox3.Text = openFileDialog1.FileName;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Run.InstallZip();

        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            textBox4.Text = openFileDialog1.FileName;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.TwrpInstalled = checkBox1.Checked;
            Properties.Settings.Default.Save();
            if (checkBox1.Checked == true)
            {
                button5.Visible = false;
                label2.Visible = false;
                textBox4.Visible = false;
                checkBox4.Visible = false;
            }
            else
            {
                button5.Visible = true;
                label2.Visible = true;
                textBox4.Visible = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            // String combo = comboBox1.SelectedValue.ToString();
            //MessageBox.Show(combo);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var item2 = comboBox1.SelectedItem;
            MyType ter = (MyType)item2;
            try { downloadVersion = ter.link; }
            catch {
                MessageBox.Show("Please select a version");
                return;
            };
            f2 = new DownloadDialog(downloadVersion, this, 0);
            f2.Show();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            getCommands test = new getCommands();
            string[,] commands;
            //MessageBox.Show(cmdAsync("dir").ToString());
            commands = test.findCommands(@"C:\latestA2fastboot\jasmine_global_images_V10.0.2.0.PDIMIFJ_9.0\flash_all.bat");
            
            int numItems = commands.GetLength(0);
            string fileLocation = @"C:\latestA2fastboot\jasmine_global_images_V10.0.2.0.PDIMIFJ_9.0\images";
            for (int i = 0; i < numItems; i++)
            {
                string est = @"flash " + commands[i , 1] + " " + fileLocation + "\\" + commands[i, 0];
                string exitCode = Run.fastboot(est);
                if (exitCode.Contains("error"))
                {
                    string message = $"An error has ocurred. Would you like to see the ADB output?";
                    string caption = "Instructions";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    buttons = MessageBoxButtons.YesNo;
                    DialogResult results = new DialogResult();
                    if (results == DialogResult.Yes)
                    {
                        MessageBox.Show(exitCode);
                    }
                    return;
                }
            }

        }

        private async void button3_Click_1Async(object sender, EventArgs e)
        {
            //string test1 = 
            //c.addToConsole(Console.Out.ToString());
            new Thread(() => Run.command("Toolkit.bat", " ")).Start();
            
            // Launches TWRP
            runExternalProcesses P = new runExternalProcesses(this);
            if (Run.checkExist(comboBox2.Text, ".img") == false)
            {
                return;
            }


            Run.autoFastboot();
            string test;
            test = Run.fastboot($"boot \"{comboBox2.Text}\"");
            MessageBox.Show(test);
        }

        private void settings2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SaveLocationTwrp = checkBox2.Checked;
            Properties.Settings.Default.Save();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
         //   comboBox2.
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SaveFlashImg = checkBox5.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SaveLocationSideLoadZip = checkBox3.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SaveLocationZipFileTwrp = checkBox4.Checked;
            Properties.Settings.Default.Save();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(this);
            settings.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://forum.xda-developers.com/mi-a2/development/kernel-hex-t3855773");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://twrp.me/xiaomi/xiaomimia2.html");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://dl.adbdriver.com/upload/adbdriver.zip");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/topjohnwu/Magisk/releases/");
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://t.me/a2_gui_toolkit");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://t.me/MiA2dev");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://t.me/MiA2OffTopic");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Invoke((Action)(() =>
            {
                System.Media.SoundPlayer sp = new System.Media.SoundPlayer(Properties.Resources.oof);
                sp.Play();
            }));
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           /* Invoke((Action)(() =>
            {
                SoundPlayer s = new SoundPlayer(".\\Programs\\Egg\\oof.wav");
                s.Play();
            }));*/
        }
    }
}


