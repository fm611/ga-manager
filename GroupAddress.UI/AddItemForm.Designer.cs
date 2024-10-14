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
            label1 = new Label();
            label3 = new Label();
            MainGroupsListBox = new ListBox();
            label2 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)GADataTable).BeginInit();
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
            GADataTable.Size = new Size(911, 524);
            GADataTable.TabIndex = 39;
            // 
            // NewItemPreStringTextBox
            // 
            NewItemPreStringTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            NewItemPreStringTextBox.Location = new Point(12, 275);
            NewItemPreStringTextBox.Name = "NewItemPreStringTextBox";
            NewItemPreStringTextBox.Size = new Size(236, 23);
            NewItemPreStringTextBox.TabIndex = 37;
            // 
            // AddItemButton
            // 
            AddItemButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AddItemButton.Location = new Point(173, 537);
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
            ItemTemplatesListBox.Size = new Size(236, 199);
            ItemTemplatesListBox.TabIndex = 36;
            ItemTemplatesListBox.SelectedIndexChanged += ItemTemplatesListBox_SelectedIndexChanged;
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(877, 773);
            label3.Name = "label3";
            label3.Size = new Size(80, 15);
            label3.TabIndex = 40;
            label3.Text = "Name / Prefix";
            // 
            // MainGroupsListBox
            // 
            MainGroupsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            MainGroupsListBox.FormattingEnabled = true;
            MainGroupsListBox.ItemHeight = 15;
            MainGroupsListBox.Location = new Point(12, 321);
            MainGroupsListBox.Name = "MainGroupsListBox";
            MainGroupsListBox.Size = new Size(236, 214);
            MainGroupsListBox.TabIndex = 36;
            MainGroupsListBox.SelectedIndexChanged += MainGroupsListBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 257);
            label2.Name = "label2";
            label2.Size = new Size(125, 15);
            label2.TabIndex = 40;
            label2.Text = "Item Name (PreString)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 301);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 40;
            label4.Text = "Hauptgruppe";
            // 
            // AddItemForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1177, 572);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(GADataTable);
            Controls.Add(NewItemPreStringTextBox);
            Controls.Add(AddItemButton);
            Controls.Add(MainGroupsListBox);
            Controls.Add(ItemTemplatesListBox);
            Name = "AddItemForm";
            Text = "AddItemForm";
            Load += AddItemForm_Load;
            ((System.ComponentModel.ISupportInitialize)GADataTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView GADataTable;
        private TextBox NewItemPreStringTextBox;
        private Button AddItemButton;
        private ListBox ItemTemplatesListBox;
        private Label label1;
        private Label label3;
        private ListBox MainGroupsListBox;
        private Label label2;
        private Label label4;
    }
}