using GroupAddress.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.UI
{
    public class GADataGridView : DataGridView
    {

        public TopLevelCollection? TopLevelCollection { get; set; }
        public bool ShowEmptyRows { get; set; } = true;
        public bool ShowAllRows { get; set; } = true;

        private List<CellPosition> _previousSelectedCells = [];

        public List<GA> SelectedGAs { get; private set; } = [];
        public List<Address> SelectedAddresses { get; private set; } = [];

        public List<string?>? FilterGroups { get; private set; }
        public string? FilterString { get; private set; }


        //Key => GA Subaddress
        //ElementAt Index => RowIndex
        public Dictionary<int, List<GA>> RowData { get; set; } = [];


        public void SetTopLevelCollection(TopLevelCollection? coll)
        {
            TopLevelCollection = coll;
            UpdateTable();
            //FillTable(null,null);
        }

        public void UpdateTable()
        {
            UpdateTable(true);
        }

        public void UpdateTable(bool resetFilter = true)
        {

            if(resetFilter)
            {
                FilterGroups = null;
                FilterString = null;
            }

            SaveSelection();
            FillTable(FilterGroups, FilterString);
            ResetSelection();
        }


        public void FilterByGroup(List<string?>? groupIds)
        {
            if (groupIds == null || groupIds.Count == 0)
            {
                UpdateTable(true);
                return;
            }

            FilterGroups = groupIds;
            FilterString = null;

            UpdateTable(false);
        }

        private void FillTable(List<string?>? filterGroups, string? filterString)
        {
            filterGroups ??= [];

            if (TopLevelCollection == null)
            {
                DataSource = null;
                return;
            }

            var table = new DataTable();
            var cols = TopLevelCollection.SubGroupNames
                .Select((x,i) => new DataColumn(i + " - " + x))
                .ToArray();

            table.Columns.AddRange(cols);

            RowData = new Dictionary<int, List<GA>>();
            var maxRow = ShowAllRows ? 256 : TopLevelCollection.MaxGASubAddress + 1;

            var allFilteredGAs = new List<GA>();

            for (int i = 0; i < maxRow; i++)
            {

                var rowGAs = TopLevelCollection
                        .GAs
                        .Where(x => x.Address.GA == i);

                var filterGAs = rowGAs.Where(x => filterGroups.Count == 0 || filterGroups.Contains(x.GroupId));
                allFilteredGAs.AddRange(filterGAs);

                if (!filterGAs.Any() && 
                    (!ShowEmptyRows || filterGroups.Count>0)) 
                    continue;

                var newRow = table.NewRow();
                for (int j = 0; j < TopLevelCollection.SubGroupNames.Length; j++)
                {
                    newRow[j] = rowGAs
                        .FirstOrDefault(x => x.Address.MiddleGroup == j)?
                        .AddressName;
                }
                table.Rows.Add(newRow);
                RowData.Add(i, rowGAs.ToList());
            }
            DataSource = table;

            if(filterGroups.Count > 0)
            {
                foreach(var ga in allFilteredGAs)
                {
                    var cellPos = GetCellPosition(ga);
                    if (cellPos == null) continue;
                    var dgvCell = GetDataGridViewCell(cellPos.Value);

                    dgvCell.Style.BackColor = Color.LightYellow;
                }
            }


            AutoResizeColumns();
        }

        private void SaveSelection()
        {
            _previousSelectedCells = SelectedCells.Cast<DataGridViewCell>().Select(x => new CellPosition(x.RowIndex, x.ColumnIndex)).ToList();
        }

        private void ResetSelection()
        {
            ClearSelection();
            foreach (var cPos in _previousSelectedCells)
            {
                var selPos = cPos;
                if (selPos.Row >= Rows.Count) selPos.Row = Rows.Count-1;
                if (selPos.Row < Rows.Count && selPos.Row >= 0) Rows[selPos.Row].Cells[selPos.Column].Selected = true;
            }
        }

        private Address GetAddress(CellPosition pos)
        {
            return new Address(TopLevelCollection?.SubAddress??-1, pos.Column, RowData.ElementAt(pos.Row).Key); 
        }

        private DataGridViewCell GetDataGridViewCell(CellPosition pos)
        {
            return Rows[pos.Row].Cells[pos.Column];
        }

        public CellPosition? GetCellPosition(GA ga)
        {
            for (int i = 0; i < RowData.Count; i++)
            {
                if (RowData.ElementAt(i).Value.Contains(ga))
                {
                    return new CellPosition(i, ga.Address.MiddleGroup);
                }
            }
            return null;
        }


        //Events

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //// Create pen.
            //Pen blackPen = new Pen(Color.Red, 2);

            //e.Graphics.DrawRectangle(blackPen, -2,-2, Width+4,Height+4);
            ////e.Graphics.
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);

            SelectedAddresses = SelectedCells
                    .Cast<DataGridViewCell>()
                    .Select(x => GetAddress(new CellPosition(x)))
                    .ToList();

            if (TopLevelCollection == null)
                SelectedGAs = [];
            else
                SelectedGAs = SelectedAddresses
                    .SelectMany(x => TopLevelCollection.GAs.Where(y => y.Address == x))
                    .ToList();
        }

        protected override void OnColumnHeaderMouseDoubleClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnColumnHeaderMouseDoubleClick(e);

            if (TopLevelCollection == null) return;
            if (ReadOnly) return;

            var currName = TopLevelCollection.SubGroupNames[e.ColumnIndex] ?? "Neue Mittelgruppe";

            var editSubGroupForm = new TextBoxDialog("Mittelgruppe",currName);
            editSubGroupForm.ShowDialog();

            if (editSubGroupForm.DialogResult != DialogResult.OK) return;

            TopLevelCollection.SetSubGroupname(e.ColumnIndex, editSubGroupForm.Content);

            UpdateTable();
        }

        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            base.OnRowPostPaint(e);

            var rowIdx = RowData.ElementAt(e.RowIndex).Key.ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);

        }

        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            base.OnCellBeginEdit(e);

            ClearSelection();
            if (TopLevelCollection == null) return;

            var pos = new CellPosition(e);
            var editCell = GetDataGridViewCell(pos);
            var addr = GetAddress(pos);

            if (!string.IsNullOrEmpty(editCell.Value as string))
            {
                var ga = SelectedGAs.FirstOrDefault(x => x.Address==addr);

                if (ga == null) return;
                editCell.Value = ga.Name;
            }
        }

        protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            base.OnCellEndEdit(e);

            if (TopLevelCollection == null) return;


            var pos = new CellPosition(e);
            var editCell = GetDataGridViewCell(pos);
            var addr = GetAddress(pos);

            if (string.IsNullOrEmpty(editCell.Value as string)) return;

            var ga = SelectedGAs.FirstOrDefault(x => x.Address == addr);

            if (ga == null)
                ga = new GA(addr, (string)editCell.Value);
            
            ga.Name = (string)editCell.Value;

            TopLevelCollection.AddGA(ga);

            BeginInvoke(new MethodInvoker(UpdateTable));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (TopLevelCollection == null) return;


            if (e.KeyValue == (char)Keys.Delete && !ReadOnly)
            {
                if (SelectedGAs.Count == 0) return;

                var res = MessageBox.Show("Gruppenadressen löschen?", "Gruppenadressen löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    SelectedGAs.ForEach(TopLevelCollection.RemoveGA);
                    UpdateTable();
                }
            }

            if (e.KeyData == (Keys.Control | Keys.C))
            {
                var ga = SelectedGAs.FirstOrDefault();

                if (ga == null) return;

                Clipboard.SetText(ga.Name);

                e.Handled = true;
            }

            if (e.KeyData == (Keys.Control | Keys.V) && SelectedCells.Count == 1 && !ReadOnly)
            {
                var pasteCell = SelectedCells.Cast<DataGridViewCell>().First();
                if (pasteCell == null) return;

                pasteCell.Value = Clipboard.GetText();

                OnCellEndEdit(new DataGridViewCellEventArgs(pasteCell.ColumnIndex, pasteCell.RowIndex));

                e.Handled = true;
            }

        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);

            Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }


}
