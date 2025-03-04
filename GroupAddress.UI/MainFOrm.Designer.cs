namespace GroupAddress.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            MainGroupsListBox = new ListBox();
            MainGroupsListBoxContextMenu = new ContextMenuStrip(components);
            AddMainGroupToolStripMenuItem = new ToolStripMenuItem();
            EditMainGroupToolStripMenuItem = new ToolStripMenuItem();
            DeleteMainGroupToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            statusStrip1 = new StatusStrip();
            StatusInfoLabel = new ToolStripStatusLabel();
            GADataTable = new GADataGridView();
            label2 = new Label();
            menuStrip1 = new MenuStrip();
            FileToolStripMenuItem = new ToolStripMenuItem();
            NewProjectMenuItem = new ToolStripMenuItem();
            OpenToolStripMenuItem = new ToolStripMenuItem();
            OpenFileStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            OpenSampleProjectToolStripMenuItem = new ToolStripMenuItem();
            OpenRecentToolStripSeparator = new ToolStripSeparator();
            SaveToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            ExportToolStripMenuItem = new ToolStripMenuItem();
            ImportToolStripMenuItem = new ToolStripMenuItem();
            GroupManagerToolStripMenuItem = new ToolStripMenuItem();
            AddCellsNumTextBox = new ToolStripTextBox();
            AddCellsStripMenuItem = new ToolStripMenuItem();
            DeleteCellsStripMenuItem = new ToolStripMenuItem();
            saveFileDialog1 = new SaveFileDialog();
            GroupsListBox = new ListBox();
            GroupsListBoxContextMenu = new ContextMenuStrip(components);
            NewEmptyGroupToolStripMenuItem = new ToolStripMenuItem();
            NewGroupToolStripMenuItem = new ToolStripMenuItem();
            EditGroupToolStripMenuItem = new ToolStripMenuItem();
            DeleteGroupToolStripMenuItem = new ToolStripMenuItem();
            label3 = new Label();
            UnselectGroupsButton = new Button();
            GADataTableBackPanel = new Panel();
            GaDataGridContextMenu = new ContextMenuStrip(components);
            testToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            FilterWithoutGroupButton = new Button();
            FilterTextBox = new TextBox();
            MainGroupsListBoxContextMenu.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GADataTable).BeginInit();
            menuStrip1.SuspendLayout();
            GroupsListBoxContextMenu.SuspendLayout();
            GADataTableBackPanel.SuspendLayout();
            GaDataGridContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // MainGroupsListBox
            // 
            MainGroupsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            MainGroupsListBox.ContextMenuStrip = MainGroupsListBoxContextMenu;
            MainGroupsListBox.FormattingEnabled = true;
            MainGroupsListBox.ItemHeight = 15;
            MainGroupsListBox.Location = new Point(12, 67);
            MainGroupsListBox.Name = "MainGroupsListBox";
            MainGroupsListBox.Size = new Size(248, 394);
            MainGroupsListBox.TabIndex = 0;
            MainGroupsListBox.SelectedIndexChanged += MainGroupsListBox_SelectedIndexChanged;
            MainGroupsListBox.KeyDown += MainGroupsListBox_KeyDown;
            // 
            // MainGroupsListBoxContextMenu
            // 
            MainGroupsListBoxContextMenu.Items.AddRange(new ToolStripItem[] { AddMainGroupToolStripMenuItem, EditMainGroupToolStripMenuItem, DeleteMainGroupToolStripMenuItem });
            MainGroupsListBoxContextMenu.Name = "contextMenuStrip1";
            MainGroupsListBoxContextMenu.Size = new Size(147, 70);
            // 
            // AddMainGroupToolStripMenuItem
            // 
            AddMainGroupToolStripMenuItem.Name = "AddMainGroupToolStripMenuItem";
            AddMainGroupToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            AddMainGroupToolStripMenuItem.Size = new Size(146, 22);
            AddMainGroupToolStripMenuItem.Text = "Neu";
            AddMainGroupToolStripMenuItem.Click += AddMainGroupToolStripMenuItem_Click;
            // 
            // EditMainGroupToolStripMenuItem
            // 
            EditMainGroupToolStripMenuItem.Name = "EditMainGroupToolStripMenuItem";
            EditMainGroupToolStripMenuItem.Size = new Size(146, 22);
            EditMainGroupToolStripMenuItem.Text = "Bearbeiten";
            EditMainGroupToolStripMenuItem.Click += EditMainGroupToolStripMenuItem_Click;
            // 
            // DeleteMainGroupToolStripMenuItem
            // 
            DeleteMainGroupToolStripMenuItem.Name = "DeleteMainGroupToolStripMenuItem";
            DeleteMainGroupToolStripMenuItem.ShortcutKeys = Keys.Delete;
            DeleteMainGroupToolStripMenuItem.Size = new Size(146, 22);
            DeleteMainGroupToolStripMenuItem.Text = "Löschen";
            DeleteMainGroupToolStripMenuItem.Click += DeleteMainGroupToolStripMenuItem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 40);
            label1.Name = "label1";
            label1.Size = new Size(85, 15);
            label1.TabIndex = 1;
            label1.Text = "Hauptgruppen";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { StatusInfoLabel });
            statusStrip1.Location = new Point(0, 785);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1324, 22);
            statusStrip1.TabIndex = 10;
            statusStrip1.Text = "statusStrip1";
            // 
            // StatusInfoLabel
            // 
            StatusInfoLabel.Name = "StatusInfoLabel";
            StatusInfoLabel.Size = new Size(118, 17);
            StatusInfoLabel.Text = "toolStripStatusLabel1";
            // 
            // GADataTable
            // 
            GADataTable.AllowUserToAddRows = false;
            GADataTable.AllowUserToDeleteRows = false;
            GADataTable.AllowUserToResizeRows = false;
            GADataTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.ControlLight;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new Padding(0, 5, 0, 5);
            dataGridViewCellStyle3.SelectionBackColor = Color.YellowGreen;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            GADataTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            GADataTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = Color.YellowGreen;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            GADataTable.DefaultCellStyle = dataGridViewCellStyle4;
            GADataTable.EnableHeadersVisualStyles = false;
            GADataTable.Location = new Point(4, 4);
            GADataTable.Name = "GADataTable";
            GADataTable.RowData = null;
            GADataTable.ShowAllRows = true;
            GADataTable.ShowEditingIcon = false;
            GADataTable.ShowEmptyRows = true;
            GADataTable.Size = new Size(1048, 709);
            GADataTable.TabIndex = 34;
            GADataTable.TopLevelCollection = null;
            GADataTable.CellMouseDown += GADataTable_CellMouseDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(266, 40);
            label2.Name = "label2";
            label2.Size = new Size(99, 15);
            label2.TabIndex = 1;
            label2.Text = "Gruppenadressen";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem, GroupManagerToolStripMenuItem, AddCellsNumTextBox, AddCellsStripMenuItem, DeleteCellsStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1324, 27);
            menuStrip1.TabIndex = 36;
            menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { NewProjectMenuItem, OpenToolStripMenuItem, SaveToolStripMenuItem, toolStripSeparator5, ExportToolStripMenuItem, ImportToolStripMenuItem });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            FileToolStripMenuItem.Size = new Size(46, 23);
            FileToolStripMenuItem.Text = "Datei";
            // 
            // NewProjectMenuItem
            // 
            NewProjectMenuItem.Name = "NewProjectMenuItem";
            NewProjectMenuItem.Size = new Size(168, 22);
            NewProjectMenuItem.Text = "Neu";
            NewProjectMenuItem.Click += NewProjectMenuItem_Click;
            // 
            // OpenToolStripMenuItem
            // 
            OpenToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { OpenFileStripMenuItem, toolStripSeparator1, OpenSampleProjectToolStripMenuItem, OpenRecentToolStripSeparator });
            OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            OpenToolStripMenuItem.Size = new Size(168, 22);
            OpenToolStripMenuItem.Text = "Öffnen";
            // 
            // OpenFileStripMenuItem
            // 
            OpenFileStripMenuItem.Name = "OpenFileStripMenuItem";
            OpenFileStripMenuItem.Size = new Size(114, 22);
            OpenFileStripMenuItem.Text = "Datei...";
            OpenFileStripMenuItem.Click += OpenFileToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(111, 6);
            // 
            // OpenSampleProjectToolStripMenuItem
            // 
            OpenSampleProjectToolStripMenuItem.Name = "OpenSampleProjectToolStripMenuItem";
            OpenSampleProjectToolStripMenuItem.Size = new Size(114, 22);
            OpenSampleProjectToolStripMenuItem.Text = "Beispiel";
            OpenSampleProjectToolStripMenuItem.Click += OpenSampleProjectToolStripMenuItem_Click;
            // 
            // OpenRecentToolStripSeparator
            // 
            OpenRecentToolStripSeparator.Name = "OpenRecentToolStripSeparator";
            OpenRecentToolStripSeparator.Size = new Size(111, 6);
            OpenRecentToolStripSeparator.Visible = false;
            // 
            // SaveToolStripMenuItem
            // 
            SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            SaveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            SaveToolStripMenuItem.Size = new Size(168, 22);
            SaveToolStripMenuItem.Text = "Speichern";
            SaveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(165, 6);
            // 
            // ExportToolStripMenuItem
            // 
            ExportToolStripMenuItem.Name = "ExportToolStripMenuItem";
            ExportToolStripMenuItem.Size = new Size(168, 22);
            ExportToolStripMenuItem.Text = "Export";
            // 
            // ImportToolStripMenuItem
            // 
            ImportToolStripMenuItem.Name = "ImportToolStripMenuItem";
            ImportToolStripMenuItem.Size = new Size(168, 22);
            ImportToolStripMenuItem.Text = "Import";
            // 
            // GroupManagerToolStripMenuItem
            // 
            GroupManagerToolStripMenuItem.Name = "GroupManagerToolStripMenuItem";
            GroupManagerToolStripMenuItem.Size = new Size(115, 23);
            GroupManagerToolStripMenuItem.Text = "Gruppen Manager";
            GroupManagerToolStripMenuItem.Click += GroupManagerToolStripMenuItem_Click;
            // 
            // AddCellsNumTextBox
            // 
            AddCellsNumTextBox.Name = "AddCellsNumTextBox";
            AddCellsNumTextBox.Size = new Size(50, 23);
            AddCellsNumTextBox.Text = "1";
            AddCellsNumTextBox.KeyPress += AddCellsNumTextBox_KeyPress;
            // 
            // AddCellsStripMenuItem
            // 
            AddCellsStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AddCellsStripMenuItem.Image = Properties.Resources.insert_row_1_16;
            AddCellsStripMenuItem.Name = "AddCellsStripMenuItem";
            AddCellsStripMenuItem.Size = new Size(28, 23);
            AddCellsStripMenuItem.Text = "toolStripMenuItem1";
            AddCellsStripMenuItem.Click += AddCellsToolStripMenuItem_Click;
            // 
            // DeleteCellsStripMenuItem
            // 
            DeleteCellsStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            DeleteCellsStripMenuItem.Image = Properties.Resources.times_square_regular;
            DeleteCellsStripMenuItem.Name = "DeleteCellsStripMenuItem";
            DeleteCellsStripMenuItem.Size = new Size(28, 23);
            DeleteCellsStripMenuItem.Text = "toolStripMenuItem2";
            DeleteCellsStripMenuItem.Click += DeleteCellsToolStripMenuItem_Click;
            // 
            // GroupsListBox
            // 
            GroupsListBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            GroupsListBox.ContextMenuStrip = GroupsListBoxContextMenu;
            GroupsListBox.FormattingEnabled = true;
            GroupsListBox.ItemHeight = 15;
            GroupsListBox.Location = new Point(12, 502);
            GroupsListBox.Name = "GroupsListBox";
            GroupsListBox.SelectionMode = SelectionMode.MultiExtended;
            GroupsListBox.Size = new Size(248, 274);
            GroupsListBox.TabIndex = 37;
            GroupsListBox.SelectedIndexChanged += GroupsListBox_SelectedIndexChanged;
            GroupsListBox.MouseDown += GroupsListBox_MouseDown;
            // 
            // GroupsListBoxContextMenu
            // 
            GroupsListBoxContextMenu.Items.AddRange(new ToolStripItem[] { NewEmptyGroupToolStripMenuItem, NewGroupToolStripMenuItem, EditGroupToolStripMenuItem, DeleteGroupToolStripMenuItem });
            GroupsListBoxContextMenu.Name = "GroupsListBoxContextMenu";
            GroupsListBoxContextMenu.Size = new Size(157, 92);
            // 
            // NewEmptyGroupToolStripMenuItem
            // 
            NewEmptyGroupToolStripMenuItem.Name = "NewEmptyGroupToolStripMenuItem";
            NewEmptyGroupToolStripMenuItem.Size = new Size(156, 22);
            NewEmptyGroupToolStripMenuItem.Text = "Neu (leer)";
            NewEmptyGroupToolStripMenuItem.Click += NewEmptyGroupToolStripMenuItem_Click;
            // 
            // NewGroupToolStripMenuItem
            // 
            NewGroupToolStripMenuItem.Name = "NewGroupToolStripMenuItem";
            NewGroupToolStripMenuItem.Size = new Size(156, 22);
            NewGroupToolStripMenuItem.Text = "Neu (Template)";
            NewGroupToolStripMenuItem.Click += NewGroupToolStripMenuItem_Click;
            // 
            // EditGroupToolStripMenuItem
            // 
            EditGroupToolStripMenuItem.Name = "EditGroupToolStripMenuItem";
            EditGroupToolStripMenuItem.Size = new Size(156, 22);
            EditGroupToolStripMenuItem.Text = "Bearbeiten";
            EditGroupToolStripMenuItem.Click += EditGroupToolStripMenuItem_Click;
            // 
            // DeleteGroupToolStripMenuItem
            // 
            DeleteGroupToolStripMenuItem.Name = "DeleteGroupToolStripMenuItem";
            DeleteGroupToolStripMenuItem.Size = new Size(156, 22);
            DeleteGroupToolStripMenuItem.Text = "Löschen";
            DeleteGroupToolStripMenuItem.Click += DeleteroupToolStripMenuItem_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(12, 475);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 38;
            label3.Text = "Gruppen";
            // 
            // UnselectGroupsButton
            // 
            UnselectGroupsButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            UnselectGroupsButton.BackgroundImageLayout = ImageLayout.Zoom;
            UnselectGroupsButton.FlatStyle = FlatStyle.Flat;
            UnselectGroupsButton.ImageAlign = ContentAlignment.TopCenter;
            UnselectGroupsButton.Location = new Point(1236, 30);
            UnselectGroupsButton.Name = "UnselectGroupsButton";
            UnselectGroupsButton.Size = new Size(82, 29);
            UnselectGroupsButton.TabIndex = 39;
            UnselectGroupsButton.Text = "Clear Filter";
            UnselectGroupsButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            UnselectGroupsButton.UseVisualStyleBackColor = true;
            UnselectGroupsButton.Click += UnselectGroupsButton_Click;
            // 
            // GADataTableBackPanel
            // 
            GADataTableBackPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GADataTableBackPanel.BackColor = Color.Transparent;
            GADataTableBackPanel.Controls.Add(GADataTable);
            GADataTableBackPanel.Location = new Point(266, 63);
            GADataTableBackPanel.Margin = new Padding(30);
            GADataTableBackPanel.Name = "GADataTableBackPanel";
            GADataTableBackPanel.Padding = new Padding(4);
            GADataTableBackPanel.Size = new Size(1056, 717);
            GADataTableBackPanel.TabIndex = 40;
            // 
            // GaDataGridContextMenu
            // 
            GaDataGridContextMenu.Items.AddRange(new ToolStripItem[] { testToolStripMenuItem, toolStripSeparator2 });
            GaDataGridContextMenu.Name = "GaDataGridContextMenu";
            GaDataGridContextMenu.ShowImageMargin = false;
            GaDataGridContextMenu.Size = new Size(172, 32);
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.Enabled = false;
            testToolStripMenuItem.Name = "testToolStripMenuItem";
            testToolStripMenuItem.Size = new Size(171, 22);
            testToolStripMenuItem.Text = "Zu Gruppe hinzufügen:";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(168, 6);
            // 
            // FilterWithoutGroupButton
            // 
            FilterWithoutGroupButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            FilterWithoutGroupButton.BackgroundImageLayout = ImageLayout.Zoom;
            FilterWithoutGroupButton.FlatStyle = FlatStyle.Flat;
            FilterWithoutGroupButton.ImageAlign = ContentAlignment.TopCenter;
            FilterWithoutGroupButton.Location = new Point(1136, 30);
            FilterWithoutGroupButton.Margin = new Padding(2);
            FilterWithoutGroupButton.Name = "FilterWithoutGroupButton";
            FilterWithoutGroupButton.Size = new Size(95, 29);
            FilterWithoutGroupButton.TabIndex = 41;
            FilterWithoutGroupButton.Text = "Ohne Gruppe";
            FilterWithoutGroupButton.UseVisualStyleBackColor = true;
            FilterWithoutGroupButton.Click += FilterWithoutGroupButton_Click;
            // 
            // FilterTextBox
            // 
            FilterTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            FilterTextBox.Location = new Point(829, 34);
            FilterTextBox.Name = "FilterTextBox";
            FilterTextBox.Size = new Size(302, 23);
            FilterTextBox.TabIndex = 42;
            FilterTextBox.KeyUp += FilterTextBox_KeyUp;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1324, 807);
            Controls.Add(FilterTextBox);
            Controls.Add(FilterWithoutGroupButton);
            Controls.Add(GADataTableBackPanel);
            Controls.Add(UnselectGroupsButton);
            Controls.Add(label3);
            Controls.Add(GroupsListBox);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(MainGroupsListBox);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Gruppenadressen Manager";
            MainGroupsListBoxContextMenu.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GADataTable).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            GroupsListBoxContextMenu.ResumeLayout(false);
            GADataTableBackPanel.ResumeLayout(false);
            GaDataGridContextMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox MainGroupsListBox;
        private Label label1;

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel StatusInfoLabel;

        private GADataGridView GADataTable;
        private Label label2;
        private ContextMenuStrip MainGroupsListBoxContextMenu;
        private ToolStripMenuItem AddMainGroupToolStripMenuItem;
        private ToolStripMenuItem EditMainGroupToolStripMenuItem;
        private ToolStripMenuItem DeleteMainGroupToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem OpenToolStripMenuItem;
        private ToolStripMenuItem SaveToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem ExportToolStripMenuItem;
        private ToolStripMenuItem ImportToolStripMenuItem;
        private ToolStripMenuItem GroupManagerToolStripMenuItem;
        private ToolStripMenuItem OpenSampleProjectToolStripMenuItem;
        private ToolStripSeparator OpenRecentToolStripSeparator;
        private ToolStripTextBox AddCellsNumTextBox;
        private ToolStripMenuItem AddCellsStripMenuItem;
        private ToolStripMenuItem DeleteCellsStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem OpenFileStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem NewProjectMenuItem;
        private ListBox GroupsListBox;
        private Label label3;
        private Button UnselectGroupsButton;
        private Panel GADataTableBackPanel;
        private ContextMenuStrip GroupsListBoxContextMenu;
        private ToolStripMenuItem NewGroupToolStripMenuItem;
        private ToolStripMenuItem EditGroupToolStripMenuItem;
        private ToolStripMenuItem DeleteGroupToolStripMenuItem;
        private ToolStripMenuItem NewEmptyGroupToolStripMenuItem;
        private ContextMenuStrip GaDataGridContextMenu;
        private ToolStripMenuItem testToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private Button FilterWithoutGroupButton;
        private TextBox FilterTextBox;
    }
}
