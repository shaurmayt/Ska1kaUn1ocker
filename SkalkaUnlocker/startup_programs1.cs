using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SkalkaUnlocker
{
    public partial class startup_programs1 : Form
    {
        private Random random;
        private TabControl tabControl;
        private ListView lvRun, lvRunOnce, lvWinlogon, lvStartupFolder, lvTaskScheduler;

        public startup_programs1()
        {
            InitializeComponent();

            // Генерация полностью случайного названия окна
            random = new Random();
            this.Text = GenerateRandomString(10); // Название из 10 случайных символов

            // Инициализация вкладок
            InitializeTabs();
        }

        private void startup_programs1_Load(object sender, EventArgs e)
        {
            // Дополнительная логика при загрузке формы
        }

        // Метод для генерации случайной строки
        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void InitializeTabs()
        {
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Инициализация вкладок
            InitializeRunTab();
            InitializeRunOnceTab();
            InitializeWinlogonTab();
            InitializeStartupFolderTab();
            InitializeTaskSchedulerTab();

            this.Controls.Add(tabControl);
        }

        private void InitializeRunTab()
        {
            TabPage runTab = new TabPage("Run (Registry)");
            lvRun = new ListView { Dock = DockStyle.Fill, View = View.Details };
            lvRun.Columns.Add("Name", 200);
            lvRun.Columns.Add("Path", 500);
            LoadRegistryData(lvRun, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");

            AddContextMenu(lvRun);
            runTab.Controls.Add(lvRun);
            tabControl.TabPages.Add(runTab);
        }

        private void InitializeRunOnceTab()
        {
            TabPage runOnceTab = new TabPage("RunOnce (Registry)");
            lvRunOnce = new ListView { Dock = DockStyle.Fill, View = View.Details };
            lvRunOnce.Columns.Add("Name", 200);
            lvRunOnce.Columns.Add("Path", 500);
            LoadRegistryData(lvRunOnce, @"SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce");

            AddContextMenu(lvRunOnce);
            runOnceTab.Controls.Add(lvRunOnce);
            tabControl.TabPages.Add(runOnceTab);
        }

        private void InitializeWinlogonTab()
        {
            TabPage winlogonTab = new TabPage("Winlogon (Registry)");
            lvWinlogon = new ListView { Dock = DockStyle.Fill, View = View.Details };
            lvWinlogon.Columns.Add("Name", 200);
            lvWinlogon.Columns.Add("Path", 500);
            LoadRegistryData(lvWinlogon, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");

            AddContextMenu(lvWinlogon);
            winlogonTab.Controls.Add(lvWinlogon);
            tabControl.TabPages.Add(winlogonTab);
        }

        private void InitializeStartupFolderTab()
        {
            TabPage startupFolderTab = new TabPage("Startup Folder");
            lvStartupFolder = new ListView { Dock = DockStyle.Fill, View = View.Details };
            lvStartupFolder.Columns.Add("Name", 200);
            lvStartupFolder.Columns.Add("Path", 500);
            LoadStartupFolderData(lvStartupFolder);

            AddContextMenu(lvStartupFolder);
            startupFolderTab.Controls.Add(lvStartupFolder);
            tabControl.TabPages.Add(startupFolderTab);
        }

        private void InitializeTaskSchedulerTab()
        {
            TabPage taskSchedulerTab = new TabPage("Task Scheduler");
            lvTaskScheduler = new ListView { Dock = DockStyle.Fill, View = View.Details };
            lvTaskScheduler.Columns.Add("Name", 200);
            lvTaskScheduler.Columns.Add("Path", 500);
            // Дополнительно: можно загрузить задачи из планировщика задач.

            AddContextMenu(lvTaskScheduler);
            taskSchedulerTab.Controls.Add(lvTaskScheduler);
            tabControl.TabPages.Add(taskSchedulerTab);
        }

        private void LoadRegistryData(ListView listView, string registryPath)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath))
            {
                if (key != null)
                {
                    foreach (string valueName in key.GetValueNames())
                    {
                        string value = key.GetValue(valueName)?.ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            listView.Items.Add(new ListViewItem(new[] { valueName, value }));
                        }
                    }
                }
            }
        }

        private void LoadStartupFolderData(ListView listView)
        {
            string startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            DirectoryInfo dirInfo = new DirectoryInfo(startupFolder);
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                listView.Items.Add(new ListViewItem(new[] { file.Name, file.FullName }));
            }
        }

        private void AddContextMenu(ListView listView)
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Удалить");
            ToolStripMenuItem openLocationItem = new ToolStripMenuItem("Открыть расположение");

            // Добавляем обработчики событий для удаления и открытия расположения
            deleteItem.Click += (s, e) => DeleteItem(listView);
            openLocationItem.Click += (s, e) => OpenItemLocation(listView);

            contextMenu.Items.AddRange(new ToolStripItem[] { deleteItem, openLocationItem });
            listView.ContextMenuStrip = contextMenu;
        }

        private void DeleteItem(ListView listView)
        {
            if (listView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView.SelectedItems[0];
                string itemName = selectedItem.SubItems[0].Text;
                string itemPath = selectedItem.SubItems[1].Text;

                // Удаление записи из автозагрузки (например, реестр)
                if (MessageBox.Show($"Вы уверены, что хотите удалить {itemName}?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        if (File.Exists(itemPath))
                        {
                            File.Delete(itemPath);
                            listView.Items.Remove(selectedItem);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка удаления: " + ex.Message);
                    }
                }
            }
        }

        private void OpenItemLocation(ListView listView)
        {
            if (listView.SelectedItems.Count > 0)
            {
                string itemPath = listView.SelectedItems[0].SubItems[1].Text;

                string actualPath;

                if (itemPath.StartsWith("\""))
                {
                    int secondQuoteIndex = itemPath.IndexOf("\"", 1);
                    actualPath = itemPath.Substring(1, secondQuoteIndex - 1);
                }
                else
                {
                    int lastBackslashIndex = itemPath.LastIndexOf('\\');
                    if (lastBackslashIndex != -1)
                    {
                        actualPath = itemPath.Substring(0, lastBackslashIndex + 1) + Path.GetFileName(itemPath);
                    }
                    else
                    {
                        actualPath = itemPath.Split(' ')[0];
                    }
                }

                if (File.Exists(actualPath))
                {
                    Process.Start("explorer.exe", $"/select,\"{actualPath}\"");
                }
                else
                {
                    MessageBox.Show("Файл не найден: " + actualPath);
                }
            }
        }
    }
}
