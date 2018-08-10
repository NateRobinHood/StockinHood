using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using IEXRESTDAL;
using IEXRESTDAL.DataObjects;
using StockinHood.Components;

namespace StockinHood.UserControls
{
    public partial class StockChart : UserControl
    {
        //Private Variables
        private StockChartPeriod m_CurrentChartPeriod = StockChartPeriod.Today;
        private string m_Symbol = "atvi";
        private BackgroundWorker m_Worker = new BackgroundWorker();
        private Queue<string> m_Queue = new Queue<string>();

        public StockChart()
        {
            InitializeComponent();

            SetLinkColors(lblToday);

            ChartSeriesData chartData = LoadData(m_Symbol, StockChartPeriod.Today);
            chartStockHistory.Series.Clear();

            foreach (Series data in chartData.Series)
            {
                chartStockHistory.Series.Add(data);
            }

            chartStockHistory.ChartAreas[0].RecalculateAxesScale();
            chartStockHistory.ChartAreas[0].AxisX.Minimum = chartData.XAxisMinimum;
            chartStockHistory.ChartAreas[0].AxisX.Maximum = chartData.XAxisMaximum;
            chartStockHistory.ChartAreas[0].AxisY.Minimum = chartData.YAxisMinimum;
            chartStockHistory.ChartAreas[0].AxisY.Maximum = chartData.YAxisMaximum;

            SetChartStyle(StockChartPeriod.Today);

            chartStockHistory.Titles.Add(string.Format("{0} Price History", m_Symbol));
            chartStockHistory.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Solid;
            chartStockHistory.ChartAreas[0].CursorX.LineColor = Color.Red;
        }

        //Private Methods
        private void SetLinkColors(LinkLabel currentLabel)
        {
            lblYearToDate.LinkColor = lblYearToDate == currentLabel ? Color.Black : Color.LightSteelBlue;
            lblSixMonths.LinkColor = lblSixMonths == currentLabel ? Color.Black : Color.LightSteelBlue;
            lblThreeMonths.LinkColor = lblThreeMonths == currentLabel ? Color.Black : Color.LightSteelBlue;
            lblMonth.LinkColor = lblMonth == currentLabel ? Color.Black : Color.LightSteelBlue;
            lblFiveDays.LinkColor = lblFiveDays == currentLabel ? Color.Black : Color.LightSteelBlue;
            lblToday.LinkColor = lblToday == currentLabel ? Color.Black : Color.LightSteelBlue;
        }

        private void SetChartStyle(StockChartPeriod period)
        {
            switch (period)
            {
                case StockChartPeriod.Today:
                    chartStockHistory.ChartAreas[0].AxisX.Interval = 30;
                    chartStockHistory.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                    chartStockHistory.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                    break;
                case StockChartPeriod.FiveDays:
                    chartStockHistory.ChartAreas[0].AxisX.Interval = 14;
                    chartStockHistory.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                    chartStockHistory.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                    break;
            }
        }

        private ChartSeriesData LoadData(string symbol, StockChartPeriod period)
        {
            ChartSeriesData retChartData = new ChartSeriesData();

            m_CurrentChartPeriod = period;

            object chartData = null;
            switch (period)
            {
                case StockChartPeriod.Today:
                    chartData = IEX_REST_DAL.GetChartLatestDay(symbol);
                    break;
                case StockChartPeriod.FiveDays:
                    chartData = IEX_REST_DAL.GetChartLastFiveDays(symbol);
                    break;
                case StockChartPeriod.Month:
                    chartData = IEX_REST_DAL.GetChartLastMonth(symbol);
                    break;
                case StockChartPeriod.ThreeMonths:
                    chartData = IEX_REST_DAL.GetChartLastThreeMonths(symbol);
                    break;
                case StockChartPeriod.SixMonths:
                    chartData = IEX_REST_DAL.GetChartLastSixMonths(symbol);
                    break;
                case StockChartPeriod.YearToDate:
                    chartData = IEX_REST_DAL.GetChartLastYearToDate(symbol);
                    break;
            }

            if (chartData is IEX_Chart_Day)
            {
                IEX_Chart_Day dayData = chartData as IEX_Chart_Day;

                retChartData = LoadDataForToday(symbol, dayData);
            }
            if (chartData is IEX_Chart_Multi_Day)
            {
                IEX_Chart_Multi_Day multiDayData = chartData as IEX_Chart_Multi_Day;

                retChartData = LoadDataForFiveDays(symbol, multiDayData);
            }
            if (chartData is IEX_Chart_Month)
            {
                IEX_Chart_Month monthData = chartData as IEX_Chart_Month;
            }
            if (chartData is IEX_Chart_Year)
            {
                IEX_Chart_Year yearData = chartData as IEX_Chart_Year;
            }

            return retChartData;
        }

