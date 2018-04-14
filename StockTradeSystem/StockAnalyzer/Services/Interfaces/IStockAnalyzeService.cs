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
        IEnumerable<PickedStockData> Analyze(DateTime start, DateTime end, AnalyzeType type);
    }
}
