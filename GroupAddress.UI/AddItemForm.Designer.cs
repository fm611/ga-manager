namespace GroupAddress.UI
{
    partial class AddItemForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            GADataTable = new DataGridView();
            NewItemPreStringTextBox = new TextBox();
            AddItemButton = new Button();
            ItemTemplatesListBox = new ListBox();
            MainGroupsListBox = new ListBox();
            label1 = new Label();
            ItemPartTemplatesListBox = new ListBox();
            label2 = new Label();
            label3 = new Label();
            ItemPartTemplatesDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)GADataTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemPartTemplatesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // GADataTable
            // 
            GADataTable.AllowUserToAddRows = false;
            GADataTable.AllowUserToDeleteRows = false;
            GADataTable.AllowUserToResizeRows = false;
            GADataTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GADataTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = Color.GreenYellow;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            GADataTable.DefaultCellStyle = dataGridViewCellStyle1;
            GADataTable.Location = new Point(254, 36);
            GADataTable.Name = "GADataTable";
            GADataTable.ReadOnly = true;
            GADataTable.RowHeadersVisible = false;
            GADataTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GADataTable.ShowEditingIcon = false;
            GADataTable.Size = new Size(911, 483);
            GADataTable.TabIndex = 39;
            // 
            // NewItemPreStringTextBox
            // 
            NewItemPreStringTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            NewItemPreStringTextBox.Location = new Point(831, 537);
            NewItemPreStringTextBox.Name = "NewItemPreStringTextBox";
            NewItemPreStringTextBox.Size = new Size(254, 23);
            NewItemPreStringTextBox.TabIndex = 37;
            // 
            // AddItemButton
            // 
            AddItemButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            AddItemButton.Location = new Point(1091, 537);
            AddItemButton.Name = "AddItemButton";
            AddItemButton.Size = new Size(75, 23);
            AddItemButton.TabIndex = 38;
            AddItemButton.Text = "Add Item";
            AddItemButton.UseVisualStyleBackColor = true;
            AddItemButton.Click += AddItemButton_Click;
            // 
            // ItemTemplatesListBox
            // 
            ItemTemplatesListBox.FormattingEnabled = true;
            ItemTemplatesListBox.ItemHeight = 15;
            ItemTemplatesListBox.Location = new Point(12, 36);
            ItemTemplatesListBox.Name = "ItemTemplatesListBox";
            ItemTemplatesListBox.Size = new Size(236, 169);
            ItemTemplatesListBox.TabIndex = 36;
            ItemTemplatesListBox.SelectedIndexChanged += ItemTemplatesListBox_SelectedIndexChanged;
            // 
            // MainGroupsListBox
            // 
            MainGroupsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            MainGroupsListBox.FormattingEnabled = true;
            MainGroupsListBox.ItemHeight = 15;
            MainGroupsListBox.Location = new Point(-250, -128);
            MainGroupsListBox.Name = "MainGroupsListBox";
            MainGroupsListBox.Size = new Size(207, 559);
            MainGroupsListBox.TabIndex = 35;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 40;
            label1.Text = "Templates";
            // 
            // ItemPartTemplatesListBox
            // 
            ItemPartTemplatesListBox.FormattingEnabled = true;
            ItemPartTemplatesListBox.ItemHeight = 15;
            ItemPartTemplatesListBox.Location = new Point(12, 248);
            ItemPartTemplatesListBox.Name = "ItemPartTemplatesListBox";
            ItemPartTemplatesListBox.Size = new Size(236, 79);
            ItemPartTemplatesListBox.TabIndex = 36;
            ItemPartTemplatesListBox.SelectedIndexChanged += ItemPartTemplatesListBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 219);
            label2.Name = "label2";
            label2.Size = new Size(122, 15);
            label2.TabIndex = 40;
            label2.Text = "Parts (Hauptgruppen)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(877, 773);
            label3.Name = "label3";
            label3.Size = new Size(80, 15);
            label3.TabIndex = 40;
            label3.Text = "Name / Prefix";
            // 
            // ItemPartTemplatesDataGridView
            // 
            ItemPartTemplatesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ItemPartTemplatesDataGridView.Location = new Point(12, 359);
            ItemPartTemplatesDataGridView.Name = "ItemPartTemplatesDataGridView";
            ItemPartTemplatesDataGridView.Size = new Size(177, 122);
            ItemPartTemplatesDataGridView.TabIndex = 41;
            // 
            // AddItemForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1177, 572);
            Controls.Add(ItemPartTemplatesDataGridView);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(GADataTable);
            Controls.Add(NewItemPreStringTextBox);
            Controls.Add(AddItemButton);
            Controls.Add(ItemPartTemplatesListBox);
            Controls.Add(ItemTemplatesListBox);
            Controls.Add(MainGroupsListBox);
            Name = "AddItemForm";
            Text = "AddItemForm";
            Load += AddItemForm_Load;
            ((System.ComponentModel.ISupportInitialize)GADataTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemPartTemplatesDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView GADataTable;
        private TextBox NewItemPreStringTextBox;
        private Button AddItemButton;
        private ListBox ItemTemplatesListBox;
        private ListBox MainGroupsListBox;
        private Label label1;
        private ListBox ItemPartTemplatesListBox;
        private Label label2;
        private Label label3;
        private DataGridView ItemPartTemplatesDataGridView;
    }
}