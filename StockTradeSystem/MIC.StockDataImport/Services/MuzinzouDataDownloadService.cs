using MIC.Common.Archives.Lzhs;
using MIC.Common.Date.Services.Interfaces;
using MIC.Common.Enums;
using MIC.Database.Connection.DataContexts;
using MIC.Database.Connection.Services.Interfaces;
using MIC.StockDataImport.Services.Interfaces;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MIC.StockDataImport.Services
{
    public class MuzinzouDataDownloadService : IStockDataDownloadService
    {
        #region Services

        private readonly IDataContextFactory<DataContext> _dataContextFactory;
        private readonly IHolidayCheckService _holidayCheckService;

        #endregion

        #region Fields

        private const string UrlFormat = "http://souba-data.com/k_data/{0}/{1}_{2}/T{3}{4}";  //http://souba-data.com/k_data/2017/17_01/T170104.zip 2014年以前は.lzh
        private const string FileNameFormat = "T{0}";
        private readonly string outputZip = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"Stock\Muzinzou", "Zip");
        private readonly string output = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"Stock\Muzinzou");
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

            if (!Directory.Exists(outputZip))
                Directory.CreateDirectory(outputZip);
        }

        #endregion


        private Func<DateTime, FileExt> GetCompression = (dt) => dt.Year <= 2014 ? FileExt.Lzh : FileExt.Zip;

        public Task DownloadAsync(DateTime date)
        {
            return Task.Run(async () =>
            {
                if (_holidayCheckService.IsHoliday(date))
                    return;

                if (IsImported(date))
                    return;

                var compression = GetCompression(date);

                OutputPath = Path.Combine(output, string.Format(FileNameFormat, date.ToString("yyMMdd")) + ".csv"); // T170104.csv
                Uri = new Uri(string.Format(UrlFormat, date.ToString("yyyy"), date.ToString("yy"), date.ToString("MM"), date.ToString("yyMMdd"), compression.GetExtension()));

                if (!RemoteFileExists(Uri.AbsoluteUri))
                    return;

                try
                {
                    var outputZip = Path.Combine(this.outputZip, string.Format(FileNameFormat, date.ToString("yyMMdd")) + compression.GetExtension());

                    var wc = new WebClient();
                    await wc.DownloadFileTaskAsync(Uri.AbsoluteUri, outputZip);

                    if (File.Exists(OutputPath) && new FileInfo(OutputPath).Length == 0)
                        File.Delete(OutputPath);

                    // ZIP解凍処理
                    if (compression == FileExt.Zip)
                        ZipFile.ExtractToDirectory(outputZip, output);
                    // Lzh解凍処理
                    else
                        UnlhaManager.UnLzh(outputZip, output);
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
