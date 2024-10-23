using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SkalkaUnlocker
{
    public partial class unlock_restrictions : Form
    {
        private List<CheckBox> checkBoxes;

        public unlock_restrictions()
        {
            InitializeComponent();
            checkBoxes = new List<CheckBox>();
            InitializeCheckBoxes();
        }

        private void unlock_restrictions_Load(object sender, EventArgs e)
        {
            // 123
        }

        private void InitializeCheckBoxes()
        {
            string[] restrictionNames = new string[]
            {
                "Disable TaskMgr", "DisableRegistryTools", "DisableCMD", "RestrictToPemittedSnapins", "NoControlPanel",
                "NoRun", "NoViewOnDrive", "NoDrives", "NoFind", "NoViewContextMenu", "NoFolderOptions", "NoSecurityTab",
                "NoFileMenu", "NoClose", "NoCommonGroups", "StartMenuLogOff", "NoChangingWallPaper", "NoWinKeys",
                "NoSetTaskbar", "DisableLockWorkstation", "DisableChangePassword", "No TrayContextMenu",
                "DenyUsersFromMachGP", "HidePowerOptions", "DisableContextMenusInStart", "DisableSR", "DisableConfig",
                "NoLogoff"
            };

            int y = 20;
            foreach (var name in restrictionNames)
            {
                CheckBox cb = new CheckBox
                {
                    Text = name,
                    Location = new Point(20, y),
                    AutoSize = true
                };
                checkBoxes.Add(cb);
                this.Controls.Add(cb);
                y += 25;
            }

            Button selectAllButton = new Button
            {
                Text = "Выбрать все",
                Location = new Point(20, y),
                Width = 100
            };
            selectAllButton.Click += SelectAllButton_Click;
            this.Controls.Add(selectAllButton);

            Button unlockButton = new Button
            {
                Text = "Разблокировать",
                Location = new Point(140, y),
                Width = 120
            };
            unlockButton.Click += UnlockButton_Click;
            this.Controls.Add(unlockButton);
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            foreach (var cb in checkBoxes)
            {
                cb.Checked = true;
            }
        }

        private void UnlockButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var cb in checkBoxes)
                {
                    if (cb.Checked)
                    {
                        UnlockRestriction(cb.Text);
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
