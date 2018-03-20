using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEXRESTDAL.Data_Objects
{
    public class IEX_Chart_Day
    {
        public List<IEX_Chart_Minute_Data> Minutes = new List<IEX_Chart_Minute_Data>();
    }

    public class IEX_Chart_Month
    {
        public List<IEX_Chart_Day_Data> Days = new List<IEX_Chart_Day_Data>();
    }

    public class IEX_Chart_Year
    {
        public List<IEX_Chart_Day_Data> Days = new List<IEX_Chart_Day_Data>();
    }
}
