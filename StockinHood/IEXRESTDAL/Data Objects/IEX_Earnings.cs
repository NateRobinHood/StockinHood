using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEXRESTDAL.Data_Objects
{
    public class IEX_Earnings_Release_Data
    {
        public double actualEPS { get; set; }
        public double consensusEPS { get; set; }
        public double estimatedEPS { get; set; }
        public string announceTime { get; set; }
        public int numberOfEstimates { get; set; }
        public double EPSSurpriseDollar { get; set; }
        public string EPSReportDate { get; set; }
        public string fiscalPeriod { get; set; }
        public string fiscalEndDate { get; set; }
    }

    public class IEX_Earnings
    {
        public string symbol { get; set; }
        public List<IEX_Earnings_Release_Data> earnings = new List<IEX_Earnings_Release_Data>();
    }
}
