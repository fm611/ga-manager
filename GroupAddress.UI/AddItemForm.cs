using GroupAddress.Core;
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
        public ListBoxWrapper<ItemPartTemplate> ItemPartTemplatesWrapper { get; set; }

        public ItemTemplate? SelectedItemTemplate { get; set; }

        public AddItemForm(AppDbContext db)
        {

            InitializeComponent();

            Db = db;

        }

        private void AddItemForm_Load(object sender, EventArgs e)
        {
            ItemTemplatesWrapper = new ListBoxWrapper<ItemTemplate>(ItemTemplatesListBox, (a, b) => a.Name.CompareTo(b.Name), "Name", "Id");
            ItemPartTemplatesWrapper = new ListBoxWrapper<ItemPartTemplate>(ItemPartTemplatesListBox, (a, b) => a.Name.CompareTo(b.Name), "Name", "Id");



            ItemTemplatesWrapper.Load(Db.ItemTemplates);
            ItemTemplatesListBox.SelectedIndex = 0;

            
        }

        private void ItemTemplatesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedItemTemplate = (ItemTemplate?)ItemTemplatesListBox.SelectedItem;
            ItemPartTemplatesWrapper.Load(Db.ItemPartTemplates.Where(x => x.ItemTemplateId == (string?)ItemTemplatesListBox.SelectedValue));
        }
    }
}
