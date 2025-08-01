using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPlot;

namespace HypoxicTimer
{
    public partial class DiaryForm : Form
    {
        private Diary aDiary;
        private string SaveTo;
        private string BackupTo;
        public class HTIHistogram
        {
            public HistogramPlot HistogramPlot;
            //public ArrayList XAxis = new ArrayList();
            public DateTime[] XAxis;
            public Double[] YAxis;
        };
        HTIHistogram FullDiaryHistogram = new HTIHistogram();
        HTIHistogram LimitedDiaryHistogram = new HTIHistogram();

        HistogramPlot lowDistributionHistogramPlot = null;
        HistogramPlot medDistributionHistogramPlot = null;
        HistogramPlot highDistributionHistogramPlot = null;

        ArrayList lowSecondsAtPercentageCounts = null;
        ArrayList medSecondsAtPercentageCounts = null;
        ArrayList highSecondsAtPercentageCounts = null;
        ArrayList lowSecondsAtPercentageValue = null;
        ArrayList medSecondsAtPercentageValue = null;
        ArrayList highSecondsAtPercentageValue = null;
        private void SetupHistorgramCounters()
        {
            Plot.InitializeHistorgramXAxis(ref lowSecondsAtPercentageCounts, Plot.LOW_MIN, Plot.LOW_MAX);
            lowDistributionHistogramPlot.AbscissaData = lowSecondsAtPercentageCounts;
            Plot.InitializeHistorgramXAxis(ref medSecondsAtPercentageCounts, Plot.MED_MIN, Plot.MED_MAX);
            medDistributionHistogramPlot.AbscissaData = medSecondsAtPercentageCounts;
            Plot.InitializeHistorgramXAxis(ref highSecondsAtPercentageCounts, Plot.HIGH_MIN, Plot.HIGH_MAX);
            highDistributionHistogramPlot.AbscissaData = highSecondsAtPercentageCounts;
        }
        private void SetupHistorgramValues(int?[] SecondsAtPercentage)
        {
            lowSecondsAtPercentageValue = new ArrayList();
            medSecondsAtPercentageValue = new ArrayList();
            highSecondsAtPercentageValue = new ArrayList();
            for (int i = Plot.LOW_MIN; i < Plot.HIGH_MAX && (i-Plot.LOW_MIN) < SecondsAtPercentage.Length; i++)
            {
                if(i < Plot.LOW_MAX)
                {
                    lowSecondsAtPercentageValue.Add(SecondsAtPercentage[i-Plot.LOW_MIN]);
                }
                else if(i < Plot.MED_MAX)
                {
                    medSecondsAtPercentageValue.Add(SecondsAtPercentage[i-Plot.LOW_MIN]);
                }
                else 
                {
                    highSecondsAtPercentageValue.Add(SecondsAtPercentage[i-Plot.LOW_MIN]);
                }
            }
            lowDistributionHistogramPlot.DataSource = lowSecondsAtPercentageValue;
            medDistributionHistogramPlot.DataSource = medSecondsAtPercentageValue;
            highDistributionHistogramPlot.DataSource = highSecondsAtPercentageValue;
        }

