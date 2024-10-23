using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Threading.Tasks;

namespace TaskManager
{
    public partial class task_manager1 : Form
    {
        private Random random = new Random();
        private DataGridView processGrid;
        private Button refreshButton;

        public task_manager1()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = GenerateRandomTitle();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(800, 600);
            this.FormBorderStyle = FormBorderStyle.Sizable;

            refreshButton = new Button
            {
                Text = "Обновить процессы",
                Dock = DockStyle.Top,
                BackColor = Color.LightBlue,
                FlatStyle = FlatStyle.Flat,
                Height = 40
            };
            refreshButton.Click += RefreshButton_Click;

            InitializeProcessGrid();

            this.Controls.Add(processGrid);
            this.Controls.Add(refreshButton);
        }

        private void task_manager_Load(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private string GenerateRandomTitle()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void InitializeProcessGrid()
        {
            processGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToResizeRows = false
            };

            processGrid.Columns.Add("Process", "Process");
            processGrid.Columns.Add("ID", "ID");
            processGrid.Columns.Add("Status", "Status");
            processGrid.Columns.Add("Username", "Username");
            processGrid.Columns.Add("Critical", "Critical");
            processGrid.Columns.Add("Description", "Description");

            foreach (DataGridViewColumn column in processGrid.Columns)
            {
                column.HeaderCell.Style.BackColor = Color.LightGreen;
                column.HeaderCell.Style.Font = new Font("Arial", 10, FontStyle.Bold);
            }

            processGrid.Rows.Add("", "", "", "", "", "");

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("End Process", null, EndProcess_Click);
            contextMenu.Items.Add("Freeze Process", null, FreezeProcess_Click);
            contextMenu.Items.Add("Make Critical", null, MakeCritical_Click);
            contextMenu.Items.Add("Open File Location", null, OpenFileLocation_Click);
            contextMenu.Items.Add("Properties", null, Properties_Click);

            processGrid.ContextMenuStrip = contextMenu;
        }

        private async void LoadProcesses()
        {
            var currentProcesses = Process.GetProcesses();

            await Task.Run(() =>
            {
                var processInfo = currentProcesses.AsParallel().Select(process =>
                {
                    string username = GetProcessOwner(process);
                    string description = GetProcessDescription(process);
                    bool isCritical = false;

                    return new
                    {
                        Id = process.Id,
                        Name = process.ProcessName,
                        Status = process.Responding ? "Running" : "Not Responding",
                        Username = username,
                        Critical = isCritical.ToString(),
                        Description = description
                    };
                }).ToArray();

                if (this.IsHandleCreated)
                {
                    this.BeginInvoke((MethodInvoker)delegate {
                        UpdateProcessGrid(processInfo);
                    });
                }
            });
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void UpdateProcessGrid(dynamic[] processInfo)
        {
            processGrid.Rows.Clear();
            processGrid.Rows.Add("", "", "", "", "", "");

            foreach (var info in processInfo)
            {
                var existingRow = processGrid.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r =>
                        r.Cells["ID"].Value != null &&
                        int.TryParse(r.Cells["ID"].Value.ToString(), out int cellId) &&
                        cellId == info.Id
                    );

                if (existingRow != null)
                {
                    existingRow.Cells["Process"].Value = info.Name;
                    existingRow.Cells["Status"].Value = info.Status;
                    existingRow.Cells["Username"].Value = info.Username;
                    existingRow.Cells["Critical"].Value = info.Critical;
                    existingRow.Cells["Description"].Value = info.Description;
                }
                else
                {
                    processGrid.Rows.Add(info.Name, info.Id, info.Status, info.Username, info.Critical, info.Description);
                }
            }
        }

        private string GetProcessOwner(Process process)
        {
            try
            {
                var query = "Select * From Win32_Process Where ProcessID = " + process.Id;
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                ManagementObjectCollection processList = searcher.Get();

                foreach (ManagementObject obj in processList)
                {
                    object[] args = new object[2];
                    int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", args));
                    if (returnVal == 0)
                        return args[0].ToString();
                }
            }
            catch { }

            return "Unknown";
        }

        private string GetProcessDescription(Process process)
        {
            try
            {
                return process.MainModule.FileVersionInfo.FileDescription;
            }
            catch
            {
                return "No Description";
            }
        }

        private void EndProcess_Click(object sender, EventArgs e)
        {
            int processId = GetSelectedProcessId();
            if (processId != -1)
            {
                Process.GetProcessById(processId).Kill();
                LoadProcesses();
            }
        }

        private void FreezeProcess_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Freeze functionality not implemented");
        }

        private void MakeCritical_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Make critical functionality not implemented");
        }

        private void OpenFileLocation_Click(object sender, EventArgs e)
        {
            int processId = GetSelectedProcessId();
            if (processId != -1)
            {
                string filePath = Process.GetProcessById(processId).MainModule.FileName;
                string directory = Path.GetDirectoryName(filePath);
                Process.Start("explorer.exe", directory);
            }
        }

        private void Properties_Click(object sender, EventArgs e)
        {
            int processId = GetSelectedProcessId();
            if (processId != -1)
            {
                string filePath = Process.GetProcessById(processId).MainModule.FileName;
                Process.Start("explorer.exe", $"/select,\"{filePath}\"");
            }
        }

        private int GetSelectedProcessId()
        {
            if (processGrid.SelectedRows.Count > 0)
            {
                return int.Parse(processGrid.SelectedRows[0].Cells["ID"].Value.ToString());
            }
            return -1;
        }
    }
}
