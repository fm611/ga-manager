using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupAddress.UI
{
    public partial class TextBoxDialog : Form
    {
        public string Content { get; set; }

        public TextBoxDialog(string title, string content)
        {
            InitializeComponent();
            Content = title;
            ContentTextBox.Text = content;
        }



        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ContentTextBox.Text))
            {
                Content = ContentTextBox.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private void SubGroupNameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            SaveButton.Enabled = !string.IsNullOrEmpty(ContentTextBox.Text);

            if (e.KeyValue == (char)Keys.Return)
            {
                SaveButton_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void SubGroupNameTextBox_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() => (sender as TextBox).SelectAll()));
        }

        private void EditSubGroupForm_Shown(object sender, EventArgs e)
        {
            ContentTextBox.Focus();
        }
    }
}
