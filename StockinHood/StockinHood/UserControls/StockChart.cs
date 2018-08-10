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

            chartStockHistory.Titles.Add(string.Format("{0} Price History", m_Symbol));
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
                lineSeries.Points.AddXY(minuteCount, minute.average);

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

            return retChartData;
        }

        #region Event Handlers

        private void ChartPeriod_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel thisLinkLabel = sender as LinkLabel;
            if (thisLinkLabel != null)
            {
                SetLinkColors(thisLinkLabel);
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
