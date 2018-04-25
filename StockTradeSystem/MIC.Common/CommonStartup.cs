using GalaSoft.MvvmLight.Ioc;
using MIC.Common.Date.Services;
using MIC.Common.Date.Services.Interfaces;
using MIC.Common.Dialogs.Interfaces;
using MIC.Common.Dialogs.Services;

namespace MIC.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommonStartup
    {
        /// <summary>
        /// 
        /// </summary>
        public static void RegisterServices()
        {
            SimpleIoc.Default.Register<IHolidayCheckService, ToshoStockExchangeHolidayCheckService>();
            SimpleIoc.Default.Register<IDialogParameterService, DialogParameterService>();
        }
    }
}
