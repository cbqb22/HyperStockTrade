using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MIC.Database.Commons;
using MIC.Database.Commons.Enums;

namespace MIC.Database.Models
{
    /// <summary>
    /// 名　　称 ： 日足データ
    /// カテゴリ ： データ
    /// </summary>
    public class DailyPrice
    {
        private const string IndexName = "DailyPriceIndex";

        /// <summary>
        /// ID
        /// </summary>
        public int DailyPriceId { get; set; }

        /// <summary>
        /// 親テーブルのキー
        /// </summary>
        public int StockCompanyId { get; set; }


        /// <summary>
        /// 取引日付
        /// </summary>
        [Required]
        public DateTime DealDate { get; set; }


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
        [Required]
        public double Volume { get; set; }


        /// <summary>
        /// 売買代金
        /// </summary>
        [Required]
        public double Turnover { get; set; }


        /// <summary>
        /// 親テーブル
        /// </summary>
        public StockCompany StockCompany { get; set; }


        /// <summary>
        ///
        /// </summary>
        public DailyPrice()
        {
        }
    }
}
