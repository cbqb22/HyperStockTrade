using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using HSTStockDataStream.Routine.Download;
using HSTMarketSpeed.Routine;
using HSTStockDataStream.Routine.FileStream;
using HSTStockDataStream.Routine.Insert;
using HSTDB.StockData.StockDataFullTableAdapters;



namespace HSTStockDataStream
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        #region コンストラクタ
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }


        #endregion

        #region クリックイベント

        private void btnStartKDBDownload_Click(object sender, RoutedEventArgs e)
        {
            RoutineForKDBDownload();
        }


        private void btnStartInsert_Click(object sender, RoutedEventArgs e)
        {
            RoutineForInsert();
        }

        private void btnStartKDBDownload6months_Click(object sender, RoutedEventArgs e)
        {
            RoutineForKDBDownload6months();
        }


        private void btnStartMSDownload_Click(object sender, RoutedEventArgs e)
        {
            RoutineForMSDownload();
        }

        private void btnStartRoutine_Click(object sender, RoutedEventArgs e)
        {
            RoutineWork();
        }

        private void btnStartOnlyStockImage_Click(object sender, RoutedEventArgs e)
        {

        }



        #endregion

        #region クラスメソッド



        /// <summary>
        /// KDBダウンロードのルーチン
        /// </summary>
        private void RoutineForKDBDownload()
        {
            StockDataDownloader sddl = new StockDataDownloader();
            DateTime dt = new DateTime(2013, 12, 30);
            sddl.StartDownload(dt);

        }

        /// <summary>
        /// Insertルーチン
        /// </summary>
        private void RoutineForInsert()
        {
            StockDataInserter sdi = new StockDataInserter();
            sdi.InsertAllIncompletedStockData();

        }

        /// <summary>
        /// MSダウンロードルーチン
        /// </summary>
        private void RoutineForMSDownload()
        {
            DateTime date = DateTime.Now;
            StockDataDownloader sdd = new StockDataDownloader();
            List<string> topicslist = sdd.GetTopicsFromKDB();
            //topicslist.Add("1301.T");
            //topicslist.Add("1305.T");
            //topicslist.Add("1306.T");
            //topicslist.Add("1308.T");
            //topicslist.Add("1332.OS");
            List<string> itemslist = StockDataReader.ReadStockDataItemList();
            //itemslist.Add("銘柄コード");
            //itemslist.Add("市場コード");
            //itemslist.Add("銘柄名称");
            //itemslist.Add("市場名称");
            //itemslist.Add("市場部略称");
            //itemslist.Add("現在日付");
            //itemslist.Add("現在値");

            var dt = MSStockDataDownloader.GetStockDatas(topicslist, itemslist);

            StockDataWriter.MarketSpeedDataWriteToCSV(date, dt);

            //StockDataInserter sdi = new StockDataInserter();
            //sdi.InsertAllIncompletedStockData();

        }

        /// <summary>
        /// KDB６ヶ月サブルーチン
        /// </summary>
        private bool RoutineForKDBDownload6months()
        {
            StockDataDownloader sddl = new StockDataDownloader();

            //今日の分がでるのは20時～24時の間
            DateTime endDate = DateTime.Now.AddDays(-1);
            if (20 <= DateTime.Now.Hour && DateTime.Now.Hour <= 24)
            {
                endDate = DateTime.Now;
            }

            DateTime startDate = endDate.AddMonths(-6);

            return sddl.StartDownload(startDate, endDate);

        }



        /// <summary>
        /// ルーチン
        /// </summary>
        private void RoutineWork()
        {
            //今日の分がでるのは20時～24時の間
            DateTime endDate = DateTime.Now.AddDays(-1);
            if (20 <= DateTime.Now.Hour && DateTime.Now.Hour <= 24)
            {
                endDate = DateTime.Now;
            }

            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0);

            DateTime startDate = endDate.AddMonths(-6);


            //ダウンロード
            bool hasDownload = RoutineForKDBDownload6months();

            //今日の画像があるか
            bool HasTodaysImage = false;

            string imagePath = @"C:\Users\poohace\Desktop\SBIマクロ\スクリーンショット\";
            var path = Directory.GetFiles(imagePath).OrderByDescending(x => x);

            foreach (var p in path)
            {
                string ip = p.Replace(imagePath, "");
                DateTime d = new DateTime(int.Parse(ip.Substring(2, 4)), int.Parse(ip.Substring(6, 2)), int.Parse(ip.Substring(8, 2)), 0, 0, 0); //5_20160518000754

                if (d == new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0))
                {
                    HasTodaysImage = true;

                    break;
                }

                continue;
            }




            if (!hasDownload)
            {
                if (!HasTodaysImage)
                {
                    OnlyTodayImage();
                    return;
                }
                else
                {
                    return;
                }
            }




            //挿入
            RoutineForInsert();
            //NULLを修正
            using (NULLの市場市場部名称市場略称を統一UPDATEQueriesTableAdapter ta = new NULLの市場市場部名称市場略称を統一UPDATEQueriesTableAdapter())
            {
                ta.NULLの市場市場部名称市場略称を統一UPDATE();
            }

            //SQL解析
            DateTime startdate = endDate;
            DateTime enddate = startdate.AddDays(-60);
            HSTDB.StockData.StockDataFull.StockDataFull解析済データDataTable insertDt = new HSTDB.StockData.StockDataFull.StockDataFull解析済データDataTable();
            HSTDB.StockData.StockDataFull.StockDataFull解析済データDataTable outputDt = new HSTDB.StockData.StockDataFull.StockDataFull解析済データDataTable();
            using (StockDataFull解析済データTableAdapter ta = new StockDataFull解析済データTableAdapter())
            {
                ta.FillBy短期急騰抽出analyzer(insertDt, startdate, enddate);

                //解析済データテーブルへInsert
                foreach (var row in insertDt)
                {
                    ta.Insert(
                        row.銘柄コード,
                        row.市場コード,
                        row.銘柄名称,
                        row.市場名称,
                        row.市場部名称,
                        row.最大値,
                        row.最大値の日付,
                        row.最小値,
                        row.最小値の日付,
                        row.上昇率,
                        row.START解析日付,
                        row.END解析日付,
                        row.削除フラグ);
                }


                ta.FillBySelectDistinct銘柄コード(outputDt);
            }


            //銘柄データCSVに出力
            string 解析データフォルダpath = @"C:\Users\poohace\Desktop\SBIマクロ\解析予定銘柄";
            string 銘柄データpath = System.IO.Path.Combine(解析データフォルダpath, "銘柄データファイル_" + enddate.ToString("yyyyMMdd") + "-" + startdate.ToString("yyyyMMdd") + ".csv");

            string MacroPath = System.IO.Path.Combine(解析データフォルダpath, "Macro" + enddate.ToString("yyyyMMdd") + "-" + startdate.ToString("yyyyMMdd") + ".csv");


            using (StreamWriter sw = new StreamWriter(銘柄データpath, false, Encoding.GetEncoding(932)))
            {
                foreach (var row in outputDt)
                {
                    sw.WriteLine("{0},{1},{2},{3},{4},{5},{6}", row.銘柄コード, row.銘柄名称, row.最大値, row.最大値の日付, row.最小値, row.最小値の日付, row.上昇率);

                }
            }

            //MacroCSVに出力
            using (StreamWriter sw = new StreamWriter(MacroPath, false, Encoding.GetEncoding(932)))
            {
                sw.WriteLine(銘柄データpath);

                sw.WriteLine("0,0,0,485,148,0,0,0,");
                sw.WriteLine("1,0,0,0,0,1,0,0,0");
                sw.WriteLine("0,0,0,592,149,0,0,0,0");
                sw.WriteLine("2,0,5000,0,0,0,0,0,");
                sw.WriteLine(@"6,0,0,0,0,0,0,0,C:\Users\poohace\Desktop\SBIマクロ\スクリーンショット");
            }

            string sbipath = @"C:\Program Files\SBI SECURITIES\HYPER SBI\ETHtsMain.exe";
            if (System.Environment.Is64BitOperatingSystem)
            {
                sbipath = @"C:\Program Files (x86)\SBI SECURITIES\HYPER SBI\ETHtsMain.exe";
            }

            //HyperSBI起動
            var processes = Process.GetProcessesByName("SBIFullBoard");

            if (processes.Count() == 1)
            {
                processes[0].Kill();
            }

            //HyperSBI起動
            var sbiprocess = Process.Start(sbipath);
            sbiprocess.WaitForExit();

            System.Threading.Thread.Sleep(2000);


            Process proc = new Process();
            proc.StartInfo.FileName = @"C:\MCSystem\MCSystem.exe";
            string argument = "sbimacro" + " " + MacroPath;
            proc.StartInfo.Verb = "RunAs";  // 管理者として実行
            proc.StartInfo.Arguments = argument;
            proc.Start();

        }



        /// <summary>
        /// SBIマクロだけのルーチン
        /// </summary>
        private void OnlyTodayImage()
        {
            string dirpath = @"C:\Users\poohace\Desktop\SBIマクロ\解析予定銘柄\";
            var paths = Directory.GetFiles(dirpath);


            //降順にする
            List<string> macroCSVs = (
                                         from x in paths
                                         where
                                            x.Replace(dirpath, "").StartsWith("Macro")
                                         select x
                                          ).OrderByDescending(x => x).ToList();  //Macro20160111-20160202.csv


            string newestTodaysCsvPath = "";

            DateTime? newestDate = null;
            foreach (var p in macroCSVs)
            {
                string ip = p.Replace(dirpath, "");

                DateTime d = new DateTime(int.Parse(ip.Substring(14, 4)), int.Parse(ip.Substring(18, 2)), int.Parse(ip.Substring(20, 2)), 0, 0, 0); //5_20160518000754

                if (newestDate == null || newestDate < d)
                {
                    newestDate = d;
                    newestTodaysCsvPath = p;
                }

                continue;
            }

            if (newestTodaysCsvPath != "")
            {
                SBIMacroRun(newestTodaysCsvPath);
            }


        }

        /// <summary>
        /// SBIマクロ
        /// </summary>
        /// <param name="macropath"></param>
        private void SBIMacroRun(string macropath)
        {
            string sbipath = @"C:\Program Files\SBI SECURITIES\HYPER SBI\ETHtsMain.exe";
            if (System.Environment.Is64BitOperatingSystem)
            {
                sbipath = @"C:\Program Files (x86)\SBI SECURITIES\HYPER SBI\ETHtsMain.exe";
            }

            //HyperSBI起動
            var processes = Process.GetProcessesByName("SBIFullBoard");

            if (processes.Count() == 1)
            {
                processes[0].Kill();
            }

            //HyperSBI起動
            var sbiprocess = Process.Start(sbipath);
            sbiprocess.WaitForExit();

            System.Threading.Thread.Sleep(2000);


            Process proc = new Process();
            proc.StartInfo.FileName = @"C:\MCSystem\MCSystem.exe";
            string argument = "sbimacro" + " " + macropath;
            proc.StartInfo.Verb = "RunAs";  // 管理者として実行
            proc.StartInfo.Arguments = argument;
            proc.Start();

        }

        #endregion

    }
}
