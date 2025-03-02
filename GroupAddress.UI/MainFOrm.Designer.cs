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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
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
            ItemManagerToolStripMenuItem = new ToolStripMenuItem();
            AddCellsNumTextBox = new ToolStripTextBox();
            AddCellsStripMenuItem = new ToolStripMenuItem();
            DeleteCellsStripMenuItem = new ToolStripMenuItem();
            saveFileDialog1 = new SaveFileDialog();
            ItemsListBox = new ListBox();
            ItemsListBoxContextMenu = new ContextMenuStrip(components);
            NewItemToolStripMenuItem = new ToolStripMenuItem();
            EditItemToolStripMenuItem = new ToolStripMenuItem();
            DeleteItemToolStripMenuItem = new ToolStripMenuItem();
            label3 = new Label();
            UnselectItemsButton = new Button();
            GADataTableBackPanel = new Panel();
            NewEmptyItemToolStripMenuItem = new ToolStripMenuItem();
            MainGroupsListBoxContextMenu.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GADataTable).BeginInit();
            menuStrip1.SuspendLayout();
            ItemsListBoxContextMenu.SuspendLayout();
            GADataTableBackPanel.SuspendLayout();
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
            GADataTable.Location = new Point(4, 4);
            GADataTable.Name = "GADataTable";
            GADataTable.RowData = null;
            GADataTable.ShowAllRows = true;
            GADataTable.ShowEditingIcon = false;
            GADataTable.ShowEmptyRows = true;
            GADataTable.Size = new Size(1048, 709);
            GADataTable.TabIndex = 34;
            GADataTable.TopLevelCollection = null;
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
            menuStrip1.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem, ItemManagerToolStripMenuItem, AddCellsNumTextBox, AddCellsStripMenuItem, DeleteCellsStripMenuItem });
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
            // ItemManagerToolStripMenuItem
            // 
            ItemManagerToolStripMenuItem.Name = "ItemManagerToolStripMenuItem";
            ItemManagerToolStripMenuItem.Size = new Size(93, 23);
            ItemManagerToolStripMenuItem.Text = "Item Manager";
            ItemManagerToolStripMenuItem.Click += ItemManagerToolStripMenuItem_Click;
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
            // ItemsListBox
            // 
            ItemsListBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ItemsListBox.ContextMenuStrip = ItemsListBoxContextMenu;
            ItemsListBox.FormattingEnabled = true;
            ItemsListBox.ItemHeight = 15;
            ItemsListBox.Location = new Point(12, 502);
            ItemsListBox.Name = "ItemsListBox";
            ItemsListBox.SelectionMode = SelectionMode.MultiExtended;
            ItemsListBox.Size = new Size(248, 274);
            ItemsListBox.TabIndex = 37;
            ItemsListBox.SelectedIndexChanged += ItemsListBox_SelectedIndexChanged;
            ItemsListBox.MouseDown += ItemsListBox_MouseDown;
            // 
            // ItemsListBoxContextMenu
            // 
            ItemsListBoxContextMenu.Items.AddRange(new ToolStripItem[] { NewEmptyItemToolStripMenuItem, NewItemToolStripMenuItem, EditItemToolStripMenuItem, DeleteItemToolStripMenuItem });
            ItemsListBoxContextMenu.Name = "ItemsListBoxContextMenu";
            ItemsListBoxContextMenu.Size = new Size(181, 114);
            // 
            // NewItemToolStripMenuItem
            // 
            NewItemToolStripMenuItem.Name = "NewItemToolStripMenuItem";
            NewItemToolStripMenuItem.Size = new Size(180, 22);
            NewItemToolStripMenuItem.Text = "Neu (Template)";
            NewItemToolStripMenuItem.Click += NewItemToolStripMenuItem_Click;
            // 
            // EditItemToolStripMenuItem
            // 
            EditItemToolStripMenuItem.Name = "EditItemToolStripMenuItem";
            EditItemToolStripMenuItem.Size = new Size(180, 22);
            EditItemToolStripMenuItem.Text = "Bearbeiten";
            EditItemToolStripMenuItem.Click += EditItemToolStripMenuItem_Click;
            // 
            // DeleteItemToolStripMenuItem
            // 
            DeleteItemToolStripMenuItem.Name = "DeleteItemToolStripMenuItem";
            DeleteItemToolStripMenuItem.Size = new Size(180, 22);
            DeleteItemToolStripMenuItem.Text = "Löschen";
            DeleteItemToolStripMenuItem.Click += DeleteItemToolStripMenuItem_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(12, 475);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 38;
            label3.Text = "Items";
            // 
            // UnselectItemsButton
            // 
            UnselectItemsButton.BackgroundImageLayout = ImageLayout.Zoom;
            UnselectItemsButton.FlatStyle = FlatStyle.Flat;
            UnselectItemsButton.ImageAlign = ContentAlignment.TopCenter;
            UnselectItemsButton.Location = new Point(178, 467);
            UnselectItemsButton.Name = "UnselectItemsButton";
            UnselectItemsButton.Size = new Size(82, 29);
            UnselectItemsButton.TabIndex = 39;
            UnselectItemsButton.Text = "Clear Filter";
            UnselectItemsButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            UnselectItemsButton.UseVisualStyleBackColor = true;
            UnselectItemsButton.Click += UnselectItemsButton_Click;
            // 
            // GADataTableBackPanel
            // 
            GADataTableBackPanel.BackColor = Color.Transparent;
            GADataTableBackPanel.Controls.Add(GADataTable);
            GADataTableBackPanel.Location = new Point(266, 63);
            GADataTableBackPanel.Margin = new Padding(30);
            GADataTableBackPanel.Name = "GADataTableBackPanel";
            GADataTableBackPanel.Padding = new Padding(4);
            GADataTableBackPanel.Size = new Size(1056, 717);
            GADataTableBackPanel.TabIndex = 40;
            // 
            // NewEmptyItemToolStripMenuItem
            // 
            NewEmptyItemToolStripMenuItem.Name = "NewEmptyItemToolStripMenuItem";
            NewEmptyItemToolStripMenuItem.Size = new Size(180, 22);
            NewEmptyItemToolStripMenuItem.Text = "Neu (leer)";
            NewEmptyItemToolStripMenuItem.Click += NewEmptyItemToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1324, 807);
            Controls.Add(GADataTableBackPanel);
            Controls.Add(UnselectItemsButton);
            Controls.Add(label3);
            Controls.Add(ItemsListBox);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(MainGroupsListBox);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Form1";
            MainGroupsListBoxContextMenu.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GADataTable).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ItemsListBoxContextMenu.ResumeLayout(false);
            GADataTableBackPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox MainGroupsListBox;
        private Label label1;
        private TextBox textBox1;
        private Label label4;

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
        private ToolStripMenuItem ItemManagerToolStripMenuItem;
        private ToolStripMenuItem OpenSampleProjectToolStripMenuItem;
        private ToolStripSeparator OpenRecentToolStripSeparator;
        private ToolStripTextBox AddCellsNumTextBox;
        private ToolStripMenuItem AddCellsStripMenuItem;
        private ToolStripMenuItem DeleteCellsStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem OpenFileStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem NewProjectMenuItem;
        private ListBox ItemsListBox;
        private Label label3;
        private Button UnselectItemsButton;
        private Panel GADataTableBackPanel;
        private ContextMenuStrip ItemsListBoxContextMenu;
        private ToolStripMenuItem NewItemToolStripMenuItem;
        private ToolStripMenuItem EditItemToolStripMenuItem;
        private ToolStripMenuItem DeleteItemToolStripMenuItem;
        private ToolStripMenuItem NewEmptyItemToolStripMenuItem;
    }
}
