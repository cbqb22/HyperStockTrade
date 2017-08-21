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
            var limitLowPrice = 150;
            var magnification = 1.5;
            var minTurnover = 5000 * 10000;
            using (var context = _dataContextFactory.Create())
            {
                var picked = context.StockCompany
                                    .Select(x => new { x.StockCompanyId, x.StockCode, x.MarketCode, x.CompanyName, DailyPrices = x.DailyPrices.Where(d => start <= d.DealDate && d.DealDate <= end) })
                                    .Where(x => x.DailyPrices.Min(m => m.ClosingPrice) * magnification <= x.DailyPrices.Max(m => m.ClosingPrice) &&
                                                minTurnover <= x.DailyPrices.Average(a => a.Turnover) &&
                                                (x.MarketCode == MarketCode.TSE_Mothers || x.MarketCode == MarketCode.JQ_Standard))
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
                                    .Where(x => limitLowPrice <= x.CurrentPrice)
                                    .Where(x => x.CurrentPrice <= x.MaxPrice / magnification)
                                    .ToList();

                return picked;
            }
        }

        #endregion
    }
}