        private ChartSeriesData LoadDataForToday(string symbol, IEX_Chart_Day iexData)
        {
            ChartSeriesData retChartData = new ChartSeriesData();

            Series stockSeries = new Series(string.Format("{0} Price History - CandleStick", symbol));
            Series lineSeries = new Series(string.Format("{0} Price History - Line", symbol));

            stockSeries.ChartType = SeriesChartType.Stock;
            stockSeries.YValuesPerPoint = 2;
            stockSeries.BorderWidth = 5;

            lineSeries.ChartType = SeriesChartType.Line;
            lineSeries.Color = Color.DarkSlateBlue;

            int minuteCount = 1;
            IEX_Chart_Minute_Data previousMinute = iexData.Minutes.FirstOrDefault();
            foreach (IEX_Chart_Minute_Data minute in iexData.Minutes)
            {
                if (minute.numberOfTrades == 0)
                {
                    minuteCount++;
                    continue;
                }

                DataPoint candlestickDataPoint = new DataPoint();
                candlestickDataPoint.SetValueXY(minuteCount, minute.high, minute.low);
                candlestickDataPoint.AxisLabel = minute.label;
                candlestickDataPoint.Tag = minute;
                if (previousMinute?.average > minute?.average)
                {
                    candlestickDataPoint.BorderColor = ColorManager.TickDecreaseColor;
                    candlestickDataPoint.Color = ColorManager.TickDecreaseColor;
                }
                else if (previousMinute?.average < minute?.average)
                {
                    candlestickDataPoint.BorderColor = ColorManager.TickIncreaseColor;
                    candlestickDataPoint.Color = ColorManager.TickIncreaseColor;
                }

                stockSeries.Points.Add(candlestickDataPoint);

                DataPoint lineDataPoint = new DataPoint();
                lineDataPoint.SetValueXY(minuteCount, minute.average);
                lineDataPoint.AxisLabel = minute.label;
                lineDataPoint.Tag = minute;

                lineSeries.Points.Add(lineDataPoint);

                minuteCount++;
                previousMinute = minute;
            }

            retChartData.YAxisMinimum = iexData.Minutes.Where(c => c.numberOfTrades > 0).Select(c => c.low).Min();
            retChartData.YAxisMaximum = iexData.Minutes.Select(c => c.high).Max();
            retChartData.XAxisMinimum = 0;
            retChartData.XAxisMaximum = (6.5 * 60);

            retChartData.Series.Add(stockSeries);
            retChartData.Series.Add(lineSeries);

            return retChartData;
        }

