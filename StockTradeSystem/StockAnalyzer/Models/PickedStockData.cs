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
        /// 現在値
        /// </summary>
        public double? CurrentPrice { get { return _currentPrice; } set { Set(ref _currentPrice, value); } }
        private double? _currentPrice;

        /// <summary>
        /// 最高値
        /// </summary>
        public double? MaxPrice { get { return _maxPrice; } set { Set(ref _maxPrice, value); } }
        private double? _maxPrice;

        /// <summary>
        /// 最安値
        /// </summary>
        public double? MinPrice { get { return _minPrice; } set { Set(ref _minPrice, value); } }
        private double? _minPrice;

        /// <summary>
        /// 最高出来高
        /// </summary>
        public double MaxVolume { get { return _maxVolume; } set { Set(ref _maxVolume, value); } }
        private double _maxVolume;

        /// <summary>
        /// 平均出来高
        /// </summary>
        public double AverageVolume { get { return _averageVolume; } set { Set(ref _averageVolume, value); } }
        private double _averageVolume;


        /// <summary>
        /// SSV出力フラグ
        /// </summary>
        public bool IsCsvOutput { get { return _isCsvOutput; } set { Set(ref _isCsvOutput, value); } }
        private bool _isCsvOutput;

    }
}
