using GalaSoft.MvvmLight.Ioc;
using MIC.StockDataImport.Services;
using MIC.StockDataImport.Services.Interfaces;

namespace MIC.StockDataImport
{
    public static class StockDataImportStartup
    {
        public static void RegisterServices()
        {
            SimpleIoc.Default.Register<IStockDataDownloadService, MuzinzouDataDownloadService>();
            SimpleIoc.Default.Register<IStockDataImportService, MuzinzouDailyDataImportService>();
            SimpleIoc.Default.Register<IStockDataBackupService, StockDataBackupService>();
        }
    }
}
