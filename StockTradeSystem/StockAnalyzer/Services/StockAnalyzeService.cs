using MIC.Database.Commons.Enums;
using MIC.Database.Connection.DataContexts;
using MIC.Database.Connection.Services.Interfaces;
using StockAnalyzer.Models;
using StockAnalyzer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Services
{
    public class StockAnalyzeService : IStockAnalyzeService
    {
        #region Services

        private readonly IDataContextFactory<DataContext> _dataContextFactory;

        #endregion

        #region Fields

        private int _limitLowPrice = 150;
        private double _magnification = 1.5;
        private int _minTurnover = 500 * 10000;

        #endregion

        #region Constractor

        public StockAnalyzeService(IDataContextFactory<DataContext> factory)
        {
            _dataContextFactory = factory;
        }

        #endregion

        #region Public

        public IEnumerable<PickedStockData> Analyze(DateTime start, DateTime end)
        {
            using (var context = _dataContextFactory.Create())
            {
                var picked = context.StockCompany
                                    .Select(x => new { x.StockCompanyId, x.StockCode, x.MarketCode, x.CompanyName, DailyPrices = x.DailyPrices.Where(d => start <= d.DealDate && d.DealDate <= end) })
                                    .Where(x => x.DailyPrices.OrderByDescending(d => d.DealDate).FirstOrDefault().ClosingPrice * _magnification > x.DailyPrices.Max(m => m.ClosingPrice) &&
                                                x.DailyPrices.Average(m => m.Volume) * 20 <= x.DailyPrices.Max(m => m.Volume) &&
                                                _minTurnover <= x.DailyPrices.Average(a => a.Turnover) &&
                                                (x.MarketCode == MarketCode.TSE_Mothers || x.MarketCode == MarketCode.JQ))
                                    //(x.MarketCode == MarketCode.TSE_Mothers || x.MarketCode == MarketCode.JQ_Standard))
                                    .ToList()
                                    .Select(x => new PickedStockData
                                    {
                                        StockCompanyId = x.StockCompanyId,
                                        StockCode = x.StockCode,
                                        MarketCode = x.MarketCode,
                                        CompanyName = x.CompanyName,
                                        CurrentPrice = x.DailyPrices.OrderByDescending(p => p.DealDate).First().ClosingPrice,
                                        MaxPrice = x.DailyPrices.Max(m => m.ClosingPrice),
                                        MinPrice = x.DailyPrices.Min(m => m.ClosingPrice),
                                        MaxVolume = x.DailyPrices.Max(m => m.Volume),
                                        AverageVolume = (int)x.DailyPrices.Average(m => m.Volume)
                                    })
                                    //.Where(x => _limitLowPrice <= x.CurrentPrice)
                                    //.Where(x => x.CurrentPrice <= x.MaxPrice / _magnification)
                                    .ToList();

                return picked;
            }
        }

        public IEnumerable<PickedStockData> Analyze2(DateTime start, DateTime end)
        {
            using (var context = _dataContextFactory.Create())
            {
                var picked = context.StockCompany
                                    .Select(x => new { x.StockCompanyId, x.StockCode, x.MarketCode, x.CompanyName, DailyPrices = x.DailyPrices.Where(d => start <= d.DealDate && d.DealDate <= end) })
                                    .Where(x => x.DailyPrices.OrderByDescending(d => d.DealDate).FirstOrDefault().ClosingPrice * _magnification <= x.DailyPrices.Max(m => m.ClosingPrice) &&
                                    //.Where(x => x.DailyPrices.Min(m => m.ClosingPrice) * magnification <= x.DailyPrices.Max(m => m.ClosingPrice) &&
                                                _minTurnover <= x.DailyPrices.Average(a => a.Turnover) &&
                                                (x.MarketCode == MarketCode.TSE_Mothers || x.MarketCode == MarketCode.JQ))
                                    .ToList()
                                    .Select(x => new PickedStockData
                                    {
                                        StockCompanyId = x.StockCompanyId,
                                        StockCode = x.StockCode,
                                        MarketCode = x.MarketCode,
                                        CompanyName = x.CompanyName,
                                        CurrentPrice = x.DailyPrices.OrderByDescending(p => p.DealDate).First().ClosingPrice,
                                        MaxPrice = x.DailyPrices.Max(m => m.ClosingPrice),
                                        MinPrice = x.DailyPrices.Min(m => m.ClosingPrice)
                                    })
                                    .Where(x => _limitLowPrice <= x.CurrentPrice)
                                    .Where(x => x.CurrentPrice <= x.MaxPrice / _magnification)
                                    .ToList();

                return picked;
            }
        }

        public IEnumerable<PickedStockData> Analyze3(DateTime start, DateTime end)
        {
            using (var context = _dataContextFactory.Create())
            {
                var picked = context.StockCompany
                                    .Select(x => new { x.StockCompanyId, x.StockCode, x.MarketCode, x.CompanyName, DailyPrices = x.DailyPrices.Where(d => start <= d.DealDate && d.DealDate <= end) })
                                    .Where(x => x.DailyPrices.OrderByDescending(d => d.DealDate).FirstOrDefault().ClosingPrice * _magnification > x.DailyPrices.Max(m => m.ClosingPrice) &&
                                                //.Where(x => x.DailyPrices.Min(m => m.ClosingPrice) * magnification <= x.DailyPrices.Max(m => m.ClosingPrice) &&
                                                _minTurnover <= x.DailyPrices.Average(a => a.Turnover)
                                           //&& (x.MarketCode == MarketCode.TSE_Mothers || x.MarketCode == MarketCode.JQ))
                                           )
                                    .ToList()
                                    .Select(x => new PickedStockData
                                    {
                                        StockCompanyId = x.StockCompanyId,
                                        StockCode = x.StockCode,
                                        MarketCode = x.MarketCode,
                                        CompanyName = x.CompanyName,
                                        CurrentPrice = x.DailyPrices.OrderByDescending(p => p.DealDate).First().ClosingPrice,
                                        MaxPrice = x.DailyPrices.Max(m => m.ClosingPrice),
                                        MinPrice = x.DailyPrices.Min(m => m.ClosingPrice)
                                    })
                                    .Where(x => _limitLowPrice <= x.CurrentPrice)
                                    .Where(x => x.CurrentPrice <= x.MaxPrice / _magnification)
                                    .ToList();

                return picked;
            }
        }


        /// <summary>
        /// 期間中に50倍の出来高が１本以上存在するもの
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<PickedStockData> Analyze4(DateTime start, DateTime end)
        {
            using (var context = _dataContextFactory.Create())
            {
                var picked = context.StockCompany
                                    .GroupBy(x => new { x.MarketCode, x.StockCode, x.CompanyName })
                                    .Select(x => new { x.Key.MarketCode, x.Key.StockCode, x.Key.CompanyName, DailyPrices = x.SelectMany(xx => xx.DailyPrices).Where(d => start <= d.DealDate && d.DealDate <= end) })
                                    .Select(x => new { x.StockCode, x.MarketCode, x.CompanyName, x.DailyPrices, Average10Volume = x.DailyPrices.OrderByDescending(d => d.DealDate).Take(10).Average(d => d.Volume), MaxVolume = x.DailyPrices.Max(d => d.Volume) })
                                    .Where(x => x.Average10Volume * 50 <= x.MaxVolume && _minTurnover <= x.DailyPrices.Average(a => a.Turnover)
                                                && (x.MarketCode == MarketCode.TSE_Mothers || x.MarketCode == MarketCode.JQ)
                                           )
                                    .ToList()
                                    .Select(x => new PickedStockData
                                    {
                                        //StockCompanyId = x.StockCompanyId,
                                        StockCode = x.StockCode,
                                        MarketCode = x.MarketCode,
                                        CompanyName = x.CompanyName,
                                        CurrentPrice = x.DailyPrices.OrderByDescending(p => p.DealDate).First().ClosingPrice,
                                        MaxPrice = x.DailyPrices.Max(m => m.ClosingPrice),
                                        MinPrice = x.DailyPrices.Min(m => m.ClosingPrice),
                                        AverageVolume = x.Average10Volume,
                                        MaxVolume = x.MaxVolume
                                    })
                                    .Where(x => _limitLowPrice <= x.CurrentPrice)
                                    .ToList();

                return picked;
            }
        }

        /// <summary>
        /// 期間中にX倍の出来高がY本以上存在するもの
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<PickedStockData> Analyze5(DateTime start, DateTime end)
        {
            using (var context = _dataContextFactory.Create())
            {
                var picked = context.StockCompany
                                    .GroupBy(x => new { x.MarketCode, x.StockCode, x.CompanyName })
                                    .Select(x => new { x.Key.MarketCode, x.Key.StockCode, x.Key.CompanyName, DailyPrices = x.FirstOrDefault().DailyPrices.Where(d => start <= d.DealDate && d.DealDate <= end) })
                                    .Select(x => new { x.StockCode, x.MarketCode, x.CompanyName, x.DailyPrices, CurrentPrice = x.DailyPrices.OrderByDescending(p => p.DealDate).FirstOrDefault().ClosingPrice, MaxPrice = x.DailyPrices.Max(m => m.ClosingPrice), MinPrice = x.DailyPrices.Min(m => m.ClosingPrice), Average10Volume = x.DailyPrices.OrderByDescending(d => d.DealDate).Take(10).Average(d => d.Volume), MaxVolume = x.DailyPrices.Max(d => d.Volume) })
                                    .Where(x => x.MarketCode == MarketCode.TSE_Mothers || x.MarketCode == MarketCode.JQ)
                                    //.Where(x => _minTurnover <= x.DailyPrices.Average(a => a.Turnover))
                                    .Where(x => 20 <= x.DailyPrices.Where(d => x.Average10Volume * 10 <= d.Volume).Count())
                                    .Where(x => x.CurrentPrice * 1.1 >= x.MaxPrice)
                                    .ToList()
                                    .Select(x => new PickedStockData
                                    {
                                        //StockCompanyId = x.StockCompanyId,
                                        StockCode = x.StockCode,
                                        MarketCode = x.MarketCode,
                                        CompanyName = x.CompanyName,
                                        CurrentPrice = x.CurrentPrice,
                                        MaxPrice = x.MaxPrice,
                                        MinPrice = x.MinPrice,
                                        AverageVolume = x.Average10Volume,
                                        MaxVolume = x.MaxVolume
                                    })
                                    .Where(x => _limitLowPrice <= x.CurrentPrice)
                                    .ToList();

                return picked;
            }
        }




        #endregion
    }
}
