using GroupAddress.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.UI
{
    public class SortBindingList<T> : BindingList<T> where T : Group
    {
        private ListSortDirection _ListSortDirection = ListSortDirection.Ascending;
        private bool _IsSorted = false;
        private PropertyDescriptor _SortProp;

        protected override bool SupportsSortingCore
        {
            get { return Items is List<T>; }
        }
        protected override ListSortDirection SortDirectionCore
        {
            get { return _ListSortDirection; }
        }
        protected override bool IsSortedCore
        {
            get { return _IsSorted; }
        }
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return _SortProp; }
        }
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            List<T> items = (List<T>)Items;
            //if (items == null)         return;  - kann nicht auftreten
            _ListSortDirection = direction;
            _SortProp = prop;
            items.Sort((x, y) => {
                IComparable a = (IComparable)prop.GetValue(x);
                IComparable b = (IComparable)prop.GetValue(y);
                // Ist niemals Descending ausser wenn die letzte Zeile auskommentiert wird
                if (direction == ListSortDirection.Ascending)
                    return a.CompareTo(b);
                return b.CompareTo(a);
            });
            _IsSorted = true;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }
}
