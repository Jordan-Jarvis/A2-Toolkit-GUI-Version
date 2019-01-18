using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp1
{
    public partial class Form1 : Form
    {
        int hasSelectedSoftwareVersion = 0;
        string downloadVersion;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hasSelectedSoftwareVersion == 1)
            {
                string downloadLink = downloadVersion;
                Form2 f2 = new Form2(downloadLink);
                f2.ShowDialog();
                //f2.Close();
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.FileName = "CMD.exe";
                startInfo.Arguments = "/c dir";
               process.StartInfo = startInfo;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                MessageBox.Show(output);
                process.WaitForExit();
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
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "adb.exe";
            startInfo.Arguments = command;
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            //MessageBox.Show(output);
            process.WaitForExit();
            return output;
        }

        public string fastboot(string command)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "fastboot.exe";
            startInfo.Arguments = command;
            MessageBox.Show(command);
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
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
            string currentPartition;
            for (int i = 0; i < partitions.Count; i++)
            {
                if (flashImg(partitions[i], textBox1.Text) == true)
                {

                }
                else
                {
                    
                    string message = $"flashing the {partitions[i]} failed.";
                    string caption = "Error!";
                    MessageBoxButtons buttons = MessageBoxButtons.AbortRetryIgnore;
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
            fastboot($"boot {textBox3.Text}");
            if (checkExist(textBox4.Text, ".img") == false)
            {
                return;
            }
            autoFastboot();
            fastboot($"boot {textBox3.Text}");

            string message = $"Booting TWRP, Once it starts please unlock and select Advanced>Sideload then swipe to start sideload and click OK";
            string caption = "Instructions";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = new DialogResult();
            if (result == DialogResult.Cancel)
            {
                return;
            }
            string zipResult;
            zipResult = adb($"sideload {textBox3.Text}");
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
    }
}


