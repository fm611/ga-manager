using System.Windows.Forms;

namespace GroupAddress.UI
{
    partial class ItemTemplateManagerForm
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
            NewItemPreStringTextBox = new TextBox();
            AddItemButton = new Button();
            ItemTemplatesListBox = new ListBox();
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
            DeleteItemTemplateButton = new Button();
            AddItemTemplateButton = new Button();
            EditItemTemplateButton = new Button();
            AddEditDeleteItemTemplatePanel = new FlowLayoutPanel();
            SaveButtonPanel = new FlowLayoutPanel();
            SaveItemTemplateButton = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            AddItemTemplateToolStripMenuItem = new ToolStripMenuItem();
            EditItemTemplateToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            DeleteItemTemplateToolStripMenuItem = new ToolStripMenuItem();
            textBox1 = new TextBox();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)GADataTable).BeginInit();
            AddEditDeleteItemTemplatePanel.SuspendLayout();
            SaveButtonPanel.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
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
            GADataTable.RowData = null;
            GADataTable.ShowAllRows = true;
            GADataTable.ShowEditingIcon = false;
            GADataTable.ShowEmptyRows = true;
            GADataTable.Size = new Size(1214, 663);
            GADataTable.TabIndex = 34;
            GADataTable.TopLevelCollection = null;
            // 
            // NewItemPreStringTextBox
            // 
            NewItemPreStringTextBox.Location = new Point(12, 345);
            NewItemPreStringTextBox.Name = "NewItemPreStringTextBox";
            NewItemPreStringTextBox.Size = new Size(236, 23);
            NewItemPreStringTextBox.TabIndex = 37;
            NewItemPreStringTextBox.Enter += NewItemPreStringTextBox_Enter;
            NewItemPreStringTextBox.KeyUp += NewItemPreStringTextBox_KeyUp;
            // 
            // AddItemButton
            // 
            AddItemButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AddItemButton.Location = new Point(12, 676);
            AddItemButton.Name = "AddItemButton";
            AddItemButton.Size = new Size(89, 23);
            AddItemButton.TabIndex = 38;
            AddItemButton.Text = "Hinzufügen";
            AddItemButton.UseVisualStyleBackColor = true;
            AddItemButton.Click += AddItemButton_Click;
            // 
            // ItemTemplatesListBox
            // 
            ItemTemplatesListBox.FormattingEnabled = true;
            ItemTemplatesListBox.ItemHeight = 15;
            ItemTemplatesListBox.Location = new Point(12, 81);
            ItemTemplatesListBox.Name = "ItemTemplatesListBox";
            ItemTemplatesListBox.Size = new Size(236, 184);
            ItemTemplatesListBox.TabIndex = 36;
            ItemTemplatesListBox.SelectedIndexChanged += ItemTemplatesListBox_SelectedIndexChanged;
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
            MainGroupsListBox.Location = new Point(12, 396);
            MainGroupsListBox.Name = "MainGroupsListBox";
            MainGroupsListBox.Size = new Size(236, 154);
            MainGroupsListBox.TabIndex = 36;
            MainGroupsListBox.SelectedIndexChanged += MainGroupsListBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 327);
            label2.Name = "label2";
            label2.Size = new Size(125, 15);
            label2.TabIndex = 40;
            label2.Text = "Item Name (PreString)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 371);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 40;
            label4.Text = "Hauptgruppe";
            // 
            // InsertAtRadioButton
            // 
            InsertAtRadioButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            InsertAtRadioButton.AutoSize = true;
            InsertAtRadioButton.Location = new Point(16, 619);
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
            InsertNextFreeRadioButton.Location = new Point(16, 644);
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
            label5.Location = new Point(9, 568);
            label5.Name = "label5";
            label5.Size = new Size(93, 15);
            label5.TabIndex = 40;
            label5.Text = "Einfüge Position";
            // 
            // InsertAtTextBox
            // 
            InsertAtTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            InsertAtTextBox.Location = new Point(127, 618);
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
            InsertAtNextBlockRadioButton.Location = new Point(16, 594);
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
            NextBlockStartingIndexTextBox.Location = new Point(127, 593);
            NextBlockStartingIndexTextBox.Name = "NextBlockStartingIndexTextBox";
            NextBlockStartingIndexTextBox.ReadOnly = true;
            NextBlockStartingIndexTextBox.Size = new Size(69, 23);
            NextBlockStartingIndexTextBox.TabIndex = 42;
            // 
            // InsertAtNextFreeTextBox
            // 
            InsertAtNextFreeTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            InsertAtNextFreeTextBox.Location = new Point(127, 643);
            InsertAtNextFreeTextBox.Name = "InsertAtNextFreeTextBox";
            InsertAtNextFreeTextBox.ReadOnly = true;
            InsertAtNextFreeTextBox.Size = new Size(69, 23);
            InsertAtNextFreeTextBox.TabIndex = 42;
            // 
            // DeleteItemTemplateButton
            // 
            DeleteItemTemplateButton.Location = new Point(84, 3);
            DeleteItemTemplateButton.Name = "DeleteItemTemplateButton";
            DeleteItemTemplateButton.Size = new Size(75, 23);
            DeleteItemTemplateButton.TabIndex = 43;
            DeleteItemTemplateButton.Text = "Löschen";
            DeleteItemTemplateButton.UseVisualStyleBackColor = true;
            // 
            // AddItemTemplateButton
            // 
            AddItemTemplateButton.Location = new Point(3, 3);
            AddItemTemplateButton.Name = "AddItemTemplateButton";
            AddItemTemplateButton.Size = new Size(75, 23);
            AddItemTemplateButton.TabIndex = 44;
            AddItemTemplateButton.Text = "Neu";
            AddItemTemplateButton.UseVisualStyleBackColor = true;
            AddItemTemplateButton.Click += AddItemTemplateButton_Click;
            // 
            // EditItemTemplateButton
            // 
            EditItemTemplateButton.Location = new Point(165, 3);
            EditItemTemplateButton.Name = "EditItemTemplateButton";
            EditItemTemplateButton.Size = new Size(75, 23);
            EditItemTemplateButton.TabIndex = 43;
            EditItemTemplateButton.Text = "Bearbeiten";
            EditItemTemplateButton.UseVisualStyleBackColor = true;
            // 
            // AddEditDeleteItemTemplatePanel
            // 
            AddEditDeleteItemTemplatePanel.AutoSize = true;
            AddEditDeleteItemTemplatePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AddEditDeleteItemTemplatePanel.Controls.Add(AddItemTemplateButton);
            AddEditDeleteItemTemplatePanel.Controls.Add(DeleteItemTemplateButton);
            AddEditDeleteItemTemplatePanel.Controls.Add(EditItemTemplateButton);
            AddEditDeleteItemTemplatePanel.Location = new Point(9, 271);
            AddEditDeleteItemTemplatePanel.Name = "AddEditDeleteItemTemplatePanel";
            AddEditDeleteItemTemplatePanel.Size = new Size(243, 29);
            AddEditDeleteItemTemplatePanel.TabIndex = 45;
            // 
            // SaveButtonPanel
            // 
            SaveButtonPanel.AutoSize = true;
            SaveButtonPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            SaveButtonPanel.Controls.Add(SaveItemTemplateButton);
            SaveButtonPanel.Enabled = false;
            SaveButtonPanel.Location = new Point(171, 271);
            SaveButtonPanel.Name = "SaveButtonPanel";
            SaveButtonPanel.Size = new Size(81, 29);
            SaveButtonPanel.TabIndex = 46;
            SaveButtonPanel.Visible = false;
            // 
            // SaveItemTemplateButton
            // 
            SaveItemTemplateButton.Location = new Point(3, 3);
            SaveItemTemplateButton.Name = "SaveItemTemplateButton";
            SaveItemTemplateButton.Size = new Size(75, 23);
            SaveItemTemplateButton.TabIndex = 43;
            SaveItemTemplateButton.Text = "Speichern";
            SaveItemTemplateButton.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { AddItemTemplateToolStripMenuItem, EditItemTemplateToolStripMenuItem, toolStripSeparator1, DeleteItemTemplateToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(131, 76);
            // 
            // AddItemTemplateToolStripMenuItem
            // 
            AddItemTemplateToolStripMenuItem.Name = "AddItemTemplateToolStripMenuItem";
            AddItemTemplateToolStripMenuItem.Size = new Size(130, 22);
            AddItemTemplateToolStripMenuItem.Text = "Neu";
            // 
            // EditItemTemplateToolStripMenuItem
            // 
            EditItemTemplateToolStripMenuItem.Name = "EditItemTemplateToolStripMenuItem";
            EditItemTemplateToolStripMenuItem.Size = new Size(130, 22);
            EditItemTemplateToolStripMenuItem.Text = "Bearbeiten";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(127, 6);
            // 
            // DeleteItemTemplateToolStripMenuItem
            // 
            DeleteItemTemplateToolStripMenuItem.Name = "DeleteItemTemplateToolStripMenuItem";
            DeleteItemTemplateToolStripMenuItem.Size = new Size(130, 22);
            DeleteItemTemplateToolStripMenuItem.Text = "Löschen";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 52);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(236, 23);
            textBox1.TabIndex = 37;
            textBox1.Enter += NewItemPreStringTextBox_Enter;
            textBox1.KeyUp += NewItemPreStringTextBox_KeyUp;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 34);
            label6.Name = "label6";
            label6.Size = new Size(125, 15);
            label6.TabIndex = 40;
            label6.Text = "Item Name (PreString)";
            // 
            // ItemTemplateManagerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1484, 711);
            Controls.Add(SaveButtonPanel);
            Controls.Add(AddEditDeleteItemTemplatePanel);
            Controls.Add(NextBlockStartingIndexTextBox);
            Controls.Add(InsertAtNextFreeTextBox);
            Controls.Add(InsertAtTextBox);
            Controls.Add(InsertNextFreeRadioButton);
            Controls.Add(InsertAtNextBlockRadioButton);
            Controls.Add(InsertAtRadioButton);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(label2);
            Controls.Add(GADataTable);
            Controls.Add(textBox1);
            Controls.Add(NewItemPreStringTextBox);
            Controls.Add(AddItemButton);
            Controls.Add(MainGroupsListBox);
            Controls.Add(ItemTemplatesListBox);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(1500, 750);
            Name = "ItemTemplateManagerForm";
            Text = "AddItemForm";
            Load += AddItemForm_Load;
            Shown += AddItemForm_Shown;
            ((System.ComponentModel.ISupportInitialize)GADataTable).EndInit();
            AddEditDeleteItemTemplatePanel.ResumeLayout(false);
            SaveButtonPanel.ResumeLayout(false);
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GADataGridView GADataTable;
        private TextBox NewItemPreStringTextBox;
        private Button AddItemButton;
        private ListBox ItemTemplatesListBox;
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
        private Button DeleteItemTemplateButton;
        private Button AddItemTemplateButton;
        private Button EditItemTemplateButton;
        private FlowLayoutPanel SaveButtonPanel;
        private FlowLayoutPanel AddEditDeleteItemTemplatePanel;
        private Button SaveItemTemplateButton;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem AddItemTemplateToolStripMenuItem;
        private ToolStripMenuItem EditItemTemplateToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem DeleteItemTemplateToolStripMenuItem;
        private TextBox textBox1;
        private Label label6;
    }
}