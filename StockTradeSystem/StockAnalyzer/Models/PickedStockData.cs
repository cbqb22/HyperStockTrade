using GalaSoft.MvvmLight;
using MIC.Database.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Models
{
    /// <summary>
    /// 抽出された株式データ
    /// </summary>
    public class PickedStockData : ViewModelBase
    {
        /// <summary>
        /// ID
        /// </summary>
        public int StockCompanyId { get { return _stockCompanyId; } set { Set(ref _stockCompanyId, value); } }
        private int _stockCompanyId;


        /// <summary>
        /// 銘柄コード
        /// </summary>
        public string StockCode { get { return _stockCode; } set { Set(ref _stockCode, value); } }
        private string _stockCode;

        /// <summary>
        /// 市場コード
        /// </summary>
        public MarketCode MarketCode { get { return _marketCode; } set { Set(ref _marketCode, value); } }
        private MarketCode _marketCode;

        /// <summary>
        /// 銘柄名称
        /// </summary>
        public string CompanyName { get { return _companyName; } set { Set(ref _companyName, value); } }
        private string _companyName;

        /// <summary>
        /// 現在価
        /// </summary>
        public double CurrentPrice { get { return _currentPrice; } set { Set(ref _currentPrice, value); } }
        private double _currentPrice;
    }
}
