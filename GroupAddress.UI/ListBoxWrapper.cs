using GroupAddress.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.UI
{
    public class ListBoxWrapper<T>
    {
        public ListBox ListBox { get; set; }
        public List<T> BackingList { get; set; }
        public BindingList<T> BindingList { get; set; }

        public Comparison<T> Comparison {get;set;}

        public ListBoxWrapper(ListBox listbox, Comparison<T> comp, string displayMember, string valueMember)
        {
            ListBox = listbox;
            BackingList = [];
            BindingList = new BindingList<T>(BackingList);

            Comparison = comp;

            ListBox.DataSource = BindingList;
            ListBox.DisplayMember = displayMember;
            ListBox.ValueMember = valueMember;

        }

        public void SortAndReset()
        {
            BackingList.Sort(Comparison);
            BindingList.ResetBindings();
        }

        public void Load(IEnumerable<T> range)
        {
            BindingList.Clear();
            BackingList.AddRange(range);
            SortAndReset();

            ListBox.SelectedIndex = -1;
            ListBox.SelectedIndex = 1;

            ListBox.SetSelected(0, true);
        }




    }
}
