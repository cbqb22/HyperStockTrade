using System;
using System.Threading.Tasks;
namespace MIC.StockDataImport.Services.Interfaces
{
    public interface IStockDataDownloadService
    {
        Uri Uri { get; set; }
        string OutputPath { get; set; }
        Task DownloadAsync(DateTime date);
    }
}
