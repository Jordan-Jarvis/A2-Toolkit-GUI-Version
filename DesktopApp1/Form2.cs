using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp1
{
    public partial class Form2 : Form
    {
        EventArgs e;
        object sender;
        string downloadLink;
        int canceled;
        public Form2(string downloadVersion)
        {
             downloadLink = downloadVersion;
            InitializeComponent();
            label1_Click(sender, e);
        }

        WebClient client;

        private void label1_Click(object sender, EventArgs e)
        {
            string url = downloadLink;
            if (!string.IsNullOrEmpty(url))
            {
                Thread thread = new Thread(() =>
                {
                    Uri uri = new Uri(url);
                    string fileLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                    string fileName = "test.tgz";
                    MessageBox.Show(fileLocation + fileName);
                    //string fileName = System.IO.Path.GetFileName(uri.AbsolutePath);
                    client.DownloadFileAsync(uri, fileLocation + fileName);
           
                });
                thread.Start();
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
                MessageBox.Show("download complete!!!");
            }
            this.Close();
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
        }
    }
    
}

/*using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace DesktopApp2
{

    public partial class Form2 : Form
    {
        WebClient webClient;               // Our WebClient that will be doing the downloading for us
        Stopwatch sw = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
        }

        public void DownloadFile(string urlAddress, string location)
        {
            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                // The variable that will be holding the url address (making sure it starts with http://)
                Uri URL = urlAddress.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ? new Uri(urlAddress) : new Uri("http://" + urlAddress);

                // Start the stopwatch which we will be using to calculate the download speed
                sw.Start();

                try
                {
                    // Start downloading the file
                    webClient.DownloadFileAsync(URL, location);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // The event that will fire whenever the progress of the WebClient is changed
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Calculate download speed and output it to labelSpeed.
            labelSpeed.Text = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));

            // Update the progressbar percentage only when the value is not the same.
            progressBar.Value = e.ProgressPercentage;

            // Show the percentage on our label.
            labelPerc.Text = e.ProgressPercentage.ToString() + "%";

            // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading
            labelDownloaded.Text = string.Format("{0} MB's / {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        }

        // The event that will trigger when the WebClient is completed
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            // Reset the stopwatch.
            sw.Reset();

            if (e.Cancelled == true)
            {
                MessageBox.Show("Download has been canceled.");
            }
            else
            {
                MessageBox.Show("Download completed!");
            }
        }

        private void labelSpeed_Click(object sender, EventArgs e)
        {

        }

        private void labelDownloaded_Click(object sender, EventArgs e)
        {

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory();
            string savePath = path;
            System.IO.Directory.CreateDirectory(Path.GetDirectoryName(Directory));

            if (File.Exists(_fullPathWhereToSave))
            {
                File.Delete(_fullPathWhereToSave);
            }
            using (WebClient client = new WebClient())
                DownloadFile("http://bigota.d.miui.com/V9.6.16.0.ODIMIFE/jasmine_global_images_V9.6.16.0.ODIMIFE_20181030.0000.00_8.1_5e098a2c1a.tgz", Directory.GetCurrentDirectory() + "temp");
        }
    }
}
*/