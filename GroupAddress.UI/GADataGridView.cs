using GroupAddress.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.UI
{
    public class GADataGridView : DataGridView
    {

        public MainGroup? MainGroup { get; set; }
        public bool ShowEmptyRows { get; set; } = true;
        public bool ShowAllRows { get; set; } = true;


        public GADataGridView() : base()
        {
            RowPostPaint += GADataGridView_RowPostPaint;
            ColumnHeaderMouseDoubleClick += GADataGridView_ColumnHeaderMouseDoubleClick;
        }

        public void SetMainGroup(MainGroup mainGroup)
        {
            MainGroup = mainGroup;
            FillTable();
        }

        private void FillTable()
        {
            if (MainGroup == null) return;

            var table = new DataTable(); 
            var cols = Enumerable
                .Range(0, 8)
                .Select(x => new DataColumn(x + " - " + MainGroup.SubGroups.FirstOrDefault(y => y.SubAddress == x)?.Name))
                .ToArray();

            table.Columns.AddRange(cols);

            for (int i = 0; i < 256; i++)
            {
                var newRow = table.NewRow();
                for (int j = 0; j < 8; j++)
                {
                    newRow[j] = MainGroup
                        .GAs
                        .FirstOrDefault(x => x.SubGroup.SubAddress == j && x.SubAddress == i)?
                        .AddressName;
                }
                table.Rows.Add(newRow);
            }

            DataSource = table; 
            AutoResizeColumns();

            FilterEmptyRows();
        }

        public void FilterEmptyRows()
        {
            ClearSelection();
            Rows[1].Visible = false;

            Curr
        }

        private void GADataGridView_ColumnHeaderMouseDoubleClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GADataGridView_RowPostPaint(object? sender, DataGridViewRowPostPaintEventArgs e)
        {
            //var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex).ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
    }
}
