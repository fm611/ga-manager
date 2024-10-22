using GroupAddress.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
            //if (MainGroup == null) return;

            //var table = new DataTable();
            //var cols = Enumerable
            //    .Range(0, 8)
            //    .Select(x => new DataColumn(x + " - " + MainGroup.SubGroups.FirstOrDefault(y => y.SubAddress == x)?.Name))
            //    .ToArray();

            //table.Columns.AddRange(cols);

            //RowData = new Dictionary<int, List<GA>>();
            //var maxRow = ShowAllRows ? 256 : MainGroup.GAs.Max(x => x.SubAddress) + 1;

            //for (int i = 0; i < maxRow; i++)
            //{
            //    var rowGAs = MainGroup
            //            .GAs
            //            .Where(x => x.SubAddress == i);

            //    if (rowGAs.Count() == 0 && !ShowEmptyRows) continue;

            //    var newRow = table.NewRow();
            //    for (int j = 0; j < 8; j++)
            //    {
            //        newRow[j] = rowGAs
            //            .FirstOrDefault(x => x.SubGroup.SubAddress == j)?
            //            .AddressName;
            //    }
            //    table.Rows.Add(newRow);
            //    RowData.Add(i, rowGAs.ToList());
            //}
            //DataSource = table;
            //AutoResizeColumns();
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
                if (selPos.Row >= Rows.Count) selPos.Row--;

                if (selPos.Row < Rows.Count) Rows[selPos.Row].Cells[selPos.Column].Selected = true;
            }
        }


        //Events


        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);

            SelectedGAs = SelectedCells
                .Cast<DataGridViewCell>()
                .SelectMany(c => RowData
                    .ElementAt(c.RowIndex)
                    .Value
                    .Where(x => x.SubGroup.SubAddress == c.ColumnIndex)
                )
                .ToList();
        }

        protected override void OnColumnHeaderMouseDoubleClick(DataGridViewCellMouseEventArgs e)
        {
            //base.OnColumnHeaderMouseDoubleClick(e);

            //if (MainGroup == null) return;

            //var subGroupAddress = e.ColumnIndex;
            //var subGroup = MainGroup.SubGroups.FirstOrDefault(x => x.SubAddress == subGroupAddress);

            //var currName = subGroup?.Name ?? "Neue Mittelgruppe";

            //var editSubGroupForm = new EditSubGroupForm(currName);
            //editSubGroupForm.ShowDialog();

            //if (editSubGroupForm.DialogResult != DialogResult.OK) return;

            //subGroup ??= SubGroup.Create(subGroupAddress, editSubGroupForm.SubGroupName, MainGroup);

            //subGroup.Name = editSubGroupForm.SubGroupName;

            //UpdateTable();
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
            if (MainGroup == null) return;

            var editCell = Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (!string.IsNullOrEmpty(editCell.Value as string))
            {
                var ga = SelectedGAs.FirstOrDefault(x => x.SubAddress == e.RowIndex && x.SubGroup.SubAddress == e.ColumnIndex);

                if (ga == null) return;
                editCell.Value = ga.Name;
            }
        }

        protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            //base.OnCellEndEdit(e);

            //if (MainGroup == null) return;

            //var editCell = Rows[e.RowIndex].Cells[e.ColumnIndex];

            //if(string.IsNullOrEmpty(editCell.Value as string)) return;

            //var subGroup = MainGroup.SubGroups.FirstOrDefault(x => x.SubAddress == e.ColumnIndex);

            //if (subGroup == null)
            //{
            //    var currName = subGroup?.Name ?? "Neue Mittelgruppe";

            //    var editSubGroupForm = new EditSubGroupForm(currName);
            //    editSubGroupForm.ShowDialog();

            //    if (editSubGroupForm.DialogResult != DialogResult.OK) return;

            //    subGroup = SubGroup.Create(e.ColumnIndex, editSubGroupForm.SubGroupName, MainGroup);
            //}

            //var ga = SelectedGAs.FirstOrDefault(x => x.SubAddress == e.RowIndex && x.SubGroup.SubAddress == e.ColumnIndex);

            //if (ga == null)
            //    new GA(subGroup, e.RowIndex, (string)editCell.Value);
            //else
            //    ga.Name = (string)editCell.Value;


            //this.BeginInvoke(new MethodInvoker(() =>
            //{
            //    UpdateTable();
            //}));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            //if (MainGroup == null) return;

            //if (e.KeyValue == (char)Keys.Delete)
            //{
            //    if (SelectedGAs.Count == 0) return;

            //    var res = MessageBox.Show("Gruppenadressen löschen?", "Gruppenadressen löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //    if (res == DialogResult.Yes)
            //    {
            //        SelectedGAs.ForEach(MainGroup.RemoveGA);                        

            //        UpdateTable();
            //    }
            //}

            //if (e.KeyData == (Keys.Control | Keys.C))
            //{
            //    var ga = SelectedGAs.FirstOrDefault();

            //    if (ga == null) return;
            //    Clipboard.SetText(ga.Name);

            //    e.Handled = true;
            //}

            //if (e.KeyData == (Keys.Control | Keys.V) && SelectedCells.Count == 1)
            //{
            //    var pasteCell = SelectedCells.Cast<DataGridViewCell>().First();
            //    if (pasteCell == null) return;

            //    pasteCell.Value = Clipboard.GetText();

            //    OnCellEndEdit(new DataGridViewCellEventArgs(pasteCell.ColumnIndex, pasteCell.RowIndex));

            //    e.Handled = true;
            //}

        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);

            Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }


}
