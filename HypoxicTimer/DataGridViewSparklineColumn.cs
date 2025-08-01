using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HypoxicTimer
{
    public class DataGridViewSparklineColumn : DataGridViewColumn
    {
        public DataGridViewSparklineColumn()
        {
            this.CellTemplate = new DataGridViewSparklineCell();
            this.ReadOnly = true;
        }
        public long MaxValue = 0;

        public void CalcMaxValue()
        {
            //if (needsRecalc)
            //{
            //    int colIndex = this.DisplayIndex;
            //    for (int rowIndex = 0; rowIndex < this.DataGridView.Rows.Count; rowIndex++)
            //    {
            //        DataGridViewRow row = this.DataGridView.Rows[rowIndex];
            //        MaxValue = Math.Max(MaxValue, Convert.ToInt64(row.Cells[colIndex].Value));
            //    }
            //    needsRecalc = false;
            //}
        }
    }
}
