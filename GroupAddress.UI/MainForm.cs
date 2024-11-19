using GroupAddress.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace GroupAddress.UI
{
    public partial class MainForm : Form
    {

        //public AppDbContext Db { get; set; }

        public Project Project { get; set; }

        //public List<MainGroup> MainGroups { get; set; }

        //public List<ItemTemplate> ItemTemplates { get; set; }

        public ListBoxWrapper<MainGroup> MainGroupWrapper { get; set; }

        public MainGroup? SelectedMainGroup { get; set; }
        public string? SelectedMainGroupId { get; set; }

        public ItemTemplateManagerForm AddItemForm { get; set; }

        public int CurrentGARowScrollIndex { get; set; }






        public MainForm()
        {
            InitializeComponent();

            Comparison<MainGroup> mainGroupComparison = (a, b) => a.SubAddress.CompareTo(b.SubAddress);

            Project = new Project();

            MainGroupWrapper = new ListBoxWrapper<MainGroup>(
                MainGroupsListBox,
                mainGroupComparison,
                nameof(MainGroup.ListBoxString),
                "Id",
                () => Project.MainGroups);

            //InitDatabase();
        }

        //private void InitDatabase()
        //{
        //    Db = new AppDbContext();
        //    Db.Database.Migrate();
        //    Db.InitData();


        //    MainGroups = Db.MainGroups
        //        .Include(x => x.GAs)
        //        .Include(x => x.Items)
        //        .ToList();

        //    ItemTemplates = Db.ItemTemplates
        //        .Include(x => x.GAs)
        //        .ToList();
        //}

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    //InitDatabase();
        //    UpdataUI();
        //}


        //private void Save()
        //{
        //    var mgToAdd = MainGroups.Where(x => !Db.MainGroups.Contains(x)).ToList();
        //    Db.MainGroups.AddRange(mgToAdd);
        //    var mgToDelete = Db.MainGroups.Where(x => !MainGroups.Contains(x)).ToList();
        //    Db.MainGroups.RemoveRange(mgToDelete);

        //    var tempToAdd = ItemTemplates.Where(x => !Db.ItemTemplates.Contains(x)).ToList();
        //    Db.ItemTemplates.AddRange(tempToAdd);
        //    var tempToDelete = Db.ItemTemplates.Where(x => !ItemTemplates.Contains(x)).ToList();
        //    Db.ItemTemplates.RemoveRange(tempToDelete);

        //    Db.SaveChanges();
        //}

        private void UpdateUI()
        {
            MainGroupWrapper.Update();
            GADataTable.UpdateTable();
        }


        #region Open Save Export Import


        private void OpenSampleProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project = Project.GetSampleProject();
            UpdateUI();
        }

        private void ItemManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AddItemForm == null)
                AddItemForm = new ItemTemplateManagerForm(Project);

            AddItemForm.LoadData();
            AddItemForm.SelectMainGroup(SelectedMainGroup?.Id);

            AddItemForm.ShowDialog();

            if (AddItemForm.DialogResult == DialogResult.OK)
            {
                UpdateUI();

                if(AddItemForm.SelectedMainGroup != null)
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

            GADataTable.SetTopLevelCollection(SelectedMainGroup);
        }

        #endregion



        private void AddCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedMainGroup == null) return;
            if (!int.TryParse(AddCellsNumTextBox.Text, out var numRows)) return;


            var colMinRows = GADataTable.SelectedAddresses
                .GroupBy(x => x.MiddleGroup)
                .Select(g => g.MinBy(y => y.GA));


            foreach (var col in colMinRows)
            {
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


    }
}
