using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkalkaUnlocker
{
    public partial class about : Form
    {
        public about()
        {
            InitializeComponent();
            this.Text = GenerateRandomTitle();
        }

        private void about_Load(object sender, EventArgs e)
        {

        }

        private void githublnk_Click(object sender, EventArgs e)
        {
            OpenUrl("https://github.com/shaurmayt/Ska1kaUn1ocker");
        }

        private void sitelnk_Click(object sender, EventArgs e)
        {
            OpenUrl("http://skalkaunlocker.pro");
        }

        private string GenerateRandomTitle()
        {
            var random = new Random();
            var title = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                char letter = (char)random.Next('A', 'Z' + 1);
                title.Append(letter);
            }
            return title.ToString();
        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось открыть ссылку: " + ex.Message);
            }
        }
    }
}
