using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.StockDataImport.Models.Csv
{
    public class DailyData
    {
        /// <summary>
        /// StockCode-MarketCode
        /// </summary>
        public string StockMarketCode { get; set; }

        /// <summary>
        /// 銘柄名
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 市場名
        /// </summary>
        public string MarketName { get; set; }

        /// <summary>
        /// 始値
        /// </summary>
        public double? OpeningPrice { get; set; }

        /// <summary>
        /// 高値
        /// </summary>
        public double? HighPrice { get; set; }

        /// <summary>
        /// 安値
        /// </summary>
        public double? LowPrice { get; set; }

        /// <summary>
        /// 終値
        /// </summary>
        public double? ClosingPrice { get; set; }


        /// <summary>
        /// 出来高
        /// </summary>
        public double Volume { get; set; }


        /// <summary>
        /// 売買代金
        /// </summary>
        public double Turnover { get; set; }



    }

    /// <summary>
    /// DailyDataEqualityComparer
    /// </summary>
    public class DailyDataEqualityComparer : IEqualityComparer<DailyData>
    {
        public bool Equals(DailyData x, DailyData y)
        {
            if (x.StockMarketCode == y.StockMarketCode &&
               x.MarketName == y.MarketName)
                return true;

            return false;
        }

        public int GetHashCode(DailyData obj)
        {
            return (string.IsNullOrEmpty(obj.StockMarketCode) ? 0 : obj.StockMarketCode.GetHashCode()) +
                   (string.IsNullOrEmpty(obj.MarketName) ? 0 : obj.MarketName.GetHashCode());
        }
    }
}
