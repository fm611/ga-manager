using GroupAddress.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupAddress.UI
{
    public partial class ItemTemplateManagerForm : Form
    {

        public List<MainGroup> MainGroups { get; set; }
        public List<ItemTemplate> ItemTemplates { get; set; }
        public ListBoxWrapper<MainGroup> MainGroupsWrapper { get; set; }
        public ListBoxWrapper<ItemTemplate> ItemTemplatesWrapper { get; set; }

        public ItemTemplate? SelectedItemTemplate { get; set; }
        public string? SelectedItemTemplateId { get; set; }
        public MainGroup? SelectedMainGroup { get; set; }
        public string? SelectedMainGroupId { get; set; }

        private string? _lastInsertTemplateId;
        private string? _lastInsertMainGroupId;

        public Item? LastInsertedItem { get; set; }


        public ItemTemplateManagerForm(List<MainGroup> mainGroups, List<ItemTemplate> itemTemplates)
        {
            InitializeComponent();

            MainGroups = mainGroups;
            ItemTemplates = itemTemplates;

            LoadData();
        }

        private void AddItemForm_Shown(object sender, EventArgs e)
        {
            SelectLastInsert();
            GADataTable.UpdateTable();
        }


        private void AddItemForm_Load(object sender, EventArgs e)
        {
        }

        public void LoadData()
        {
            ItemTemplatesWrapper = new ListBoxWrapper<ItemTemplate>(
                ItemTemplatesListBox,
                (a, b) => a.Name.CompareTo(b.Name),
                nameof(ItemTemplate.Name),
                nameof(ItemTemplate.Id),
                () => ItemTemplates);
            ItemTemplatesWrapper.Update();

            if (ItemTemplatesListBox.Items.Count > 0 && ItemTemplatesListBox.SelectedItem == null)
                ItemTemplatesListBox.SelectedIndex = 0;


            MainGroupsWrapper = new ListBoxWrapper<MainGroup>(
                MainGroupsListBox,
                (a, b) => a.AddressName.CompareTo(b.AddressName),
                nameof(MainGroup.AddressName),
                nameof(MainGroup.Id),
                () => MainGroups);
            MainGroupsWrapper.Update();

            if (MainGroupsListBox.Items.Count > 0 && MainGroupsListBox.SelectedItem == null)
                MainGroupsListBox.SelectedIndex = 0;

        }

        public void SelectLastInsert()
        {
            if (!string.IsNullOrEmpty(_lastInsertMainGroupId)) MainGroupsListBox.SelectedValue = _lastInsertMainGroupId;
            if (!string.IsNullOrEmpty(_lastInsertTemplateId)) ItemTemplatesListBox.SelectedValue = _lastInsertTemplateId;

            NewItemPreStringTextBox.Focus();
        }





        public void SelectMainGroup(string? id)
        {
            MainGroupsListBox.SelectedValue = id;
        }

        private void ItemTemplatesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            SelectedItemTemplate = (ItemTemplate?)ItemTemplatesListBox.SelectedItem;
            SelectedItemTemplateId = (string?)ItemTemplatesListBox.SelectedValue;

            GADataTable.SetTopLevelCollection(SelectedItemTemplate);

        }

        #region Add Edit Delete Save Template

        public void AddItemTemplateButton_Click(object sender, EventArgs e)
        {

        }

        #endregion


        #region MainGroup

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
        private void AddItemButton_Click(object sender, EventArgs e)
        {
            if (SelectedMainGroup == null || SelectedItemTemplate == null)
            {
                DialogResult = DialogResult.Abort;
                return;
            }


            LastInsertedItem = SelectedMainGroup.AddItem(SelectedItemTemplate, NewItemPreStringTextBox.Text, GetInsertIndex());

            _lastInsertMainGroupId = SelectedMainGroup.Id;
            _lastInsertTemplateId = SelectedItemTemplate.Id;

            DialogResult = DialogResult.OK;
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
        private void NewItemPreStringTextBox_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() => (sender as TextBox).SelectAll()));

        }
        private void NewItemPreStringTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Return)
            {
                AddItemButton_Click(null, null);
            }
        }

        #endregion
    }
}