        public DiaryForm(Diary aDiary, string SaveTo, string BackupTo)
        {
            InitializeComponent();
            this.SaveTo = SaveTo;
            this.BackupTo = BackupTo;
            DiaryPlotSurface2D.Clear();
            DiaryPlotSurface2D.BackColor = this.BackColor;

            Grid grid = new Grid();
            grid.VerticalGridType = Grid.GridType.Coarse;
            grid.HorizontalGridType = Grid.GridType.Coarse;
            grid.MajorGridPen = new Pen(Color.LightGray, 1.0f);
            DiaryPlotSurface2D.Add(grid);

            HistogramPlotSurface2D.Clear();
            HistogramPlotSurface2D.BackColor = this.BackColor;

            Grid hgrid = new Grid();
            hgrid.VerticalGridType = Grid.GridType.Coarse;
            hgrid.HorizontalGridType = Grid.GridType.Coarse;
            hgrid.MajorGridPen = new Pen(Color.LightGray, 1.0f);
            HistogramPlotSurface2D.Add(grid);

            
            this.aDiary = aDiary;
            CreateHistorgramPlot(ref FullDiaryHistogram.HistogramPlot, Color.Red, RectangleBrushes.HorizontalCenterFade.FaintRedFade);
            CreateHistorgramPlot(ref LimitedDiaryHistogram.HistogramPlot, Color.Blue, RectangleBrushes.HorizontalCenterFade.FaintBlueFade);
            PopulateDiaryHistorgrams();
            DiaryPlotSurface2D.Add(FullDiaryHistogram.HistogramPlot, NPlot.PlotSurface2D.XAxisPosition.Bottom, NPlot.PlotSurface2D.YAxisPosition.Left);
            DiaryPlotSurface2D.Add(LimitedDiaryHistogram.HistogramPlot, NPlot.PlotSurface2D.XAxisPosition.Bottom, NPlot.PlotSurface2D.YAxisPosition.Left);

            System.Drawing.Font TitleFont = new System.Drawing.Font("Arial", 12);
            System.Drawing.Font AxisFont = new System.Drawing.Font("Arial", 10);
            System.Drawing.Font TickFont = new System.Drawing.Font("Arial", 8);
            //DiaryPlotSurface2D.XAxis1.Label = "Timestamp";
            DiaryPlotSurface2D.XAxis1.LabelFont = AxisFont;
            DiaryPlotSurface2D.XAxis1.TickTextFont = TickFont;
            DiaryPlotSurface2D.XAxis1.NumberFormat = "yyyy-MM-dd";
            DiaryPlotSurface2D.XAxis1.TicksLabelAngle = 90;
            DiaryPlotSurface2D.XAxis1.TickTextNextToAxis = true;
            DiaryPlotSurface2D.XAxis1.FlipTicksLabel = true;
            DiaryPlotSurface2D.XAxis1.LabelOffset = 110;
            DiaryPlotSurface2D.XAxis1.LabelOffsetAbsolute = true;
            DiaryPlotSurface2D.Refresh();
            PopulateDataGrid();


            Plot.CreateHistorgramPlots(ref lowDistributionHistogramPlot, ref medDistributionHistogramPlot, ref highDistributionHistogramPlot, lowSecondsAtPercentageCounts, medSecondsAtPercentageCounts, highSecondsAtPercentageCounts);
            SetupHistorgramCounters();
        }

        private void PopulateDataGrid()
        {
            SparklineRenderer.HorizontalBandingColor[] HorizontalBandingColors = new SparklineRenderer.HorizontalBandingColor[3];
            HorizontalBandingColors[0] = new SparklineRenderer.HorizontalBandingColor(Plot.LOW_MAX - Plot.LOW_MIN, Color.Red);
            HorizontalBandingColors[1] = new SparklineRenderer.HorizontalBandingColor(Plot.MED_MAX - Plot.LOW_MIN, Color.Blue);
            HorizontalBandingColors[2] = new SparklineRenderer.HorizontalBandingColor(Plot.HIGH_MAX - Plot.LOW_MIN, Color.Green);

            foreach (KeyValuePair<DateTime, Diary.DiaryEntry> aKeyValuePair in aDiary.Entries)
            {
                Diary.DiaryEntry aDiaryEntry = aKeyValuePair.Value;
                if (aDiaryEntry.HypoxicTraningIndexFull > 0)
                {
                    DiaryDataGridView.Rows.Insert(0, 1);
                    DataGridViewRow aDataGridViewRow = DiaryDataGridView.Rows[DiaryDataGridView.Rows.GetFirstRow(DataGridViewElementStates.Visible)];
                    aDataGridViewRow.Cells[this.Date.Index].Value = aKeyValuePair.Key.ToShortDateString(); ;
                    aDataGridViewRow.Cells[this.Comment.Index].Value = aDiaryEntry.comment;
                    aDataGridViewRow.Cells[this.LHTi.Index].Value = aDiaryEntry.HypoxicTraningIndexLimited;
                    aDataGridViewRow.Cells[this.FHTI.Index].Value = aDiaryEntry.HypoxicTraningIndexFull;
                    aDataGridViewRow.Cells[this.HidenEntry.Index].Value = aDiaryEntry;

                    //sparkline
                    SparklineRenderer aSparklineRenderer = ((DataGridViewSparklineCell)aDataGridViewRow.Cells[this.SpO2.Index]).Renderer;
                    aSparklineRenderer.Height = 10;
                    aSparklineRenderer.HorizontalBandingColors = HorizontalBandingColors;
                    aSparklineRenderer.LineColor = Color.Black;
                    if (aDiaryEntry.SecondsAtPercentage != null)
                    {
                        aSparklineRenderer.DataElements = aDiaryEntry.SecondsAtPercentage.ToList<int?>();
                    }
                    else
                    {
                        aSparklineRenderer.DataElements = null; // (new int?[1]).ToList<int?>();
                    }
                    aSparklineRenderer.PixelsPerElement = 1;
                }
            }
        }

