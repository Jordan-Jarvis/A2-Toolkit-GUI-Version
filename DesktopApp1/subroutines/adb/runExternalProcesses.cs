using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp1.subroutines.adb
{
    class runExternalProcesses
    {
        private StartPage Start;

        public runExternalProcesses(StartPage startPage)
        {
            this.Start = startPage;
        }
        public string EDL()
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
            return output;
        }
        public bool checkExist(string location, string extension)
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
        public int InstallZip()
        {
            if (checkExist(location: Start.textBox3.Text, extension: ".zip") == false)
            {
                return 0;
            }
            autoFastboot();
            fastboot($"boot \"{Start.textBox3.Text}\"");
            if (checkExist(Start.textBox4.Text, ".img") == false)
            {
                return 0;
            }
            //autoFastboot();
            // fastboot($"boot {textBox3.Text}");

            string message = $"Booting TWRP, Once it starts please unlock and select Advanced>Sideload then swipe to start sideload and click OK";
            string caption = "Instructions";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = new DialogResult();
            if (result == DialogResult.Cancel)
            {
                return 0;
            }
            string zipResult = "failed";
            while (zipResult.Contains("failed") || zipResult.Length > 5)
            {

                //System.Threading.Thread.Sleep(15000);
                zipResult = adb($"sideload \"{Start.textBox3.Text}\"");
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
                    return 1;
                }
            }

            return 1;
        }

        public void autoFastboot()
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
    }
}
