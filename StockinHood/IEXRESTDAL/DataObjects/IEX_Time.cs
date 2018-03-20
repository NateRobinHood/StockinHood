using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEXRESTDAL.DataObjects
{
    public class IEX_Chart_Minute_Data
    {
        public string date { get; set; }
        public string minute { get; set; }
        public string label { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double average { get; set; }
        public double volume { get; set; }
        public double notional { get; set; }
        public double numberOfTrades { get; set; }
        public double marketHigh { get; set; }
        public double marketLow { get; set; }
        public double marketAverage { get; set; }
        public double marketVolume { get; set; }
        public double marketNotional { get; set; }
        public double marketNumberOfTrades { get; set; }
        public double changeOverTime { get; set; }
        public double marketChangeOverTime { get; set; }
    }

    public class IEX_Chart_Day_Data
    {
        public string date { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public double volume { get; set; }
        public double unadjustedVolume { get; set; }
        public double change { get; set; }
        public double changePercent { get; set; }
        public double vwap { get; set; }
        public string label { get; set; }
        public double changeOverTime { get; set; }
    }
}
