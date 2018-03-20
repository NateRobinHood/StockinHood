using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEXRESTDAL.DataObjects
{
    public class IEX_Stats
    {
        public string companyName { get; set; }
        public double marketcap { get; set; }
        public double beta { get; set; }
        public double week52high { get; set; }
        public double week52low { get; set; }
        public double week52change { get; set; }
        public double shortInterest { get; set; }
        public string shortDate { get; set; }
        public double dividendRate { get; set; }
        public double dividendYield { get; set; }
        public string exDividendDate { get; set; }
        public double latestEPS { get; set; }
        public string latestEPSDate { get; set; }
        public double sharesOutstanding { get; set; }
        public double @float { get; set; }
        public double returnOnEquity { get; set; }
        public double consensusEPS { get; set; }
        public double numberOfEstimates { get; set; }
        public object EPSSurpriseDollar { get; set; }
        public double EPSSurprisePercent { get; set; }
        public string symbol { get; set; }
        public double EBITDA { get; set; }
        public double revenue { get; set; }
        public double grossProfit { get; set; }
        public double cash { get; set; }
        public double debt { get; set; }
        public double ttmEPS { get; set; }
        public double revenuePerShare { get; set; }
        public double revenuePerEmployee { get; set; }
        public double peRatioHigh { get; set; }
        public double peRatioLow { get; set; }
        public double returnOnAssets { get; set; }
        public object returnOnCapital { get; set; }
        public double profitMargin { get; set; }
        public double priceToSales { get; set; }
        public double priceToBook { get; set; }
        public double day200MovingAvg { get; set; }
        public double day50MovingAvg { get; set; }
        public double institutionPercent { get; set; }
        public object insiderPercent { get; set; }
        public double shortRatio { get; set; }
        public double year5ChangePercent { get; set; }
        public double year2ChangePercent { get; set; }
        public double year1ChangePercent { get; set; }
        public double ytdChangePercent { get; set; }
        public double month6ChangePercent { get; set; }
        public double month3ChangePercent { get; set; }
        public double month1ChangePercent { get; set; }
        public double day5ChangePercent { get; set; }
        public double day30ChangePercent { get; set; }
    }
}
