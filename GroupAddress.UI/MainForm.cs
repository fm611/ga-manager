using GroupAddress.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.LinkLabel;

namespace GroupAddress.UI
{
    public partial class MainForm : Form
    {
        private string? currentProjectFile;

        public Project Project { get; set; }

        public ListBoxWrapper<MainGroup> MainGroupWrapper { get; set; }
        public ListBoxWrapper<Item> ItemWrapper { get; set; }

        public MainGroup? SelectedMainGroup { get; set; }
        public string? SelectedMainGroupId { get; set; }

        public List<Item> SelectedItems { get; set; }


        public ItemTemplateManagerForm? AddItemForm { get; set; }

        public int CurrentGARowScrollIndex { get; set; }

        public string? CurrentProjectFile
        {
            get => currentProjectFile; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Text = "Neues Projekt";
                }
                else
                {
                    Text = new FileInfo(value).Name;
                }
                currentProjectFile = value;
            }
        }

        public string RecentFilesJsonPath { get; set; }
        public List<FileInfo> RecentFiles { get; set; }
        public ToolStripItem[] RecentFilesToolStripItems { get; set; } = [];

        public MainForm()
        {
            InitializeComponent();

            CurrentProjectFile = "";
            Project = new Project();

            MainGroupWrapper = new ListBoxWrapper<MainGroup>(
                MainGroupsListBox,
                (a, b) => a.SubAddress.CompareTo(b.SubAddress),
                nameof(MainGroup.ListBoxString),
                nameof(MainGroup.Id),
                () => Project.MainGroups);

            ItemWrapper = new ListBoxWrapper<Item>(
                ItemsListBox,
                (a, b) => a.Name.CompareTo(b.Name),
                nameof(Item.Name),
                nameof(Item.Id),
                () => Project.GetItems(SelectedMainGroup),
                false);

            RecentFilesJsonPath = "recent.json";
            RecentFiles = [];


            ReadRecentFilesList();
        }





        private void UpdateUI()
        {
            MainGroupWrapper.Update();
            ItemWrapper.Update();
            GADataTable.UpdateTable();
        }


        #region Open Save Export Import

        private void ReadRecentFilesList()
        {
            if (File.Exists(RecentFilesJsonPath))
            {
                using StreamReader reader = new StreamReader(RecentFilesJsonPath);
                var json = reader.ReadToEnd();
                try
                {
                    var obj = JsonSerializer.Deserialize<List<string>>(json);
                    if (obj == null) throw new Exception();
                    RecentFiles = obj.Select(x => new FileInfo(x)).Where(x => x.Exists).Take(10).ToList();
                }
                catch
                {
                    RecentFiles = [];
                }
            }
            UpdateRecentFilesMenu();
        }

        private void WriteRecentFilesList()
        {
            using StreamWriter outputFile = new StreamWriter(RecentFilesJsonPath, false);
            var json = JsonSerializer.Serialize(RecentFiles.Select(x => x.FullName),
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                }
            );
            outputFile.Write(json);
        }

        private void UpdateRecentFilesMenu()
        {
            foreach (var item in RecentFilesToolStripItems)
            {
                OpenToolStripMenuItem.DropDownItems.Remove(item);
            }
            RecentFilesToolStripItems = [];

            var toolStripItems = RecentFiles.Select(r => new ToolStripMenuItem(r.Name, null, (sender, e) => OpenProjectFile(r.FullName))).ToArray();

            if (RecentFiles.Any())
                RecentFilesToolStripItems = [.. new ToolStripItem[] { new ToolStripSeparator() }, .. toolStripItems];

            OpenToolStripMenuItem.DropDownItems.AddRange(RecentFilesToolStripItems);
        }

        private void EnqueueRecentFiles(string path)
        {
            var oldEntries = RecentFiles.Where(x => x.FullName != path).Take(9).ToList();
            RecentFiles.Clear();

            RecentFiles.Add(new FileInfo(path));
            RecentFiles.AddRange(oldEntries);

            WriteRecentFilesList();
            UpdateRecentFilesMenu();
        }

        private void OpenProjectFile(string path)
        {
            if (!File.Exists(path))
            {
                var fi = RecentFiles.FirstOrDefault(x => x.FullName == path);
                if (fi != null)
                    RecentFiles.Remove(fi);
                fi = new FileInfo(path);
                WriteRecentFilesList();
                UpdateRecentFilesMenu();
                MessageBox.Show("Projektdatei nicht gefunden.", fi.Name, MessageBoxButtons.OK);
                return;
            }

            using StreamReader reader = new StreamReader(path);
            var json = reader.ReadToEnd();

            var obj = JsonSerializer.Deserialize<Project>(json);

            if (obj != null)
            {
                Project = obj;
                UpdateUI();
                EnqueueRecentFiles(path);
            }
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(CurrentProjectFile))
            {
                var saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "gaproj";
                saveDialog.Filter = "Project files (*.gaproj)|*.gaproj";

                var res = saveDialog.ShowDialog();

                if (res == DialogResult.OK)
                {
                    var filePath = saveDialog.FileName;
                    CurrentProjectFile = filePath;
                }
            }

            if (!string.IsNullOrEmpty(CurrentProjectFile))
            {
                using StreamWriter outputFile = new StreamWriter(CurrentProjectFile, false);
                var json = Project.GetJson();
                outputFile.Write(Project.GetJson());

                EnqueueRecentFiles(CurrentProjectFile);
            }
        }

        private void OpenProject()
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "Project files (*.gaproj)|*.gaproj";
            openDialog.RestoreDirectory = true;

            var res = openDialog.ShowDialog();

            if (res == DialogResult.OK)
            {
                OpenProjectFile(openDialog.FileName);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Save();
        }

        private void OpenSampleProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project = Project.GetSampleProject();
            UpdateUI();
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        #endregion

        #region MainGroup

        private void AddMainGroup()
        {
            var addMainGroupForm = new AddEditMainGroupForm(Project.MainGroups);
            var result = addMainGroupForm.ShowDialog();

            if (result != DialogResult.OK || addMainGroupForm.MainGroup == null) return;

            Project.MainGroups.Add(addMainGroupForm.MainGroup);
            MainGroupWrapper.Update();
        }
        private void EditMainGroup(string id)
        {
            var addMainGroupForm = new AddEditMainGroupForm(Project.MainGroups, id);
            var result = addMainGroupForm.ShowDialog();

            if (result != DialogResult.OK || addMainGroupForm.MainGroup == null) return;

            MainGroupWrapper.Update();
        }
        private void DeleteMainGroup(string id)
        {
            if (SelectedMainGroup == null) return;

            if (MessageBox.Show("Haupgruppe löschen?", "Hauptgruppe löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Project.MainGroups.Remove(SelectedMainGroup);
            }

            UpdateUI();
        }


        private void MainGroupsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedMainGroup = (MainGroup?)MainGroupsListBox.SelectedItem;
            SelectedMainGroupId = (string?)MainGroupsListBox.SelectedValue;

            ItemWrapper.Update();
            GADataTable.SetTopLevelCollection(SelectedMainGroup);
        }

        #endregion


        private void ItemManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedMainGroup == null)
            {
                MessageBox.Show("Bitte erst eine Hauptgruppe erstellen/auswählen.", "Keine Hauptgruppe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (AddItemForm == null)
                AddItemForm = new ItemTemplateManagerForm(Project);

            AddItemForm.LoadData();
            AddItemForm.SelectMainGroup(SelectedMainGroup?.Id);

            AddItemForm.ShowDialog();

            if (AddItemForm.DialogResult == DialogResult.OK)
            {
                UpdateUI();

                if (AddItemForm.SelectedMainGroup != null)
                {
                    var insertMainGroup = AddItemForm.SelectedMainGroup;
                    MainGroupsListBox.SelectedValue = AddItemForm.SelectedMainGroup?.Id;

                    GADataTable.SetTopLevelCollection(insertMainGroup);

                    var newItem = AddItemForm.LastInsertedItem;

                    if (newItem != null)
                    {
                        var itemGAs = insertMainGroup.GetItemGAs(newItem);
                        var minGA = itemGAs.MinBy(x => x.Addresse.GA);
                        GADataTable.FirstDisplayedScrollingRowIndex = minGA == null ? 0 : GADataTable.GetCell(minGA)?.Row ?? 0;
                    }
                }
            }

        }


        private void AddCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedMainGroup == null) return;
            if (!int.TryParse(AddCellsNumTextBox.Text, out var numRows)) return;


            var colMinRows = GADataTable.SelectedAddresses
                .GroupBy(x => x.MiddleGroup)
                .Select(g => g.MinBy(y => y.GA));


            foreach (var col in colMinRows)
            {
                if (col == null) continue;
                var gas = SelectedMainGroup.GAs.Where(x => x.Addresse.MiddleGroup == col.MiddleGroup && x.Addresse.GA >= col.GA);
                foreach (var g in gas)
                {
                    g.Addresse.GA += numRows;
                }
            }

            GADataTable.UpdateTable();

        }

        private void AddCellsNumTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete) e.Handled = true;
        }

        private void DeleteCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedMainGroup == null) return;

            var gasDelete = GADataTable.SelectedGAs;
            if (gasDelete.Count > 0)
            {
                var res = MessageBox.Show("Gruppenadressen löschen?", "Gruppenadressen löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    gasDelete.ForEach(x => SelectedMainGroup.RemoveGA(x));
                }
                else
                    return;
            }
            foreach (var add in GADataTable.SelectedAddresses.OrderByDescending(a => a.GA))
            {
                var gasMoveUp = SelectedMainGroup.GAs.Where(x => x.Addresse.MiddleGroup == add.MiddleGroup && x.Addresse.GA > add.GA);
                gasMoveUp.ToList().ForEach(x => x.Addresse.GA--);
            }

            GADataTable.UpdateTable();
        }

        private void AddMainGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMainGroup();
        }

        private void EditMainGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedMainGroup == null) return;
            EditMainGroup(SelectedMainGroup.Id);
        }

        private void DeleteMainGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedMainGroup == null) return;
            DeleteMainGroup(SelectedMainGroup.Id);
        }

        private void MainGroupsListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Delete)
            {
                if (SelectedMainGroup == null) return;
                DeleteMainGroup(SelectedMainGroup.Id);
            }
            if (e.KeyData == (Keys.Control | Keys.N))
            {
                if (SelectedMainGroup == null) return;
                AddMainGroup();
            }
        }

        private void UnselectItemsButton_Click(object sender, EventArgs e)
        {
            ItemsListBox.ClearSelected();
        }

        private void ItemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItems = ItemsListBox.SelectedItems.Cast<Item>().ToList();

            if (selectedItems != null && selectedItems.Count > 0)
            {
                GADataTableBackPanel.BackColor = Color.Red;
                GADataTable.FilterByItem(selectedItems);
            } else
            {
                GADataTableBackPanel.BackColor = Color.Transparent;
                GADataTable.UpdateTable();
            }


        }
    }
}