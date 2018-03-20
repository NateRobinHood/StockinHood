using IEXRESTDAL.Data_Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IEXRESTDAL
{
    public static class IEX_REST_DAL
    {
        //https://iextrading.com/developer/docs/

        public static IEX_Quote GetQuote(string symbol)
        {
            string RequestUri = string.Format("https://api.iextrading.com/1.0/stock/{0}/quote", symbol);
            string content = MakeRequest(RequestUri);
            if (!string.IsNullOrEmpty(content))
            {
                IEX_Quote contentData = JsonConvert.DeserializeObject<IEX_Quote>(content);

                return contentData;
            }

            return null;
        }

        public static IEX_Chart_Day GetChartByDay(string symbol, int year, int month, int day)
        {
            string RequestUri = string.Format("https://api.iextrading.com/1.0/stock/{0}/chart/date/{1:D4}{2:D2}{3:D2}", symbol, year, month, day);
            string content = MakeRequest(RequestUri);
            if (!string.IsNullOrEmpty(content))
            {
                IEX_Chart_Day contentData = JsonConvert.DeserializeObject<IEX_Chart_Day>(content);

                return contentData;
            }

            return null;
        }

        public static IEX_Chart_Day GetChartLatestDay(string symbol)
        {
            string RequestUri = string.Format("https://api.iextrading.com/1.0/stock/{0}/chart/1d", symbol);
            string content = MakeRequest(RequestUri);
            if (!string.IsNullOrEmpty(content))
            {
                IEX_Chart_Day contentData = JsonConvert.DeserializeObject<IEX_Chart_Day>(content);

                return contentData;
            }

            return null;
        }

        public static IEX_Chart_Month GetChartLastMonth(string symbol)
        {
            string RequestUri = string.Format("https://api.iextrading.com/1.0/stock/{0}/chart/1m", symbol);
            string content = MakeRequest(RequestUri);
            if (!string.IsNullOrEmpty(content))
            {
                IEX_Chart_Month contentData = JsonConvert.DeserializeObject<IEX_Chart_Month>(content);

                return contentData;
            }

            return null;
        }

        public static IEX_Chart_Month GetChartLastThreeMonths(string symbol)
        {
            string RequestUri = string.Format("https://api.iextrading.com/1.0/stock/{0}/chart/3m", symbol);
            string content = MakeRequest(RequestUri);
            if (!string.IsNullOrEmpty(content))
            {
                IEX_Chart_Month contentData = JsonConvert.DeserializeObject<IEX_Chart_Month>(content);

                return contentData;
            }

            return null;
        }

        public static IEX_Chart_Month GetChartLastSixMonths(string symbol)
        {
            string RequestUri = string.Format("https://api.iextrading.com/1.0/stock/{0}/chart/6m", symbol);
            string content = MakeRequest(RequestUri);
            if (!string.IsNullOrEmpty(content))
            {
                IEX_Chart_Month contentData = JsonConvert.DeserializeObject<IEX_Chart_Month>(content);

                return contentData;
            }

            return null;
        }

        public static IEX_Chart_Year GetChartLastYear(string symbol)
        {
            string RequestUri = string.Format("https://api.iextrading.com/1.0/stock/{0}/chart/1y", symbol);
            string content = MakeRequest(RequestUri);
            if (!string.IsNullOrEmpty(content))
            {
                IEX_Chart_Year contentData = JsonConvert.DeserializeObject<IEX_Chart_Year>(content);

                return contentData;
            }

            return null;
        }

        public static IEX_Chart_Year GetChartLastTwoYears(string symbol)
        {
            string RequestUri = string.Format("https://api.iextrading.com/1.0/stock/{0}/chart/2y", symbol);
            string content = MakeRequest(RequestUri);
            if (!string.IsNullOrEmpty(content))
            {
                IEX_Chart_Year contentData = JsonConvert.DeserializeObject<IEX_Chart_Year>(content);

                return contentData;
            }

            return null;
        }

        public static IEX_Chart_Year GetChartLastFiveYears(string symbol)
        {
            string RequestUri = string.Format("https://api.iextrading.com/1.0/stock/{0}/chart/5y", symbol);
            string content = MakeRequest(RequestUri);
            if (!string.IsNullOrEmpty(content))
            {
                IEX_Chart_Year contentData = JsonConvert.DeserializeObject<IEX_Chart_Year>(content);

                return contentData;
            }

            return null;
        }

        public static IEX_Chart_Year GetChartLastYearToDate(string symbol)
        {
            string RequestUri = string.Format("https://api.iextrading.com/1.0/stock/{0}/chart/ytd", symbol);
            string content = MakeRequest(RequestUri);
            if (!string.IsNullOrEmpty(content))
            {
                IEX_Chart_Year contentData = JsonConvert.DeserializeObject<IEX_Chart_Year>(content);

                return contentData;
            }

            return null;
        }

        public static IEX_Earnings GetEarnings(string symbol)
        {
            string RequestUri = string.Format("https://api.iextrading.com/1.0/stock/{0}/earnings", symbol);
            string content = MakeRequest(RequestUri);
            if (!string.IsNullOrEmpty(content))
            {
                IEX_Earnings contentData = JsonConvert.DeserializeObject<IEX_Earnings>(content);

                return contentData;
            }

            return null;
        }



        private static string MakeRequest(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string content = reader.ReadToEnd();
                            return content;
                        }
                    }
                    else
                    {
                        string exMessage = string.Format("Failed request to {0} with reponse code {1}", uri, response.StatusCode);
                        throw new Exception(exMessage); //eventually want to type this
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse response)
                {
                    string exMessage = string.Format("Failed request to {0} with reponse code {1}", uri, response.StatusCode);
                    throw new Exception(exMessage); //eventually want to type this
                }
                if (ex.InnerException is System.Net.Sockets.SocketException sockEx)
                {
                    string exMessage = string.Format("Failed request to {0} with socket error code {1} and message {2}", uri, sockEx.SocketErrorCode, sockEx.Message);
                    throw new Exception(exMessage); //eventually want to type this
                }
            }

            return string.Empty;
        }
    }
}
