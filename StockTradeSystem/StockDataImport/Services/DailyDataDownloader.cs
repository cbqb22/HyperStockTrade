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
    public class DailyDataDownloader : IStockDataDownloader
    {
        #region Fields

        private const string Url = "http://k-db.com/stocks/2014-04-07?download=csv";
        private const string Output = @"C:\Users\m_mikami\Desktop\Stock";

        #endregion

        #region Constractor

        public DailyDataDownloader()
        {
            Uri = new Uri(Url);
            OutputPath = Path.Combine(Output, DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv");
        }

        #endregion

        public Uri Uri { get; set; }
        public string OutputPath { get; set; }

        public async Task DownloadAsync()
        {
            try
            {
                var wc = new WebClient();
                await wc.DownloadFileTaskAsync(Uri.AbsoluteUri, OutputPath);
            }
            catch (AggregateException ex)
            {
                throw ex.Flatten().GetBaseException();
            }
        }
    }
}
