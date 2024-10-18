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

        private List<CellPosition> _previousSelectedCells;

        public List<GA> SelectedGAs { get; private set; }


        public Dictionary<int, List<GA>> RowData { get; set; }

        public GADataGridView() : base()
        {
            RowPostPaint += GADataGridView_RowPostPaint;
            ColumnHeaderMouseDoubleClick += GADataGridView_ColumnHeaderMouseDoubleClick;
            SelectionChanged += GADataGridView_SelectionChanged;
        }



        public void SetMainGroup(MainGroup mainGroup)
        {
            MainGroup = mainGroup;
            FillTable();
        }

        public void UpdateTable()
        {
            SaveSelection();
            FillTable();
            ResetSelection();
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

            RowData = new Dictionary<int, List<GA>>();
            var maxRow = ShowAllRows ? 256 : MainGroup.GAs.Max(x => x.SubAddress) + 1;

            for (int i = 0; i < maxRow; i++)
            {
                var rowGAs = MainGroup
                        .GAs
                        .Where(x => x.SubAddress == i);

                if (rowGAs.Count() == 0 && !ShowEmptyRows) continue;

                var newRow = table.NewRow();
                for (int j = 0; j < 8; j++)
                {
                    newRow[j] = rowGAs
                        .FirstOrDefault(x => x.SubGroup.SubAddress == j)?
                        .AddressName;
                }
                table.Rows.Add(newRow);
                RowData.Add(i, rowGAs.ToList());
            }
            DataSource = table;
            AutoResizeColumns();
        }

        private void SaveSelection()
        {
            _previousSelectedCells = SelectedCells.Cast<DataGridViewCell>().Select(x => new CellPosition(x.RowIndex,x.ColumnIndex)).ToList();
        }

        private void ResetSelection()
        {

            ClearSelection();
            foreach (var cPos in _previousSelectedCells)
            {
                var selPos = cPos;
                if (selPos.Row >= Rows.Count) selPos.Row--;

                if (selPos.Row < Rows.Count) Rows[selPos.Row].Cells[selPos.Column].Selected = true;
            }
        }


        //Events
        private void GADataGridView_SelectionChanged(object? sender, EventArgs e)
        {
            SelectedGAs = SelectedCells
                .Cast<DataGridViewCell>()
                .SelectMany(c => RowData
                    .ElementAt(c.RowIndex)
                    .Value
                    .Where(x => x.SubGroup.SubAddress == c.ColumnIndex)
                )
                .ToList();
        }

        private void GADataGridView_ColumnHeaderMouseDoubleClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (MainGroup == null) return;

            var subGroupAddress = e.ColumnIndex;
            var subGroup = MainGroup.SubGroups.FirstOrDefault(x => x.SubAddress == subGroupAddress);

            var currName = subGroup?.Name ?? "Neue Mittelgruppe";

            var editSubGroupForm = new EditSubGroupForm(currName);
            editSubGroupForm.ShowDialog();

            if (editSubGroupForm.DialogResult != DialogResult.OK) return;

            subGroup ??= SubGroup.Create(subGroupAddress, editSubGroupForm.SubGroupName, MainGroup);

            subGroup.Name = editSubGroupForm.SubGroupName;

            UpdateTable();
        }

        private void GADataGridView_RowPostPaint(object? sender, DataGridViewRowPostPaintEventArgs e)
        {
            var rowIdx = RowData.ElementAt(e.RowIndex).Key.ToString();

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
