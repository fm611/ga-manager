using GroupAddress.Core;
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
    public partial class DeleteGroupDialog : Form
    {
        public bool IncludeGAs { get; set; }

        public DeleteGroupDialog(List<GA> gas)
        {
            InitializeComponent();

            Text = "Gruppe löschen?";

            IncludeGAs = false;

            GaListBox.Items.AddRange(gas.OrderBy(x => x).Select(x => x.AddressName).ToArray());

        }

        private void DeleteWithGAsButton_Click(object sender, EventArgs e)
        {
            IncludeGAs = true;
            DialogResult = DialogResult.OK;


        }
    }
}
