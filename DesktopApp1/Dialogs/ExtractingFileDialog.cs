using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp1
{
    public partial class ExtractFileDialog : Form
    {
        EventArgs e;
        object sender;
        public WebClient client;
        public static long Elength = 0;

        StartPage Start;

        private int action;
        private string sourceArchive;
        private string destination;
        public ExtractFileDialog(string SourceArchive, string Destination, StartPage Start, int action)
        {
            this.sourceArchive = SourceArchive;
            this.destination = Destination;
            this.action = action;
            this.Start = Start;

            InitializeComponent();
            if (action == 0)
            {

                //MessageBox.Show("The extraction can take a long time depending on your computer, please be patient. There is no loading bar, but if something goes wrong, I'll let you know.");
                progressBar1.Value = 30;
                lblStatus.Text = "EXTRACTING.... PLEASE WAIT! \n The extraction can take a long time.";
                
                new Thread(() => ExtractFile(sourceArchive, destination)).Start();
                
            }
            else if(action == 1)
            {
                progressBar1.Value = 70;
                lblStatus.Text = "EXTRACTING.... PLEASE WAIT!";

                new Thread(() => ExtractFile(sourceArchive, destination)).Start();
            }
            else if(action == 2)
            {
               

            }
            else
            {
                MessageBox.Show("This option has not been implimented, did you mean 2?");
            }
            
            
        }
        ~ExtractFileDialog() { }

        


        public void ExtractFile(string sourceArchive, string destination)
        {

            string program = " \"" + Directory.GetCurrentDirectory() + Properties.Settings.Default.Files + "7z\\7za.exe\" ";
            string arguments = ("x \"" + sourceArchive + "\" -aoa -o\"" + destination + "\"");
            Start.c.addToConsole("Running command: " + program + " " + arguments + "\n");

            Directory.CreateDirectory(destination);
            long Clength = 1;
            int temp = 0;
            new Thread(() =>
            {
                while (temp < 100)
                {
                    if (File.Exists(destination))
                    {
                        Clength = new System.IO.FileInfo(destination).Length;
                        temp = Convert.ToInt32((Elength / Clength) * 100) ;
                        lblStatus.Invoke((Action)(() =>
                        {
                            lblStatus.Text = $"Extracting {string.Format("{0:0.##}", temp.ToString())}%";
                        }));
                        progressBar1.Invoke((Action)(() => progressBar1.Value = temp));
                    }
                    
                    System.Threading.Thread.Sleep(1000);
                }

            }).Start();
            System.Diagnostics.Process process0 = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            startInfo.FileName = program;
            startInfo.Arguments = arguments;
            process0.StartInfo = startInfo;
            process0.OutputDataReceived += CaptureOutput;
            //process.ErrorDataReceived += CaptureError;
            process0.Start();
            process0.BeginOutputReadLine();

            process0.WaitForExit();
            Invoke((Action)(() => Close()));

        }

        void CaptureOutput(object sender, DataReceivedEventArgs e)
        {
            string Output;
            if (e.Data != null)
            {
                
                Start.Run.ShowOutput(e.Data, ConsoleColor.Green);
                Start.c.addToConsole(e.Data.ToString() + "\n");
                Output = e.Data.ToString();
            }
            else
            {
                return;
            }
                        

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if(action == 0)
            {

            }


        }




        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        
        if (client != null)
           {
                
              client.CancelAsync();
           }
            Close();
        }
        
    }
    
}