        private ChartSeriesData LoadDataForFiveDays(string symbol, IEX_Chart_Multi_Day iexData)
        {
            ChartSeriesData retChartData = new ChartSeriesData();

            //parse the date out of the iexData for each day and make request for each individual day
            Dictionary<ParsedDate, IEX_Chart_Day> mulitDayMinuteData = new Dictionary<ParsedDate, IEX_Chart_Day>();
            foreach (IEX_Chart_Day_Data dayData in iexData.Days)
            {
                ParsedDate parsedDate = new ParsedDate(dayData.date);

                mulitDayMinuteData.Add(parsedDate, IEX_REST_DAL.GetChartByDay(symbol, parsedDate.Year, parsedDate.Month, parsedDate.Day));
            }

            List<ParsedDate> sortedDates = mulitDayMinuteData.Keys.OrderBy(c => c).ToList();

            Series lineSeries = new Series(string.Format("{0} Price History - Line", symbol));

            lineSeries.ChartType = SeriesChartType.Line;
            lineSeries.Color = Color.DarkSlateBlue;
            lineSeries.BorderWidth = 4;

            int thirtyMinuteIncriment = 1;
            double minimumAverage = double.MaxValue;
            double maximumAverage = double.MinValue;
            for (int i = 0; i < sortedDates.Count; i++)
            {
                IEX_Chart_Day dayData = mulitDayMinuteData[sortedDates[i]];

                IEX_Chart_Minute_Data firstDataPoint = dayData.Minutes.First(c => c.numberOfTrades != 0);

                DataPoint dataPointFirst = new DataPoint();
                dataPointFirst.SetValueXY(thirtyMinuteIncriment, firstDataPoint.average);
                dataPointFirst.AxisLabel = ParsedDate.FormatMinuteDate(firstDataPoint.date);
                dataPointFirst.Tag = firstDataPoint;

                lineSeries.Points.Add(dataPointFirst);
                thirtyMinuteIncriment++;

                minimumAverage = minimumAverage > firstDataPoint.average ? firstDataPoint.average : minimumAverage;
                maximumAverage = maximumAverage < firstDataPoint.average ? firstDataPoint.average : maximumAverage;

                //don't go to the end of the day, go to 30 minutes before the end
                for (int minute = 30; minute < dayData.Minutes.Count - 30; minute += 30)
                {
                    IEX_Chart_Minute_Data dataPoint = dayData.Minutes.Skip(minute).First(c => c.numberOfTrades != 0);

                    DataPoint dataPointIncrement = new DataPoint();
                    dataPointIncrement.SetValueXY(thirtyMinuteIncriment, dataPoint.average);
                    dataPointIncrement.AxisLabel = ParsedDate.FormatMinuteDate(dataPoint.date);
                    dataPointIncrement.Tag = dataPoint;

                    lineSeries.Points.Add(dataPointIncrement);
                    thirtyMinuteIncriment++;

                    minimumAverage = minimumAverage > dataPoint.average ? dataPoint.average : minimumAverage;
                    maximumAverage = maximumAverage < dataPoint.average ? dataPoint.average : maximumAverage;
                }

                IEX_Chart_Minute_Data lastDataPoint = dayData.Minutes.Last(c => c.numberOfTrades != 0);

                DataPoint dataPointLast = new DataPoint();
                dataPointLast.SetValueXY(thirtyMinuteIncriment, firstDataPoint.average);
                dataPointLast.AxisLabel = ParsedDate.FormatMinuteDate(lastDataPoint.date);
                dataPointLast.Tag = lastDataPoint;

                lineSeries.Points.Add(dataPointLast);
                thirtyMinuteIncriment++;

                minimumAverage = minimumAverage > lastDataPoint.average ? lastDataPoint.average : minimumAverage;
                maximumAverage = maximumAverage < lastDataPoint.average ? lastDataPoint.average : maximumAverage;
            }

            retChartData.YAxisMinimum = minimumAverage;
            retChartData.YAxisMaximum = maximumAverage;
            retChartData.XAxisMinimum = 0;
            retChartData.XAxisMaximum = thirtyMinuteIncriment;

            retChartData.Series.Add(lineSeries);

            return retChartData;
        }

        #region Event Handlers

        private void ChartPeriod_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel thisLinkLabel = sender as LinkLabel;
            if (thisLinkLabel != null)
            {
                SetLinkColors(thisLinkLabel);

                StockChartPeriod thisPeriod = StockChartPeriod.Today;

                if (lblToday == thisLinkLabel)
                    thisPeriod = StockChartPeriod.Today;
                if (lblFiveDays == thisLinkLabel)
                    thisPeriod = StockChartPeriod.FiveDays;

                ChartSeriesData chartData = LoadData(m_Symbol, thisPeriod);
                chartStockHistory.Series.Clear();

                foreach (Series data in chartData.Series)
                {
                    chartStockHistory.Series.Add(data);
                }

                chartStockHistory.ChartAreas[0].RecalculateAxesScale();
                chartStockHistory.ChartAreas[0].AxisX.Minimum = chartData.XAxisMinimum;
                chartStockHistory.ChartAreas[0].AxisX.Maximum = chartData.XAxisMaximum;
                chartStockHistory.ChartAreas[0].AxisY.Minimum = chartData.YAxisMinimum;
                chartStockHistory.ChartAreas[0].AxisY.Maximum = chartData.YAxisMaximum;

                SetChartStyle(thisPeriod);
            }
        }

