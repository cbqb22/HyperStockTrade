using GalaSoft.MvvmLight.Ioc;
using StockAnalyzer.Services;
using StockAnalyzer.Services.Interfaces;

namespace StockAnalyzer
{
    public static class StoclAnalyzerStartup
    {
        public static void RegisterServices()
        {
            SimpleIoc.Default.Register<IStockAnalyzeService, StockAnalyzeService>();
            SimpleIoc.Default.Register<ICsvService, CsvService>();
        }
    }
}
