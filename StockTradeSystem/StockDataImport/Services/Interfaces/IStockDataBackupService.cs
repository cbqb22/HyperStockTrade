using System;
using System.Threading.Tasks;
namespace StockDataImport.Services.Interfaces
{
    public interface IStockDataBackupService
    {
        Task BackupAsync();
    }
}
