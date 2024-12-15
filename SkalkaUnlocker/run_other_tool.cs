using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkalkaUnlocker
{
    public partial class run_other_tool : Form
    {
        public run_other_tool()
        {
            InitializeComponent();
            GenerateRandomWindowTitle();
            SetupUI();
        }

        private void GenerateRandomWindowTitle()
        {
            var random = new Random();
            var titleLength = random.Next(8, 16);
            var title = new StringBuilder();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            for (int i = 0; i < titleLength; i++)
                title.Append(chars[random.Next(chars.Length)]);

            this.Text = title.ToString();
        }

        private void SetupUI()
        {
            this.Width = 500;
            this.Height = 400;

            var programs = new[]
            {
                new { Name = "Process Hacker", Description = "Инструмент для мониторинга процессов и системной активности.", Link = "https://processhacker.sourceforge.io/downloads.php" },
                new { Name = "Explorer++", Description = "Легковесный и мощный файловый менеджер.", Link = "https://explorerplusplus.com/download" },
                new { Name = "Autoruns", Description = "Утилита для управления автозагрузкой.", Link = "https://learn.microsoft.com/en-us/sysinternals/downloads/autoruns" },
                new { Name = "7-Zip", Description = "Мощный архиватор файлов с открытым исходным кодом.", Link = "https://www.7-zip.org/download.html" },
                new { Name = "Notepad++", Description = "Расширенный текстовый редактор для разработчиков.", Link = "https://notepad-plus-plus.org/downloads/" },
                new { Name = "HWMonitor", Description = "Мониторинг температуры и напряжений оборудования.", Link = "https://www.cpuid.com/softwares/hwmonitor.html" },
                new { Name = "WinDirStat", Description = "Анализатор использования дискового пространства.", Link = "https://windirstat.net/" },
                new { Name = "Recuva", Description = "Утилита для восстановления удаленных файлов.", Link = "https://www.ccleaner.com/recuva" }
            };

            int yOffset = 20;
            foreach (var program in programs)
            {
                var label = new Label
                {
                    Text = $"{program.Name}: {program.Description}",
                    AutoSize = true,
                    Location = new Point(10, yOffset),
                    Width = this.ClientSize.Width - 20
                };
                this.Controls.Add(label);

                var button = new Button
                {
                    Text = "Скачать",
                    Location = new Point(10, yOffset + 30),
                    AutoSize = true,
                    Tag = program.Link
                };
                button.Click += (sender, args) => Process.Start(new ProcessStartInfo
                {
                    FileName = button.Tag.ToString(),
                    UseShellExecute = true
                });
                this.Controls.Add(button);

                yOffset += 70;
            }
        }
    }
}
