using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MIC.Database.Commons;
using MIC.Database.Commons.Enums;
using System.Collections.Generic;

namespace MIC.Database.Models
{
    /// <summary>
    /// 名　　称 ： 株式銘柄
    /// カテゴリ ： データ
    /// </summary>
    public class StockCompany
    {
        // [0] 銘柄コード
        // [1] 市場コード
        // [2] 銘柄名称
        private const string IndexName = "StockCompanyIndex";

        /// <summary>
        /// ID
        /// </summary>
        public int StockCompanyId { get; set; }

        /// <summary>
        /// 銘柄コード
        /// </summary>
        [Required,
         Index(IndexName, IsUnique = true, Order = 0),
         MaxLength(ColumnMaxLength.StockCodeLength)]
        public string StockCode { get; set; }

        /// <summary>
        /// 市場コード
        /// </summary>
        [Required,
         Index(IndexName, IsUnique = true, Order = 1)]
        public MarketCode MarketCode { get; set; }

        /// <summary>
        /// 銘柄名称
        /// </summary>
        [Required(AllowEmptyStrings = true),
         MaxLength(ColumnMaxLength.VersionLength)]
        public string CompanyName { get; set; }


        /// <summary>
        /// 日足データ
        /// 子テーブルへのリレーション
        /// </summary>
        public virtual ICollection<DailyPrice> DailyPrices { get; set; }


        /// <summary>
        ///
        /// </summary>
        public StockCompany()
        {
        }
    }
}
