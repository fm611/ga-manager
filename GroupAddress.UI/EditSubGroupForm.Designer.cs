namespace GroupAddress.UI
{
    partial class EditSubGroupForm
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
            SaveButton = new Button();
            SubGroupNameTextBox = new TextBox();
            SuspendLayout();
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(166, 41);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 0;
            SaveButton.Text = "Speichern";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // SubGroupNameTextBox
            // 
            SubGroupNameTextBox.Location = new Point(12, 12);
            SubGroupNameTextBox.Name = "SubGroupNameTextBox";
            SubGroupNameTextBox.Size = new Size(229, 23);
            SubGroupNameTextBox.TabIndex = 1;
            SubGroupNameTextBox.Enter += SubGroupNameTextBox_Enter;
            SubGroupNameTextBox.KeyUp += SubGroupNameTextBox_KeyUp;
            // 
            // EditSubGroupForm
            // 
            AcceptButton = SaveButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(249, 72);
            Controls.Add(SubGroupNameTextBox);
            Controls.Add(SaveButton);
            Name = "EditSubGroupForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Mittelgruppe";
            Load += EditSubGroupForm_Shown;
            Shown += EditSubGroupForm_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SaveButton;
        private TextBox SubGroupNameTextBox;
    }
}