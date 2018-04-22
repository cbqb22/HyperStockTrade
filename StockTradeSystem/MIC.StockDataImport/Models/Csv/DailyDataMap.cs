using CsvHelper.Configuration;
using System;

namespace MIC.StockDataImport.Models.Csv
{
    public class DailyDataMap : CsvClassMap<DailyData>
    {
        [Obsolete]
        public override void CreateMap()
        {
            Map(m => m.StockMarketCode).Index(0);
            Map(m => m.CompanyName).Index(1);
            Map(m => m.MarketName).Index(2);
            Map(m => m.OpeningPrice).Index(3);
            Map(m => m.HighPrice).Index(4);
            Map(m => m.LowPrice).Index(5);
            Map(m => m.ClosingPrice).Index(6);
            Map(m => m.Volume).Index(7);
            Map(m => m.Turnover).Index(8);
        }
    }
}
