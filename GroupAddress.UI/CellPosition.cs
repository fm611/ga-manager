using GroupAddress.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.UI
{
    //public record struct CellPosition(int Row, int Column);

    public struct CellPosition
    {
        public int Row;
        public int Column;

        public CellPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public CellPosition(DataGridViewCellEventArgs e)
        {
            Row = e.RowIndex;
            Column = e.ColumnIndex;
        }
        public CellPosition(DataGridViewCellCancelEventArgs e)
        {
            Row = e.RowIndex;
            Column = e.ColumnIndex;
        }
        public CellPosition(DataGridViewCell e)
        {
            Row = e.RowIndex;
            Column = e.ColumnIndex;
        }
        public static bool operator ==(CellPosition a, CellPosition b)
        {
            return a.Row == b.Row && a.Column == b.Column;
        }
        public static bool operator !=(CellPosition a, CellPosition b)
        {
            return !(a == b);
        }
        public static bool operator ==(CellPosition a, Address b)
        {
            return a.Row == b.GA && a.Column == b.MiddleGroup;
        }
        public static bool operator !=(CellPosition a, Address b)
        {
            return !(a == b);
        }
        public static bool operator ==(Address a, CellPosition b)
        {
            return b == a;
        }
        public static bool operator !=(Address a, CellPosition b)
        {
            return !(a == b);
        }

        public override bool Equals(object? obj)
        {
            return obj is CellPosition position &&
                   Row == position.Row &&
                   Column == position.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
    }

}
