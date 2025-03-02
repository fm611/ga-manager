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
    public partial class DeleteItemDialog : Form
    {
        public bool IncludeGAs { get; set; }

        public DeleteItemDialog(List<GA> gas)
        {
            InitializeComponent();

            Text = "Item löschen?";

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
