using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp1.Dialogs
{
    public partial class Settings : Form
    {
        List<string> settings;
        StartPage Start;
        string[] comboboxSettings1 = new string[10];
        public Settings(StartPage Start)
        {
            InitializeComponent();
            this.Start = Start;
            checkBox1.Checked = Properties.Settings.Default.ShowConsole;
        }
        ~Settings()
        {
            //Properties.Settings.Default.Save();

        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();

            comboBox1.Text = openFileDialog1.FileName;
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowConsole = checkBox1.Checked;



        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            Properties.Settings.Default.Reload();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            applySettings();
        }
        private void applySettings()
        {
            Properties.Settings.Default.Save();
            if (checkBox1.Checked)
            {
                if(Start.c.IsDisposed)
                {
                    Start.c = new ConsoleDisplay();
                }
                else
                {
                    Start.c.Show();
                }   
                Focus();
            }
            else
            {
                Start.c.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            applySettings();
            Close();
        }
    }
}
