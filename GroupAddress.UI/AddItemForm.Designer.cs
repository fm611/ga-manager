using System.Windows.Forms;

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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddItemForm));
            GADataTable = new GADataGridView();
            NewItemPreStringTextBox = new TextBox();
            AddItemButton = new Button();
            ItemTemplatesListBox = new ListBox();
            label1 = new Label();
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
            ((System.ComponentModel.ISupportInitialize)GADataTable).BeginInit();
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
            GADataTable.Location = new Point(254, 36);
            GADataTable.Name = "GADataTable";
            GADataTable.RowData = null;
            GADataTable.ShowAllRows = true;
            GADataTable.ShowEditingIcon = false;
            GADataTable.ShowEmptyRows = true;
            GADataTable.Size = new Size(1046, 488);
            GADataTable.TabIndex = 34;
            GADataTable.TopLevelCollection = null;
            // 
            // NewItemPreStringTextBox
            // 
            NewItemPreStringTextBox.Location = new Point(12, 275);
            NewItemPreStringTextBox.Name = "NewItemPreStringTextBox";
            NewItemPreStringTextBox.Size = new Size(236, 23);
            NewItemPreStringTextBox.TabIndex = 37;
            NewItemPreStringTextBox.Enter += NewItemPreStringTextBox_Enter;
            NewItemPreStringTextBox.KeyUp += NewItemPreStringTextBox_KeyUp;
            // 
            // AddItemButton
            // 
            AddItemButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AddItemButton.Location = new Point(173, 712);
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
            MainGroupsListBox.Size = new Size(236, 244);
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
            // InsertAtRadioButton
            // 
            InsertAtRadioButton.AutoSize = true;
            InsertAtRadioButton.Location = new Point(19, 630);
            InsertAtRadioButton.Name = "InsertAtRadioButton";
            InsertAtRadioButton.Size = new Size(40, 19);
            InsertAtRadioButton.TabIndex = 41;
            InsertAtRadioButton.TabStop = true;
            InsertAtRadioButton.Text = "At:";
            InsertAtRadioButton.UseVisualStyleBackColor = true;
            InsertAtRadioButton.CheckedChanged += InsertAtRadioButton_CheckedChanged;
            // 
            // InsertNextFreeRadioButton
            // 
            InsertNextFreeRadioButton.AutoSize = true;
            InsertNextFreeRadioButton.Location = new Point(19, 655);
            InsertNextFreeRadioButton.Name = "InsertNextFreeRadioButton";
            InsertNextFreeRadioButton.Size = new Size(53, 19);
            InsertNextFreeRadioButton.TabIndex = 41;
            InsertNextFreeRadioButton.TabStop = true;
            InsertNextFreeRadioButton.Text = "Next:";
            InsertNextFreeRadioButton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 579);
            label5.Name = "label5";
            label5.Size = new Size(82, 15);
            label5.TabIndex = 40;
            label5.Text = "Insert Position";
            // 
            // InsertAtTextBox
            // 
            InsertAtTextBox.Location = new Point(134, 626);
            InsertAtTextBox.Name = "InsertAtTextBox";
            InsertAtTextBox.ReadOnly = true;
            InsertAtTextBox.Size = new Size(69, 23);
            InsertAtTextBox.TabIndex = 42;
            InsertAtTextBox.Enter += InsertAtTextBox_Enter;
            // 
            // InsertAtNextBlockRadioButton
            // 
            InsertAtNextBlockRadioButton.AutoSize = true;
            InsertAtNextBlockRadioButton.Checked = true;
            InsertAtNextBlockRadioButton.Location = new Point(19, 605);
            InsertAtNextBlockRadioButton.Name = "InsertAtNextBlockRadioButton";
            InsertAtNextBlockRadioButton.Size = new Size(85, 19);
            InsertAtNextBlockRadioButton.TabIndex = 41;
            InsertAtNextBlockRadioButton.TabStop = true;
            InsertAtNextBlockRadioButton.Text = "Next Block:";
            InsertAtNextBlockRadioButton.UseVisualStyleBackColor = true;
            InsertAtNextBlockRadioButton.CheckedChanged += InsertAtRadioButton_CheckedChanged;
            // 
            // NextBlockStartingIndexTextBox
            // 
            NextBlockStartingIndexTextBox.Location = new Point(134, 601);
            NextBlockStartingIndexTextBox.Name = "NextBlockStartingIndexTextBox";
            NextBlockStartingIndexTextBox.ReadOnly = true;
            NextBlockStartingIndexTextBox.Size = new Size(69, 23);
            NextBlockStartingIndexTextBox.TabIndex = 42;
            // 
            // InsertAtNextFreeTextBox
            // 
            InsertAtNextFreeTextBox.Location = new Point(134, 651);
            InsertAtNextFreeTextBox.Name = "InsertAtNextFreeTextBox";
            InsertAtNextFreeTextBox.ReadOnly = true;
            InsertAtNextFreeTextBox.Size = new Size(69, 23);
            InsertAtNextFreeTextBox.TabIndex = 42;
            // 
            // AddItemForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1177, 747);
            Controls.Add(NextBlockStartingIndexTextBox);
            Controls.Add(InsertAtNextFreeTextBox);
            Controls.Add(InsertAtTextBox);
            Controls.Add(InsertNextFreeRadioButton);
            Controls.Add(InsertAtNextBlockRadioButton);
            Controls.Add(InsertAtRadioButton);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label5);
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
            Shown += AddItemForm_Shown;
            ((System.ComponentModel.ISupportInitialize)GADataTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GADataGridView GADataTable;
        private TextBox NewItemPreStringTextBox;
        private Button AddItemButton;
        private ListBox ItemTemplatesListBox;
        private Label label1;
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
    }
}