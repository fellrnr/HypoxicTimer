using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Drawing;
using System.Media;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using NPlot;

namespace HypoxicTimer
{
    class Plot
    {
        public const int LOW_MIN = 60;
        public const int LOW_MAX = 75;
        public const int MED_MIN = 75;
        public const int MED_MAX = 90;
        public const int HIGH_MIN = 90;
        public const int HIGH_MAX = 101;
        public static void AddHistogramPoint(RealTimePacket aRealPacket, ArrayList lowCounts, ArrayList medCounts, ArrayList highCounts)
        {
            if (aRealPacket.SpO2 > HIGH_MAX)
            {
            }
            else if (aRealPacket.SpO2 >= HIGH_MIN)
            {
                highCounts[aRealPacket.SpO2 - HIGH_MIN] = ((int)highCounts[aRealPacket.SpO2 - HIGH_MIN]) + 1;
            }
            else if (aRealPacket.SpO2 >= MED_MIN)
            {
                medCounts[aRealPacket.SpO2 - MED_MIN] = ((int)medCounts[aRealPacket.SpO2 - MED_MIN]) + 1;
            }
            else if (aRealPacket.SpO2 >= LOW_MIN)
            {
                lowCounts[aRealPacket.SpO2 - LOW_MIN] = ((int)lowCounts[aRealPacket.SpO2 - LOW_MIN]) + 1;
            }
        }

        public static void InitializeHistorgramXAxis(ref ArrayList count, int min, int max)
        {
            if (count == null)
            {
                count = new ArrayList();
                for (int i = min; i < max; i++)
                {
                    count.Add(i);
                }
            }
        }

        public static void InitializeHistorgramYAxis(ref ArrayList count, int min, int max)
        {
            if (count == null)
            {
                count = new ArrayList();
                for (int i = min; i < max; i++)
                {
                    count.Add(0);
                }
            }
        }

        //!goal - create histogram in plot.cs
        public static void CreateGoalHistorgramPlot(ref HistogramPlot goalHistogramPlot, ArrayList list)
        {
            goalHistogramPlot = new HistogramPlot();
            goalHistogramPlot.Color = Color.Blue;
            goalHistogramPlot.Filled = true;
            goalHistogramPlot.RectangleBrush = RectangleBrushes.HorizontalCenterFade.FaintGreenFade;
            goalHistogramPlot.Pen.Width = 2f;
            ArrayList xaxis = new ArrayList();
            xaxis.Add(1);
            goalHistogramPlot.AbscissaData = xaxis;

            goalHistogramPlot.DataSource = list;

        }

        public static void CreateHistorgramPlot(ref HistogramPlot distributionHistogramPlot, int min, int max, Color color, IRectangleBrush brush, ArrayList list)
        {
            distributionHistogramPlot = new HistogramPlot();
            distributionHistogramPlot.Color = color;
            distributionHistogramPlot.Filled = true;
            distributionHistogramPlot.RectangleBrush = brush;
            distributionHistogramPlot.Pen.Width = 2f;


            ArrayList xaxis = new ArrayList();
            for (int i = min; i <= max; i++)
            {
                xaxis.Add(i);
            }
            distributionHistogramPlot.AbscissaData = xaxis;
            distributionHistogramPlot.DataSource = list;
        }

        public static void CreateHistorgramPlots(ref HistogramPlot lowDistributionHistogramPlot, ref HistogramPlot medDistributionHistogramPlot, ref HistogramPlot highDistributionHistogramPlot, ArrayList lowCounts, ArrayList medCounts, ArrayList highCounts)
        {
            //Plot.CreateHistorgramPlot(ref lowDistributionHistogramPlot, Plot.LOW_MIN, Plot.LOW_MAX, Color.Red, RectangleBrushes.Solid.Red, lowCounts);
            //Plot.CreateHistorgramPlot(ref medDistributionHistogramPlot, Plot.MED_MIN, Plot.MED_MAX, Color.Blue, RectangleBrushes.Solid.Blue, medCounts);
            //Plot.CreateHistorgramPlot(ref highDistributionHistogramPlot, Plot.HIGH_MIN, Plot.HIGH_MAX, Color.Green, RectangleBrushes.Solid.Green, highCounts);
            Plot.CreateHistorgramPlot(ref lowDistributionHistogramPlot, Plot.LOW_MIN, Plot.LOW_MAX, Color.Red, RectangleBrushes.HorizontalCenterFade.FaintRedFade, lowCounts);
            Plot.CreateHistorgramPlot(ref medDistributionHistogramPlot, Plot.MED_MIN, Plot.MED_MAX, Color.Blue, RectangleBrushes.HorizontalCenterFade.FaintBlueFade, medCounts);
            Plot.CreateHistorgramPlot(ref highDistributionHistogramPlot, Plot.HIGH_MIN, Plot.HIGH_MAX, Color.Green, RectangleBrushes.HorizontalCenterFade.FaintGreenFade, highCounts);
        }

    }
}
