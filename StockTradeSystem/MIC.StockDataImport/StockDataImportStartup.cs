using GalaSoft.MvvmLight.Ioc;
using MIC.StockDataImport.Services;
using MIC.StockDataImport.Services.Interfaces;

namespace MIC.StockDataImport
{
    public static class StockDataImportStartup
    {
        public static void RegisterServices()
        {
            // KB-Com 株価データサイトは2017/12/31でサービス終了 
            //SimpleIoc.Default.Register<IStockDataDownloadService, DailyDataDownloadService>();
            //SimpleIoc.Default.Register<IStockDataImportService, DailyDataImportService>();

            SimpleIoc.Default.Register<IStockDataDownloadService, MuzinzouDataDownloadService>();
            SimpleIoc.Default.Register<IStockDataImportService, MuzinzouDailyDataImportService>();
            SimpleIoc.Default.Register<IStockDataBackupService, StockDataBackupService>();
        }
    }
}
