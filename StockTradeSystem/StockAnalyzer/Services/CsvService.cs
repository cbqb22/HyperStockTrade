using CsvHelper;
using CsvHelper.Configuration;
using MIC.Database.Commons.Enums;
using MIC.Database.Connection.DataContexts;
using MIC.Database.Connection.Services.Interfaces;
using StockAnalyzer.Models;
using StockAnalyzer.Models.Csv;
using StockAnalyzer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Services
{
    public class CsvService : ICsvService
    {
        #region Services

        #endregion

        #region Fields

        #endregion

        #region Constractor

        public CsvService()
        {
        }

        #endregion

        #region Public

        public void Write(string path, IEnumerable<PickedStockData> records)
        {
            var config = new CsvConfiguration() { Encoding = Encoding.UTF8, HasHeaderRecord = true };
            config.RegisterClassMap<PickedStockDataMap>();

            using (var csv = new CsvWriter(File.CreateText(path), config))
            {
                csv.WriteRecords(records);
            }
        }


        #endregion
    }
}
