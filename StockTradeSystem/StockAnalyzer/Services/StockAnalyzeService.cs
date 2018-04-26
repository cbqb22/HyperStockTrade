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
        private int _minTurnover = 500 * 10000;

        #endregion

        #region Constractor

        public StockAnalyzeService(IDataContextFactory<DataContext> factory)
        {
            _dataContextFactory = factory;
        }

        #endregion

        #region Public

        /// <summary>
        /// 期間中にX倍の出来高がY本以上存在するもの
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<PickedStockData> Analyze(DateTime start, DateTime end, AnalyzeType type)
        {
            using (var context = _dataContextFactory.Create())
            {
                switch (type)
                {
                    case AnalyzeType.Type1:

                        var picked = context.StockCompany
                                            .GroupBy(x => new { x.MarketCode, x.StockCode, x.CompanyName })
                                            .Select(x => new { x.Key.MarketCode, x.Key.StockCode, x.Key.CompanyName, DailyPrices = x.FirstOrDefault().DailyPrices.Where(d => start <= d.DealDate && d.DealDate <= end) })
                                            .Select(x => new { x.StockCode, x.MarketCode, x.CompanyName, x.DailyPrices, AveragePrice = x.DailyPrices.Average(xx => xx.ClosingPrice), CurrentPrice = x.DailyPrices.OrderByDescending(p => p.DealDate).FirstOrDefault().ClosingPrice, MaxPrice = x.DailyPrices.Max(m => m.ClosingPrice), MinPrice = x.DailyPrices.Where(d => d.ClosingPrice != 0).Min(m => m.ClosingPrice), AverageVolume = x.DailyPrices.Average(d => d.Volume)  , Average10Volume = x.DailyPrices.OrderByDescending(d => d.DealDate).Take(10).Average(d => d.Volume), MaxVolume = x.DailyPrices.Max(d => d.Volume) })
                                            .Where(x => x.MarketCode == MarketCode.TSE_Mothers || x.MarketCode == MarketCode.JQ)
                                            .Where(x => _minTurnover <= x.DailyPrices.Average(a => a.Turnover))
                                            .Where(x => 5 <= x.DailyPrices.Where(d => x.AverageVolume * 5 <= d.Volume).Count())
                                            .Where(x => x.CurrentPrice <= x.AveragePrice && x.AveragePrice <= x.CurrentPrice * 1.2)
                                            .Select(x => new PickedStockData
                                            {
                                                //StockCompanyId = x.StockCompanyId,
                                                StockCode = x.StockCode,
                                                MarketCode = x.MarketCode,
                                                CompanyName = x.CompanyName,
                                                CurrentPrice = x.CurrentPrice,
                                                MaxPrice = x.MaxPrice,
                                                MinPrice = x.MinPrice,
                                                AverageVolume = (int)x.AverageVolume,
                                                MaxVolume = x.MaxVolume
                                            })
                                            .Where(x => _limitLowPrice <= x.CurrentPrice)
                                            .ToList();

                        return picked;
                }

            }

            return null;
        }
    }


    #endregion
}
