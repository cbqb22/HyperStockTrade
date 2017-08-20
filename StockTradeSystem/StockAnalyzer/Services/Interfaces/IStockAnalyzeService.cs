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
        PickedStockData Analyze(DateTime start, DateTime end);
    }
}
