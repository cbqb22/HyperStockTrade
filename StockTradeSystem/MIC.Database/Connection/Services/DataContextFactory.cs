using System.Data.Common;
using MIC.Database.Connection.Services.Interfaces;
using MIC.Database.Connection.DataContexts;
using MIC.Database.Connection.Models;
using MIC.Common.ExtensionMethods;
using System.IO;

namespace MIC.Database.Connection.Services
{
    public class DataContextFactory : IDataContextFactory<DataContext>
    {
        #region Services

        private readonly IConnectFilePathService _connectFilePathService;

        #endregion



        #region Constructor

        public DataContextFactory(IConnectFilePathService connectFilePathService)
        {
            _connectFilePathService = connectFilePathService;
        }

        #endregion

        #region 

        private const string DefaultConnectionString = @"Data Source=.\SQL2012;Initial Catalog=StockTrade;Trusted_Connection=Yes";
        //private const string DefaultConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=StockTrade;Trusted_Connection=Yes";

        #endregion

        public DataContext Create()
        {
            var connectionString = DefaultConnectionString;
            var filePath = _connectFilePathService.GetConnectFilePath();

            if (File.Exists(filePath))
            {
                var text = File.ReadAllText(filePath);
                var dbSettings = text.ParseXml<DatabaseSetting>();
                if (dbSettings != null)
                    connectionString = dbSettings.ToConnectionString();
            }



            return new DataContext(connectionString);
        }

        public DataContext Create(string connectionString)
        {
            return new DataContext(connectionString);
        }


        public DataContext Create(DbConnection connection)
        {
            return new DataContext(connection);
        }
    }
}
