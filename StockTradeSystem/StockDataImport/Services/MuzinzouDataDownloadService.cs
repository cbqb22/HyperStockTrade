using MIC.Common.Date;
using MIC.Common.Date.Services.Interfaces;
using MIC.Database.Connection.DataContexts;
using MIC.Database.Connection.Services.Interfaces;
using StockDataImport.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockDataImport.Services
{
    public class MuzinzouDataDownloadService : IStockDataDownloadService
    {
        #region Services

        private readonly IDataContextFactory<DataContext> _dataContextFactory;
        private readonly IHolidayCheckService _holidayCheckService;

        #endregion

        #region Fields

        private const string UrlFormat = "http://souba-data.com/k_data/{0}/{1}_{2}/T{3}.zip";  //http://souba-data.com/k_data/2017/17_01/T170104.zip
        private const string FileNameFormat = "T{0}";
        private string OutputZip = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"Stock\Muzinzou", "Zip");
        private string Output = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"Stock\Muzinzou");
        #endregion

        #region Properties

        public Uri Uri { get; set; }
        public string OutputPath { get; set; }

        #endregion

        #region Constractor

        public MuzinzouDataDownloadService(IHolidayCheckService holidayCheckService,
                                           IDataContextFactory<DataContext> factory)
        {
            _dataContextFactory = factory;
            _holidayCheckService = holidayCheckService;

            if (!Directory.Exists(OutputZip))
                Directory.CreateDirectory(OutputZip);
        }

        #endregion


        public async Task DownloadAsync(DateTime date)
        {
            if (_holidayCheckService.IsHoliday(date))
                return;

            if (IsImported(date))
                return;

            OutputPath = Path.Combine(Output, string.Format(FileNameFormat , date.ToString("yyMMdd")) + ".csv"); // T170104.csv
            Uri = new Uri(string.Format(UrlFormat, date.ToString("yyyy"), date.ToString("yy"), date.ToString("MM"), date.ToString("yyMMdd")));

            if (!RemoteFileExists(Uri.AbsoluteUri))
                return;

            try
            {
                var outputZip = Path.Combine(OutputZip, string.Format(FileNameFormat , date.ToString("yyMMdd")) + ".zip");

                var wc = new WebClient();
                await wc.DownloadFileTaskAsync(Uri.AbsoluteUri, outputZip);

                if (File.Exists(OutputPath) && new FileInfo(OutputPath).Length == 0)
                    File.Delete(OutputPath);

                // ZIP解凍処理
                ZipFile.ExtractToDirectory(outputZip, Output);

            }
            catch (AggregateException ex)
            {
                throw ex.Flatten().GetBaseException();
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        /// <summary>
        /// インポート済みか
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool IsImported(DateTime date)
        {
            using (var context = _dataContextFactory.Create())
            {
                return context.DailyPrice.Any(x => x.DealDate == date);
            }
        }

        /// <summary>
        /// リモートファイルが存在するかチェック
        /// これも使いすぎるとサーバー側に拒否られる。
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                var request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                var response = request.GetResponse() as HttpWebResponse;
                //Returns TURE if the Status code == 200
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

    }
}
