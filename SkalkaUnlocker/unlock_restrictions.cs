using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SkalkaUnlocker
{
    public partial class unlock_restrictions : Form
    {
        private ListView listView;
        private Button selectAllButton;
        private bool isAllSelected = false;

        public unlock_restrictions()
        {
            InitializeComponent();
            this.Text = GenerateRandomTitle();
            this.Icon = SystemIcons.Application;
            InitializeListView();
        }

        private string GenerateRandomTitle()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var title = new StringBuilder(10);
            for (int i = 0; i < 10; i++)
            {
                title.Append(chars[random.Next(chars.Length)]);
            }
            return title.ToString();
        }

        private void unlock_restrictions_Load(object sender, EventArgs e)
        {
            
        }

        private void InitializeListView()
        {
            listView = new ListView
            {
                Location = new Point(20, 20),
                Size = new Size(414, 504), 
                View = View.Details,
                CheckBoxes = true,
                FullRowSelect = true, 
                GridLines = true
            };

            listView.Columns.Add("Ограничение", 170);
            listView.Columns.Add("Описание", 240);

            string[,] restrictionItems = new string[,]
            {
                { "Disable TaskMgr", "Отключен диспетчер задач" },
                { "DisableRegistryTools", "Отключен редактор реестра" },
                { "DisableCMD", "Отключена командная строка" },
                { "RestrictToPemittedSnapins", "Ограничен доступ к оснасткам" },
                { "NoControlPanel", "Скрыта панель управления" },
                { "NoRun", "Отключена команда Выполнить" },
                { "NoViewOnDrive", "Скрыты диски" },
                { "NoDrives", "Отключено отображение дисков" },
                { "NoFind", "Отключен поиск" },
                { "NoViewContextMenu", "Отключено контекстное меню" },
                { "NoFolderOptions", "Скрыты параметры папок" },
                { "NoSecurityTab", "Скрыта вкладка Безопасность" },
                { "NoFileMenu", "Отключено меню Файл" },
                { "NoClose", "Отключен выход из системы" },
                { "NoCommonGroups", "Отключены общие группы" },
                { "StartMenuLogOff", "Отключен выход в меню Пуск" },
                { "NoChangingWallPaper", "Запрещена смена обоев" },
                { "NoWinKeys", "Отключены клавиши Win" },
                { "NoSetTaskbar", "Запрещена настройка панели задач" },
                { "DisableLockWorkstation", "Запрещена блокировка" },
                { "DisableChangePassword", "Запрещена смена пароля" },
                { "No TrayContextMenu", "Отключено контекстное меню трея" },
                { "DenyUsersFromMachGP", "Запрещен доступ к настройкам GP" },
                { "HidePowerOptions", "Скрыты параметры питания" },
                { "DisableContextMenusInStart", "Отключено контекстное меню в Пуск" },
                { "DisableSR", "Отключено восстановление системы" },
                { "DisableConfig", "Отключены настройки конфигурации" },
                { "NoLogoff", "Отключен выход из системы" }
            };

            for (int i = 0; i < restrictionItems.GetLength(0); i++)
            {
                ListViewItem item = new ListViewItem(restrictionItems[i, 0]);
                item.SubItems.Add(restrictionItems[i, 1]);
                listView.Items.Add(item);
            }

            listView.ItemChecked += ListView_ItemChecked;
            this.Controls.Add(listView);

            selectAllButton = new Button
            {
                Text = "Выбрать все",
                Location = new Point(84, 550),
                Width = 100
            };
            selectAllButton.Click += SelectAllButton_Click;
            this.Controls.Add(selectAllButton);

            Button unlockButton = new Button
            {
                Text = "Разблокировать",
                Location = new Point(204, 550),
                Width = 120
            };
            unlockButton.Click += UnlockButton_Click;
            this.Controls.Add(unlockButton);
        }

        private void ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            e.Item.Selected = e.Item.Checked;
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            isAllSelected = !isAllSelected;
            foreach (ListViewItem item in listView.Items)
            {
                item.Checked = isAllSelected;
            }

            selectAllButton.Text = isAllSelected ? "Снять все" : "Выбрать все";
        }

        private void UnlockButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem item in listView.Items)
                {
                    if (item.Checked)
                    {
                        UnlockRestriction(item.Text);
                    }
                }
                MessageBox.Show("Ограничения успешно разблокированы.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при разблокировке: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UnlockRestriction(string restrictionName)
        {
            if (restrictionName == "Disable TaskMgr")
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
                if (key != null)
                {
                    key.DeleteValue("DisableTaskMgr", false);
                    key.Close();
                }
            }
        }
    }
}
