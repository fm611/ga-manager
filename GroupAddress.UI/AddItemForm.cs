using GroupAddress.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupAddress.UI
{
    public partial class AddItemForm : Form
    {

        public AppDbContext Db { get; set; }
        public ListBoxWrapper<ItemTemplate> ItemTemplatesWrapper { get; set; }
        public ListBoxWrapper<MainGroup> MainGroupsWrapper { get; set; }

        public ItemTemplate? SelectedItemTemplate { get; set; }
        public string? SelectedItemTemplateId { get; set; }
        public MainGroup? SelectedMainGroup { get; set; }
        public string? SelectedMainGroupId { get; set; }

        public AddItemForm(AppDbContext db)
        {

            InitializeComponent();

            Db = db;

            ItemTemplatesWrapper = new ListBoxWrapper<ItemTemplate>(ItemTemplatesListBox, (a, b) => a.Name.CompareTo(b.Name), "Name", "Id");
            ItemTemplatesWrapper.Load(Db.ItemTemplates);

            if (ItemTemplatesListBox.Items.Count > 0)
                ItemTemplatesListBox.SelectedIndex = 0;


            MainGroupsWrapper = new ListBoxWrapper<MainGroup>(MainGroupsListBox, (a, b) => a.AddressName.CompareTo(b.AddressName), "AddressName", "Id");
            MainGroupsWrapper.Load(Db.MainGroups);

            if (MainGroupsListBox.Items.Count > 0)
                MainGroupsListBox.SelectedIndex = 0;

        }
        private void AddItemForm_Load(object sender, EventArgs e)
        {
            GADataTable.AutoResizeColumns();
        }

        public void SelectMainGroup(string? id)
        {
            MainGroupsListBox.SelectedValue = id;
        }

        private void ItemTemplatesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedItemTemplate = (ItemTemplate?)ItemTemplatesListBox.SelectedItem;
            SelectedItemTemplateId = (string?)ItemTemplatesListBox.SelectedValue;


            var table = new DataTable();

            if (SelectedItemTemplate != null)
            {
                var subGroups = Db.GATemplates
                    .Where(x => x.ItemTemplateId == (string?)ItemTemplatesListBox.SelectedValue)
                    .SelectMany(x => x.GAParts).Select(x => x.SubGroupTemplate)
                    .GroupBy(x => x.SubAddress)
                    .Select(x => new { SubAddress = x.Key, Name = x.First().Name });


                var gatemps = Db.GATemplates
                    .Where(x => x.ItemTemplateId == (string?)ItemTemplatesListBox.SelectedValue)
                    .Include(x => x.GAParts)
                    .ThenInclude(x => x.SubGroupTemplate)
                    .ToList()
                    .SelectMany(x => x.GAParts.Select(y => new
                    {
                        SubGroupAddress = y.SubGroupTemplate.SubAddress,
                        GAAddress = x.SubAddress,
                        Name = "x/" + y.SubGroupTemplate.SubAddress + "/" + x.SubAddress + " - " + string.Join("_", new[] { x.BaseString, y.AddonString }.Where(s => !string.IsNullOrEmpty(s)))
                    })).ToList();

                var cols = Enumerable
                    .Range(0, 8)
                    .Select(x =>
                        new DataColumn(x + " - " + subGroups.FirstOrDefault(y => y.SubAddress == x)?.Name))
                    .ToArray();

                table.Columns.Add(new DataColumn("#"));
                table.Columns.AddRange(cols);




                for (int i = 0; i <= gatemps.Max(x => x.GAAddress); i++)
                {
                    var newRow = table.NewRow();
                    newRow[0] = i;

                    for (int j = 0; j < 8; j++)
                    {
                        newRow[j + 1] = gatemps
                            .Where(x => x.GAAddress == i)
                            .Where(x => x.SubGroupAddress == j)
                            .Select(x => x.Name).FirstOrDefault();

                    }
                    table.Rows.Add(newRow);
                }
            }

            GADataTable.DataSource = table;

            if (GADataTable.Columns.Count > 0)
            {
                GADataTable.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GADataTable.Columns[0].DefaultCellStyle.BackColor = Color.LightGray;
            }
            GADataTable.AutoResizeColumns();

        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {
            if (SelectedMainGroup == null || SelectedItemTemplate == null)
            {
                DialogResult = DialogResult.Abort;
                return;
            }                       

            SelectedMainGroup.AddItem(SelectedItemTemplate, NewItemPreStringTextBox.Text,GetInsertIndex());
            DialogResult = DialogResult.OK;
        }

        private int GetInsertIndex()
        {
            if (SelectedMainGroup == null) return -1;

            var insertAtParseResult = int.TryParse(InsertAtTextBox.Text, out var insertAtIndex);
            var nextFreeIndex = SelectedMainGroup.MaxGASubAddress + 1;

            if (InsertAtNextBlockRadioButton.Checked) return SelectedMainGroup.GetNextStartingBlockIndex();
            if (InsertAtRadioButton.Checked) return insertAtParseResult ? insertAtIndex : nextFreeIndex;
            if (InsertNextFreeRadioButton.Checked) return nextFreeIndex;

            return nextFreeIndex;
        }

        private void MainGroupsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedMainGroup = (MainGroup?)MainGroupsListBox.SelectedItem;
            SelectedMainGroupId = (string?)MainGroupsListBox.SelectedValue;

            if (SelectedMainGroup != null)
            {
                NextBlockStartingIndexTextBox.Text = SelectedMainGroup.GetNextStartingBlockIndex().ToString();
                InsertAtTextBox.Text = SelectedMainGroup.GetNextStartingBlockIndex().ToString();
                InsertAtNextFreeTextBox.Text = (SelectedMainGroup.MaxGASubAddress + 1).ToString();
            }

        }

        private void InsertAtRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            InsertAtTextBox.ReadOnly = !InsertAtRadioButton.Checked;

            if (InsertAtRadioButton.Checked)
            {
                InsertAtTextBox.Focus();
            }
        }


        private void InsertAtTextBox_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() => (sender as TextBox).SelectAll()));
        }
    }
}
