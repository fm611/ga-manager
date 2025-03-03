namespace GroupAddress.UI
{
    partial class DeleteGroupDialog
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
            GaListBox = new ListBox();
            IncludeGaPanel = new Panel();
            CancelButton1 = new Button();
            DeleteOnlyGroupButton = new Button();
            DeleteWithGAsButton = new Button();
            label1 = new Label();
            IncludeGaPanel.SuspendLayout();
            SuspendLayout();
            // 
            // GaListBox
            // 
            GaListBox.FormattingEnabled = true;
            GaListBox.ItemHeight = 15;
            GaListBox.Location = new Point(3, 28);
            GaListBox.Name = "GaListBox";
            GaListBox.SelectionMode = SelectionMode.MultiExtended;
            GaListBox.Size = new Size(411, 139);
            GaListBox.TabIndex = 0;
            // 
            // IncludeGaPanel
            // 
            IncludeGaPanel.Controls.Add(CancelButton1);
            IncludeGaPanel.Controls.Add(DeleteOnlyGroupButton);
            IncludeGaPanel.Controls.Add(DeleteWithGAsButton);
            IncludeGaPanel.Controls.Add(label1);
            IncludeGaPanel.Controls.Add(GaListBox);
            IncludeGaPanel.Location = new Point(12, 12);
            IncludeGaPanel.Name = "IncludeGaPanel";
            IncludeGaPanel.Size = new Size(423, 203);
            IncludeGaPanel.TabIndex = 1;
            // 
            // CancelButton1
            // 
            CancelButton1.DialogResult = DialogResult.Cancel;
            CancelButton1.Location = new Point(339, 173);
            CancelButton1.Name = "CancelButton1";
            CancelButton1.Size = new Size(75, 23);
            CancelButton1.TabIndex = 2;
            CancelButton1.Text = "Abbrechen";
            CancelButton1.UseVisualStyleBackColor = true;
            // 
            // DeleteOnlyItemButton
            // 
            DeleteOnlyGroupButton.DialogResult = DialogResult.OK;
            DeleteOnlyGroupButton.Location = new Point(179, 173);
            DeleteOnlyGroupButton.Name = "DeleteOnlyGroupButton";
            DeleteOnlyGroupButton.Size = new Size(115, 23);
            DeleteOnlyGroupButton.TabIndex = 3;
            DeleteOnlyGroupButton.Text = "Nur Gruppe löschen";
            DeleteOnlyGroupButton.UseVisualStyleBackColor = true;
            // 
            // DeleteWithGAsButton
            // 
            DeleteWithGAsButton.Location = new Point(3, 173);
            DeleteWithGAsButton.Name = "DeleteWithGAsButton";
            DeleteWithGAsButton.Size = new Size(141, 23);
            DeleteWithGAsButton.TabIndex = 2;
            DeleteWithGAsButton.Text = "Gruppe + GA's löschen";
            DeleteWithGAsButton.UseVisualStyleBackColor = true;
            DeleteWithGAsButton.Click += DeleteWithGAsButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 10);
            label1.Name = "label1";
            label1.Size = new Size(241, 15);
            label1.TabIndex = 1;
            label1.Text = "Die Gruppe enthält folgende Gruppenadressen:";
            // 
            // DeleteItemDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            CancelButton = CancelButton1;
            ClientSize = new Size(449, 226);
            Controls.Add(IncludeGaPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "DeleteGroupDialog";
            Text = "Gruppe löschen";
            IncludeGaPanel.ResumeLayout(false);
            IncludeGaPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListBox GaListBox;
        private Panel IncludeGaPanel;
        private Button DeleteOnlyGroupButton;
        private Button DeleteWithGAsButton;
        private Label label1;
        private Button CancelButton1;
    }
}