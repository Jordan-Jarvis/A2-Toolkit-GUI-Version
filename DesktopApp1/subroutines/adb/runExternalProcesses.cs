using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp1.subroutines.adb
{
    public class runExternalProcesses
    {
        
        private StartPage Start;
        private static string fastbootOutput;
        private static string adbOutput;
        public static string cmdOutput;
        
        public static string OutputError;
        public string Output { get; set; }
        public runExternalProcesses(StartPage startPage)
        {
            this.Start = startPage;
            
            
        }
        public void EDL()
        {
            adb("reboot bootloader");
            new Thread(() =>
            {
                if (autoFastboot())
                {
                    
                    command("." + Properties.Settings.Default.Files + "adb\\fastboot_edl.exe", "reboot-edl");
                };

            }).Start();

            
            
            return;
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
            if (!autoFastboot())
            {
                return 0;
            }
            string message;
            if (checkExist(Start.textBox3.Text, ".zip") == false)
            {
                return 0;
            }
            if (!Properties.Settings.Default.TwrpInstalled)
            {
                message = $"Booting TWRP, Once it starts please unlock and select Advanced>Sideload then swipe to start sideload then click OK";
                if (checkExist(Start.textBox4.Text, ".img") == false)
                {
                    



                    DialogResult results = MessageBox.Show("Warning, the file selected is not in the typical .img format, are you sure you want to continue?", "Warning", MessageBoxButtons.OKCancel);
                    if (results == DialogResult.Cancel)
                    {
                        return 0;
                    }
                    
                }
                fastboot($"boot \"{Start.textBox4.Text}\"");
                
            }
            else
            {
                message = ("Please enter TWRP now. Once it starts please unlock and select Advanced>Sideload then swipe to start sideload then click OK");
            }
            //autoFastboot();
            // fastboot($"boot {textBox3.Text}");

            
            string caption = "Instructions";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = new DialogResult();
            if (result == DialogResult.Cancel)
            {
                return 0;
            }
            string zipResult = "failed";
            int i = 0;
            while (i < 1)
            {

                //System.Threading.Thread.Sleep(15000);
                zipResult = adb($"sideload \"{Start.textBox3.Text}\"");
                if (zipResult.Contains("failed"))
                {
                    message = $"An error has ocurred while sideloading. Would you like to retry?";
                    caption = "Instructions";
                    buttons = MessageBoxButtons.YesNo;
                    DialogResult results = new DialogResult();
                    if (results == DialogResult.Yes)
                    {
                        i = 0;
                    }
                    else
                    {
                        i = 1;
                    }
                }
            }

            return 1;
        }

        public bool autoFastboot()
        {
            string isDevice = null;

            foreach (var process in Process.GetProcessesByName("fastboot"))
            {
                process.Kill();
            }

            new Thread(() => fastboot("devices")).Start();
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
                    return false;
                }
            }
            else
            {
                return true;
            }
            return true;
        }
        public string adb(string arguments)
        {
            string adbOutput = command(".\\Programs\\adb\\adb.exe ", arguments);
            return adbOutput;
        }

        public string fastboot(string arguments)
        {
            string fastbootOutput = command(".\\Programs\\adb\\fastboot.exe ", arguments);
            return fastbootOutput;
        }
        

        public string command(string filePath, string arguments)
        {
            Start.c.addToConsole("Running command: " + filePath + " " + arguments + "\n");
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            
            startInfo.FileName = filePath;
            startInfo.Arguments = arguments;
            process.StartInfo = startInfo;
            process.OutputDataReceived += CaptureOutput;
            process.ErrorDataReceived += CaptureError;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            //Start.c.addToConsole(process.StandardOutput.ReadToEnd().ToString());
            //process.
            process.WaitForExit();
           //currentOutput = (process.StandardOutput.ReadToEnd() + process.StandardError.ReadToEnd());

            return cmdOutput;
        }
        void CaptureOutput(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                ShowOutput(e.Data, ConsoleColor.Green);
                Start.c.addToConsole(e.Data.ToString() + "\n");
                cmdOutput += (e.Data.ToString() + "\n");
                Output = e.Data.ToString();
            }
            else
            {
                Start.c.addToConsole( "\n");
                return;
            }
            

    }

         void CaptureError(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                //ShowOutput(e.Data, ConsoleColor.Green);

                Start.c.addToConsole(e.Data.ToString() + "\n");
                cmdOutput += (e.Data.ToString() + "\n");
                OutputError += e.Data.ToString();
            }
            else
            {
                return;
            }
            //ShowOutput(e.Data, ConsoleColor.Red);
            //cmdOutput += (e.Data.ToString() + "\n");
        }
        public void ShowOutput(string data, ConsoleColor color)
        {
            if (data != null)
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine("Received: {0}", data);
                Output = data;
                Console.ForegroundColor = oldColor;
            }
        }
    }

}
