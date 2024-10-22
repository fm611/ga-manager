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

        private enum IdValidState
        {
            Invalid,
            ValidNew,
            ValidExisting
        }


        private enum GroupType
        {
            MainGroup,
            SubGroup,
            GA
        }

        public AppDbContext Db { get; set; }


        public List<MainGroup> MainGroups { get; set; }


        public ListBoxWrapper<MainGroup> MainGroupWrapper { get; set; }

        public MainGroup? SelectedMainGroup { get; set; }
        public string? SelectedMainGroupId { get; set; }

        public AddItemForm AddItemForm { get; set; }

        public int CurrentGARowScrollIndex { get; set; }






        public MainForm()
        {
            InitializeComponent();


            //Comparison<Group> groupComparison = (a, b) => a.AddressName.CompareTo(b.AddressName);

            //MainGroups = [];

            //MainGroupWrapper = new ListBoxWrapper<MainGroup>(
            //    MainGroupsListBox,
            //    groupComparison,
            //    nameof(MainGroup.ListBoxString),
            //    "Id",
            //    () => MainGroups);

            InitDatabase();
        }

        private void InitDatabase()
        {
            Db = new AppDbContext();
            Db.Database.Migrate();
            Db.InitData();


            //MainGroups = Db.MainGroups
            //    .Include(x => x.SubGroups)
            //    .ThenInclude(x => x.GAs)
            //    .Include(x => x.Items)
            //    .ToList();

            //ItemTemplates = Db.ItemTemplates
            //    .Include(x => x.GATemplates)
            //    .ThenInclude(x => x.GAParts)
            //    .ToList();
                

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitDatabase();
            UpdataUI();
        }



        private void Save()
        {
            var toAdd = MainGroups.Where(x => !Db.MainGroups.Contains(x)).ToList();
            Db.MainGroups.AddRange(toAdd);

            Db.SaveChanges();
        }

        private void UpdataUI()
        {
            MainGroupWrapper.Update();
            GADataTable.FirstDisplayedScrollingRowIndex = CurrentGARowScrollIndex;
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


        private void MainGroupsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {


            SelectedMainGroup = (MainGroup?)MainGroupsListBox.SelectedItem;
            SelectedMainGroupId = (string?)MainGroupsListBox.SelectedValue;

            GADataTable.SetMainGroup(SelectedMainGroup);

            //FillGADataTable();
        }

        private void AddMainGroupNameTextBox_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() => (sender as TextBox).SelectAll()));
        }
        private void AddMainGroupIdTextBox_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() => (sender as TextBox).SelectAll()));
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
                AddItemForm = new AddItemForm(MainGroups);

            AddItemForm.LoadData();

            AddItemForm.SelectMainGroup(SelectedMainGroup?.Id);

            AddItemForm.ShowDialog();

            if (AddItemForm.DialogResult == DialogResult.OK)
            {
                UpdataUI();

                MainGroupsListBox.SelectedValue = AddItemForm.SelectedMainGroup?.Id;
                //GADataTable.FirstDisplayedScrollingRowIndex = AddItemForm.LastInsertedItem.MinGaAddress;

            }
        }


        private void AddRowButton_Click(object sender, EventArgs e)
        {
            if (SelectedMainGroup == null) return;
            if (!int.TryParse(AddRowNumTextBox.Text, out var numRows)) return;

            var selectedCells = GADataTable
                .SelectedCells
                .Cast<DataGridViewCell>()
                .Select(x => new { Row = x.RowIndex, Column = x.ColumnIndex })
                .ToList();


            var colMinRow = selectedCells
                .GroupBy(x => x.Column)
                .Select(x => new { SubGroupAddress = x.Key, MinRow = x.Min(y => y.Row) }).ToList();

            foreach (var col in colMinRow)
            {
                var subGroup = SelectedMainGroup.SubGroups.FirstOrDefault(x => x.SubAddress == col.SubGroupAddress);
                if (subGroup == null) continue;

                var gas = subGroup.GAs.Where(x => x.SubAddress >= col.MinRow);

                foreach (var g in gas)
                {
                    g.SubAddress += numRows;
                }
            }

            UpdataUI();

            GADataTable.ClearSelection();

            foreach (var cell in colMinRow)
            {
                GADataTable.Rows[cell.MinRow].Cells[cell.SubGroupAddress].Selected = true;
            }
        }

        private void AddRowNumTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void DeleteCellsButton_Click(object sender, EventArgs e)
        {
            //if (SelectedMainGroup == null) return;

            //var selectedCells = GADataTable
            //    .SelectedCells
            //    .Cast<DataGridViewCell>()
            //    .Select(x => new { SubGroupAddress = x.ColumnIndex, GAAddress = x.RowIndex }).ToList();

            //var gasDelete = SelectedMainGroup
            //    .GAs
            //    .Where(x => selectedCells
            //        .Contains(new { SubGroupAddress = x.SubGroup.SubAddress, GAAddress = x.SubAddress }))
            //    .GroupBy(x => x.SubGroup).ToList();
            //if (gasDelete.Count > 0)
            //{
            //    var res = MessageBox.Show("Gruppenadressen löschen?", "Gruppenadressen löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //    if (res == DialogResult.Yes)
            //    {
            //        gasDelete.ForEach(x =>
            //            x.ToList().ForEach(ga =>
            //                SelectedMainGroup
            //                    .SubGroups
            //                    .First(sub => sub.Id == x.Key.Id).GAs.Remove(ga)));
            //    }
            //}

            //foreach (var cell in selectedCells)
            //{
            //    var subGroup = SelectedMainGroup.SubGroups.FirstOrDefault(x => x.SubAddress == cell.SubGroupAddress);
            //    if (subGroup == null) continue;

            //    var gasMoveUp = subGroup.GAs.Where(x => x.SubAddress > cell.GAAddress);

            //    gasMoveUp.ToList().ForEach(x => x.SubAddress--);
            //}

            //UpdataUI();


            //GADataTable.ClearSelection();

            //foreach (var cell in selectedCells)
            //{
            //    GADataTable.Rows[cell.GAAddress].Cells[cell.SubGroupAddress].Selected = true;
            //}

        }


        private void DeleteMainGroupButton_Click(object sender, EventArgs e)
        {
            if (SelectedMainGroup == null) return;

            if (MessageBox.Show("Haupgruppe löschen?", "Hauptgruppe löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                MainGroups.Remove(SelectedMainGroup);
            }

            UpdataUI();
        }

        private void AddMainGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMainGroup();
        }

        private void EditMainGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(SelectedMainGroup == null) return;
            EditMainGroup(SelectedMainGroup.Id);
        }
    }
}
