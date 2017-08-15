using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows;
using System.Threading;
using HSTCommon;
namespace HSTStockDataStream.Routine.Download
{
    public class StockDataDownloader
    {
        private string DOWNLOAD_URL = null;
        private string SAVE_DESTINATION = null;



        private void SetBeforeWork(DateTime date)
        {
            this.DOWNLOAD_URL = string.Format(HSTStockDataStream.Properties.Settings.Default.StockDataDownloadSiteURL2, date.Year, date.ToString("MM").PadLeft(2, '0'), date.ToString("dd").PadLeft(2, '0'));
            //this.DOWNLOAD_URL = string.Format(HSTStockDataStream.Properties.Settings.Default.StockDataDownloadSiteURL2, date.Year, date.Month, date.Day);
            // this.DOWNLOAD_URL = string.Format(HSTStockDataStream.Properties.Settings.Default.StockDataDownloadSiteURL1, date.Year, date.Month, date.Day);
            this.SAVE_DESTINATION = string.Format(@"{0}\{1}_StockAllData_KDB.csv", Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), HSTStockDataStream.Properties.Settings.Default.DownloadFolderPathFromDesktop), date.ToString("yyyyMMddHHmm"));
        }

        /// <summary>
        /// ダウンロード前のチェック
        /// </summary>
        /// <param name="date"></param>
        /// <returns>true=ダウンロード可</returns>
        /// <returns>false=不可</returns>
        private bool CheckBeforeWork(DateTime date)
        {
            //土日・祝日ならばfalse
            //WEEKDAY = 月～日 != 平日
            var dayInfo = HSTCommon.Date.HolidayChecker.Holiday(date);
            if (dayInfo.holiday != HSTCommon.Date.HolidayChecker.HolidayInfo.HOLIDAY.WEEKDAY // 祝日
                ||
                (dayInfo.week == DayOfWeek.Saturday || dayInfo.week == DayOfWeek.Sunday)
                ||
                (dayInfo.name == "１２月３１日" || dayInfo.name == "１月２日" || dayInfo.name == "１月３日") // １２月３１日～１月３日は休場 (１月１日は元旦で祝日)
                )    

            {
                return false;
            }

            //過去にダウンロード済みならばfalse
            if (CheckAlreadyDownloadorNot(date))
            {
                return false;
            }

            return true;

        }


        /// <summary>
        /// ダウンロード１回のみ
        /// </summary>
        /// <param name="date"></param>
        public bool StartDownload(DateTime date)
        {

            if (!CheckBeforeWork(date))
            {
                return false;
            }

            SetBeforeWork(date);
            WebClient wc = new WebClient();
            try
            {
                if (RemoteFileExists(DOWNLOAD_URL))
                {
                    wc.DownloadFile(DOWNLOAD_URL, SAVE_DESTINATION);
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                // MessageBox.Show(we.Message + we.StackTrace);
                return false;
            }
            finally
            {
                wc.Dispose();
            }

            return true;


        }


        public List<string> GetTopicsFromKDB()
        {
            SetBeforeWork(DateTime.Now);
            //http://k-db.com/stocks/2014-04-07?download=csv
            WebClient wc = new WebClient();
            string str = null;
            try
            {
                if (RemoteFileExists(DOWNLOAD_URL))
                {
                    str = wc.DownloadString(DOWNLOAD_URL);
                }
                else
                {
                    throw new Exception("リモートファイルが存在しない為、終了しました。");
                }

            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                wc.Dispose();
            }

            if (str == null)
            {
                throw new Exception("リモートファイルが存在しない為、終了しました。エラー２");
            }

            List<string> topics = new List<string>();

            string nl = Environment.NewLine;
            var sepa = str.Split(new string[] { nl }, StringSplitOptions.None);
            foreach (var row in sepa)
            {
                var sepa2 = row.Split(',');

                if (sepa2.Length == 0)
                {
                    continue;
                }

                string topic = sepa2[0];
                var sepa3 = topic.Split('-');

                if (sepa3.Length != 2)
                {
                    continue;
                }

                int result;
                if (int.TryParse(sepa3[0], out result) == false)
                {
                    continue;
                }

                topics.Add(string.Format("{0}.{1}", result, sepa3[1]));



                //var sepa2 = row.Split(',');

                //int result;
                //if (int.TryParse(sepa2[0], out result) == false)
                //{
                //    continue;
                //}

                //// 1301未満10000以上の場合は不要
                //if (result < 1301 || 9999 < result)
                //{
                //    continue;
                //}

                //// 市場コードをMarketSpeed形式に変換しておく
                //if (sepa2[1].Contains("東証"))
                //{
                //    topics.Add(string.Format("{0}.{1}", result, "T"));
                //}
                //else if (sepa2[1].Contains("JQ"))
                //{
                //    topics.Add(string.Format("{0}.{1}",result,"Q"));
                //}
                //else if (sepa2[1].Contains("大証"))
                //{
                //    topics.Add(string.Format("{0}.{1}", result, "OS"));
                //}
                //else if (sepa2[1] == "福証")
                //{
                //    topics.Add(string.Format("{0}.{1}", result, "F"));
                //}
                //else if (sepa2[1] == "札証")
                //{
                //    topics.Add(string.Format("{0}.{1}", result, "S"));
                //}
                //else
                //{
                //    continue;
                //}


            }

            return topics;




        }



        /// <summary>
        /// 期間を指定してダウンロード
        /// サーバー側で拒否られるので５秒間隔で！！
        /// ２秒以下になると拒否られます
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public bool StartDownload(DateTime startDate, DateTime endDate)
        {
            bool b = false;
            for (DateTime d = endDate; startDate <= d; d = d.AddDays(-1))
            {
                using (WebClient wc = new WebClient())
                {
                    if (!CheckBeforeWork(d))
                    {
                        continue;
                    }

                    SetBeforeWork(d);
                    Thread.Sleep(5000);
                    try
                    {
                        Uri uri = new Uri(DOWNLOAD_URL);
                        wc.DownloadFile(uri, SAVE_DESTINATION);

                        b = true;

                    }
                    catch
                    {
                        // MessageBox.Show(we.Message + we.StackTrace);
                        return b;
                    }
                }
            }

            return b;
        }

        /// <summary>
        /// すでに過去にダウンロードしたことないかチェック
        /// </summary>
        /// <param name="date"></param>
        /// <returns>true=過去ダウンロードあり</returns>
        /// <returns>false=なし</returns>
        private bool CheckAlreadyDownloadorNot(DateTime date)
        {
            string downloadfolderpath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), HSTStockDataStream.Properties.Settings.Default.DownloadFolderPathFromDesktop);
            string completefolderpath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), HSTStockDataStream.Properties.Settings.Default.InsertCompleteFolderPathFromDesktop);

            // abortデータはもう一度ダウンロードする。
            // string abortfolderpath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), HSTStockDataStream.Properties.Settings.Default.InsertAbortFolderPathFromDesktop);

            var files1 = System.IO.Directory.GetFiles(downloadfolderpath);
            foreach (var file in files1)
            {
                if (file.Contains(date.ToString("yyyyMMdd")))
                {
                    return true;
                }

            }

            var files2 = System.IO.Directory.GetFiles(completefolderpath);
            foreach (var file in files2)
            {
                if (file.Contains(date.ToString("yyyyMMdd")))
                {
                    return true;
                }
            }

            
            return false;
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
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
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
