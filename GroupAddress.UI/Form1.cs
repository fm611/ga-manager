using GroupAddress.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace GroupAddress.UI
{
    public partial class Form1 : Form
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



        public Form1()
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
                .Include(x => x.Items));

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

            AddMainGroupIdTextBox.Text = SelectedMainGroup?.SubAddress.ToString();
            AddMainGroupNameTextBox.Text = SelectedMainGroup?.Name;
            AddMainGroupDefaultBlockLengthTextBox.Text = SelectedMainGroup?.DefaultBlockLength.ToString();

            FillGADataTable();
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


        private void FillGADataTable()
        {
            var table = new DataTable();

            if (SelectedMainGroup != null)
            {
                var cols = Enumerable
                    .Range(0, 8)
                    .Select(x =>
                        new DataColumn(x + " - " + SelectedMainGroup.SubGroups.FirstOrDefault(y => y.SubAddress == x)?.Name))
                    .ToArray();


                table.Columns.Add(new DataColumn("#"));
                table.Columns.AddRange(cols);

                for (int i = 0; i < 256; i++)
                {
                    var newRow = table.NewRow();
                    newRow[0] = i;
                    for (int j = 0; j < 8; j++)
                    {
                        newRow[j + 1] = SelectedMainGroup
                            .SubGroups
                            .FirstOrDefault(x => x.SubAddress == j)?
                            .GAs
                            .FirstOrDefault(x => x.SubAddress == i)?
                            .AddressName;
                    }
                    table.Rows.Add(newRow);
                }
            }
            //GADataTable.DataSource = null;
            GADataTable.DataSource = table;
            if (GADataTable.Columns.Count > 0)
            {
                GADataTable.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GADataTable.Columns[0].DefaultCellStyle.BackColor = Color.LightGray;
            }
            GADataTable.AutoResizeColumns();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
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
                //GADataTable.FirstDisplayedScrollingRowIndex = GADataTable.RowCount - 1;
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
                            .FirstOrDefault(x => x.SubAddress == currCell.ColumnIndex - 1)?
                            .GAs
                            .FirstOrDefault(x => x.SubAddress == currCell.RowIndex);
                currCell.Value = ga.Name;
            }
        }

        private void GADataTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (SelectedMainGroup == null) return;

            var currCell = ((DataGridView)sender).CurrentCell;

            if (!string.IsNullOrEmpty(currCell.Value as string))
            {
                var subGroupAddress = e.ColumnIndex - 1;

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

        private void GADataTable_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (SelectedMainGroup == null) return;

            var subGroupAddress = e.ColumnIndex - 1;

            var subGroup = SelectedMainGroup.SubGroups.FirstOrDefault(x => x.SubAddress == subGroupAddress);

            if (subGroup == null)
            {
                var editSubGroupForm = new EditSubGroupForm("Neue Mittelgruppe");
                editSubGroupForm.ShowDialog();

                if (editSubGroupForm.DialogResult == DialogResult.OK)
                {
                    SubGroup.Create(subGroupAddress, editSubGroupForm.SubGroupName, SelectedMainGroup);
                }
            }
            else
            {
                var editSubGroupForm = new EditSubGroupForm(subGroup.Name);
                editSubGroupForm.ShowDialog();

                if (editSubGroupForm.DialogResult == DialogResult.OK)
                {
                    subGroup.Name = editSubGroupForm.SubGroupName;
                }
            }
            LoadDatabase();

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
                var res = MessageBox.Show("Gruppenadressen löschen?", "Gruppenadressen löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    var selectedCells = GADataTable
                        .SelectedCells
                        .Cast<DataGridViewCell>()
                        .Select(x => new { SubGroupAddress = x.ColumnIndex - 1, GAAddress = x.RowIndex }); ;

                    var gas = SelectedMainGroup
                        .GAs
                        .Where(x => selectedCells
                            .Contains(new { SubGroupAddress = x.SubGroup.SubAddress, GAAddress = x.SubAddress }))
                        .GroupBy(x => x.SubGroup).ToList();

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
            if(SelectedMainGroup == null) return;

            var currCell = GADataTable.CurrentCell;
            if(currCell == null) return;

            if (e.KeyData == (Keys.Control | Keys.C) && GADataTable.SelectedCells.Count == 1)
            {
                var subGroup = SelectedMainGroup
                    .SubGroups
                    .FirstOrDefault(x => x.SubAddress == currCell.ColumnIndex - 1);

                if(subGroup == null) return;

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
    }
}
