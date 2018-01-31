using GalaSoft.MvvmLight.Ioc;
using StockDataImport.Services;
using StockDataImport.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDataImport
{
    internal static class StockDataImportStartup
    {
        internal static void RegisterServices()
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
