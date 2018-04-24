using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using System.Windows;
using MIC.Database;
using MIC.Common;
using CommonServiceLocator;

namespace StockDataImport
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {

        static App()
        {
            DispatcherHelper.Initialize();
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            StockDataImportStartup.RegisterServices();
            DatabaseStartup.RegisterServices();
            CommonStartup.RegisterServices();
        }

    }
}
