using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp1
{
    class BootloaderOptions
    {
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
                fastboot("reboot");
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
