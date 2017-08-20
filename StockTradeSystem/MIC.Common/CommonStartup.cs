using GalaSoft.MvvmLight.Ioc;
using MIC.Common.Date.Services;
using MIC.Common.Date.Services.Interfaces;

namespace MIC.Common
{
    public static class CommonStartup
    {
        public static void RegisterServices()
        {
            SimpleIoc.Default.Register<IHolidayCheckService, TolyoStockExchangeHolidayCheckService>();
        }
    }
}
