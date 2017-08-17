using System;
using System.Threading.Tasks;
namespace StockDataImport.Services.Interfaces
{
    public interface IStockDataDownloader
    {
        Uri Uri { get; set; }
        string OutputPath { get; set; }
        Task DownloadAsync();
    }
}
