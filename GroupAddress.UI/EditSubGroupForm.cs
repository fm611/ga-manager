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
    public partial class EditSubGroupForm : Form
    {
        public string SubGroupName { get; set; }

        public EditSubGroupForm(string name = "Neue Mittelgruppe")
        {
            InitializeComponent();
            SubGroupNameTextBox.Text = name;
        }



        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SubGroupNameTextBox.Text))
            {
                SubGroupName = SubGroupNameTextBox.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private void SubGroupNameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            SaveButton.Enabled = !string.IsNullOrEmpty(SubGroupNameTextBox.Text);

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
            SubGroupNameTextBox.Focus();
        }
    }
}
