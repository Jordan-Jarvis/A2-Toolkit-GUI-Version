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
    public partial class DownloadDialog : Form
    {
        EventArgs e;
        object sender;
        public WebClient client;
        public static long Elength = 0;
        string downloadLink;
        StartPage Start;
        int canceled = 0;
        public DownloadDialog(string downloadVersion, StartPage Start, int action)
        {
            this.Start = Start;
            downloadLink = downloadVersion;
            InitializeComponent();
            label1_Click(sender, e);
            
            
        }
        ~DownloadDialog() { }

        

        private void label1_Click(object sender, EventArgs e)
        {

           // ExtractFile(Directory.GetCurrentDirectory() + "\\temp\\temp.tgz", Directory.GetCurrentDirectory() + "\\temp\\");

            string url = downloadLink;
            if (!string.IsNullOrEmpty(url))
            {
                Thread thread = new Thread(() =>
                {
                    Uri uri = new Uri(url);
                    string pathString = Directory.GetCurrentDirectory() + "\\temp\\";
                    string fileName = "temp.tgz";
                    Directory.CreateDirectory(pathString);
                    System.Console.WriteLine(pathString + fileName);
                    while(client == null)
                    {
                        System.Threading.Thread.Sleep(500);
                    }
                    client.DownloadFileAsync(uri, pathString + "\\" + fileName);


                });
                thread.Start();

            }
            
        }

        public void ExtractFile(string sourceArchive, string destination)
        {

            string program = " \"" + Directory.GetCurrentDirectory() + Properties.Settings.Default.Files + "7z\\7za.exe\" ";
            string arguments = ("x \"" + sourceArchive + "\" -aoa -o\"" + destination + "\"");
            // MessageBox.Show(program);
            // MessageBox.Show(arguments);
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
            startInfo.Arguments = "l \"" + sourceArchive + "\"";
            process0.StartInfo = startInfo;
            process0.OutputDataReceived += CaptureOutput;
            //process.ErrorDataReceived += CaptureError;
            process0.Start();
            process0.BeginOutputReadLine();

            process0.WaitForExit();



            System.Diagnostics.Process process = new System.Diagnostics.Process();
            startInfo.Arguments = arguments;
            process.StartInfo = startInfo;
            process.Start();
            process.BeginOutputReadLine();

            
            process.WaitForExit();
            Invoke((Action)(() => Close()));

        }

        void CaptureOutput(object sender, DataReceivedEventArgs e)
        {
            string Output;
            if (e.Data != null)
            {
                progressBar1.Invoke((Action)(() => progressBar1.Value = 50));
                lblStatus.Invoke((Action)(() => lblStatus.Text = "EXTRACTING.... PLEASE WAIT!"));
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
            
            client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;

        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (canceled == 1)
            {
                MessageBox.Show("Download Cancelled");
            }
            else
            {
                MessageBox.Show("The extraction can take a long time depending on your computer, please be patient. There is no loading bar, but if something goes wrong, I'll let you know.");
                ExtractFile(Directory.GetCurrentDirectory() + "\\temp\\temp.tgz", Directory.GetCurrentDirectory() + "\\temp\\");
                //File.Delete(Directory.GetCurrentDirectory() + "\\temp\\temp.tgz");
                Elength = 0;
                ExtractFile(Directory.GetCurrentDirectory() + "\\temp\\temp.tar", Directory.GetCurrentDirectory() + "\\Images\\");
                //File.Delete(Directory.GetCurrentDirectory() + "\\temp\\temp.tar");
                Directory.Delete(Directory.GetCurrentDirectory() + "\\temp\\", true);
            }

            
            //catch { return; }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                progressBar1.Minimum = 0;
                double receive = double.Parse(e.BytesReceived.ToString());
                double total = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = receive / total * 100;
                lblStatus.Text = $"Downloaded {string.Format("{0:0.##}", percentage)}%";
                progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            }));
        }





        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        
        if (client != null)
           {
                canceled = 1;
              client.CancelAsync();
           }
            Close();
        }
        
    }
    
}
