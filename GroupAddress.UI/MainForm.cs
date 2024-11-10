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

        public AppDbContext Db { get; set; }

        public List<MainGroup> MainGroups { get; set; }

        public List<ItemTemplate> ItemTemplates { get; set; }

        public ListBoxWrapper<MainGroup> MainGroupWrapper { get; set; }

        public MainGroup? SelectedMainGroup { get; set; }
        public string? SelectedMainGroupId { get; set; }

        public ItemTemplateManagerForm AddItemForm { get; set; }

        public int CurrentGARowScrollIndex { get; set; }






        public MainForm()
        {
            InitializeComponent();


            Comparison<MainGroup> mainGroupComparison = (a, b) => a.SubAddress.CompareTo(b.SubAddress);

            MainGroups = [];

            MainGroupWrapper = new ListBoxWrapper<MainGroup>(
                MainGroupsListBox,
                mainGroupComparison,
                nameof(MainGroup.ListBoxString),
                "Id",
                () => MainGroups);

            InitDatabase();
        }

        private void InitDatabase()
        {
            Db = new AppDbContext();
            Db.Database.Migrate();
            Db.InitData();


            MainGroups = Db.MainGroups
                .Include(x => x.GAs)
                .Include(x => x.Items)
                .ToList();

            ItemTemplates = Db.ItemTemplates
                .Include(x => x.GAs)
                .ToList();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitDatabase();
            UpdataUI();
        }


        private void Save()
        {
            var mgToAdd = MainGroups.Where(x => !Db.MainGroups.Contains(x)).ToList();
            Db.MainGroups.AddRange(mgToAdd);
            var mgToDelete = Db.MainGroups.Where(x => !MainGroups.Contains(x)).ToList();
            Db.MainGroups.RemoveRange(mgToDelete);

            var tempToAdd = ItemTemplates.Where(x => !Db.ItemTemplates.Contains(x)).ToList();
            Db.ItemTemplates.AddRange(tempToAdd);
            var tempToDelete = Db.ItemTemplates.Where(x => !ItemTemplates.Contains(x)).ToList();
            Db.ItemTemplates.RemoveRange(tempToDelete);

            Db.SaveChanges();
        }

        private void UpdataUI()
        {
            MainGroupWrapper.Update();
            GADataTable.UpdateTable();
        }


        #region MainGroup

        private void AddMainGroup()
        {
            var addMainGroupForm = new AddEditMainGroupForm(MainGroups);
            var result = addMainGroupForm.ShowDialog();

            if (result != DialogResult.OK || addMainGroupForm.MainGroup == null) return;

            MainGroups.Add(addMainGroupForm.MainGroup);
            MainGroupWrapper.Update();
        }
        private void EditMainGroup(string id)
        {
            var addMainGroupForm = new AddEditMainGroupForm(MainGroups, id);
            var result = addMainGroupForm.ShowDialog();

            if (result != DialogResult.OK || addMainGroupForm.MainGroup == null) return;

            MainGroupWrapper.Update();
        }
        private void DeleteMainGroup(string id)
        {
            if (SelectedMainGroup == null) return;

            if (MessageBox.Show("Haupgruppe löschen?", "Hauptgruppe löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                MainGroups.Remove(SelectedMainGroup);
            }

            UpdataUI();
        }


        private void MainGroupsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedMainGroup = (MainGroup?)MainGroupsListBox.SelectedItem;
            SelectedMainGroupId = (string?)MainGroupsListBox.SelectedValue;

            GADataTable.SetTopLevelCollection(SelectedMainGroup);
        }

        #endregion


        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Db = new AppDbContext();
            Db.Database.Migrate();
            Db.InitData();

            UpdataUI();
        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {
            if (AddItemForm == null)
                AddItemForm = new ItemTemplateManagerForm(MainGroups, ItemTemplates);

            AddItemForm.LoadData();
            AddItemForm.SelectMainGroup(SelectedMainGroup?.Id);

            AddItemForm.ShowDialog();

            if (AddItemForm.DialogResult == DialogResult.OK)
            {
                UpdataUI();

                MainGroupsListBox.SelectedValue = AddItemForm.SelectedMainGroup?.Id;
                GADataTable.FirstDisplayedScrollingRowIndex = AddItemForm.LastInsertedItem?.MinGASubAddress ?? 0;

            }
        }

        private void AddRowButton_Click(object sender, EventArgs e)
        {
            if (SelectedMainGroup == null) return;
            if (!int.TryParse(AddRowNumTextBox.Text, out var numRows)) return;


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

        private void AddRowNumTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete) e.Handled = true;
        }

        private void DeleteCellsButton_Click(object sender, EventArgs e)
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
