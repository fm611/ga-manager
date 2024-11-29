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

        public Comparison<T> Comparison { get; set; }

        public Func<IEnumerable<T>> SourceFunc { get; set; }

        public bool AlwaysSelect { get; set; }

        public ListBoxWrapper(ListBox listbox, Comparison<T> comp, string displayMember, string valueMember, Func<IEnumerable<T>> sourceFunc, bool alwaysSelect = true)
        {
            ListBox = listbox;
            BackingList = [];
            BindingList = new BindingList<T>(BackingList);
            SourceFunc = sourceFunc;

            Comparison = comp;
            AlwaysSelect = alwaysSelect;

            ListBox.DataSource = BindingList;
            ListBox.DisplayMember = displayMember;
            ListBox.ValueMember = valueMember;

        }


        public void SortAndReset()
        {
            BackingList.Sort(Comparison);
            BindingList.ResetBindings();
        }



        public void Update()
        {

            var selectionMode = ListBox.SelectionMode;
            ListBox.SelectionMode = SelectionMode.None;

            var currSelectedItem = ListBox.SelectedItem;
            var currSelectedItems = ListBox.SelectedItems.Cast<T>().ToList();

            BindingList.Clear();
            BackingList.AddRange(SourceFunc());
            SortAndReset();

            ListBox.SelectionMode = selectionMode;


            if (currSelectedItems.Count == 0 && !AlwaysSelect) return;

            var itemsInList = currSelectedItems.OfType<T>().Where(x => x!=null).Where(x => ListBox.Items.Contains(x)).ToList();



            var valueIndexes = currSelectedItems.Where(x => x!=null).Select(x => ListBox.Items.IndexOf(x)).Where(x => x>=0).ToList();

            if (valueIndexes.Count == 0 && ListBox.Items.Count > 0) valueIndexes.Add(0);


            foreach(var i  in valueIndexes)
            {
                ListBox.SetSelected(i, true);
            }
        }




    }
}
