using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdleShutdown
{
    public partial class FrmMain : Form
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        public string prevX = string.Empty;
        public string prevY = string.Empty;


        private int counter = 0;


        private Form frm;

        private int timeInSeconds; 



        public FrmMain()
        {

            InitializeComponent();
            
            timer1.Start();
            timer2.Start();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            //TODO : Check if file is Existing
            //  If not force user to create it




            timeInSeconds = 0;
            frm = new FrmWarning();
            string textFile = AppDomain.CurrentDomain.BaseDirectory + @"Config.dump";
            string [] lines = File.ReadAllLines(textFile);

            timeInSeconds = Convert.ToInt32(lines[0].Split('=')[1]);


            txtSetTime.Text = timeInSeconds.ToString();


            this.WindowState = FormWindowState.Minimized;
            //foreach (string line in lines)
            //    Console.WriteLine(line);


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            Point pt = new Point();

            GetCursorPos(ref pt);
            textBox1.Text = pt.X.ToString();
            textBox2.Text = pt.Y.ToString();



            //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mouse_event
            //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getcursorpos
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label1.Text = counter.ToString();

            if (counter == timeInSeconds - 30)
            {
                frm.TopMost = true;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Show();
            }


            if (textBox1.Text != prevX)
            {
                prevX = textBox1.Text;
                counter = 0;
                frm.Hide();
                return;
            }


            counter = counter + 1;






            if (counter == timeInSeconds + 60)
            {

                var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi);



                foreach (Process p in Process.GetProcesses(System.Environment.MachineName))

                {

                    if (p.MainWindowHandle != IntPtr.Zero)

                    {

                        p.Kill();



                    }

                }
            }
            



        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            bool mousePointerNotOnTaskBar = Screen.GetWorkingArea(this).Contains(Cursor.Position);
            if(this.WindowState == FormWindowState.Minimized && mousePointerNotOnTaskBar)
            {
                notifyIcon1.Icon = new Icon(AppDomain.CurrentDomain.BaseDirectory + @"mouse.ico");
                notifyIcon1.BalloonTipText = "Pisonet Idle State Detector";
                notifyIcon1.ShowBalloonTip(500);
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }



        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;

            if(this.WindowState == FormWindowState.Normal)
            {
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
        }
    }
}
