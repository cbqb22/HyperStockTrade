using MIC.Database.Connection.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIC.Database.Connection.DataContexts;
using System.Data.Common;

namespace MIC.Database.Connection.Services
{
    public class DataContextFactory : IDataContextFactory<DataContext>
    {
        #region 

        //private const string DefaultConnectionString = @"Data Source=.\SQL2012;Initial Catalog=StockTrade;Trusted_Connection=Yes";
        private const string DefaultConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=StockTrade;Trusted_Connection=Yes";

        #endregion

        public DataContext Create()
        {
            return new DataContext(DefaultConnectionString);
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
