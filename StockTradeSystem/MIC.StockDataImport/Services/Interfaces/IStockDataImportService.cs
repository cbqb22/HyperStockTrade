using System;
using System.Threading.Tasks;
namespace MIC.StockDataImport.Services.Interfaces
{
    public interface IStockDataImportService
    {
        Task<bool> ImportAsync(string filePath);
    }
}
