namespace GroupAddress.UI
{
    partial class AddEditMainGroupForm
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
            AddressTextBox = new NumericUpDown();
            DefaultBlockLengthTextBox = new NumericUpDown();
            NameTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SaveButton = new Button();
            ErrorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)AddressTextBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DefaultBlockLengthTextBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ErrorProvider).BeginInit();
            SuspendLayout();
            // 
            // AddressTextBox
            // 
            AddressTextBox.Location = new Point(99, 8);
            AddressTextBox.Maximum = new decimal(new int[] { 31, 0, 0, 0 });
            AddressTextBox.Name = "AddressTextBox";
            AddressTextBox.Size = new Size(59, 23);
            AddressTextBox.TabIndex = 1;
            AddressTextBox.KeyUp += AddressTextBox_KeyUp;
            AddressTextBox.Leave += AddressTextBox_Leave;
            AddressTextBox.MouseUp += AddressTextBox_MouseUp;
            // 
            // DefaultBlockLengthTextBox
            // 
            DefaultBlockLengthTextBox.Location = new Point(99, 66);
            DefaultBlockLengthTextBox.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            DefaultBlockLengthTextBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            DefaultBlockLengthTextBox.Name = "DefaultBlockLengthTextBox";
            DefaultBlockLengthTextBox.Size = new Size(59, 23);
            DefaultBlockLengthTextBox.TabIndex = 3;
            DefaultBlockLengthTextBox.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(99, 37);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(193, 23);
            NameTextBox.TabIndex = 2;
            NameTextBox.TextChanged += NameTextBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 3;
            label1.Text = "Addresse";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 40);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 3;
            label2.Text = "Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 69);
            label3.Name = "label3";
            label3.Size = new Size(71, 15);
            label3.TabIndex = 3;
            label3.Text = "Block Länge";
            // 
            // SaveButton
            // 
            SaveButton.DialogResult = DialogResult.OK;
            SaveButton.Location = new Point(217, 101);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 4;
            SaveButton.Text = "Speichern";
            SaveButton.UseVisualStyleBackColor = true;
            // 
            // ErrorProvider
            // 
            ErrorProvider.ContainerControl = this;
            // 
            // AddEditMainGroupForm
            // 
            AcceptButton = SaveButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(319, 134);
            Controls.Add(SaveButton);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(NameTextBox);
            Controls.Add(AddressTextBox);
            Controls.Add(DefaultBlockLengthTextBox);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AddEditMainGroupForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "AddEditMainGroupForm";
            ((System.ComponentModel.ISupportInitialize)AddressTextBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)DefaultBlockLengthTextBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)ErrorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown AddressTextBox;
        private NumericUpDown DefaultBlockLengthTextBox;
        private TextBox NameTextBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button SaveButton;
        private ErrorProvider ErrorProvider;
    }
}