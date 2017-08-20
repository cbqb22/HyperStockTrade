using MIC.Database.Connection.DataContexts;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Database.Connection.Services.Interfaces
{
    public interface IDataContextFactory<TClass> where TClass : DataContext
    {
        TClass Create();
        TClass Create(string connectionString);
        TClass Create(DbConnection connection);
    }
}
