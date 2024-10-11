namespace GroupAddress.UI
{
    partial class Form1
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            MainGroupsListBox = new ListBox();
            label1 = new Label();
            AddMainGroupButton = new Button();
            AddMainGroupIdTextBox = new TextBox();
            AddMainGroupNameTextBox = new TextBox();
            DeleteMainGroupButton = new Button();
            statusStrip1 = new StatusStrip();
            StatusInfoLabel = new ToolStripStatusLabel();
            EditMainGroupButton = new Button();
            GADataTable = new DataGridView();
            mainGroupBindingSource = new BindingSource(components);
            toolStrip1 = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            toolStripButton3 = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            AddItemButton = new ToolStripButton();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GADataTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainGroupBindingSource).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // MainGroupsListBox
            // 
            MainGroupsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            MainGroupsListBox.FormattingEnabled = true;
            MainGroupsListBox.ItemHeight = 15;
            MainGroupsListBox.Location = new Point(12, 67);
            MainGroupsListBox.Name = "MainGroupsListBox";
            MainGroupsListBox.Size = new Size(207, 484);
            MainGroupsListBox.TabIndex = 0;
            MainGroupsListBox.SelectedIndexChanged += MainGroupsListBox_SelectedIndexChanged;
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
            // AddMainGroupButton
            // 
            AddMainGroupButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AddMainGroupButton.Location = new Point(144, 589);
            AddMainGroupButton.Name = "AddMainGroupButton";
            AddMainGroupButton.Size = new Size(75, 23);
            AddMainGroupButton.TabIndex = 4;
            AddMainGroupButton.Text = "Add";
            AddMainGroupButton.UseVisualStyleBackColor = true;
            AddMainGroupButton.Click += AddMainGroupButton_Click;
            // 
            // AddMainGroupIdTextBox
            // 
            AddMainGroupIdTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AddMainGroupIdTextBox.Location = new Point(12, 560);
            AddMainGroupIdTextBox.Name = "AddMainGroupIdTextBox";
            AddMainGroupIdTextBox.Size = new Size(41, 23);
            AddMainGroupIdTextBox.TabIndex = 1;
            AddMainGroupIdTextBox.TextChanged += AddMainGroupIdTextBox_TextChanged;
            AddMainGroupIdTextBox.Enter += AddMainGroupIdTextBox_Enter;
            // 
            // AddMainGroupNameTextBox
            // 
            AddMainGroupNameTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AddMainGroupNameTextBox.Location = new Point(59, 560);
            AddMainGroupNameTextBox.Name = "AddMainGroupNameTextBox";
            AddMainGroupNameTextBox.Size = new Size(160, 23);
            AddMainGroupNameTextBox.TabIndex = 2;
            AddMainGroupNameTextBox.Enter += AddMainGroupNameTextBox_Enter;
            AddMainGroupNameTextBox.KeyUp += AddMainGroupNameTextBox_KeyUp;
            // 
            // DeleteMainGroupButton
            // 
            DeleteMainGroupButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DeleteMainGroupButton.Location = new Point(144, 618);
            DeleteMainGroupButton.Name = "DeleteMainGroupButton";
            DeleteMainGroupButton.Size = new Size(75, 23);
            DeleteMainGroupButton.TabIndex = 5;
            DeleteMainGroupButton.Text = "Delete";
            DeleteMainGroupButton.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { StatusInfoLabel });
            statusStrip1.Location = new Point(0, 781);
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
            // EditMainGroupButton
            // 
            EditMainGroupButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            EditMainGroupButton.Enabled = false;
            EditMainGroupButton.Location = new Point(63, 589);
            EditMainGroupButton.Name = "EditMainGroupButton";
            EditMainGroupButton.Size = new Size(75, 23);
            EditMainGroupButton.TabIndex = 4;
            EditMainGroupButton.Text = "Edit";
            EditMainGroupButton.UseVisualStyleBackColor = true;
            EditMainGroupButton.Click += EditMainGroupButton_Click;
            // 
            // GADataTable
            // 
            GADataTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GADataTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.YellowGreen;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            GADataTable.DefaultCellStyle = dataGridViewCellStyle2;
            GADataTable.Location = new Point(225, 67);
            GADataTable.Name = "GADataTable";
            GADataTable.ReadOnly = true;
            GADataTable.RowHeadersVisible = false;
            GADataTable.SelectionMode = DataGridViewSelectionMode.CellSelect;
            GADataTable.ShowEditingIcon = false;
            GADataTable.Size = new Size(1087, 484);
            GADataTable.TabIndex = 34;
            // 
            // mainGroupBindingSource
            // 
            mainGroupBindingSource.DataSource = typeof(Core.MainGroup);
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripButton3, toolStripSeparator1, AddItemButton });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1324, 25);
            toolStrip1.TabIndex = 35;
            toolStrip1.Text = "toolStrip1";
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
            AddItemButton.Click += this.AddItemButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1324, 803);
            Controls.Add(toolStrip1);
            Controls.Add(GADataTable);
            Controls.Add(statusStrip1);
            Controls.Add(AddMainGroupNameTextBox);
            Controls.Add(AddMainGroupIdTextBox);
            Controls.Add(DeleteMainGroupButton);
            Controls.Add(EditMainGroupButton);
            Controls.Add(AddMainGroupButton);
            Controls.Add(label1);
            Controls.Add(MainGroupsListBox);
            Name = "Form1";
            Text = "Form1";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GADataTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainGroupBindingSource).EndInit();
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
        private Button AddMainGroupButton;
        private TextBox AddMainGroupIdTextBox;
        private TextBox AddMainGroupNameTextBox;
        private Button DeleteMainGroupButton;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel StatusInfoLabel;
        private Button EditMainGroupButton;
        private DataGridView GADataTable;
        private BindingSource mainGroupBindingSource;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton3;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton AddItemButton;
    }
}