        private void chartStockHistory_MouseMove(object sender, MouseEventArgs e)
        {
            HitTestResult hitTest = chartStockHistory.HitTest(e.X, e.Y, ChartElementType.Gridlines);

            if (hitTest != null && hitTest.Series != null && hitTest.PointIndex < hitTest.Series.Points.Count)
            {
                DataPoint thisDataPoint = hitTest.Series.Points[hitTest.PointIndex];
                if (thisDataPoint != null && thisDataPoint.Tag != null)
                {
                    if (thisDataPoint.Tag is IEX_Chart_Minute_Data)
                    {
                        hitTest.Series.Points[hitTest.PointIndex].Label = ((IEX_Chart_Minute_Data)thisDataPoint.Tag).label;
                    }
                }
            }
        }

        #endregion

        public enum StockChartPeriod
        {
            Today = 0,
            FiveDays,
            Month,
            ThreeMonths,
            SixMonths,
            YearToDate
        }

        private class ParsedDate : IComparable
        {
            private int m_Year;
            private int m_Month;
            private int m_Day;

            public ParsedDate(string iexDate)
            {
                m_Year = int.Parse(iexDate.Substring(0, 4));
                m_Month = int.Parse(iexDate.Substring(5, 2));
                m_Day = int.Parse(iexDate.Substring(8, 2));
            }

            public int Year
            {
                get
                {
                    return m_Year;
                }
            }

            public int Month
            {
                get
                {
                    return m_Month;
                }
            }

            public int Day
            {
                get
                {
                    return m_Day;
                }
            }

            public int CompareTo(object obj)
            {
                if (obj is ParsedDate)
                {
                    ParsedDate comparingDate = obj as ParsedDate;
                    if (this.Year == comparingDate.Year &&
                        this.Month == comparingDate.Month &&
                        this.Day == comparingDate.Day)
                        return 0;

                    if (this.Year >= comparingDate.Year)
                    {
                        if (this.Month >= comparingDate.Month)
                        {
                            if (this.Day >= comparingDate.Day)
                            {
                                return 1;
                            }
                            else
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }

                }
                else
                {
                    throw new Exception(string.Format("Connot compare ParsedDate to {0}", obj.GetType().Name));
                }
            }

            public static string FormatMinuteDate(string date)
            {
                return string.Format("{0}-{1}-{2}", date.Substring(4, 2), date.Substring(6, 2), date.Substring(0, 4));
            }
        }

        private class ChartSeriesData
        {
            private List<Series> m_Series = new List<Series>();
            private double m_XAxisMin = 0;
            private double m_XAxisMax = 10;
            private double m_YAxisMin = 0;
            private double m_YAxisMax = 10;

            public ChartSeriesData()
            {

            }

            public ChartSeriesData(Series series, double xAxisMin, double xAxisMax, double yAxisMin, double yAxisMax)
            {
                m_Series.Add(series);
                m_XAxisMin = xAxisMin;
                m_XAxisMax = xAxisMax;
                m_YAxisMin = yAxisMin;
                m_YAxisMax = yAxisMax;
            }

            public List<Series> Series
            {
                get
                {
                    return m_Series;
                }
            }

            public double XAxisMinimum
            {
                get
                {
                    return m_XAxisMin;
                }
                set
                {
                    m_XAxisMin = value;
                }
            }

            public double XAxisMaximum
            {
                get
                {
                    return m_XAxisMax;
                }
                set
                {
                    m_XAxisMax = value;
                }
            }

            public double YAxisMinimum
            {
                get
                {
                    return m_YAxisMin;
                }
                set
                {
                    m_YAxisMin = value;
                }
            }

            public double YAxisMaximum
            {
                get
                {
                    return m_YAxisMax;
                }
                set
                {
                    m_YAxisMax = value;
                }
            }
        }
    }
}
