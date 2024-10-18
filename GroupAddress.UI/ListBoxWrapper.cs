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

        public Func<IEnumerable<T>> SourceFunc {get;set;}

        public ListBoxWrapper(ListBox listbox, Comparison<T> comp, string displayMember, string valueMember, Func<IEnumerable<T>> sourceFunc)
        {
            ListBox = listbox;
            BackingList = [];
            BindingList = new BindingList<T>(BackingList);
            SourceFunc = sourceFunc;

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

        public void Load()
        {
            var currSelectedIndex = ListBox.SelectedIndex >= 0 ? ListBox.SelectedIndex : 0;

            BindingList.Clear();
            BackingList.AddRange(SourceFunc());
            SortAndReset();

            currSelectedIndex = ListBox.Items.Count <= currSelectedIndex ? ListBox.Items.Count-1 : currSelectedIndex;

            if (BindingList.Count > 0)
                ListBox.SetSelected(currSelectedIndex, true);
        }




    }
}
