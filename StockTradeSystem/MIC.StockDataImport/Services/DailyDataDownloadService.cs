using MIC.Common.Date.Services.Interfaces;
using MIC.Database.Connection.DataContexts;
using MIC.Database.Connection.Services.Interfaces;
using MIC.StockDataImport.Services.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MIC.StockDataImport.Services
{
    public class DailyDataDownloadService : IStockDataDownloadService
    {
        #region Services

        private readonly IDataContextFactory<DataContext> _dataContextFactory;
        private readonly IHolidayCheckService _holidayCheckService;

        #endregion

        #region Fields

        private const string UrlFormat = "http://k-db.com/stocks/{0}-{1}-{2}?download=csv";
        private readonly string Output = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Stock");

        #endregion

        #region Properties

        public Uri Uri { get; set; }
        public string OutputPath { get; set; }

        #endregion

        #region Constractor

        public DailyDataDownloadService(IHolidayCheckService holidayCheckService,
                                        IDataContextFactory<DataContext> factory)
        {
            _dataContextFactory = factory;
            _holidayCheckService = holidayCheckService;

            if (!Directory.Exists(Output))
                Directory.CreateDirectory(Output);
        }

        #endregion


        public Task DownloadAsync(DateTime date)
        {
            return Task.Run(async () =>
            {
                if (_holidayCheckService.IsHoliday(date))
                    return;

                if (IsImported(date))
                    return;

                OutputPath = Path.Combine(Output, date.ToString("yyyyMMdd") + ".csv");
                Uri = new Uri(string.Format(UrlFormat, date.Year.ToString().PadLeft(2, '0'), date.Month.ToString().PadLeft(2, '0'), date.Day.ToString().PadLeft(2, '0')));

                if (!RemoteFileExists(Uri.AbsoluteUri))
                    return;

                try
                {
                    await Task.Delay(500);

                    var wc = new WebClient();
                    await wc.DownloadFileTaskAsync(Uri.AbsoluteUri, OutputPath);

                    if (File.Exists(OutputPath) && new FileInfo(OutputPath).Length == 0)
                        File.Delete(OutputPath);
                }
                catch (AggregateException ex)
                {
                    throw ex.Flatten().GetBaseException();
                }
                catch (Exception ex)
                {
                    throw ex.GetBaseException();
                }
            });
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
