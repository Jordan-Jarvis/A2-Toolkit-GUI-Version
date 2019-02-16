using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
namespace DesktopApp1
{
    public partial class Form1 : Form
    {
        int hasSelectedSoftwareVersion = 0;
        public string downloadVersion;
        public Form1()
        {
            InitializeComponent();
            XMLReader xmlList = new XMLReader("./files/convertjson.xml");
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
        }



        private async System.Threading.Tasks.Task<string> cmdAsync(string command)
        {
            string currentOutput;
           
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "@ /c " + command;
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            currentOutput = await process.StandardOutput.ReadLineAsync();
            process.WaitForExit();
            return output;
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

            string isDevice = fastboot("devices");
            if (string.IsNullOrEmpty(isDevice) || string.IsNullOrWhiteSpace(isDevice))
            {
                adb("reboot");
            }
            else
            {
                fastboot("reboot");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            adb("reboot bootloader");
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "fastboot_edl.exe";
            startInfo.Arguments = "reboot-edl";
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            //return output;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            adb("reboot bootloader");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fastboot("reboot");
        }

        private void autoFastboot()
        {
            string isDevice = fastboot("devices");
            if (string.IsNullOrEmpty(isDevice) || string.IsNullOrWhiteSpace(isDevice))
            {
                string devices = adb("devices -l");
                if (devices.Contains("jasmine"))
                {
                    adb("reboot bootloader");
                }
                else
                {
                    MessageBox.Show("No devices found. Please ensure it is plugged in and the proper drivers are installed.");
                }
            }
            else
            {
                return;
            }
        }
    public string adb(string command)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "./adb/adb.exe";
            startInfo.Arguments = command;
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            output += process.StandardError.ReadToEnd();
            //MessageBox.Show(output);
            process.WaitForExit();
            return output;
        }

        public string fastboot(string command)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "./adb/fastboot.exe";
            startInfo.Arguments = command;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //MessageBox.Show(command);
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            output += process.StandardError.ReadToEnd();
            //MessageBox.Show(output);
            process.WaitForExit();
            return output;
        }


        private void unlockFlashing_Click(object sender, EventArgs e)
        {
            autoFastboot();
            fastboot("flashing unlock");
        }

        private void unlockOEM_Click(object sender, EventArgs e)
        {
            autoFastboot();
            fastboot("oem unlock");
        }

        private void unlockCritical_Click(object sender, EventArgs e)
        {
            autoFastboot();
            fastboot("flashing unlock_critical");
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
                output = fastboot("flash {currentPartition} {filePath}");
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

            if (checkExist(textBox1.Text, ".img") == false)
            {
                return;
            }
            autoFastboot();
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

        private bool checkExist(string location, string extension)
        {


            if (File.Exists(location))
            {
                if (Path.GetExtension(location) != extension)
                {
                    MessageBox.Show($"The file selected is not a flashable image. Please select a flashable {extension} file.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("The file chosen could not be found!");
                return false;
            }
            return true;
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

        private void button3_Click_1(object sender, EventArgs e)
        {
            // Launches TWRP
            if (checkExist(textBox2.Text, ".img") == false)
            {
                return;
            }
            

            autoFastboot();
            string test;
            test = fastboot($"boot \"{textBox2.Text}\"");
            MessageBox.Show(test);
        }

        private void browseTwrp_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            textBox2.Text = openFileDialog1.FileName;


            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
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
            if (checkExist(textBox3.Text, ".zip") == false)
            {
                return;
            }
            autoFastboot();
            fastboot($"boot \"{textBox3.Text}\"");
            if (checkExist(textBox4.Text, ".img") == false)
            {
                return;
            }
            //autoFastboot();
           // fastboot($"boot {textBox3.Text}");

            string message = $"Booting TWRP, Once it starts please unlock and select Advanced>Sideload then swipe to start sideload and click OK";
            string caption = "Instructions";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = new DialogResult();
            if (result == DialogResult.Cancel)
            {
                return;
            }
            string zipResult = "failed";
            while (zipResult.Contains("failed") || zipResult.Length > 5)
            {
                
                System.Threading.Thread.Sleep(15000);
                zipResult = adb($"sideload \"{textBox3.Text}\"");
                Console.WriteLine(zipResult);
                if (zipResult.Contains("error"))
                {
                    message = $"An error has ocurred. Would you like to see the ADB output?";
                    caption = "Instructions";
                    buttons = MessageBoxButtons.YesNo;
                    DialogResult results = new DialogResult();
                    if (results == DialogResult.Yes)
                    {
                        MessageBox.Show(zipResult);
                    }
                    return;
                }
            }
            MessageBox.Show(zipResult);
            return;

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
            if (checkBox1.Checked == true)
            {
                button5.Visible = false;
                label2.Visible = false;
                textBox4.Visible = false;
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
            Form2 f2 = new Form2(downloadVersion);
            f2.ShowDialog();
            

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
                string exitCode = fastboot(est);
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
    }
}


