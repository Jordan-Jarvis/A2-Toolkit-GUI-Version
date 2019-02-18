using System;
using System.Windows.Forms;
namespace DesktopApp1
{
    public partial class ConsoleDisplay : Form
    {
        
        private StickyWindow stickyWindow;


        //private System.ComponentModel.Container components = null;
        public ConsoleDisplay()
        {



            InitializeComponent();
            Console.Text = " ";
            Show();
            stickyWindow = new StickyWindow(this);
            stickyWindow.StickOnMove = true;
            stickyWindow.StickGap = 20;
        }

        private void Console_TextChanged(object sender, EventArgs e)
        {
            
        }
        public void addToConsole(string t)
        {


            if (Console.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                {

                    Console.Text = Console.Text + t;
                    // Console.Text = Console.Text + t;
                }));
            }
            //System.Console.WriteLine(t);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate ()
            {

                Console.Text = " ";
                // Console.Text = Console.Text + t;
            }));
            return;
        }
    }
}
