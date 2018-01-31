using MIC.Common.Date;
using MIC.Common.Date.Services.Interfaces;
using MIC.Database.Connection.DataContexts;
using MIC.Database.Connection.Services.Interfaces;
using StockDataImport.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace StockDataImport.Services
{
    public class StockDataBackupService : IStockDataBackupService
    {
        #region Services

        #endregion

        #region Fields

        public readonly string _outputFolder;
        public readonly string _backupFolder;

        #endregion

        #region Properties


        #endregion

        #region Constractor

        public StockDataBackupService()
        {
            _backupFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"Stock\Muzinzou");
            _outputFolder = Path.Combine(_backupFolder, "Backup");
        }

        #endregion

        public async Task BackupAsync()
        {
            if (!Directory.Exists(_backupFolder)) return;

            if (!Directory.Exists(_outputFolder))
                Directory.CreateDirectory(_outputFolder);


            await Task.Run(() =>
            {
                Directory.GetFiles(_backupFolder, "*.csv", SearchOption.TopDirectoryOnly).ToList().ForEach(x => File.Move(x, string.Format(@"{0}\{1}", _outputFolder, Path.GetFileName(x))));
            });


        }
    }
}
