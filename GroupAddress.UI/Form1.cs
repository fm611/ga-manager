using GroupAddress.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel;
using System.Data;
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



        public Form1()
        {
            InitializeComponent();

            Db = new AppDbContext();
            Db.Database.Migrate();
            Db.InitData();


            Comparison<Group> groupComparison = (a, b) => a.AddressName.CompareTo(b.AddressName);

            MainGroupWrapper = new ListBoxWrapper<MainGroup>(MainGroupsListBox, groupComparison, "AddressName", "Id");
            //SubGroupWrapper = new ListBoxWrapper<SubGroup>(SubGroupsListBox, groupComparison, "AddressName", "Id");
            //ItemTemplatesWrapper = new ListBoxWrapper<ItemTemplate>(ItemTemplatesListBox, (a, b) => a.Name.CompareTo(b.Name), "Name", "Id");
            //GAWrapper = new ListBoxWrapper<GA>(GAsListBox, (a, b) => a.Address.CompareTo(b.Address), "AddressName", "Id");


            AddMainGroupIdTextBox_TextChanged(null, null);
        }


        private void Save()
        {
            var toAdd = MainGroupWrapper.BindingList.Where(x => !Db.MainGroups.Contains(x)).ToList();
            Db.MainGroups.AddRange(toAdd);

            Db.SaveChanges();
        }

        private void Load()
        {

            //Main Groups

            MainGroupWrapper.Load(Db.MainGroups.Include(x => x.SubGroups).ThenInclude(x => x.GAs));

            //Item Templates

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

            var newMainGroup = new MainGroup(addresse, AddMainGroupNameTextBox.Text);
            MainGroupWrapper.BindingList.Add(newMainGroup);

            MainGroupsListBox.SelectedValue = newMainGroup.Id;

            MainGroupWrapper.SortAndReset();

            AddMainGroupIdTextBox_TextChanged(null, null);

            AddMainGroupIdTextBox.Focus();
        }
        private void MainGroupsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            SelectedMainGroup = (MainGroup?)MainGroupsListBox.SelectedItem;

            AddMainGroupIdTextBox.Text = SelectedMainGroup?.SubAddress.ToString();
            AddMainGroupNameTextBox.Text = SelectedMainGroup?.Name;


            var table = new DataTable();

            if (SelectedMainGroup != null)
            {
                var cols = Enumerable
                    .Range(0, 8)
                    .Select(x =>
                        new DataColumn(x + " - " + SelectedMainGroup.SubGroups.FirstOrDefault(y => y.SubAddress == x)?.Name))
                    .ToArray();

                //table.Columns.Add("Addr");
                table.Columns.AddRange(cols);

                for (int i = 0; i < 256; i++)
                {
                    var newRow = table.NewRow();
                    //newRow[0] = i;

                    for (int j = 0; j < 8; j++)
                    {
                        newRow[j] = SelectedMainGroup
                            .SubGroups
                            .FirstOrDefault(x => x.SubAddress == j)?
                            .GAs
                            .FirstOrDefault(x => x.SubAddress == i)?
                            .AddressName;
                    }

                    table.Rows.Add(newRow);
                }
            }

            GADataTable.DataSource = table;
            GADataTable.AutoResizeColumns();

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


            var mGroup = MainGroupWrapper.BindingList.FirstOrDefault(g => g.Id == (string)MainGroupsListBox.SelectedValue);

            if (mGroup == null)
            {
                StatusInfoLabel.Text = "No MainGroup found.";
                return;
            }

            mGroup.SubAddress = addresse;
            mGroup.Name = AddMainGroupNameTextBox.Text;


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
                    //case IdType.SubGroup:
                    //    if (id < 0 || id >= 8) return (IdValidState.Invalid, -1);
                    //    return (SubGroups.Any(x => x.Id == id) ? IdValidState.ValidExisting : IdValidState.ValidNew, id);
                    //case IdType.GA:
                    //    if (id < 0 || id >= 256) return (IdValidState.Invalid, -1);
                    //    return (GAs.Any(x => x.Id == id) ? IdValidState.ValidExisting : IdValidState.ValidNew, id);
            }

            return (IdValidState.Invalid, -1);
        }



        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {
            var addItemForm = new AddItemForm(Db);
            addItemForm.ShowDialog();
        }
    }
}
