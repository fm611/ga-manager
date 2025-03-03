using System.Windows.Forms;

namespace GroupAddress.UI
{
    partial class GroupTemplateManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            GADataTable = new GADataGridView();
            NewGroupPreStringTextBox = new TextBox();
            AddGroupButton = new Button();
            GroupTemplatesListBox = new ListBox();
            label3 = new Label();
            MainGroupsListBox = new ListBox();
            label2 = new Label();
            label4 = new Label();
            InsertAtRadioButton = new RadioButton();
            InsertNextFreeRadioButton = new RadioButton();
            label5 = new Label();
            InsertAtTextBox = new TextBox();
            InsertAtNextBlockRadioButton = new RadioButton();
            NextBlockStartingIndexTextBox = new TextBox();
            InsertAtNextFreeTextBox = new TextBox();
            DeleteGroupTemplateButton = new Button();
            AddGroupTemplateButton = new Button();
            EditGroupTemplateButton = new Button();
            AddEditDeleteGroupTemplatePanel = new FlowLayoutPanel();
            SaveButtonPanel = new FlowLayoutPanel();
            SaveGroupTemplateButton = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            AddGroupTemplateToolStripMenuItem = new ToolStripMenuItem();
            EditGroupTemplateToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            DeleteGroupTemplateToolStripMenuItem = new ToolStripMenuItem();
            GroupTemplateNameTextBox = new TextBox();
            label6 = new Label();
            AddGroupPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)GADataTable).BeginInit();
            AddEditDeleteGroupTemplatePanel.SuspendLayout();
            SaveButtonPanel.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            AddGroupPanel.SuspendLayout();
            SuspendLayout();
            // 
            // GADataTable
            // 
            GADataTable.AllowUserToAddRows = false;
            GADataTable.AllowUserToDeleteRows = false;
            GADataTable.AllowUserToResizeRows = false;
            GADataTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.ControlLight;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new Padding(0, 5, 0, 5);
            dataGridViewCellStyle1.SelectionBackColor = Color.YellowGreen;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            GADataTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            GADataTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.YellowGreen;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            GADataTable.DefaultCellStyle = dataGridViewCellStyle2;
            GADataTable.EnableHeadersVisualStyles = false;
            GADataTable.Location = new Point(258, 36);
            GADataTable.Name = "GADataTable";
            GADataTable.ReadOnly = true;
            GADataTable.RowData = null;
            GADataTable.ShowAllRows = true;
            GADataTable.ShowEditingIcon = false;
            GADataTable.ShowEmptyRows = true;
            GADataTable.Size = new Size(1214, 663);
            GADataTable.TabIndex = 34;
            GADataTable.TopLevelCollection = null;
            // 
            // NewGroupPreStringTextBox
            // 
            NewGroupPreStringTextBox.Location = new Point(3, 23);
            NewGroupPreStringTextBox.Name = "NewGroupPreStringTextBox";
            NewGroupPreStringTextBox.Size = new Size(236, 23);
            NewGroupPreStringTextBox.TabIndex = 37;
            NewGroupPreStringTextBox.Enter += NewGroupPreStringTextBox_Enter;
            NewGroupPreStringTextBox.KeyUp += NewGroupPreStringTextBox_KeyUp;
            // 
            // AddGroupButton
            // 
            AddGroupButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AddGroupButton.Location = new Point(3, 367);
            AddGroupButton.Name = "AddGroupButton";
            AddGroupButton.Size = new Size(89, 23);
            AddGroupButton.TabIndex = 38;
            AddGroupButton.Text = "Hinzufügen";
            AddGroupButton.UseVisualStyleBackColor = true;
            AddGroupButton.Click += AddGroupButton_Click;
            // 
            // GroupTemplatesListBox
            // 
            GroupTemplatesListBox.FormattingEnabled = true;
            GroupTemplatesListBox.ItemHeight = 15;
            GroupTemplatesListBox.Location = new Point(12, 81);
            GroupTemplatesListBox.Name = "GroupTemplatesListBox";
            GroupTemplatesListBox.Size = new Size(236, 184);
            GroupTemplatesListBox.TabIndex = 36;
            GroupTemplatesListBox.SelectedIndexChanged += GroupTemplatesListBox_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(877, 773);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 40;
            label3.Text = "Name / Prefix";
            // 
            // MainGroupsListBox
            // 
            MainGroupsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            MainGroupsListBox.FormattingEnabled = true;
            MainGroupsListBox.ItemHeight = 15;
            MainGroupsListBox.Location = new Point(3, 74);
            MainGroupsListBox.Name = "MainGroupsListBox";
            MainGroupsListBox.Size = new Size(236, 169);
            MainGroupsListBox.TabIndex = 36;
            MainGroupsListBox.SelectedIndexChanged += MainGroupsListBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 5);
            label2.Name = "label2";
            label2.Size = new Size(147, 15);
            label2.TabIndex = 40;
            label2.Text = "Gruppen Name (PreString)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 49);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 40;
            label4.Text = "Hauptgruppe";
            // 
            // InsertAtRadioButton
            // 
            InsertAtRadioButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            InsertAtRadioButton.AutoSize = true;
            InsertAtRadioButton.Location = new Point(7, 310);
            InsertAtRadioButton.Name = "InsertAtRadioButton";
            InsertAtRadioButton.Size = new Size(89, 19);
            InsertAtRadioButton.TabIndex = 41;
            InsertAtRadioButton.TabStop = true;
            InsertAtRadioButton.Text = "An Position:";
            InsertAtRadioButton.UseVisualStyleBackColor = true;
            InsertAtRadioButton.CheckedChanged += InsertAtRadioButton_CheckedChanged;
            // 
            // InsertNextFreeRadioButton
            // 
            InsertNextFreeRadioButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            InsertNextFreeRadioButton.AutoSize = true;
            InsertNextFreeRadioButton.Location = new Point(7, 335);
            InsertNextFreeRadioButton.Name = "InsertNextFreeRadioButton";
            InsertNextFreeRadioButton.Size = new Size(99, 19);
            InsertNextFreeRadioButton.TabIndex = 41;
            InsertNextFreeRadioButton.TabStop = true;
            InsertNextFreeRadioButton.Text = "Nächste Freie:";
            InsertNextFreeRadioButton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(0, 259);
            label5.Name = "label5";
            label5.Size = new Size(93, 15);
            label5.TabIndex = 40;
            label5.Text = "Einfüge Position";
            // 
            // InsertAtTextBox
            // 
            InsertAtTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            InsertAtTextBox.Location = new Point(118, 309);
            InsertAtTextBox.Name = "InsertAtTextBox";
            InsertAtTextBox.ReadOnly = true;
            InsertAtTextBox.Size = new Size(69, 23);
            InsertAtTextBox.TabIndex = 42;
            InsertAtTextBox.Enter += InsertAtTextBox_Enter;
            // 
            // InsertAtNextBlockRadioButton
            // 
            InsertAtNextBlockRadioButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            InsertAtNextBlockRadioButton.AutoSize = true;
            InsertAtNextBlockRadioButton.Checked = true;
            InsertAtNextBlockRadioButton.Location = new Point(7, 285);
            InsertAtNextBlockRadioButton.Name = "InsertAtNextBlockRadioButton";
            InsertAtNextBlockRadioButton.Size = new Size(107, 19);
            InsertAtNextBlockRadioButton.TabIndex = 41;
            InsertAtNextBlockRadioButton.TabStop = true;
            InsertAtNextBlockRadioButton.Text = "Nächster Block:";
            InsertAtNextBlockRadioButton.UseVisualStyleBackColor = true;
            InsertAtNextBlockRadioButton.CheckedChanged += InsertAtRadioButton_CheckedChanged;
            // 
            // NextBlockStartingIndexTextBox
            // 
            NextBlockStartingIndexTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            NextBlockStartingIndexTextBox.Location = new Point(118, 284);
            NextBlockStartingIndexTextBox.Name = "NextBlockStartingIndexTextBox";
            NextBlockStartingIndexTextBox.ReadOnly = true;
            NextBlockStartingIndexTextBox.Size = new Size(69, 23);
            NextBlockStartingIndexTextBox.TabIndex = 42;
            // 
            // InsertAtNextFreeTextBox
            // 
            InsertAtNextFreeTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            InsertAtNextFreeTextBox.Location = new Point(118, 334);
            InsertAtNextFreeTextBox.Name = "InsertAtNextFreeTextBox";
            InsertAtNextFreeTextBox.ReadOnly = true;
            InsertAtNextFreeTextBox.Size = new Size(69, 23);
            InsertAtNextFreeTextBox.TabIndex = 42;
            // 
            // DeleteGroupTemplateButton
            // 
            DeleteGroupTemplateButton.Location = new Point(84, 3);
            DeleteGroupTemplateButton.Name = "DeleteGroupTemplateButton";
            DeleteGroupTemplateButton.Size = new Size(75, 23);
            DeleteGroupTemplateButton.TabIndex = 43;
            DeleteGroupTemplateButton.Text = "Löschen";
            DeleteGroupTemplateButton.UseVisualStyleBackColor = true;
            DeleteGroupTemplateButton.Click += DeleteGroupTemplateButton_Click;
            // 
            // AddGroupTemplateButton
            // 
            AddGroupTemplateButton.Location = new Point(3, 3);
            AddGroupTemplateButton.Name = "AddGroupTemplateButton";
            AddGroupTemplateButton.Size = new Size(75, 23);
            AddGroupTemplateButton.TabIndex = 44;
            AddGroupTemplateButton.Text = "Neu";
            AddGroupTemplateButton.UseVisualStyleBackColor = true;
            AddGroupTemplateButton.Click += AddroupTemplateButton_Click;
            // 
            // EditGroupTemplateButton
            // 
            EditGroupTemplateButton.Location = new Point(165, 3);
            EditGroupTemplateButton.Name = "EditGroupTemplateButton";
            EditGroupTemplateButton.Size = new Size(75, 23);
            EditGroupTemplateButton.TabIndex = 43;
            EditGroupTemplateButton.Text = "Bearbeiten";
            EditGroupTemplateButton.UseVisualStyleBackColor = true;
            EditGroupTemplateButton.Click += EditGroupTemplateButton_Click;
            // 
            // AddEditDeleteGroupTemplatePanel
            // 
            AddEditDeleteGroupTemplatePanel.AutoSize = true;
            AddEditDeleteGroupTemplatePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AddEditDeleteGroupTemplatePanel.Controls.Add(AddGroupTemplateButton);
            AddEditDeleteGroupTemplatePanel.Controls.Add(DeleteGroupTemplateButton);
            AddEditDeleteGroupTemplatePanel.Controls.Add(EditGroupTemplateButton);
            AddEditDeleteGroupTemplatePanel.Location = new Point(9, 271);
            AddEditDeleteGroupTemplatePanel.Name = "AddEditDeleteGroupTemplatePanel";
            AddEditDeleteGroupTemplatePanel.Size = new Size(243, 29);
            AddEditDeleteGroupTemplatePanel.TabIndex = 45;
            // 
            // SaveButtonPanel
            // 
            SaveButtonPanel.AutoSize = true;
            SaveButtonPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            SaveButtonPanel.Controls.Add(SaveGroupTemplateButton);
            SaveButtonPanel.Enabled = false;
            SaveButtonPanel.Location = new Point(171, 271);
            SaveButtonPanel.Name = "SaveButtonPanel";
            SaveButtonPanel.Size = new Size(81, 29);
            SaveButtonPanel.TabIndex = 46;
            SaveButtonPanel.Visible = false;
            // 
            // SaveGroupTemplateButton
            // 
            SaveGroupTemplateButton.Location = new Point(3, 3);
            SaveGroupTemplateButton.Name = "SaveGroupTemplateButton";
            SaveGroupTemplateButton.Size = new Size(75, 23);
            SaveGroupTemplateButton.TabIndex = 43;
            SaveGroupTemplateButton.Text = "Fertig";
            SaveGroupTemplateButton.UseVisualStyleBackColor = true;
            SaveGroupTemplateButton.Click += SaveGroupTemplateButton_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { AddGroupTemplateToolStripMenuItem, EditGroupTemplateToolStripMenuItem, toolStripSeparator1, DeleteGroupTemplateToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(131, 76);
            // 
            // AddGroupTemplateToolStripMenuItem
            // 
            AddGroupTemplateToolStripMenuItem.Name = "AddGroupTemplateToolStripMenuItem";
            AddGroupTemplateToolStripMenuItem.Size = new Size(130, 22);
            AddGroupTemplateToolStripMenuItem.Text = "Neu";
            // 
            // EditGroupTemplateToolStripMenuItem
            // 
            EditGroupTemplateToolStripMenuItem.Name = "EditGroupTemplateToolStripMenuItem";
            EditGroupTemplateToolStripMenuItem.Size = new Size(130, 22);
            EditGroupTemplateToolStripMenuItem.Text = "Bearbeiten";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(127, 6);
            // 
            // DeleteGroupTemplateToolStripMenuItem
            // 
            DeleteGroupTemplateToolStripMenuItem.Name = "DeleteGroupTemplateToolStripMenuItem";
            DeleteGroupTemplateToolStripMenuItem.Size = new Size(130, 22);
            DeleteGroupTemplateToolStripMenuItem.Text = "Löschen";
            // 
            // GroupTemplateNameTextBox
            // 
            GroupTemplateNameTextBox.Location = new Point(12, 52);
            GroupTemplateNameTextBox.Name = "GroupTemplateNameTextBox";
            GroupTemplateNameTextBox.ReadOnly = true;
            GroupTemplateNameTextBox.Size = new Size(236, 23);
            GroupTemplateNameTextBox.TabIndex = 37;
            GroupTemplateNameTextBox.Enter += NewGroupPreStringTextBox_Enter;
            GroupTemplateNameTextBox.KeyUp += NewGroupPreStringTextBox_KeyUp;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 34);
            label6.Name = "label6";
            label6.Size = new Size(105, 15);
            label6.TabIndex = 40;
            label6.Text = "Gruppen Template";
            // 
            // AddGroupPanel
            // 
            AddGroupPanel.Controls.Add(MainGroupsListBox);
            AddGroupPanel.Controls.Add(AddGroupButton);
            AddGroupPanel.Controls.Add(NewGroupPreStringTextBox);
            AddGroupPanel.Controls.Add(label2);
            AddGroupPanel.Controls.Add(NextBlockStartingIndexTextBox);
            AddGroupPanel.Controls.Add(label5);
            AddGroupPanel.Controls.Add(InsertAtNextFreeTextBox);
            AddGroupPanel.Controls.Add(label4);
            AddGroupPanel.Controls.Add(InsertAtTextBox);
            AddGroupPanel.Controls.Add(InsertAtRadioButton);
            AddGroupPanel.Controls.Add(InsertNextFreeRadioButton);
            AddGroupPanel.Controls.Add(InsertAtNextBlockRadioButton);
            AddGroupPanel.Location = new Point(6, 306);
            AddGroupPanel.Name = "AddGroupPanel";
            AddGroupPanel.Size = new Size(243, 393);
            AddGroupPanel.TabIndex = 47;
            // 
            // GroupTemplateManagerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1484, 711);
            Controls.Add(AddGroupPanel);
            Controls.Add(SaveButtonPanel);
            Controls.Add(AddEditDeleteGroupTemplatePanel);
            Controls.Add(label3);
            Controls.Add(label6);
            Controls.Add(GADataTable);
            Controls.Add(GroupTemplateNameTextBox);
            Controls.Add(GroupTemplatesListBox);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(1500, 750);
            Name = "GroupTemplateManagerForm";
            Text = "Gruppen Manager";
            FormClosing += GroupTemplateManagerForm_FormClosing;
            Load += AddGroupForm_Load;
            Shown += AddGroupForm_Shown;
            ((System.ComponentModel.ISupportInitialize)GADataTable).EndInit();
            AddEditDeleteGroupTemplatePanel.ResumeLayout(false);
            SaveButtonPanel.ResumeLayout(false);
            contextMenuStrip1.ResumeLayout(false);
            AddGroupPanel.ResumeLayout(false);
            AddGroupPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GADataGridView GADataTable;
        private TextBox NewGroupPreStringTextBox;
        private Button AddGroupButton;
        private ListBox GroupTemplatesListBox;
        private Label label3;
        private ListBox MainGroupsListBox;
        private Label label2;
        private Label label4;
        private RadioButton InsertAtRadioButton;
        private RadioButton InsertNextFreeRadioButton;
        private Label label5;
        private TextBox InsertAtTextBox;
        private RadioButton InsertAtNextBlockRadioButton;
        private TextBox NextBlockStartingIndexTextBox;
        private TextBox InsertAtNextFreeTextBox;
        private Button DeleteGroupTemplateButton;
        private Button AddGroupTemplateButton;
        private Button EditGroupTemplateButton;
        private FlowLayoutPanel SaveButtonPanel;
        private FlowLayoutPanel AddEditDeleteGroupTemplatePanel;
        private Button SaveGroupTemplateButton;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem AddGroupTemplateToolStripMenuItem;
        private ToolStripMenuItem EditGroupTemplateToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem DeleteGroupTemplateToolStripMenuItem;
        private TextBox GroupTemplateNameTextBox;
        private Label label6;
        private Panel AddGroupPanel;
    }
}