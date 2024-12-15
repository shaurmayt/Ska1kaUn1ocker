using System;
using System.Drawing;
using System.Windows.Forms;
using TaskManager;

namespace SkalkaUnlocker
{
    public partial class Form1 : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private task_manager1 managerForm;
        private startup_programs1 startupForm;
        private unlock_restrictions unlockForm;
        private del_screen_eff screenEffForm;
        private about aboutForm;
        private run_other_tool toolForm;

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Text = GenerateRandomString(10);
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void task_manager_Click(object sender, EventArgs e)
        {
            if (managerForm == null || managerForm.IsDisposed)
            {
                managerForm = new task_manager1();
                managerForm.Show();
            }
            else
            {
                if (managerForm.WindowState == FormWindowState.Minimized)
                {
                    managerForm.WindowState = FormWindowState.Normal;
                }
                managerForm.BringToFront();
            }
        }

        private void startup_programs_Click(object sender, EventArgs e)
        {
            if (startupForm == null || startupForm.IsDisposed)
            {
                startupForm = new startup_programs1();
                startupForm.Show();
            }
            else
            {
                if (startupForm.WindowState == FormWindowState.Minimized)
                {
                    startupForm.WindowState = FormWindowState.Normal;
                }
                startupForm.BringToFront();
            }
        }

        private void run_other_tools_Click(object sender, EventArgs e)
        {
            if (toolForm == null || toolForm.IsDisposed)
            {
                toolForm = new run_other_tool();
                toolForm.Show();
            }
            else
            {
                if (toolForm.WindowState == FormWindowState.Minimized)
                {
                    toolForm.WindowState = FormWindowState.Normal;
                }
                toolForm.BringToFront();
            }
        }

        private void unlock_restrictions_Click(object sender, EventArgs e)
        {
            if (unlockForm == null || unlockForm.IsDisposed)
            {
                unlockForm = new unlock_restrictions();
                unlockForm.Show();
            }
            else
            {
                if (unlockForm.WindowState == FormWindowState.Minimized)
                {
                    unlockForm.WindowState = FormWindowState.Normal;
                }
                unlockForm.BringToFront();
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (screenEffForm == null || screenEffForm.IsDisposed)
            {
                screenEffForm = new del_screen_eff();
                screenEffForm.Show();
            }
            else
            {
                if (screenEffForm.WindowState == FormWindowState.Minimized)
                {
                    screenEffForm.WindowState = FormWindowState.Normal;
                }
                screenEffForm.BringToFront();
            }
        }

        private void web_traffic_control_picbox_Click(object sender, EventArgs e)
        {
            if (aboutForm == null || aboutForm.IsDisposed)
            {
                aboutForm = new about();
                aboutForm.Show();
            }
            else
            {
                if (aboutForm.WindowState == FormWindowState.Minimized)
                {
                    aboutForm.WindowState = FormWindowState.Normal;
                }
                aboutForm.BringToFront();
            }
        }
    }
}
