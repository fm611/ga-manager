using GroupAddress.Core;
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
        private Project project;

        public Project Project
        {
            get => project;
            set
            {
                project = value;
                project.Changed += Project_Changed;
                ProjectDirty = false;
            }
        }
        private bool _projectDirty;
        public bool ProjectDirty
        {
            get => _projectDirty;
            set
            {
                _projectDirty = value;
                SetTitle(value);
            }
        }

        private void Project_Changed(object? sender, EventArgs e)
        {
            ProjectDirty = true;
        }

        public ListBoxWrapper<MainGroup> MainGroupWrapper { get; set; }
        public ListBoxWrapper<Item> ItemWrapper { get; set; }

        public MainGroup? SelectedMainGroup { get; set; }
        public string? SelectedMainGroupId { get; set; }

        public List<Item> SelectedItems { get; set; } = [];


        public ItemTemplateManagerForm? AddItemForm { get; set; }

        public int CurrentGARowScrollIndex { get; set; }

        public string? CurrentProjectFile
        {
            get => currentProjectFile; set
            {
                currentProjectFile = value;
                SetTitle();
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

        private void SetTitle(bool dirty = false)
        {
            if (string.IsNullOrEmpty(CurrentProjectFile))
            {
                Text = "Neues Projekt";
            }
            else
            {
                Text = new FileInfo(CurrentProjectFile).Name;
            }

            if (dirty) Text += "*";
        }


        private void UpdateUI()
        {
            MainGroupWrapper.Update();
            MainGroupsListBox_SelectedIndexChanged(null, null);

            ItemWrapper.Update();
            ItemsListBox_SelectedIndexChanged(null, null);
            GADataTable.UpdateTable();
        }


        #region Open Save Export Import

        private bool HandleProjectChanged()
        {
            if (!ProjectDirty) return true;

            var res2 = MessageBox.Show("Änderungen am aktuellen Projekt speichern?", "Projekt geändert", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (res2 == DialogResult.Cancel) return false;
            if (res2 == DialogResult.Yes) return Save();
            return true;
        }

        private void NewProjectMenuItem_Click(object sender, EventArgs e)
        {
            if (!HandleProjectChanged()) return;

            SetProjectFile(new Project());
        }

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

            var toolStripItems = RecentFiles.Select(r => new ToolStripMenuItem(
                    r.Name,
                    null,
                    (sender, e) =>
                    {
                        if (!HandleProjectChanged()) return;

                        ReadProjectFile(r.FullName);
                    }
                    )).ToArray();

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

        private void ReadProjectFile(string path)
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

            var obj = Project.FromJson(json);

            if (obj != null)
            {
                //CurrentProjectFile = path;
                SetProjectFile(obj, path);
                EnqueueRecentFiles(path);
            }
        }

        private bool Save()
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
                else
                {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(CurrentProjectFile))
            {
                using StreamWriter outputFile = new StreamWriter(CurrentProjectFile, false);
                var json = Project.GetJson();
                outputFile.Write(Project.GetJson());

                EnqueueRecentFiles(CurrentProjectFile);
                ProjectDirty = false;

                return true;

            }
            return false;
        }

        private void OpenProjectFile()
        {

            if (!HandleProjectChanged()) return;


            var openDialog = new OpenFileDialog();
            openDialog.Filter = "Project files (*.gaproj)|*.gaproj";
            openDialog.RestoreDirectory = true;

            var res = openDialog.ShowDialog();

            if (res == DialogResult.OK)
            {
                ReadProjectFile(openDialog.FileName);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void OpenSampleProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HandleProjectChanged()) return;

            SetProjectFile(Project.GetSampleProject());
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProjectFile();
        }

        private void SetProjectFile(Project project, string? path = null)
        {
            CurrentProjectFile = path;
            Project = project;
            AddItemForm = null;
            UpdateUI();
        }


        #endregion

        #region MainGroup

        private void AddMainGroup()
        {
            var addMainGroupForm = new AddEditMainGroupForm(Project.MainGroups);
            var result = addMainGroupForm.ShowDialog();

            if (result != DialogResult.OK || addMainGroupForm.MainGroup == null) return;

            //Project.MainGroups.Add(addMainGroupForm.MainGroup);
            Project.AddMainGroup(addMainGroupForm.MainGroup);
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
                //Project.MainGroups.Remove(SelectedMainGroup);
                Project.RemoveMainGroup(SelectedMainGroup);
            }

            UpdateUI();
        }

        private void MainGroupsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedMainGroup == (MainGroup?)MainGroupsListBox.SelectedItem) return;

            SelectedMainGroup = (MainGroup?)MainGroupsListBox.SelectedItem;
            SelectedMainGroupId = (string?)MainGroupsListBox.SelectedValue;

            ItemWrapper.Update();
            GADataTable.SetTopLevelCollection(SelectedMainGroup);
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
                        var minGA = itemGAs.MinBy(x => x.Address.GA);
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
                var gas = SelectedMainGroup.GAs.Where(x => x.Address.MiddleGroup == col.MiddleGroup && x.Address.GA >= col.GA);
                foreach (var g in gas)
                {
                    g.Address.GA += numRows;
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
                var gasMoveUp = SelectedMainGroup.GAs.Where(x => x.Address.MiddleGroup == add.MiddleGroup && x.Address.GA > add.GA);
                gasMoveUp.ToList().ForEach(x => x.Address.GA--);
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


        private void UnselectItemsButton_Click(object sender, EventArgs e)
        {
            ItemsListBox.ClearSelected();
        }

        private void ItemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newSelectedItems = ItemsListBox.SelectedItems.Cast<Item>().ToList();

            if (SelectedItems.All(newSelectedItems.Contains) && SelectedItems.Count == newSelectedItems.Count) return;

            SelectedItems = newSelectedItems;

            SetGaItemFilter();
        }

        private void SetGaItemFilter()
        {
            GADataTable.FilterByItem(SelectedItems);

            if(SelectedItems != null && SelectedItems.Count > 0)
                GADataTableBackPanel.BackColor = Color.Red;
            else
                GADataTableBackPanel.BackColor = Color.Transparent;


        }

        #region Items ListBox

        private void NewItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemManagerToolStripMenuItem_Click(sender, e);
        }

        private void EditItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = (Item?)ItemsListBox.SelectedItem;
            if (selectedItem == null) return;


            var diag = new TextBoxDialog("Item", ((Item)ItemsListBox.SelectedItem).Name);
            var res = diag.ShowDialog();

            if (res == DialogResult.OK)
            {
                ((Item)ItemsListBox.SelectedItem).Name = diag.Content;
                UpdateUI();
            }


        }

        private void DeleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedMainGroup == null) return;
            if (SelectedItems == null || SelectedItems.Count == 0) return;

            var item = SelectedItems.FirstOrDefault();
            if (item == null) return;

            //var itemGAs = SelectedMainGroup.GAs.Where(ga => SelectedItems.Select(x => x.Id).Contains(ga.ItemId)).ToList();
            var itemGAs = SelectedMainGroup.GAs.Where(ga => ga.ItemId==item.Id).ToList();

            if (itemGAs.Count == 0)
            {
                var res = MessageBox.Show("Möchten Sie das Item wirklich löschen?", "Item löschen", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (res != DialogResult.OK) return;
            } 
            else
            {
                var delItemDiag = new DeleteItemDialog(itemGAs);
                var res = delItemDiag.ShowDialog();

                if (res != DialogResult.OK) return;

                if(delItemDiag.IncludeGAs)
                {
                    itemGAs.ForEach(SelectedMainGroup.RemoveGA);
                }
            }

            SelectedMainGroup.GAs.Where(ga => SelectedItems.Select(x => x.Id).Contains(ga.ItemId)).ToList().ForEach(x => x.ItemId = null);
            Project.RemoveItem(item);

            UpdateUI();

        }
        private void ItemsListBox_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                int index = this.ItemsListBox.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    ItemsListBox.ClearSelected();
                    ItemsListBox.SelectedIndex = index;
                }
            }
        }

        #endregion

    }
}