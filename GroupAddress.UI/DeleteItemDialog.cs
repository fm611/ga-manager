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
    public partial class DeleteItemDialog: Form
    {
        public DeleteItemDialog(List<GA> gas)
        {
            InitializeComponent();

            Text = "Item löschen?";

            GaListBox.Items.AddRange(gas.OrderBy(x => x).Select(x => x.AddressName).ToArray());

        }
    }
}
