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
        public Project Project { get; set; }
        public ListBoxWrapper<MainGroup> MainGroupsWrapper { get; set; }
        public ListBoxWrapper<ItemTemplate> ItemTemplatesWrapper { get; set; }

        public ItemTemplate? SelectedItemTemplate { get; set; }
        public string? SelectedItemTemplateId { get; set; }
        public MainGroup? SelectedMainGroup { get; set; }
        public string? SelectedMainGroupId { get; set; }

        private string? _lastInsertTemplateId;
        private string? _lastInsertMainGroupId;

        public Item? LastInsertedItem { get; set; }

        private bool _editMode = false;


        public ItemTemplateManagerForm(Project project)
        {
            InitializeComponent();

            Project = project;

            ItemTemplatesWrapper = new ListBoxWrapper<ItemTemplate>(
                ItemTemplatesListBox,
                (a, b) => a.Name.CompareTo(b.Name),
                nameof(ItemTemplate.Name),
                nameof(ItemTemplate.Id),
                () => Project.ItemTemplates);

            MainGroupsWrapper = new ListBoxWrapper<MainGroup>(
                MainGroupsListBox,
                (a, b) => a.AddressName.CompareTo(b.AddressName),
                nameof(MainGroup.AddressName),
                nameof(MainGroup.Id),
                () => Project.MainGroups);

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

        private void ItemTemplateManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetEditMode(false);
        }


        public void LoadData()
        {

            ItemTemplatesWrapper.Update();

            if (ItemTemplatesListBox.Items.Count > 0 && ItemTemplatesListBox.SelectedItem == null)
                ItemTemplatesListBox.SelectedIndex = 0;


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
            if(!string.IsNullOrEmpty(id))
                MainGroupsListBox.SelectedValue = id;
        }

        private void ItemTemplatesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            SelectedItemTemplate = (ItemTemplate?)ItemTemplatesListBox.SelectedItem;
            SelectedItemTemplateId = (string?)ItemTemplatesListBox.SelectedValue;

            GADataTable.SetTopLevelCollection(SelectedItemTemplate);

            if (SelectedItemTemplate != null)
            {
                ItemTemplateNameTextBox.Text = SelectedItemTemplate.Name;

            }

        }

        #region Add Edit Delete Save Template

        public void AddItemTemplateButton_Click(object sender, EventArgs e)
        {
            var newTemplate = new ItemTemplate("Neues Template", []);
            Project.ItemTemplates.Add(newTemplate);
            ItemTemplatesWrapper.Update();

            ItemTemplatesListBox.SelectedValue = newTemplate.Id;
            SetEditMode(true);
            ItemTemplateNameTextBox.Focus();
        }
        public void EditItemTemplateButton_Click(object sender, EventArgs e)
        {
            if (SelectedItemTemplate == null) return;

            SetEditMode(true);
        }

        private void DeleteItemTemplateButton_Click(object sender, EventArgs e)
        {
            if (SelectedItemTemplate == null) return;

            var res = MessageBox.Show("Template löschen?", "Template löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                Project.ItemTemplates.Remove(SelectedItemTemplate);
                ItemTemplatesWrapper.Update();
            }
        }


        private void SaveItemTemplateButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ItemTemplateNameTextBox.Text))
                SelectedItemTemplate.Name = ItemTemplateNameTextBox.Text;

            SetEditMode(false);
            ItemTemplatesWrapper.Update();
        }


        private void SetEditMode(bool editMode)
        {
            _editMode = editMode;
            ItemTemplateNameTextBox.ReadOnly = !editMode;

            AddEditDeleteItemTemplatePanel.Visible = !editMode;
            SaveButtonPanel.Visible = editMode;
            SaveButtonPanel.Enabled = editMode;

            ItemTemplatesListBox.Enabled = !editMode;

            GADataTable.ReadOnly = !editMode;

            AddItemPanel.Enabled = !editMode;
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
            if(LastInsertedItem !=null)
                Project.Items.Add(LastInsertedItem);


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
