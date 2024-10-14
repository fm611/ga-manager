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

        }

        private void AddItemForm_Load(object sender, EventArgs e)
        {
            ItemTemplatesWrapper = new ListBoxWrapper<ItemTemplate>(ItemTemplatesListBox, (a, b) => a.Name.CompareTo(b.Name), "Name", "Id");
            ItemTemplatesWrapper.Load(Db.ItemTemplates);

            if (ItemTemplatesListBox.Items.Count > 0)
                ItemTemplatesListBox.SelectedIndex = 0;


            MainGroupsWrapper = new ListBoxWrapper<MainGroup>(MainGroupsListBox, (a, b) => a.AddressName.CompareTo(b.AddressName), "AddressName", "Id");
            MainGroupsWrapper.Load(Db.MainGroups);

            if (MainGroupsListBox.Items.Count > 0)
                MainGroupsListBox.SelectedIndex = 0;
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

                table.Columns.AddRange(cols);



                for (int i = 0; i < 256; i++)
                {
                    var newRow = table.NewRow();

                    for (int j = 0; j < 8; j++)
                    {
                        newRow[j] = gatemps
                            .Where(x => x.GAAddress == i)
                            .Where(x => x.SubGroupAddress == j)
                            .Select(x => x.Name).FirstOrDefault();

                    }
                    table.Rows.Add(newRow);
                }
            }

            GADataTable.DataSource = table;
            GADataTable.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {

        }

        private void MainGroupsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedMainGroup = (MainGroup?)MainGroupsListBox.SelectedItem;
            SelectedMainGroupId = (string?)MainGroupsListBox.SelectedValue;
        }
    }
}
