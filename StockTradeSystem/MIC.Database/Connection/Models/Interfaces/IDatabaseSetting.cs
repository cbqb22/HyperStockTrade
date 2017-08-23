using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Database.Connection.Models.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDatabaseSetting
    {
        /// <summary>
        /// 
        /// </summary>
        string ServerName { get; }
        /// <summary>
        /// 
        /// </summary>
        string Instance { get; }
        /// <summary>
        /// 
        /// </summary>
        string DbName { get; }
        /// <summary>
        /// 
        /// </summary>
        string UserId { get; }
        /// <summary>
        /// 
        /// </summary>
        string Password { get; }
        /// <summary>
        /// 
        /// </summary>
        int ConnectionTimeout { get; }
        /// <summary>
        /// 
        /// </summary>
        int CommandTimeout { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string ToConnectionString();
    }
}
