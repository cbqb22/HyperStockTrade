using System;
using System.Threading.Tasks;
namespace StockDataImport.Services.Interfaces
{
    public interface IStockDataImportService
    {
        Task<bool> ImportAsync(string FilePath);
    }
}
