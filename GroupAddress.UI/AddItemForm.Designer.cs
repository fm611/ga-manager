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
            textBox1 = new TextBox();
            AddItemButton = new Button();
            ItemTemplatesListBox = new ListBox();
            MainGroupsListBox = new ListBox();
            label1 = new Label();
            ItemPartTemplatesListBox = new ListBox();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)GADataTable).BeginInit();
            SuspendLayout();
            // 
            // GADataTable
            // 
            GADataTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GADataTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = Color.YellowGreen;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            GADataTable.DefaultCellStyle = dataGridViewCellStyle1;
            GADataTable.Location = new Point(254, 56);
            GADataTable.Name = "GADataTable";
            GADataTable.ReadOnly = true;
            GADataTable.RowHeadersVisible = false;
            GADataTable.SelectionMode = DataGridViewSelectionMode.CellSelect;
            GADataTable.ShowEditingIcon = false;
            GADataTable.Size = new Size(1062, 695);
            GADataTable.TabIndex = 39;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            textBox1.Location = new Point(982, 769);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(254, 23);
            textBox1.TabIndex = 37;
            // 
            // AddItemButton
            // 
            AddItemButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            AddItemButton.Location = new Point(1242, 769);
            AddItemButton.Name = "AddItemButton";
            AddItemButton.Size = new Size(75, 23);
            AddItemButton.TabIndex = 38;
            AddItemButton.Text = "Add Item";
            AddItemButton.UseVisualStyleBackColor = true;
            // 
            // ItemTemplatesListBox
            // 
            ItemTemplatesListBox.FormattingEnabled = true;
            ItemTemplatesListBox.ItemHeight = 15;
            ItemTemplatesListBox.Location = new Point(12, 56);
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
            MainGroupsListBox.Size = new Size(207, 814);
            MainGroupsListBox.TabIndex = 35;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 32);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 40;
            label1.Text = "Templates";
            // 
            // ItemPartTemplatesListBox
            // 
            ItemPartTemplatesListBox.FormattingEnabled = true;
            ItemPartTemplatesListBox.ItemHeight = 15;
            ItemPartTemplatesListBox.Location = new Point(12, 268);
            ItemPartTemplatesListBox.Name = "ItemPartTemplatesListBox";
            ItemPartTemplatesListBox.Size = new Size(236, 169);
            ItemPartTemplatesListBox.TabIndex = 36;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 239);
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
            // AddItemForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1328, 804);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(GADataTable);
            Controls.Add(textBox1);
            Controls.Add(AddItemButton);
            Controls.Add(ItemPartTemplatesListBox);
            Controls.Add(ItemTemplatesListBox);
            Controls.Add(MainGroupsListBox);
            Name = "AddItemForm";
            Text = "AddItemForm";
            Load += AddItemForm_Load;
            ((System.ComponentModel.ISupportInitialize)GADataTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView GADataTable;
        private TextBox textBox1;
        private Button AddItemButton;
        private ListBox ItemTemplatesListBox;
        private ListBox MainGroupsListBox;
        private Label label1;
        private ListBox ItemPartTemplatesListBox;
        private Label label2;
        private Label label3;
    }
}