using DesktopApp1.Dialogs;
using DesktopApp1.subroutines.adb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;
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
        public DownloadDialog f0;
        internal runExternalProcesses Run { get => run; set => run = value; }



        public StartPage()
        {
            if (!Directory.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Programs\\"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Programs\\");
                //downloadDependencies();
                // Download dependencies function!!!
            }
            //if(Properties.Settings.Default.ImagesLocation.Contains("default") && !Properties.Settings.Default.ImagesLocationIsCustom)
            {
                Properties.Settings.Default.ImagesLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Images\\";
            }
            
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
                
                TwrpImage.Text = Properties.Settings.Default.SaveLocationTwrpLocation;
                checkBox2.Checked = true;
            }
            checkBox1.Checked = Properties.Settings.Default.TwrpInstalled;







            //get list items from saved location
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!Directory.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Images\\"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Images\\");
            }
            string[] temp = Directory.GetDirectories(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Images\\");

            for(int r = 0; r < temp.Length; r++ )
            {
                string[] tem = temp[r].Split('\\');

                listBox1.Items.Add(tem[tem.Length - 1]);
            }
            if (!Directory.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\backups\\"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\backups\\");
            }
            string[] tempt = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\backups\\");
            string temporary = null;
            for (int r = 0; r < tempt.Length; r++ )
            {
                string[] tempr = null;
                string[] tem1 = tempt[r].Split('\\' , '.');
                    listBox3.Items.Add(tem1[tem1.Length - 2]);
            }

                
            





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
            
            comboBox1.Sorted = true;
            comboBox1.Text = "Select Version";
            textBox4.AutoCompleteMode = AutoCompleteMode.Suggest;
            
            new Thread(() => jarvisBlink()).Start();
        }

        private bool downloadDependencies()
        {
            DownloadDialog d;
            d = new DownloadDialog("https://github.com/da-ha3ker/Jarvinator-A2-Toolkit-Dependencies/archive/master.zip", this, 1);
            d.ShowDialog();
            ExtractFileDialog e;
            e = new ExtractFileDialog(Directory.GetCurrentDirectory() + "\\temp\\temp.zip", Directory.GetCurrentDirectory() + "\\Programs\\", this, 0);
            e.ShowDialog();
            return true;
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
                if (Run.autoFastboot())
                {
                    Run.InstallZip();
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
                if (temp != null)
                {
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
            
                output = Run.fastboot("flash " + currentPartition + " \"" +  filePath + "\"");
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
            new Thread(() =>
            {
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
                        else if (result == DialogResult.Abort)
                        {
                            return;
                        }
                    }
                }
                Run.fastboot("reboot");
            }).Start();


        }
            


            //partitions.Add(checkedListBox1.)

        

        
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
            TwrpImage.Text = openFileDialog1.FileName;
            
        }

        public void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SaveLocationSideLoadZip)
            {
                Properties.Settings.Default.SaveLocationSideLoadZipLocation = textBox3.Text;
            }
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
            if (Properties.Settings.Default.SaveLocationZipFileTwrp)
            {
                Properties.Settings.Default.SaveLocationZipFileTwrpLocation = textBox4.Text;
            }
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
                checkBox4.Visible = true;
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
            string message = $"This will download roughly 2GB and take around 3-4 GB once extracted. Please ensure your computer has at least 8GB free space during the extraction process. Are you sure you want to continue?";
            string caption = "Instructions";
            MessageBoxButtons buttons;
            buttons = MessageBoxButtons.YesNo;
            DialogResult results = MessageBox.Show(message, caption, buttons);
            if (results == DialogResult.No)
            {
                return;
            }
            f0 = new DownloadDialog(downloadVersion, this, 0);
            f0.ShowDialog();
            
   
            ExtractFileDialog f2;
            f2 = new ExtractFileDialog(Directory.GetCurrentDirectory() + "\\temp\\temp.tgz", Directory.GetCurrentDirectory() + "\\temp\\", this, 0);
            f2.ShowDialog();
            ExtractFileDialog f3;
            f3 = new ExtractFileDialog(Directory.GetCurrentDirectory() + "\\temp\\temp.tar", Directory.GetCurrentDirectory() + "\\Images\\", this, 1);
            f3.ShowDialog();
           // MessageBox.Show("asdfasdfasfasf");
            Directory.Delete(Directory.GetCurrentDirectory() + "\\temp\\", true);

        }

        private void button7_Click(object sender, EventArgs e)
        {
        
            getCommands test = new getCommands();
            string[,] commands;
            if(listBox1.SelectedItem == null)
            {
                MessageBox.Show("ERROR! Please select an image from the \"Detected\\Downloaded Images\" list! If your list is empty, please select a version to download and click Download. Alternatively you can place the folder containing the batch files and fastboot images into the \"Images\" folder.");
                return;
            }
            if (listBox2.SelectedItem == null)
            {
                MessageBox.Show("ERROR! Please select a batch file to parse!");
                return;
            }
            string path = Properties.Settings.Default.ImagesLocation + listBox1.SelectedItem + "\\" + listBox2.SelectedItem;
            commands = test.findCommands(path);
            
            int numItems = commands.GetLength(0);
            string fileLocation = @"C:\latestA2fastboot\jasmine_global_images_V10.0.2.0.PDIMIFJ_9.0\images";
            new Thread(() =>
            {
                if (!Run.autoFastboot())
                {
                    return;
                }
                for (int i = 0; i < numItems; i++)
                {

                    string est = @"flash " + commands[i, 1] + " " + fileLocation + "\\" + commands[i, 0];

                    string exitCode = Run.fastboot(est);

                    
                    if (exitCode.Contains("error"))
                    {
                        string message = $"An error has ocurred. Would you like to retry? NOTE, it is very common for this to fail for no apparent reason. Before closing, retry a few times.";
                        string caption = "Instructions";
                        MessageBoxButtons buttons;
                        buttons = MessageBoxButtons.YesNo;
                        DialogResult results = MessageBox.Show(message, caption, buttons);
                        if (results == DialogResult.Yes)
                        {
                            i--;
                        }
                        else
                        {
                            return;
                        }
                        
                    }
                }

            }).Start();
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (Run.checkExist(TwrpImage.Text, ".img"))
            {
                new Thread(() =>
                {
                    if (Run.autoFastboot())
                    {
                        string test;
                        test = Run.fastboot($"boot \"{textBox4.Text}\"");
                        MessageBox.Show("Command sent!");
                    }
                    return;
                }).Start();
            }
            

        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SaveLocationTwrp = checkBox2.Checked;
            Properties.Settings.Default.SaveLocationTwrpLocation = TwrpImage.Text;
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

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SaveFlashImg = checkBox5.Checked;
            Properties.Settings.Default.Save();
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

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }





        //Links
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

        //Toolstrip stuff

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(this);
            settings.ShowDialog();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void settings2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_2(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SaveLocationTwrp)
            {

                Properties.Settings.Default.SaveLocationTwrpLocation = TwrpImage.Text;
                Properties.Settings.Default.Save();
                
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            //MessageBox.Show( listBox1.SelectedItem.ToString());
                foreach (String file in Directory.GetFiles(Properties.Settings.Default.ImagesLocation + "\\" + listBox1.SelectedItem))
                {
                string[] tem = file.Split('.');
                if (tem[tem.Length - 1].Contains("bat")){
                    string[] tem2 = file.Split('\\');
                    listBox2.Items.Add(tem2[tem2.Length - 1]);
                }
                //listBox2.Items.Add(file);
                }
                

            
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            if (textBox2.Text.ToString().Contains("Enter a name for your backup here"))
            {
                MessageBox.Show("Please enter a name for your backup");
                return;
            }
            string message = "Are you sure you want to run the backup now?";
            string caption = "Hello";
            MessageBoxButtons buttons;
            buttons = MessageBoxButtons.YesNo;
            DialogResult results = MessageBox.Show(message, caption, buttons);
            if (results == DialogResult.No)
            {
                return;
            }
            new Thread(() =>
            {
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\backups\\"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\backups\\");
                }
                string temp; 

                if (run.adb("devices").Contains("no devices"))
                {
                    MessageBox.Show("No device detected! Please ensure it is plugged in, has debugging enabled and your pc has the proper drivers!");
                    return;
                }
                new Thread(() => MessageBox.Show("Please unlock your phone and follow the instructions from there.")).Start();
                temp = run.adb(" backup -apk -shared -all -f \"" + Directory.GetCurrentDirectory() + "\\backups\\" + textBox2.Text + ".ab\"");
                if (temp.Contains("error"))
                {
                    MessageBox.Show("An error occured, please check the console to see the ADB output.");
                    return;
                }
                
            }).Start();
            
        }

        private void textBox2_TextChanged_3(object sender, EventArgs e)
        {

        }
    }

}


