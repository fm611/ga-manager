using GroupAddress.Core;
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
    public partial class GroupTemplateManagerForm : Form
    {
        public Project Project { get; set; }
        public ListBoxWrapper<MainGroup> MainGroupsWrapper { get; set; }
        public ListBoxWrapper<GroupTemplate> GroupTemplatesWrapper { get; set; }

        public GroupTemplate? SelectedGroupTemplate { get; set; }
        public string? SelectedGroupTemplateId { get; set; }
        public MainGroup? SelectedMainGroup { get; set; }
        public string? SelectedMainGroupId { get; set; }

        private string? _lastInsertTemplateId;
        private string? _lastInsertMainGroupId;

        public Group? LastInsertedGroup { get; set; }

        private bool _editMode = false;


        public GroupTemplateManagerForm(Project project)
        {
            InitializeComponent();

            Project = project;

            GroupTemplatesWrapper = new ListBoxWrapper<GroupTemplate>(
                GroupTemplatesListBox,
                (a, b) => a.Name.CompareTo(b.Name),
                nameof(GroupTemplate.Name),
                nameof(GroupTemplate.Id),
                () => Project.GroupTemplates);

            MainGroupsWrapper = new ListBoxWrapper<MainGroup>(
                MainGroupsListBox,
                (a, b) => a.AddressName.CompareTo(b.AddressName),
                nameof(MainGroup.AddressName),
                nameof(MainGroup.Id),
                () => Project.MainGroups);

            AddGroupButton.Enabled = false;
            LoadData();
        }

        private void AddGroupForm_Shown(object sender, EventArgs e)
        {
            SelectLastInsert();
            GADataTable.UpdateTable();
        }


        private void AddGroupForm_Load(object sender, EventArgs e)
        {
        }

        private void GroupTemplateManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetEditMode(false);
        }


        public void LoadData()
        {

            GroupTemplatesWrapper.Update();

            if (GroupTemplatesListBox.Items.Count > 0 && GroupTemplatesListBox.SelectedItem == null)
                GroupTemplatesListBox.SelectedIndex = 0;
            if (GroupTemplatesListBox.SelectedItem == null)
                AddGroupButton.Enabled = false;


            MainGroupsWrapper.Update();

            if (MainGroupsListBox.Items.Count > 0 && MainGroupsListBox.SelectedItem == null)
                MainGroupsListBox.SelectedIndex = 0;

        }

        public void SelectLastInsert()
        {
            if (!string.IsNullOrEmpty(_lastInsertMainGroupId)) MainGroupsListBox.SelectedValue = _lastInsertMainGroupId;
            if (!string.IsNullOrEmpty(_lastInsertTemplateId)) GroupTemplatesListBox.SelectedValue = _lastInsertTemplateId;

            NewGroupPreStringTextBox.Focus();
        }


        public void SelectMainGroup(string? id)
        {
            if(!string.IsNullOrEmpty(id))
                MainGroupsListBox.SelectedValue = id;
        }

        private void GroupTemplatesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            SelectedGroupTemplate = (GroupTemplate?)GroupTemplatesListBox.SelectedItem;
            SelectedGroupTemplateId = (string?)GroupTemplatesListBox.SelectedValue;

            GADataTable.SetTopLevelCollection(SelectedGroupTemplate);

            if (SelectedGroupTemplate != null)
            {
                GroupTemplateNameTextBox.Text = SelectedGroupTemplate.Name;
            }
            UpdateAddGroupButton();

        }

        #region Add Edit Delete Save Template

        public void AddroupTemplateButton_Click(object sender, EventArgs e)
        {
            var newTemplate = new GroupTemplate("Neues Template", []);
            Project.AddGroupTemplate(newTemplate);
            GroupTemplatesWrapper.Update();

            GroupTemplatesListBox.SelectedValue = newTemplate.Id;
            SetEditMode(true);
            GroupTemplateNameTextBox.Focus();
        }
        public void EditGroupTemplateButton_Click(object sender, EventArgs e)
        {
            if (SelectedGroupTemplate == null) return;

            SetEditMode(true);
        }

        private void DeleteGroupTemplateButton_Click(object sender, EventArgs e)
        {
            if (SelectedGroupTemplate == null) return;

            var res = MessageBox.Show("Template löschen?", "Template löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                Project.RemoveGroupTemplate(SelectedGroupTemplate);
                GroupTemplatesWrapper.Update();
            }
        }


        private void SaveGroupTemplateButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(GroupTemplateNameTextBox.Text))
                SelectedGroupTemplate.Name = GroupTemplateNameTextBox.Text;

            SetEditMode(false);
            GroupTemplatesWrapper.Update();
        }


        private void SetEditMode(bool editMode)
        {
            _editMode = editMode;
            GroupTemplateNameTextBox.ReadOnly = !editMode;

            AddEditDeleteGroupTemplatePanel.Visible = !editMode;
            SaveButtonPanel.Visible = editMode;
            SaveButtonPanel.Enabled = editMode;

            GroupTemplatesListBox.Enabled = !editMode;

            GADataTable.ReadOnly = !editMode;

            AddGroupPanel.Enabled = !editMode;
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
        private void AddGroupButton_Click(object sender, EventArgs e)
        {
            if (SelectedMainGroup == null || SelectedGroupTemplate == null)
            {
                DialogResult = DialogResult.Abort;
                return;
            }

            LastInsertedGroup = SelectedMainGroup.AddGroup(SelectedGroupTemplate, NewGroupPreStringTextBox.Text, GetInsertIndex());
            if(LastInsertedGroup !=null)
                Project.AddGroup(LastInsertedGroup);


            _lastInsertMainGroupId = SelectedMainGroup.Id;
            _lastInsertTemplateId = SelectedGroupTemplate.Id;

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
        private void NewGroupPreStringTextBox_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() => (sender as TextBox).SelectAll()));

        }
        private void NewGroupPreStringTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (!UpdateAddGroupButton()) return;
            if (e.KeyValue == (char)Keys.Return)
            {
                AddGroupButton_Click(null, null);
            }
        }

        private bool UpdateAddGroupButton()
        {
            AddGroupButton.Enabled = false;

            if(!string.IsNullOrEmpty(NewGroupPreStringTextBox.Text) && SelectedGroupTemplate != null)
                AddGroupButton.Enabled = true;

            return AddGroupButton.Enabled;
        }

        #endregion


    }
}
