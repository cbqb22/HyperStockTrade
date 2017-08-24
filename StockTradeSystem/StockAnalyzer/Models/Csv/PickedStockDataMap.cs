using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Models.Csv
{
    public class PickedStockDataMap : CsvClassMap<PickedStockData>
    {
        [Obsolete]
        public override void CreateMap()
        {
            Map(m => m.StockCode).Index(0);
            Map(m => m.MarketCode).Index(1);
            Map(m => m.CompanyName).Index(2);
            Map(m => m.CurrentPrice).Index(3);
            Map(m => m.MaxPrice).Index(4);
            Map(m => m.MinPrice).Index(5);
            Map(m => m.MaxVolume).Index(6);
            Map(m => m.AverageVolume).Index(7);
            Map(m => m.IsCsvOutput).Index(8);
        }
    }
}
