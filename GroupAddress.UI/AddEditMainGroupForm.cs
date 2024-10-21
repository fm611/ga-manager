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
    public partial class AddEditMainGroupForm : Form
    {
        private bool _valid;
        public bool Valid
        {
            get => _valid; set
            {

            }
        }

        public List<MainGroup> MainGroups { get; set; }
        public MainGroup? MainGroup { get; set; }

        public AddEditMainGroupForm(List<MainGroup> mainGroups)
        {
            InitializeComponent();


            MainGroups = mainGroups;

            var nextId = MainGroups.Max(x => x.SubAddress) + 1;
            if (!MainGroup.IsValidSubAddress(nextId))
            {
                nextId = Enumerable.Range(0, 32).Where(x => !MainGroups.Any(y => y.SubAddress == x)).Min();
            }
            AddressTextBox.Value = nextId;
            NameTextBox.Text = "Neue Hauptgruppe";
            DefaultBlockLengthTextBox.Value = 1;

            Text = "Hauptgruppe hinzufügen";

        }

        public AddEditMainGroupForm(List<MainGroup> mainGroups, string id) : this(mainGroups)
        {
            MainGroup = MainGroups.FirstOrDefault(x => x.Id == id);

            if (MainGroup == null)
            {
                DialogResult = DialogResult.Abort;
                return;
            }

            AddressTextBox.Value = MainGroup.SubAddress;
            NameTextBox.Text = MainGroup.Name;
            DefaultBlockLengthTextBox.Value = MainGroup.DefaultBlockLength;

            Text = "Hauptgruppe bearbeiten";
        }



        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateName();
        }
        private void AddressTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ValidateAddress();
        }
        private void AddressTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            ValidateAddress();
        }




        private bool ValidateAddress()
        {
            var status = true;

            // neue Hauptgruppe
            if (MainGroup == null)
            {
                if (MainGroups.Any(x => x.SubAddress == AddressTextBox.Value))
                {
                    status = false;
                    ErrorProvider.SetError(AddressTextBox, "Adresse bereits belegt.");
                }
            }
            else
            {
                if (MainGroups.Any(x => x.Id != MainGroup.Id && x.SubAddress == AddressTextBox.Value))
                {
                    status = false;
                    ErrorProvider.SetError(AddressTextBox, "Adresse bereits belegt.");
                }
            }


            if (!MainGroup.IsValidSubAddress((int)AddressTextBox.Value))
            {
                status = false;
                ErrorProvider.SetError(AddressTextBox, "Adresse ungültig (0-31)");
            }

            if (status) ErrorProvider.SetError(AddressTextBox, string.Empty);
            UpdateUI();
            return status;
        }

        private bool ValidateName()
        {
            var status = true;

            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                status = false;
                ErrorProvider.SetError(NameTextBox, "Name kann nicht leer sein");
            }

            if (status) ErrorProvider.SetError(NameTextBox, string.Empty);
            UpdateUI();
            return status;
        }


        private void UpdateUI()
        {
            SaveButton.Enabled = !HasErrors();
        }

        private bool HasErrors()
        {
            Control[] controlList = [AddressTextBox, NameTextBox, DefaultBlockLengthTextBox];

            return controlList.Any(x => !string.IsNullOrEmpty(ErrorProvider.GetError(x)));
        }

        private void AddressTextBox_Leave(object sender, EventArgs e)
        {
            AddressTextBox.Text = AddressTextBox.Value.ToString();
        }
    }
}
