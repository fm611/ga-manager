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

        public ListBoxWrapper<MainGroup> MainGroupWrapper { get; set; }

        public MainGroup? SelectedMainGroup { get; set; }
        public string? SelectedMainGroupId { get; set; }

        public AddItemForm AddItemForm { get; set; }

        public int CurrentGARowScrollIndex { get; set; }



        public MainForm()
        {
            InitializeComponent();

            Db = new AppDbContext();
            Db.Database.Migrate();
            Db.InitData();

            Comparison<Group> groupComparison = (a, b) => a.AddressName.CompareTo(b.AddressName);

            MainGroupWrapper = new ListBoxWrapper<MainGroup>(
                MainGroupsListBox,
                groupComparison,
                nameof(MainGroup.ListBoxString),
                "Id",
                () => Db.MainGroups
                .Include(x => x.SubGroups)
                .ThenInclude(x => x.GAs)
                .Include(x => x.Items)
                .ToList()
                .Where(x => Db.Entry(x).State != EntityState.Deleted));

            AddMainGroupIdTextBox_TextChanged(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDatabase();
        }



        private void Save()
        {
            var toAdd = MainGroupWrapper.BindingList.Where(x => !Db.MainGroups.Contains(x)).ToList();
            Db.MainGroups.AddRange(toAdd);

            Db.SaveChanges();
        }

        private void LoadDatabase()
        {
            MainGroupWrapper.Load();

            GADataTable.FirstDisplayedScrollingRowIndex = CurrentGARowScrollIndex;
        }


        #region MainGroup

        private void AddMainGroupButton_Click(object sender, EventArgs e)
        {
            var (state, addresse) = ValidateSubAddress(GroupType.MainGroup);
            if (state != IdValidState.ValidNew)
            {
                StatusInfoLabel.Text = "Id ungültig";
                return;
            }

            if (!int.TryParse(AddMainGroupDefaultBlockLengthTextBox.Text, out var defaultBlockLength))
            {
                StatusInfoLabel.Text = "Blocklänge ungültig";
                return;
            }


            var newMainGroup = new MainGroup(addresse, AddMainGroupNameTextBox.Text, defaultBlockLength);
            MainGroupWrapper.BindingList.Add(newMainGroup);

            MainGroupsListBox.SelectedValue = newMainGroup.Id;

            MainGroupWrapper.SortAndReset();

            AddMainGroupIdTextBox_TextChanged(null, null);

            AddMainGroupIdTextBox.Focus();
        }
        private void MainGroupsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {


            SelectedMainGroup = (MainGroup?)MainGroupsListBox.SelectedItem;
            SelectedMainGroupId = (string?)MainGroupsListBox.SelectedValue;

            if (SelectedMainGroup == null) return;

            AddMainGroupIdTextBox.Text = SelectedMainGroup.SubAddress.ToString();
            AddMainGroupNameTextBox.Text = SelectedMainGroup.Name;
            AddMainGroupDefaultBlockLengthTextBox.Text = SelectedMainGroup.DefaultBlockLength.ToString();


            GADataTable.SetMainGroup(SelectedMainGroup);

            //FillGADataTable();
        }

        private void AddMainGroupIdTextBox_TextChanged(object sender, EventArgs e)
        {
            var (state, addresse) = ValidateSubAddress(GroupType.MainGroup);

            switch (state)
            {
                case IdValidState.Invalid:
                    AddMainGroupIdTextBox.BackColor = Color.Red;
                    AddMainGroupButton.Enabled = false;
                    EditMainGroupButton.Enabled = false;
                    break;
                case IdValidState.ValidExisting:
                    AddMainGroupIdTextBox.BackColor = Color.Yellow;
                    AddMainGroupButton.Enabled = false;
                    EditMainGroupButton.Enabled = ((MainGroup)MainGroupsListBox.SelectedItem).SubAddress == addresse;
                    break;
                case IdValidState.ValidNew:
                    AddMainGroupIdTextBox.BackColor = Color.White;
                    AddMainGroupButton.Enabled = true;
                    EditMainGroupButton.Enabled = MainGroupWrapper.BindingList.Any(x => x.SubAddress == ((MainGroup)MainGroupsListBox.SelectedItem).SubAddress);
                    break;
            }
            MainGroupWrapper.SortAndReset();
        }
        private void AddMainGroupNameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Return)
            {
                AddMainGroupButton_Click(null, null);
            }
        }
        private void EditMainGroupButton_Click(object sender, EventArgs e)
        {
            var (state, addresse) = ValidateSubAddress(GroupType.MainGroup);
            if (state != IdValidState.ValidExisting)
            {
                StatusInfoLabel.Text = "Id ungültig";
                return;
            }

            if (!int.TryParse(AddMainGroupDefaultBlockLengthTextBox.Text, out var defaultBlockLength))
            {
                StatusInfoLabel.Text = "Blocklänge ungültig";
                return;
            }

            var mGroup = MainGroupWrapper.BindingList.FirstOrDefault(g => g.Id == (string?)MainGroupsListBox.SelectedValue);

            if (mGroup == null)
            {
                StatusInfoLabel.Text = "No MainGroup found.";
                return;
            }

            mGroup.SubAddress = addresse;
            mGroup.Name = AddMainGroupNameTextBox.Text;
            mGroup.DefaultBlockLength = defaultBlockLength;


            MainGroupWrapper.SortAndReset();
            MainGroupsListBox.SelectedValue = mGroup.Id;

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


        private (IdValidState, int) ValidateSubAddress(GroupType type)
        {

            switch (type)
            {
                case GroupType.MainGroup:
                    if (!Int32.TryParse(AddMainGroupIdTextBox.Text, out var id)) return (IdValidState.Invalid, -1);
                    if (id < 0 || id >= 32) return (IdValidState.Invalid, -1);
                    return (MainGroupWrapper.BindingList.Any(x => x.SubAddress == id) ? IdValidState.ValidExisting : IdValidState.ValidNew, id);
            }

            return (IdValidState.Invalid, -1);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Db = new AppDbContext();
            Db.Database.Migrate();
            Db.InitData();

            LoadDatabase();
        }


        private void AddItemButton_Click(object sender, EventArgs e)
        {
            if (AddItemForm == null)
                AddItemForm = new AddItemForm(Db);

            AddItemForm.LoadData();

            AddItemForm.SelectMainGroup(SelectedMainGroup?.Id);

            AddItemForm.ShowDialog();

            if (AddItemForm.DialogResult == DialogResult.OK)
            {
                LoadDatabase();

                MainGroupsListBox.SelectedValue = AddItemForm.SelectedMainGroup?.Id;
                GADataTable.FirstDisplayedScrollingRowIndex = AddItemForm.LastInsertedItem.MinGaAddress;

            }
        }

        private void GADataTable_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (SelectedMainGroup == null) return;

            var currCell = ((DataGridView)sender).CurrentCell;

            if (!string.IsNullOrEmpty(currCell.Value as string))
            {
                var ga = SelectedMainGroup
                            .SubGroups
                            .FirstOrDefault(x => x.SubAddress == currCell.ColumnIndex)?
                            .GAs
                            .FirstOrDefault(x => x.SubAddress == currCell.RowIndex);

                if (ga == null) return;
                currCell.Value = ga.Name;
            }
        }

        private void GADataTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (SelectedMainGroup == null) return;

            var currCell = ((DataGridView)sender).CurrentCell;

            if (!string.IsNullOrEmpty(currCell.Value as string))
            {
                var subGroupAddress = e.ColumnIndex;

                var subGroup = SelectedMainGroup.SubGroups.FirstOrDefault(x => x.SubAddress == subGroupAddress);

                if (subGroup == null)
                {
                    var editSubGroupForm = new EditSubGroupForm("Neue Mittelgruppe");
                    editSubGroupForm.ShowDialog();

                    if (editSubGroupForm.DialogResult == DialogResult.OK)
                    {
                        subGroup = SubGroup.Create(subGroupAddress, editSubGroupForm.SubGroupName, SelectedMainGroup);
                    }
                    else
                    {
                        return;
                    }
                }

                var ga = subGroup
                            .GAs
                            .FirstOrDefault(x => x.SubAddress == currCell.RowIndex);

                // new GA
                if (ga == null)
                    new GA(subGroup, currCell.RowIndex, (string)currCell.Value);
                else
                    ga.Name = (string)currCell.Value;
            }

            this.BeginInvoke(new MethodInvoker(() =>
            {
                LoadDatabase();
            }));

        }


        private void GADataTable_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            ((DataGridView)sender).Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;

        }

        private void GADataTable_KeyUp(object sender, KeyEventArgs e)
        {
            if (SelectedMainGroup == null) return;

            if (e.KeyValue == (char)Keys.Delete)
            {
                var selectedCells = GADataTable
                    .SelectedCells
                    .Cast<DataGridViewCell>()
                    .Select(x => new { SubGroupAddress = x.ColumnIndex, GAAddress = x.RowIndex });

                var gas = SelectedMainGroup
                    .GAs
                    .Where(x => selectedCells
                        .Contains(new { SubGroupAddress = x.SubGroup.SubAddress, GAAddress = x.SubAddress }))
                    .GroupBy(x => x.SubGroup).ToList();

                if (gas.Count == 0) return;

                var res = MessageBox.Show("Gruppenadressen löschen?", "Gruppenadressen löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    gas.ForEach(x =>
                        x.ToList().ForEach(ga =>
                            SelectedMainGroup
                                .SubGroups
                                .First(sub => sub.Id == x.Key.Id).GAs.Remove(ga)));

                    LoadDatabase();
                }
            }

        }

        private void GADataTable_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                CurrentGARowScrollIndex = e.NewValue;
            }
        }

        private void GADataTable_KeyDown(object sender, KeyEventArgs e)
        {
            if (SelectedMainGroup == null) return;

            var currCell = GADataTable.CurrentCell;
            if (currCell == null) return;

            if (e.KeyData == (Keys.Control | Keys.C) && GADataTable.SelectedCells.Count == 1)
            {
                var subGroup = SelectedMainGroup
                    .SubGroups
                    .FirstOrDefault(x => x.SubAddress == currCell.ColumnIndex);

                if (subGroup == null) return;

                var ga = subGroup
                    .GAs
                    .FirstOrDefault(x => x.SubAddress == currCell.RowIndex);

                if (ga == null) return;
                Clipboard.SetText(ga.Name);

                e.Handled = true;
            }

            if (e.KeyData == (Keys.Control | Keys.V) && GADataTable.SelectedCells.Count == 1)
            {
                currCell.Value = Clipboard.GetText();

                GADataTable_CellEndEdit(GADataTable, new DataGridViewCellEventArgs(currCell.ColumnIndex, currCell.RowIndex));


                e.Handled = true;
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

            LoadDatabase();

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
            if (SelectedMainGroup == null) return;

            var selectedCells = GADataTable
                .SelectedCells
                .Cast<DataGridViewCell>()
                .Select(x => new { SubGroupAddress = x.ColumnIndex, GAAddress = x.RowIndex }).ToList();

            var gasDelete = SelectedMainGroup
                .GAs
                .Where(x => selectedCells
                    .Contains(new { SubGroupAddress = x.SubGroup.SubAddress, GAAddress = x.SubAddress }))
                .GroupBy(x => x.SubGroup).ToList();
            if (gasDelete.Count > 0)
            {
                var res = MessageBox.Show("Gruppenadressen löschen?", "Gruppenadressen löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    gasDelete.ForEach(x =>
                        x.ToList().ForEach(ga =>
                            SelectedMainGroup
                                .SubGroups
                                .First(sub => sub.Id == x.Key.Id).GAs.Remove(ga)));
                }
            }

            foreach (var cell in selectedCells)
            {
                var subGroup = SelectedMainGroup.SubGroups.FirstOrDefault(x => x.SubAddress == cell.SubGroupAddress);
                if (subGroup == null) continue;

                var gasMoveUp = subGroup.GAs.Where(x => x.SubAddress > cell.GAAddress);

                gasMoveUp.ToList().ForEach(x => x.SubAddress--);
            }

            LoadDatabase();


            GADataTable.ClearSelection();

            foreach (var cell in selectedCells)
            {
                GADataTable.Rows[cell.GAAddress].Cells[cell.SubGroupAddress].Selected = true;
            }

        }


        private void DeleteMainGroupButton_Click(object sender, EventArgs e)
        {
            if(SelectedMainGroup == null) return;

            if (MessageBox.Show("Haupgruppe löschen?", "Hauptgruppe löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Db.MainGroups.Remove(SelectedMainGroup);
                //Db.SaveChanges();
            }

            LoadDatabase();
        }
    }
}
