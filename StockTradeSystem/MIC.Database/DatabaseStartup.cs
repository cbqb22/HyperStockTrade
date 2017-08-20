using GalaSoft.MvvmLight.Ioc;
using MIC.Database.Connection.DataContexts;
using MIC.Database.Connection.Services;
using MIC.Database.Connection.Services.Interfaces;

namespace MIC.Database
{
    public static class DatabaseStartup
    {
        public static void RegisterServices()
        {
            SimpleIoc.Default.Register<IConnectFilePathService, ConnectFilePlainTextPathService>();
            SimpleIoc.Default.Register<IDataContextFactory<DataContext>, DataContextFactory>();
        }
    }
}
