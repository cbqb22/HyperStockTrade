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

        public PickedStockData Analyze(DateTime start, DateTime end)
        {
            var magnification = 1.5;
            var minTurnover = 100000; 
            using (var context = _dataContextFactory.Create())
            {
                var picked = context.StockCompany
                                    .Where(x => x.DailyPrices.Min(m => m.ClosingPrice) * magnification <= x.DailyPrices.Max(m => m.ClosingPrice) &&
                                                minTurnover <= x.DailyPrices.Average(a => a.Turnover) &&
                                                (x.MarketCode == MarketCode.TSE_Mothers || x.MarketCode == MarketCode.JQ_Standard));
            }

            return new PickedStockData();
        }

        #endregion
    }
}
