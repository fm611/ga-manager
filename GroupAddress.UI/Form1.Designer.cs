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
            MainGroupsListBox = new ListBox();
            label1 = new Label();
            label2 = new Label();
            SubGroupsListBox = new ListBox();
            label3 = new Label();
            GAsListBox = new ListBox();
            ItemTemplatesListBox = new ListBox();
            AddItemButton = new Button();
            textBox1 = new TextBox();
            label4 = new Label();
            AddMainGroupButton = new Button();
            AddMainGroupIdTextBox = new TextBox();
            AddMainGroupNameTextBox = new TextBox();
            DeleteMainGroupButton = new Button();
            statusStrip1 = new StatusStrip();
            StatusInfoLabel = new ToolStripStatusLabel();
            EditMainGroupButton = new Button();
            SaveButton = new Button();
            LoadButton = new Button();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // MainGroupsListBox
            // 
            MainGroupsListBox.FormattingEnabled = true;
            MainGroupsListBox.ItemHeight = 15;
            MainGroupsListBox.Location = new Point(12, 67);
            MainGroupsListBox.Name = "MainGroupsListBox";
            MainGroupsListBox.Size = new Size(207, 394);
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(225, 40);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 3;
            label2.Text = "Mittelgruppen";
            // 
            // SubGroupsListBox
            // 
            SubGroupsListBox.FormattingEnabled = true;
            SubGroupsListBox.ItemHeight = 15;
            SubGroupsListBox.Location = new Point(225, 67);
            SubGroupsListBox.Name = "SubGroupsListBox";
            SubGroupsListBox.Size = new Size(207, 484);
            SubGroupsListBox.Sorted = true;
            SubGroupsListBox.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(438, 40);
            label3.Name = "label3";
            label3.Size = new Size(99, 15);
            label3.TabIndex = 5;
            label3.Text = "Gruppenadressen";
            // 
            // GAsListBox
            // 
            GAsListBox.FormattingEnabled = true;
            GAsListBox.ItemHeight = 15;
            GAsListBox.Location = new Point(438, 67);
            GAsListBox.Name = "GAsListBox";
            GAsListBox.Size = new Size(207, 484);
            GAsListBox.TabIndex = 20;
            // 
            // ItemTemplatesListBox
            // 
            ItemTemplatesListBox.FormattingEnabled = true;
            ItemTemplatesListBox.ItemHeight = 15;
            ItemTemplatesListBox.Location = new Point(703, 67);
            ItemTemplatesListBox.Name = "ItemTemplatesListBox";
            ItemTemplatesListBox.Size = new Size(236, 184);
            ItemTemplatesListBox.TabIndex = 30;
            // 
            // AddItemButton
            // 
            AddItemButton.Location = new Point(945, 114);
            AddItemButton.Name = "AddItemButton";
            AddItemButton.Size = new Size(75, 23);
            AddItemButton.TabIndex = 32;
            AddItemButton.Text = "Add Item";
            AddItemButton.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(945, 85);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(280, 23);
            textBox1.TabIndex = 31;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(945, 67);
            label4.Name = "label4";
            label4.Size = new Size(125, 15);
            label4.TabIndex = 9;
            label4.Text = "Item Name (PreString)";
            // 
            // AddMainGroupButton
            // 
            AddMainGroupButton.Location = new Point(144, 496);
            AddMainGroupButton.Name = "AddMainGroupButton";
            AddMainGroupButton.Size = new Size(75, 23);
            AddMainGroupButton.TabIndex = 4;
            AddMainGroupButton.Text = "Add";
            AddMainGroupButton.UseVisualStyleBackColor = true;
            AddMainGroupButton.Click += AddMainGroupButton_Click;
            // 
            // AddMainGroupIdTextBox
            // 
            AddMainGroupIdTextBox.Location = new Point(12, 467);
            AddMainGroupIdTextBox.Name = "AddMainGroupIdTextBox";
            AddMainGroupIdTextBox.Size = new Size(41, 23);
            AddMainGroupIdTextBox.TabIndex = 1;
            AddMainGroupIdTextBox.TextChanged += AddMainGroupIdTextBox_TextChanged;
            AddMainGroupIdTextBox.Enter += AddMainGroupIdTextBox_Enter;
            // 
            // AddMainGroupNameTextBox
            // 
            AddMainGroupNameTextBox.Location = new Point(59, 467);
            AddMainGroupNameTextBox.Name = "AddMainGroupNameTextBox";
            AddMainGroupNameTextBox.Size = new Size(160, 23);
            AddMainGroupNameTextBox.TabIndex = 2;
            AddMainGroupNameTextBox.Enter += AddMainGroupNameTextBox_Enter;
            AddMainGroupNameTextBox.KeyUp += AddMainGroupNameTextBox_KeyUp;
            // 
            // DeleteMainGroupButton
            // 
            DeleteMainGroupButton.Location = new Point(144, 525);
            DeleteMainGroupButton.Name = "DeleteMainGroupButton";
            DeleteMainGroupButton.Size = new Size(75, 23);
            DeleteMainGroupButton.TabIndex = 5;
            DeleteMainGroupButton.Text = "Delete";
            DeleteMainGroupButton.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { StatusInfoLabel });
            statusStrip1.Location = new Point(0, 597);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1250, 22);
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
            EditMainGroupButton.Enabled = false;
            EditMainGroupButton.Location = new Point(63, 496);
            EditMainGroupButton.Name = "EditMainGroupButton";
            EditMainGroupButton.Size = new Size(75, 23);
            EditMainGroupButton.TabIndex = 4;
            EditMainGroupButton.Text = "Edit";
            EditMainGroupButton.UseVisualStyleBackColor = true;
            EditMainGroupButton.Click += EditMainGroupButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(1150, 558);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 33;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // LoadButton
            // 
            LoadButton.Location = new Point(1069, 558);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(75, 23);
            LoadButton.TabIndex = 33;
            LoadButton.Text = "Load";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1250, 619);
            Controls.Add(LoadButton);
            Controls.Add(SaveButton);
            Controls.Add(statusStrip1);
            Controls.Add(label4);
            Controls.Add(AddMainGroupNameTextBox);
            Controls.Add(AddMainGroupIdTextBox);
            Controls.Add(textBox1);
            Controls.Add(DeleteMainGroupButton);
            Controls.Add(EditMainGroupButton);
            Controls.Add(AddMainGroupButton);
            Controls.Add(AddItemButton);
            Controls.Add(ItemTemplatesListBox);
            Controls.Add(label3);
            Controls.Add(GAsListBox);
            Controls.Add(label2);
            Controls.Add(SubGroupsListBox);
            Controls.Add(label1);
            Controls.Add(MainGroupsListBox);
            Name = "Form1";
            Text = "Form1";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox MainGroupsListBox;
        private Label label1;
        private Label label2;
        private ListBox SubGroupsListBox;
        private Label label3;
        private ListBox GAsListBox;
        private ListBox ItemTemplatesListBox;
        private Button AddItemButton;
        private TextBox textBox1;
        private Label label4;
        private Button AddMainGroupButton;
        private TextBox AddMainGroupIdTextBox;
        private TextBox AddMainGroupNameTextBox;
        private Button DeleteMainGroupButton;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel StatusInfoLabel;
        private Button EditMainGroupButton;
        private Button SaveButton;
        private Button LoadButton;
    }
}
