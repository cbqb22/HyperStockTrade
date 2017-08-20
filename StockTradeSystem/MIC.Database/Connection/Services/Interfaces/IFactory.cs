using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Database.Connection.Services.Interfaces
{
    public interface IFactory<TClass>
    {
        TClass Create();
    }
}
