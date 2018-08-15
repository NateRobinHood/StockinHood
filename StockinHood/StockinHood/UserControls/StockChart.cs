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
        private Queue<Tuple<string, StockChartPeriod>> m_Queue = new Queue<Tuple<string, StockChartPeriod>>();
        private DataPoint m_PreviousDataPoint = null;
        private StripLine m_PreviousXAxisStripLine = null;
        private StripLine m_PreviousYAxisStripLine = null;

        public StockChart()
        {
            InitializeComponent();

            m_Worker.DoWork += M_Worker_DoWork;
            m_Worker.RunWorkerCompleted += M_Worker_RunWorkerCompleted;

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

        //Public Properties
        public string Symbol
        {
            get { return m_Symbol; }
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
            chartStockHistory.ChartAreas[0].AxisX.Interval = 0;
            chartStockHistory.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chartStockHistory.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartStockHistory.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            chartStockHistory.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartStockHistory.ChartAreas[0].AxisX.CustomLabels.Clear();
            chartStockHistory.ChartAreas[0].AxisY.CustomLabels.Clear();
            chartStockHistory.ChartAreas[0].AxisX.StripLines.Clear();
            chartStockHistory.ChartAreas[0].AxisY.StripLines.Clear();

            chartStockHistory.ChartAreas[0].AxisY.LabelStyle.Format = "${##.##}";

            switch (period)
            {
                case StockChartPeriod.Today:
                    chartStockHistory.ChartAreas[0].AxisX.Interval = 30;
                    chartStockHistory.ChartAreas[0].AxisX.MajorTickMark.Enabled = true;
                    chartStockHistory.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                    chartStockHistory.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                    chartStockHistory.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;

                    IEX_Chart_Minute_Data openMinute = chartStockHistory.Series[0].Points.Select(c => c.Tag as IEX_Chart_Minute_Data).First(c => c.marketNumberOfTrades > 0);
                    StripLine openStripLine = new StripLine();
                    openStripLine.IntervalOffset = openMinute.average;
                    openStripLine.StripWidth = 0;
                    openStripLine.BorderDashStyle = ChartDashStyle.Dash;
                    openStripLine.BorderColor = Color.LightGray;
                    openStripLine.Text = "Open";
                    openStripLine.TextAlignment = StringAlignment.Center;

                    chartStockHistory.ChartAreas[0].AxisY.StripLines.Add(openStripLine);
                    break;
                case StockChartPeriod.FiveDays:
                    chartStockHistory.ChartAreas[0].AxisX.Interval = 13;
                    chartStockHistory.ChartAreas[0].AxisX.MajorTickMark.Enabled = true;
                    chartStockHistory.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                    chartStockHistory.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                    chartStockHistory.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                    break;
                case StockChartPeriod.ThreeMonths:
                case StockChartPeriod.SixMonths:
                case StockChartPeriod.YearToDate:
                    chartStockHistory.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
                    chartStockHistory.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

                    List<Tuple<DataPoint, IEX_Chart_Day_Data>> seriesData = chartStockHistory.Series[0].Points.Select(c => new Tuple<DataPoint, IEX_Chart_Day_Data>(c,  c.Tag as IEX_Chart_Day_Data)).ToList();

                    double previousAxisValue = 0;
                    string previousDate = seriesData.First().Item2.date;
                    foreach (Tuple<DataPoint, IEX_Chart_Day_Data> dayData in seriesData)
                    {
                        if (ParsedDate.IsDifferentMonth(previousDate, dayData.Item2.date))
                        {
                            int firstOfMonthIdx = chartStockHistory.Series[0].Points.IndexOf(dayData.Item1);
                            double thisAxisValue = dayData.Item1.XValue;

                            CustomLabel monthLabel = new CustomLabel(previousAxisValue, thisAxisValue, ParsedDate.GetMonthText(previousDate), 0, LabelMarkStyle.None, GridTickTypes.None);

                            chartStockHistory.ChartAreas[0].AxisX.CustomLabels.Add(monthLabel);

                            previousAxisValue = thisAxisValue;

                            StripLine monthStripLine = new StripLine();
                            monthStripLine.IntervalOffset = firstOfMonthIdx;
                            monthStripLine.StripWidth = 0;
                            monthStripLine.BackColor = Color.LightGray;
                            monthStripLine.BorderDashStyle = ChartDashStyle.Dash;
                            monthStripLine.BorderColor = Color.LightGray;

                            chartStockHistory.ChartAreas[0].AxisX.StripLines.Add(monthStripLine);
                        }

                        previousDate = dayData.Item2.date;
                    }

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

                retChartData = LoadDataForMonths(symbol, monthData);
            }
            if (chartData is IEX_Chart_Year)
            {
                IEX_Chart_Year yearData = chartData as IEX_Chart_Year;

                retChartData = LoadDataForYears(symbol, yearData);
            }

            return retChartData;
        }

        private ChartSeriesData LoadDataForToday(string symbol, IEX_Chart_Day iexData)
        {
            ChartSeriesData retChartData = new ChartSeriesData();

            Series stockSeries = new Series("Candlestick");
            Series lineSeries = new Series("Line");

            stockSeries.ChartType = SeriesChartType.Stock;
            stockSeries.YValuesPerPoint = 2;
            stockSeries.BorderWidth = 5;

            lineSeries.ChartType = SeriesChartType.Line;
            lineSeries.Color = Color.DarkSlateBlue;

            int minuteCount = 1;
            IEX_Chart_Minute_Data previousMinute = iexData.Minutes.FirstOrDefault();
            foreach (IEX_Chart_Minute_Data minute in iexData.Minutes)
            {
                if (minute.marketVolume <= 0)
                {
                    minuteCount++;
                    continue;
                }

                DataPoint candlestickDataPoint = new DataPoint();
                candlestickDataPoint.SetValueXY(minuteCount, minute.marketHigh, minute.marketLow);
                candlestickDataPoint.AxisLabel = minute.label;
                candlestickDataPoint.Tag = minute;
                if (previousMinute?.marketAverage > minute?.marketAverage)
                {
                    candlestickDataPoint.BorderColor = ColorManager.TickDecreaseColor;
                    candlestickDataPoint.Color = ColorManager.TickDecreaseColor;
                }
                else if (previousMinute?.marketAverage < minute?.marketAverage)
                {
                    candlestickDataPoint.BorderColor = ColorManager.TickIncreaseColor;
                    candlestickDataPoint.Color = ColorManager.TickIncreaseColor;
                }

                stockSeries.Points.Add(candlestickDataPoint);

                DataPoint lineDataPoint = new DataPoint();
                lineDataPoint.SetValueXY(minuteCount, minute.marketAverage);
                lineDataPoint.AxisLabel = minute.label;
                lineDataPoint.Tag = minute;

                lineSeries.Points.Add(lineDataPoint);

                minuteCount++;
                previousMinute = minute;
            }

            retChartData.YAxisMinimum = iexData.Minutes.Where(c => c.marketNumberOfTrades > 0).Select(c => c.marketLow).Min();
            retChartData.YAxisMaximum = iexData.Minutes.Where(c => c.marketNumberOfTrades > 0).Select(c => c.marketHigh).Max();
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

            Series lineSeries = new Series("Line");

            lineSeries.ChartType = SeriesChartType.Line;
            lineSeries.Color = Color.DarkSlateBlue;
            lineSeries.BorderWidth = 4;

            int thirtyMinuteIncriment = 1;
            double minimumAverage = double.MaxValue;
            double maximumAverage = double.MinValue;
            for (int i = 0; i < sortedDates.Count; i++)
            {
                IEX_Chart_Day dayData = mulitDayMinuteData[sortedDates[i]];

                IEX_Chart_Minute_Data firstDataPoint = dayData.Minutes.First(c => c.marketNumberOfTrades > 0);

                DataPoint dataPointFirst = new DataPoint();
                dataPointFirst.SetValueXY(thirtyMinuteIncriment, firstDataPoint.marketAverage);
                dataPointFirst.AxisLabel = ParsedDate.FormatMinuteDate(firstDataPoint.date);
                dataPointFirst.Tag = firstDataPoint;

                lineSeries.Points.Add(dataPointFirst);
                thirtyMinuteIncriment++;

                minimumAverage = minimumAverage > firstDataPoint.marketAverage ? firstDataPoint.marketAverage : minimumAverage;
                maximumAverage = maximumAverage < firstDataPoint.marketAverage ? firstDataPoint.marketAverage : maximumAverage;

                //don't go to the end of the day, go to 30 minutes before the end
                for (int minute = 30; minute < dayData.Minutes.Count - 30; minute += 30)
                {
                    IEX_Chart_Minute_Data dataPoint = dayData.Minutes.Skip(minute).First(c => c.marketNumberOfTrades > 0);

                    DataPoint dataPointIncrement = new DataPoint();
                    dataPointIncrement.SetValueXY(thirtyMinuteIncriment, dataPoint.marketAverage);
                    dataPointIncrement.AxisLabel = ParsedDate.FormatMinuteDate(dataPoint.date);
                    dataPointIncrement.Tag = dataPoint;

                    lineSeries.Points.Add(dataPointIncrement);
                    thirtyMinuteIncriment++;

                    minimumAverage = minimumAverage > dataPoint.marketAverage ? dataPoint.marketAverage : minimumAverage;
                    maximumAverage = maximumAverage < dataPoint.marketAverage ? dataPoint.marketAverage : maximumAverage;
                }

                IEX_Chart_Minute_Data lastDataPoint = dayData.Minutes.Last(c => c.marketNumberOfTrades > 0);

                DataPoint dataPointLast = new DataPoint();
                dataPointLast.SetValueXY(thirtyMinuteIncriment, firstDataPoint.marketAverage);
                dataPointLast.AxisLabel = ParsedDate.FormatMinuteDate(lastDataPoint.date);
                dataPointLast.Tag = lastDataPoint;

                lineSeries.Points.Add(dataPointLast);
                thirtyMinuteIncriment++;

                minimumAverage = minimumAverage > lastDataPoint.marketAverage ? lastDataPoint.marketAverage : minimumAverage;
                maximumAverage = maximumAverage < lastDataPoint.marketAverage ? lastDataPoint.marketAverage : maximumAverage;
            }

            retChartData.YAxisMinimum = minimumAverage;
            retChartData.YAxisMaximum = maximumAverage;
            retChartData.XAxisMinimum = 0;
            retChartData.XAxisMaximum = thirtyMinuteIncriment;

            retChartData.Series.Add(lineSeries);

            return retChartData;
        }

        private ChartSeriesData LoadDataForMonths(string symbol, IEX_Chart_Month iexData)
        {
            ChartSeriesData retChartData = new ChartSeriesData();

            Series lineSeries = new Series("Line");

            lineSeries.ChartType = SeriesChartType.Line;
            lineSeries.Color = Color.DarkSlateBlue;
            lineSeries.BorderWidth = 4;

            int dayCount = 1;
            double minimumAverage = double.MaxValue;
            double maximumAverage = double.MinValue;
            foreach (IEX_Chart_Day_Data dayData in iexData.Days)
            {
                double dayAverage = (dayData.low + dayData.high) / 2;

                DataPoint dataPoint = new DataPoint();
                dataPoint.SetValueXY(dayCount, dayAverage);
                dataPoint.AxisLabel = ParsedDate.FormatMinuteDate(dayData.date);
                dataPoint.Tag = dayData;

                minimumAverage = minimumAverage > dayAverage ? dayAverage : minimumAverage;
                maximumAverage = maximumAverage < dayAverage ? dayAverage : maximumAverage;

                lineSeries.Points.Add(dataPoint);
                dayCount++;
            }

            retChartData.YAxisMinimum = minimumAverage;
            retChartData.YAxisMaximum = maximumAverage;
            retChartData.XAxisMinimum = 0;
            retChartData.XAxisMaximum = dayCount;

            retChartData.Series.Add(lineSeries);

            return retChartData;
        }

        private ChartSeriesData LoadDataForYears(string symbol, IEX_Chart_Year iexData)
        {
            ChartSeriesData retChartData = new ChartSeriesData();

            Series lineSeries = new Series("Line");

            lineSeries.ChartType = SeriesChartType.Line;
            lineSeries.Color = Color.DarkSlateBlue;
            lineSeries.BorderWidth = 4;

            int dayCount = 1;
            double minimumAverage = double.MaxValue;
            double maximumAverage = double.MinValue;
            foreach (IEX_Chart_Day_Data dayData in iexData.Days)
            {
                double dayAverage = (dayData.low + dayData.high) / 2;

                DataPoint dataPoint = new DataPoint();
                dataPoint.SetValueXY(dayCount, dayAverage);
                dataPoint.AxisLabel = ParsedDate.FormatMinuteDate(dayData.date);
                dataPoint.Tag = dayData;

                minimumAverage = minimumAverage > dayAverage ? dayAverage : minimumAverage;
                maximumAverage = maximumAverage < dayAverage ? dayAverage : maximumAverage;

                lineSeries.Points.Add(dataPoint);
                dayCount++;
            }

            retChartData.YAxisMinimum = minimumAverage;
            retChartData.YAxisMaximum = maximumAverage;
            retChartData.XAxisMinimum = 0;
            retChartData.XAxisMaximum = dayCount;

            retChartData.Series.Add(lineSeries);

            return retChartData;
        }

        //Public Methods
        public void SetSymbol(string symbol)
        {
            m_Symbol = symbol;
            chartStockHistory.Titles.Clear();
            chartStockHistory.Titles.Add(string.Format("{0} Price History", m_Symbol));

            ChartSeriesData chartData = LoadData(m_Symbol, m_CurrentChartPeriod);
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

            SetChartStyle(m_CurrentChartPeriod);
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
                if(lblMonth == thisLinkLabel)
                    thisPeriod = StockChartPeriod.Month;
                if(lblThreeMonths == thisLinkLabel)
                    thisPeriod = StockChartPeriod.ThreeMonths;
                if (lblSixMonths == thisLinkLabel)
                    thisPeriod = StockChartPeriod.SixMonths;
                if (lblYearToDate == thisLinkLabel)
                    thisPeriod = StockChartPeriod.YearToDate;

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
            HitTestResult hitTest = chartStockHistory.HitTest(e.X, e.Y);

            if (hitTest != null && hitTest.ChartArea != null)
            {
                if (m_PreviousDataPoint != null)
                {
                    m_PreviousDataPoint.MarkerSize = 0;
                    m_PreviousDataPoint.MarkerStyle = MarkerStyle.None;
                    m_PreviousDataPoint.Label = string.Empty;
                }
                if (m_PreviousYAxisStripLine != null)
                {
                    chartStockHistory.ChartAreas[0].AxisY.StripLines.Remove(m_PreviousYAxisStripLine);
                }

                double xAxisValue = hitTest.ChartArea.AxisX.PixelPositionToValue(e.X);
                DataPoint theDataPoint = chartStockHistory.Series["Line"].Points.OrderBy(c => Math.Abs(xAxisValue - c.XValue)).FirstOrDefault();
                theDataPoint.MarkerStyle = MarkerStyle.Circle;
                theDataPoint.MarkerSize = 10;

                if (theDataPoint.Tag is IEX_Chart_Minute_Data)
                {
                    IEX_Chart_Minute_Data minuteData = theDataPoint.Tag as IEX_Chart_Minute_Data;

                    theDataPoint.Label = minuteData.label;

                    StripLine yAxisStripLine = new StripLine();
                    yAxisStripLine.IntervalOffset = minuteData.average;
                    yAxisStripLine.StripWidth = 0;
                    yAxisStripLine.BorderColor = Color.LightGray;
                    yAxisStripLine.BorderDashStyle = ChartDashStyle.Dash;
                    yAxisStripLine.BorderWidth = 2;
                    yAxisStripLine.Text = minuteData.average.ToString("$##.##");
                    yAxisStripLine.TextAlignment = StringAlignment.Near;

                    chartStockHistory.ChartAreas[0].AxisY.StripLines.Add(yAxisStripLine);

                    m_PreviousYAxisStripLine = yAxisStripLine;
                }
                if (theDataPoint.Tag is IEX_Chart_Day_Data)
                {
                    IEX_Chart_Day_Data dayData = theDataPoint.Tag as IEX_Chart_Day_Data;

                    theDataPoint.Label = dayData.label;

                    double dayAverage = (dayData.low + dayData.high) / 2;

                    StripLine yAxisStripLine = new StripLine();
                    yAxisStripLine.IntervalOffset = dayAverage;
                    yAxisStripLine.StripWidth = 0;
                    yAxisStripLine.BorderColor = Color.LightGray;
                    yAxisStripLine.BorderDashStyle = ChartDashStyle.Dash;
                    yAxisStripLine.BorderWidth = 2;
                    yAxisStripLine.Text = dayAverage.ToString("$##.##");
                    yAxisStripLine.TextAlignment = StringAlignment.Near;

                    chartStockHistory.ChartAreas[0].AxisY.StripLines.Add(yAxisStripLine);

                    m_PreviousYAxisStripLine = yAxisStripLine;
                }

                m_PreviousDataPoint = theDataPoint;
            }
        }

        private void M_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private void M_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
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
                if(date.Contains('-'))
                    return string.Format("{0}-{1}-{2}", date.Substring(5, 2), date.Substring(8, 2), date.Substring(0, 4));
                else
                    return string.Format("{0}-{1}-{2}", date.Substring(4, 2), date.Substring(6, 2), date.Substring(0, 4));
            }

            public static bool IsDifferentMonth(string date1, string date2)
            {
                if (date1.Contains('-'))
                {
                    if (date2.Contains('-'))
                    {
                        return (date1.Substring(5, 2) != date2.Substring(5, 2));
                    }
                    else
                    {
                        return (date1.Substring(5, 2) != date2.Substring(4, 2));
                    }
                }
                else
                {
                    if (date2.Contains('-'))
                    {
                        return (date1.Substring(4, 2) != date2.Substring(5, 2));
                    }
                    else
                    {
                        return (date1.Substring(4, 2) != date2.Substring(4, 2));
                    }
                }
            }

            public static string GetMonthText(string date)
            {
                string[] months = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthGenitiveNames;

                if (date.Contains('-'))
                {
                    int monthIdx = int.Parse(date.Substring(5, 2));
                    return months[monthIdx - 1];
                }
                else
                {
                    int monthIdx = int.Parse(date.Substring(4, 2));
                    return months[monthIdx - 1];
                }
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
