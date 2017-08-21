using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Practices.ServiceLocation;
using MIC.Database;

namespace StockAnalyzer
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
            StoclAnalyzerStartup.RegisterServices();
            DatabaseStartup.RegisterServices();
        }
    }
}
