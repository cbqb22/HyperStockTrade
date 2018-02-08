using StockAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Services.Interfaces
{
    public interface IStockAnalyzeService
    {
        IEnumerable<PickedStockData> Analyze(DateTime start, DateTime end);
        IEnumerable<PickedStockData> Analyze2(DateTime start, DateTime end);
        IEnumerable<PickedStockData> Analyze3(DateTime start, DateTime end);
        IEnumerable<PickedStockData> Analyze4(DateTime start, DateTime end);
    }
}
