using StockAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Services.Interfaces
{
    public interface ICsvService
    {
        void Write(string path, IEnumerable<PickedStockData> records);
    }
}
