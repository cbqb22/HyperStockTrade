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
            SimpleIoc.Default.Register<IStockDataDownloadService, DailyDataDownloadService>();
            SimpleIoc.Default.Register<IStockDataImportService, DailyDataImportService>();
        }
    }
}
