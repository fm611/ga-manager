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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            MainGroupsListBox = new ListBox();
            MainGroupsListBoxContextMenu = new ContextMenuStrip(components);
            AddMainGroupToolStripMenuItem = new ToolStripMenuItem();
            EditMainGroupToolStripMenuItem = new ToolStripMenuItem();
            löschenToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            statusStrip1 = new StatusStrip();
            StatusInfoLabel = new ToolStripStatusLabel();
            GADataTable = new GADataGridView();
            label2 = new Label();
            toolStripButton1 = new ToolStripButton();
            toolStripButton3 = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            AddItemButton = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            AddRowNumTextBox = new ToolStripTextBox();
            AddRowButton = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            DeleteCellsButton = new ToolStripButton();
            toolStrip1 = new ToolStrip();
            MainGroupsListBoxContextMenu.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GADataTable).BeginInit();
            toolStrip1.SuspendLayout();
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
            MainGroupsListBox.Size = new Size(248, 439);
            MainGroupsListBox.TabIndex = 0;
            MainGroupsListBox.SelectedIndexChanged += MainGroupsListBox_SelectedIndexChanged;
            // 
            // MainGroupsListBoxContextMenu
            // 
            MainGroupsListBoxContextMenu.Items.AddRange(new ToolStripItem[] { AddMainGroupToolStripMenuItem, EditMainGroupToolStripMenuItem, löschenToolStripMenuItem });
            MainGroupsListBoxContextMenu.Name = "contextMenuStrip1";
            MainGroupsListBoxContextMenu.Size = new Size(131, 70);
            // 
            // AddMainGroupToolStripMenuItem
            // 
            AddMainGroupToolStripMenuItem.Name = "AddMainGroupToolStripMenuItem";
            AddMainGroupToolStripMenuItem.Size = new Size(130, 22);
            AddMainGroupToolStripMenuItem.Text = "Neu";
            AddMainGroupToolStripMenuItem.Click += AddMainGroupToolStripMenuItem_Click;
            // 
            // EditMainGroupToolStripMenuItem
            // 
            EditMainGroupToolStripMenuItem.Name = "EditMainGroupToolStripMenuItem";
            EditMainGroupToolStripMenuItem.Size = new Size(130, 22);
            EditMainGroupToolStripMenuItem.Text = "Bearbeiten";
            EditMainGroupToolStripMenuItem.Click += EditMainGroupToolStripMenuItem_Click;
            // 
            // löschenToolStripMenuItem
            // 
            löschenToolStripMenuItem.Name = "löschenToolStripMenuItem";
            löschenToolStripMenuItem.Size = new Size(130, 22);
            löschenToolStripMenuItem.Text = "Löschen";
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
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
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
            GADataTable.Location = new Point(266, 67);
            GADataTable.MainGroup = null;
            GADataTable.Name = "GADataTable";
            GADataTable.RowData = null;
            GADataTable.ShowAllRows = true;
            GADataTable.ShowEditingIcon = false;
            GADataTable.ShowEmptyRows = true;
            GADataTable.Size = new Size(1046, 488);
            GADataTable.TabIndex = 34;
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
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(37, 22);
            toolStripButton1.Text = "Load";
            toolStripButton1.TextImageRelation = TextImageRelation.TextBeforeImage;
            toolStripButton1.Click += LoadButton_Click;
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(35, 22);
            toolStripButton3.Text = "Save";
            toolStripButton3.Click += SaveButton_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // AddItemButton
            // 
            AddItemButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            AddItemButton.Image = (Image)resources.GetObject("AddItemButton.Image");
            AddItemButton.ImageTransparentColor = Color.Magenta;
            AddItemButton.Name = "AddItemButton";
            AddItemButton.Size = new Size(60, 22);
            AddItemButton.Text = "Add Item";
            AddItemButton.Click += AddItemButton_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // AddRowNumTextBox
            // 
            AddRowNumTextBox.Name = "AddRowNumTextBox";
            AddRowNumTextBox.Size = new Size(50, 25);
            AddRowNumTextBox.Text = "1";
            AddRowNumTextBox.KeyPress += AddRowNumTextBox_KeyPress;
            // 
            // AddRowButton
            // 
            AddRowButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AddRowButton.Image = (Image)resources.GetObject("AddRowButton.Image");
            AddRowButton.ImageTransparentColor = Color.Magenta;
            AddRowButton.Name = "AddRowButton";
            AddRowButton.Size = new Size(23, 22);
            AddRowButton.Text = "Zeile einfügen";
            AddRowButton.Click += AddRowButton_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // DeleteCellsButton
            // 
            DeleteCellsButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            DeleteCellsButton.Image = (Image)resources.GetObject("DeleteCellsButton.Image");
            DeleteCellsButton.ImageTransparentColor = Color.Magenta;
            DeleteCellsButton.Name = "DeleteCellsButton";
            DeleteCellsButton.Size = new Size(23, 22);
            DeleteCellsButton.Text = "Zellen löschen";
            DeleteCellsButton.Click += DeleteCellsButton_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripButton3, toolStripSeparator1, AddItemButton, toolStripSeparator2, AddRowNumTextBox, AddRowButton, toolStripSeparator3, DeleteCellsButton });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1324, 25);
            toolStrip1.TabIndex = 35;
            toolStrip1.Text = "toolStrip1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1324, 807);
            Controls.Add(toolStrip1);
            Controls.Add(GADataTable);
            Controls.Add(statusStrip1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(MainGroupsListBox);
            Name = "MainForm";
            Text = "Form1";
            Load += Form1_Load;
            MainGroupsListBoxContextMenu.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GADataTable).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
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
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton3;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton AddItemButton;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripTextBox AddRowNumTextBox;
        private ToolStripButton AddRowButton;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton DeleteCellsButton;
        private ToolStrip toolStrip1;
        private ContextMenuStrip MainGroupsListBoxContextMenu;
        private ToolStripMenuItem AddMainGroupToolStripMenuItem;
        private ToolStripMenuItem EditMainGroupToolStripMenuItem;
        private ToolStripMenuItem löschenToolStripMenuItem;
    }
}
