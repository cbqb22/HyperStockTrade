using MIC.StockDataImport.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MIC.Common.ExtensionMethods;

namespace MIC.StockDataImport.Services
{
    public class StockDataBackupService : IStockDataBackupService
    {
        #region Services

        #endregion

        #region Fields

        public readonly string _backUpFolder;
        public readonly string _targetFolder;

        #endregion

        #region Properties


        #endregion

        #region Constractor

        public StockDataBackupService()
        {
            _targetFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"Stock\Muzinzou");
            _backUpFolder = Path.Combine(_targetFolder, "Backup");
        }

        #endregion

        public async Task BackupAsync()
        {
            if (!Directory.Exists(_targetFolder))
                Directory.CreateDirectory(_targetFolder);

            if (!Directory.Exists(_backUpFolder))
                Directory.CreateDirectory(_backUpFolder);


            await Task.Run(() =>
            {
                Directory.GetFiles(_targetFolder, "*.csv", SearchOption.TopDirectoryOnly)
                         .ToList()
                         .ForEach(x =>
                         {
                             var outputFile = string.Format(@"{0}\{1}", _backUpFolder, Path.GetFileName(x));
                             if (File.Exists(outputFile)) File.Delete(outputFile);
                             File.Move(x, outputFile);
                         });
            });


        }
    }
}
