using MIC.Database.Connection.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Database.Connection.Services
{
    public class ConnectFilePlainTextPathService : IConnectFilePathService
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetConnectFilePath()
        {
            return @"C:\ProgramData\MIC\Settings\ConnectInfo.config";
        }

    }
}
