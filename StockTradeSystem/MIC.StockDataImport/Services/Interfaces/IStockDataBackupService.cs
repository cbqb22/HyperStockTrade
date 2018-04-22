using System;
using System.Threading.Tasks;
namespace MIC.StockDataImport.Services.Interfaces
{
    public interface IStockDataBackupService
    {
        Task BackupAsync();
    }
}