        private void PopulateDiaryHistorgrams()
        {
            int i = 0;
            FullDiaryHistogram.XAxis = new DateTime[aDiary.Entries.Count];
            LimitedDiaryHistogram.XAxis = new DateTime[aDiary.Entries.Count];
            FullDiaryHistogram.YAxis = new double[aDiary.Entries.Count];
            LimitedDiaryHistogram.YAxis = new double[aDiary.Entries.Count];

            foreach (KeyValuePair<DateTime, Diary.DiaryEntry> aDiaryEntry in aDiary.Entries)
            {
                FullDiaryHistogram.XAxis[i] = aDiaryEntry.Key;
                //FullHistogram.XAxis.Add(aDiaryEntry.Key);
                FullDiaryHistogram.YAxis[i] = aDiaryEntry.Value.HypoxicTraningIndexFull;
                LimitedDiaryHistogram.XAxis[i] = aDiaryEntry.Key;
                //LimitedHistogram.XAxis.Add(aDiaryEntry.Key);
                LimitedDiaryHistogram.YAxis[i] = aDiaryEntry.Value.HypoxicTraningIndexLimited;
                i++;
            }
            LimitedDiaryHistogram.HistogramPlot.AbscissaData = LimitedDiaryHistogram.XAxis;
            LimitedDiaryHistogram.HistogramPlot.DataSource = LimitedDiaryHistogram.YAxis;
            //LimitedHistogram.HistogramPlot.BaseWidth = 0.5f;
            //LimitedHistogram.HistogramPlot.Center = false;

            FullDiaryHistogram.HistogramPlot.AbscissaData = FullDiaryHistogram.XAxis;
            FullDiaryHistogram.HistogramPlot.DataSource = FullDiaryHistogram.YAxis;
            //FullHistogram.HistogramPlot.Center = false;
            //FullHistogram.HistogramPlot.BaseWidth = 0.5f;
        }
        private void CreateHistorgramPlot(ref HistogramPlot aHistogramPlot, Color color, IRectangleBrush brush)
        {
            aHistogramPlot = new HistogramPlot();
            aHistogramPlot.Color = color;
            aHistogramPlot.Filled = true;
            aHistogramPlot.RectangleBrush = brush;
            aHistogramPlot.Pen.Width = 2f;
        }


        private void DiaryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            aDiary.SaveXML(SaveTo, BackupTo);
            //e.Cancel = true;
            //this.Hide();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DiaryDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.Comment.Index)
            {
                Diary.DiaryEntry aDiaryEntry = (Diary.DiaryEntry)(DiaryDataGridView.Rows[e.RowIndex].Cells[this.HidenEntry.Index].Value);
                aDiaryEntry.comment = (string)DiaryDataGridView.Rows[e.RowIndex].Cells[this.Comment.Index].Value;
            }
        }

        private void DiaryDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            Diary.DiaryEntry aDiaryEntry = (Diary.DiaryEntry)(DiaryDataGridView.Rows[e.RowIndex].Cells[this.HidenEntry.Index].Value);

            if (aDiaryEntry.SecondsAtPercentage != null)
            {
                SetupHistorgramValues(aDiaryEntry.SecondsAtPercentage);
            }
            else
            {
                int[] dummy = new int[0];
                //dummy[0] = 0;
                lowDistributionHistogramPlot.DataSource = dummy;
                medDistributionHistogramPlot.DataSource = dummy;
                highDistributionHistogramPlot.DataSource = dummy;
            }
            HistogramPlotSurface2D.Remove(lowDistributionHistogramPlot, true);
            HistogramPlotSurface2D.Remove(medDistributionHistogramPlot, true);
            HistogramPlotSurface2D.Remove(highDistributionHistogramPlot, true);
            HistogramPlotSurface2D.Add(medDistributionHistogramPlot);
            HistogramPlotSurface2D.Add(lowDistributionHistogramPlot);
            HistogramPlotSurface2D.Add(highDistributionHistogramPlot);
            HistogramPlotSurface2D.Refresh();

        }

    }
}
