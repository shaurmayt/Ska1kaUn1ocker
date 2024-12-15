using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace SkalkaUnlocker
{
    public partial class del_screen_eff : Form
    {
        private Label statusLabel;
        private int clearCount = 0;
        private Point originalLocation;

        public del_screen_eff()
        {
            InitializeComponent();
        }

        private void del_screen_eff_Load(object sender, EventArgs e)
        {
            originalLocation = this.Location;

            this.Text = GenerateRandomWindowTitle();

            statusLabel = new Label();
            statusLabel.Text = "";
            statusLabel.Size = new Size(300, 40);
            statusLabel.Location = new Point(50, 20);
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            statusLabel.Visible = false;

            Button clearButton = new Button();
            clearButton.Text = "Очистить";
            clearButton.Size = new Size(100, 40);
            clearButton.Location = new Point(150, 80);
            clearButton.Click += new EventHandler(this.clearButton_Click);

            this.Controls.Add(clearButton);
            this.Controls.Add(statusLabel);
        }

        private string GenerateRandomWindowTitle()
        {
            Random rand = new Random();
            StringBuilder sb = new StringBuilder();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            for (int i = 0; i < 10; i++)
            {
                sb.Append(chars[rand.Next(chars.Length)]);
            }

            return sb.ToString();
        }

        private void ClearScreenEffects()
        {
            try
            {
                ClearRandomSymbols();
                ClearScreenBlurAndArtifacts();
                RestoreOriginalScreenPosition();

                clearCount++;

                statusLabel.Text = $"Эффекты очищены!{(clearCount > 1 ? $" (x{clearCount})" : "")}";
                statusLabel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при очистке: " + ex.Message);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearScreenEffects();
        }

        private void ClearRandomSymbols()
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.Clear(this.BackColor);
            }
            Console.WriteLine("Случайные символы и артефакты очищены.");
        }
        private void ClearScreenBlurAndArtifacts()
        {
            this.Refresh();
            Console.WriteLine("Размытость и разноцветные артефакты очищены.");
        }
        private void RestoreOriginalScreenPosition()
        {
            this.Location = originalLocation;
            Console.WriteLine("Положение экрана восстановлено.");
        }
    }
}
