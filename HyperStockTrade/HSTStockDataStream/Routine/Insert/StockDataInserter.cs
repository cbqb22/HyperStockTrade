using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HSTDB.StockData;
using HSTDB.StockData.StockDataFullTableAdapters;
using HSTDB.StockData.StockDataFull挿入済株価データ日付TableAdapters;
using HSTMarketSpeed.Routine;

namespace HSTStockDataStream.Routine.Insert
{
    public class StockDataInserter
    {

        // [0] 銘柄コード
        // [1] 市場コード
        // [2] 銘柄名称
        // [3] 市場名称
        // [4] 市場部名称
        // [5] 市場部略称
        // [6] 現在日付
        // [7] 現在値
        // [8] 前日比
        // [9] 前日比率
        // [10] 前日終値
        // [11] 前日日付
        // [12] 出来高
        // [13] 売買代金
        // [14] 出来高加重平均
        // [15] 始値
        // [16] 高値
        // [17] 安値
        // [18] 始値時刻
        // [19] 高値時刻
        // [20] 安値時刻
        // [21] 前場出来高
        // [22] 信用貸借区分
        // [23] 逆日歩
        // [24] 逆日歩更新日付
        // [25] 信用売残
        // [26] 信用売残前週比
        // [27] 信用買残
        // [28] 信用買残前週比
        // [29] 信用倍率
        // [30] 証金コード
        // [31] 証金残更新日付
        // [32] 新規貸株
        // [33] 新規融資
        // [34] 返済貸株
        // [35] 返済融資
        // [36] 残高貸株
        // [37] 残高融資
        // [38] 残高差引
        // [39] 前日比貸株
        // [40] 前日比融資
        // [41] 前日比差引
        // [42] 回転日数
        // [43] 貸借倍率
        // [44] 単位株数
        // [45] 配当
        // [46] 配当落日
        // [47] 権利落日
        // [48] ＰＥＲ
        // [49] ＰＢＲ
        // [50] 年初来高値
        // [51] 年初来安値
        // [52] 年初来高値日付
        // [53] 年初来安値日付
        // [54] 上場来高値
        // [55] 上場来安値
        // [56] 上場来高値日付
        // [57] 上場来安値日付
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="銘柄コード"></param>
        /// <param name="市場コード"></param>
        /// <param name="銘柄名称"></param>
        /// <param name="市場名称"></param>
        /// <param name="市場部名称"></param>
        /// <param name="市場部略称"></param>
        /// <param name="現在日付"></param>
        /// <param name="現在値"></param>
        /// <param name="前日比"></param>
        /// <param name="前日比率"></param>
        /// <param name="前日終値"></param>
        /// <param name="前日日付"></param>
        /// <param name="出来高"></param>
        /// <param name="売買代金"></param>
        /// <param name="出来高加重平均"></param>
        /// <param name="始値"></param>
        /// <param name="高値"></param>
        /// <param name="安値"></param>
        /// <param name="始値時刻"></param>
        /// <param name="高値時刻"></param>
        /// <param name="安値時刻"></param>
        /// <param name="前場出来高"></param>
        /// <param name="信用貸借区分"></param>
        /// <param name="逆日歩"></param>
        /// <param name="逆日歩更新日付"></param>
        /// <param name="信用売残"></param>
        /// <param name="信用売残前週比"></param>
        /// <param name="信用買残"></param>
        /// <param name="信用買残前週比"></param>
        /// <param name="信用倍率"></param>
        /// <param name="証金コード"></param>
        /// <param name="証金残更新日付"></param>
        /// <param name="新規貸株"></param>
        /// <param name="新規融資"></param>
        /// <param name="返済貸株"></param>
        /// <param name="返済融資"></param>
        /// <param name="残高貸株"></param>
        /// <param name="残高融資"></param>
        /// <param name="残高差引"></param>
        /// <param name="前日比貸株"></param>
        /// <param name="前日比融資"></param>
        /// <param name="前日比差引"></param>
        /// <param name="回転日数"></param>
        /// <param name="貸借倍率"></param>
        /// <param name="単位株数"></param>
        /// <param name="配当"></param>
        /// <param name="配当落日"></param>
        /// <param name="権利落日"></param>
        /// <param name="ＰＥＲ"></param>
        /// <param name="ＰＢＲ"></param>
        /// <param name="年初来高値"></param>
        /// <param name="年初来安値"></param>
        /// <param name="年初来高値日付"></param>
        /// <param name="年初来安値日付"></param>
        /// <param name="上場来高値"></param>
        /// <param name="上場来安値"></param>
        /// <param name="上場来高値日付"></param>
        /// <param name="上場来安値日付"></param>
        public void StartInsert
            (
                DateTime 挿入日付,
                int 銘柄コード,
                string 市場コード,
                string 銘柄名称,
                string 市場名称,
                string 市場部名称,
                string 市場部略称,
                DateTime? 現在日付,
                double? 現在値,
                double? 前日比,
                double? 前日比率,
                double? 前日終値,
                DateTime? 前日日付,
                double? 出来高,
                double? 売買代金,
                double? 出来高加重平均,
                double? 始値,
                double? 高値,
                double? 安値,
                DateTime? 始値時刻,
                DateTime? 高値時刻,
                DateTime? 安値時刻,
                double? 前場出来高,
                string 信用貸借区分,
                double? 逆日歩,
                DateTime? 逆日歩更新日付,
                double? 信用売残,
                double? 信用売残前週比,
                double? 信用買残,
                double? 信用買残前週比,
                double? 信用倍率,
                string 証金コード,
                DateTime? 証金残更新日付,
                double? 新規貸株,
                double? 新規融資,
                double? 返済貸株,
                double? 返済融資,
                double? 残高貸株,
                double? 残高融資,
                double? 残高差引,
                double? 前日比貸株,
                double? 前日比融資,
                double? 前日比差引,
                double? 回転日数,
                double? 貸借倍率,
                double? 単位株数,
                double? 配当,
                DateTime? 配当落日,
                DateTime? 権利落日,
                double? ＰＥＲ,
                double? ＰＢＲ,
                double? 年初来高値,
                double? 年初来安値,
                DateTime? 年初来高値日付,
                DateTime? 年初来安値日付,
                double? 上場来高値,
                double? 上場来安値,
                DateTime? 上場来高値日付,
                DateTime? 上場来安値日付
            )
        {
            using (var ta = new StockDataFullTableAdapter())
            {
                ta.Insert
                    (
                        銘柄コード,
                        市場コード,
                        銘柄名称,
                        市場名称,
                        市場部名称,
                        市場部略称,
                        現在日付 != null ?
                            現在日付 == new DateTime(1900, 1, 1) ? 挿入日付 : (DateTime?)new DateTime(現在日付.Value.Year, 現在日付.Value.Month, 現在日付.Value.Day, 0, 0, 0) : null, // 手動データの為、日付以下を切り捨て
                        現在値,
                        前日比,
                        前日比率,
                        前日終値,
                        前日日付,
                        出来高,
                        売買代金,
                        出来高加重平均,
                        始値,
                        高値,
                        安値,
                        始値時刻,
                        高値時刻,
                        安値時刻,
                        前場出来高,
                        信用貸借区分,
                        逆日歩,
                        逆日歩更新日付,
                        信用売残,
                        信用売残前週比,
                        信用買残,
                        信用買残前週比,
                        信用倍率,
                        証金コード,
                        証金残更新日付,
                        新規貸株,
                        新規融資,
                        返済貸株,
                        返済融資,
                        残高貸株,
                        残高融資,
                        残高差引,
                        前日比貸株,
                        前日比融資,
                        前日比差引,
                        回転日数,
                        貸借倍率,
                        単位株数,
                        配当,
                        配当落日,
                        権利落日,
                        ＰＥＲ,
                        ＰＢＲ,
                        年初来高値,
                        年初来安値,
                        年初来高値日付,
                        年初来安値日付,
                        上場来高値,
                        上場来安値,
                        上場来高値日付,
                        上場来安値日付);
            }

        }

        /// <summary>
        /// まだ未挿入のStockDataを一括でInsert
        /// </summary>
        public void InsertAllIncompletedStockData()
        {

            var paths = Directory.GetFiles(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), HSTStockDataStream.Properties.Settings.Default.DownloadFolderPathFromDesktop)).OrderByDescending(x => x).ToList();

            foreach (var path in paths)
            {
                // MarketSpeedからのデータ
                if (path.Contains("_MS"))
                {
                    MSInserter(path);
                }
                // KDBダウンロードサイトからのデータ
                else if (path.Contains("_KDB"))
                {
                    KDBInserter(path);
                }
                // 無尽蔵ダウンロードサイトからのデータ
                else if (path.Contains("_T"))
                {
                    MUJINZOUInserter(path);
                }
                else
                {
                    System.Windows.MessageBox.Show("挿入しようとしたデータは不明なデータです。処理を中断します。\r\n\r\n" + "FileName:" + path);
                    return;
                }


            }
        }



        // TODO: Fix MarketSpeed Inserterの動作確認。.
        /// <summary>
        /// MarketSpeedから取得したデータの挿入
        /// </summary>
        /// <param name="path"></param>
        private void MSInserter(string path)
        {
            var sepapath = path.Split('\\');

            if (sepapath.Count() < 1)
            {
                return;
            }

            string filename = sepapath[sepapath.Count() - 1];
            DateTime date;
            string datestr = filename.Substring(0, 12);
            try
            {
                date = new DateTime(int.Parse(datestr.Substring(0, 4)), int.Parse(datestr.Substring(4, 2)), int.Parse(datestr.Substring(6, 2)), int.Parse(datestr.Substring(8, 2)), int.Parse(datestr.Substring(10, 2)), 0);
            }
            catch
            {
                return;
            }

            using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding(932)))
            {
                double counter = 0;
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    counter++;

                    if (counter <= 1)
                    {
                        continue;
                    }

                    var sepa = line.Split(',');

                    if (sepa[0].Length != 4)
                    {
                        continue;
                        throw new Exception("不明な銘柄コードが検出されました。\r\n" + "銘柄コード:" + sepa[0]);
                    }

                    StockDataFull.StockDataFullDataTable dt = new StockDataFull.StockDataFullDataTable();

                    var row = dt.NewRow() as StockDataFull.StockDataFullRow;

                    StockDataConverter.ConvertToDBFormat("", "銘柄コード", sepa[0], row);
                    StockDataConverter.ConvertToDBFormat("", "市場コード", sepa[1], row);
                    StockDataConverter.ConvertToDBFormat("", "銘柄名称", sepa[2], row);
                    StockDataConverter.ConvertToDBFormat("", "市場名称", sepa[3], row);
                    StockDataConverter.ConvertToDBFormat("", "市場部名称", sepa[4], row);
                    StockDataConverter.ConvertToDBFormat("", "市場部略称", sepa[5], row);
                    StockDataConverter.ConvertToDBFormat("", "現在日付", sepa[6], row);
                    StockDataConverter.ConvertToDBFormat("", "現在値", sepa[7], row);
                    StockDataConverter.ConvertToDBFormat("", "前日比", sepa[8], row);
                    StockDataConverter.ConvertToDBFormat("", "前日比率", sepa[9], row);
                    StockDataConverter.ConvertToDBFormat("", "前日終値", sepa[10], row);
                    StockDataConverter.ConvertToDBFormat("", "前日日付", sepa[11], row);
                    StockDataConverter.ConvertToDBFormat("", "出来高", sepa[12], row);
                    StockDataConverter.ConvertToDBFormat("", "売買代金", sepa[13], row);
                    StockDataConverter.ConvertToDBFormat("", "出来高加重平均", sepa[14], row);
                    StockDataConverter.ConvertToDBFormat("", "始値", sepa[15], row);
                    StockDataConverter.ConvertToDBFormat("", "高値", sepa[16], row);
                    StockDataConverter.ConvertToDBFormat("", "安値", sepa[17], row);
                    StockDataConverter.ConvertToDBFormat("", "始値時刻", sepa[18], row);
                    StockDataConverter.ConvertToDBFormat("", "高値時刻", sepa[19], row);
                    StockDataConverter.ConvertToDBFormat("", "安値時刻", sepa[20], row);
                    StockDataConverter.ConvertToDBFormat("", "前場出来高", sepa[21], row);
                    StockDataConverter.ConvertToDBFormat("", "信用貸借区分", sepa[22], row);
                    StockDataConverter.ConvertToDBFormat("", "逆日歩", sepa[23], row);
                    StockDataConverter.ConvertToDBFormat("", "逆日歩更新日付", sepa[24], row);
                    StockDataConverter.ConvertToDBFormat("", "信用売残", sepa[25], row);
                    StockDataConverter.ConvertToDBFormat("", "信用売残前週比", sepa[26], row);
                    StockDataConverter.ConvertToDBFormat("", "信用買残", sepa[27], row);
                    StockDataConverter.ConvertToDBFormat("", "信用買残前週比", sepa[28], row);
                    StockDataConverter.ConvertToDBFormat("", "信用倍率", sepa[29], row);
                    StockDataConverter.ConvertToDBFormat("", "証金コード", sepa[30], row);
                    StockDataConverter.ConvertToDBFormat("", "証金残更新日付", sepa[31], row);
                    StockDataConverter.ConvertToDBFormat("", "新規貸株", sepa[32], row);
                    StockDataConverter.ConvertToDBFormat("", "新規融資", sepa[33], row);
                    StockDataConverter.ConvertToDBFormat("", "返済貸株", sepa[34], row);
                    StockDataConverter.ConvertToDBFormat("", "返済融資", sepa[35], row);
                    StockDataConverter.ConvertToDBFormat("", "残高貸株", sepa[36], row);
                    StockDataConverter.ConvertToDBFormat("", "残高融資", sepa[37], row);
                    StockDataConverter.ConvertToDBFormat("", "残高差引", sepa[38], row);
                    StockDataConverter.ConvertToDBFormat("", "前日比貸株", sepa[39], row);
                    StockDataConverter.ConvertToDBFormat("", "前日比融資", sepa[40], row);
                    StockDataConverter.ConvertToDBFormat("", "前日比差引", sepa[41], row);
                    StockDataConverter.ConvertToDBFormat("", "回転日数", sepa[42], row);
                    StockDataConverter.ConvertToDBFormat("", "貸借倍率", sepa[43], row);
                    StockDataConverter.ConvertToDBFormat("", "単位株数", sepa[44], row);
                    StockDataConverter.ConvertToDBFormat("", "配当", sepa[45], row);
                    StockDataConverter.ConvertToDBFormat("", "配当落日", sepa[46], row);
                    StockDataConverter.ConvertToDBFormat("", "権利落日", sepa[47], row);
                    StockDataConverter.ConvertToDBFormat("", "ＰＥＲ", sepa[48], row);
                    StockDataConverter.ConvertToDBFormat("", "ＰＢＲ", sepa[49], row);
                    StockDataConverter.ConvertToDBFormat("", "年初来高値", sepa[50], row);
                    StockDataConverter.ConvertToDBFormat("", "年初来安値", sepa[51], row);
                    StockDataConverter.ConvertToDBFormat("", "年初来高値日付", sepa[52], row);
                    StockDataConverter.ConvertToDBFormat("", "年初来安値日付", sepa[53], row);
                    StockDataConverter.ConvertToDBFormat("", "上場来高値", sepa[54], row);
                    StockDataConverter.ConvertToDBFormat("", "上場来安値", sepa[55], row);
                    StockDataConverter.ConvertToDBFormat("", "上場来高値日付", sepa[56], row);
                    StockDataConverter.ConvertToDBFormat("", "上場来安値日付", sepa[57], row);

                    try
                    {
                        StartInsert
                        (
                            date,
                            row.銘柄コード,
                            row.市場コード,
                            row.Is銘柄名称Null() ? null : row.銘柄名称,
                            row.Is市場名称Null() ? null : row.市場名称,
                            row.Is市場部名称Null() ? null : row.市場部名称,
                            row.Is市場部略称Null() ? null : row.市場部略称,
                            row.現在日付,
                            row.Is現在値Null() ? (double?)null : row.現在値,
                            row.Is前日比Null() ? (double?)null : row.前日比,
                            row.Is前日比率Null() ? (double?)null : row.前日比率,
                            row.Is前日終値Null() ? (double?)null : row.前日終値,
                            row.Is前日日付Null() ? (DateTime?)null : row.前日日付,
                            row.Is出来高Null() ? (double?)null : row.出来高,
                            row.Is売買代金Null() ? (double?)null : row.売買代金,
                            row.Is出来高加重平均Null() ? (double?)null : row.出来高加重平均,
                            row.Is始値Null() ? (double?)null : row.始値,
                            row.Is高値Null() ? (double?)null : row.高値,
                            row.Is安値Null() ? (double?)null : row.安値,
                            row.Is始値時刻Null() ? (DateTime?)null : row.始値時刻,
                            row.Is高値時刻Null() ? (DateTime?)null : row.高値時刻,
                            row.Is安値時刻Null() ? (DateTime?)null : row.安値時刻,
                            row.Is前場出来高Null() ? (double?)null : row.前場出来高,
                            row.Is信用貸借区分Null() ? null : row.信用貸借区分,
                            row.Is逆日歩Null() ? (double?)null : row.逆日歩,
                            row.Is逆日歩更新日付Null() ? (DateTime?)null : row.逆日歩更新日付,
                            row.Is信用売残Null() ? (double?)null : row.信用売残,
                            row.Is信用売残前週比Null() ? (double?)null : row.信用売残前週比,
                            row.Is信用買残Null() ? (double?)null : row.信用買残,
                            row.Is信用買残前週比Null() ? (double?)null : row.信用買残前週比,
                            row.Is信用倍率Null() ? (double?)null : row.信用倍率,
                            row.Is証金コードNull() ? null : row.証金コード,
                            row.Is証金残更新日付Null() ? (DateTime?)null : row.証金残更新日付,
                            row.Is新規貸株Null() ? (double?)null : row.新規貸株,
                            row.Is新規融資Null() ? (double?)null : row.新規融資,
                            row.Is返済貸株Null() ? (double?)null : row.返済貸株,
                            row.Is返済融資Null() ? (double?)null : row.返済融資,
                            row.Is残高貸株Null() ? (double?)null : row.残高貸株,
                            row.Is残高融資Null() ? (double?)null : row.残高融資,
                            row.Is残高差引Null() ? (double?)null : row.残高差引,
                            row.Is前日比貸株Null() ? (double?)null : row.前日比貸株,
                            row.Is前日比融資Null() ? (double?)null : row.前日比融資,
                            row.Is前日比差引Null() ? (double?)null : row.前日比差引,
                            row.Is回転日数Null() ? (double?)null : row.回転日数,
                            row.Is貸借倍率Null() ? (double?)null : row.貸借倍率,
                            row.Is単位株数Null() ? (double?)null : row.単位株数,
                            row.Is配当Null() ? (double?)null : row.配当,
                            row.Is配当落日Null() ? (DateTime?)null : row.配当落日,
                            row.Is権利落日Null() ? (DateTime?)null : row.権利落日,
                            row.IsＰＥＲNull() ? (double?)null : row.ＰＥＲ,
                            row.IsＰＢＲNull() ? (double?)null : row.ＰＢＲ,
                            row.Is年初来高値Null() ? (double?)null : row.年初来高値,
                            row.Is年初来安値Null() ? (double?)null : row.年初来安値,
                            row.Is年初来高値日付Null() ? (DateTime?)null : row.年初来高値日付,
                            row.Is年初来安値日付Null() ? (DateTime?)null : row.年初来安値日付,
                            row.Is上場来高値Null() ? (double?)null : row.上場来高値,
                            row.Is上場来安値Null() ? (double?)null : row.上場来安値,
                            row.Is上場来高値日付Null() ? (DateTime?)null : row.上場来高値日付,
                            row.Is上場来安値日付Null() ? (DateTime?)null : row.上場来安値日付

                        );

                    }
                    catch (Exception ex)
                    {
                        continue;
                    }



                    System.Diagnostics.Debug.WriteLine(string.Format("銘柄:{0}.{1}  {2}  現在値:{3}", row.銘柄コード, row.市場コード, row.Is銘柄名称Null() ? "DBNull" : row.銘柄名称.ToString(), row.Is現在値Null() ? "DBNull" : row.現在値.ToString()));
                }
            }

            string destipath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Path.Combine(HSTStockDataStream.Properties.Settings.Default.InsertCompleteFolderPathFromDesktop, filename));
            File.Move(path, destipath);
            StockDataデータ挿入履歴Inserter(date, DateTime.Now);


        }

        /// <summary>
        /// 株価ダウンロードサイトから取得したデータの挿入
        /// </summary>
        /// <param name="path"></param>
        private void KDBInserter(string path)
        {
            var sepapath = path.Split('\\');

            if (sepapath.Count() < 1)
            {
                return;
            }

            string filename = sepapath[sepapath.Count() - 1];
            DateTime date;
            string datestr = filename.Substring(0, 12);
            try
            {
                date = new DateTime(int.Parse(datestr.Substring(0, 4)), int.Parse(datestr.Substring(4, 2)), int.Parse(datestr.Substring(6, 2)), int.Parse(datestr.Substring(8, 2)), int.Parse(datestr.Substring(10, 2)), 0);
            }
            catch
            {
                return;
            }

            using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding(932)))
            {
                double counter = 0;
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    counter++;

                    //if (counter <= 2)
                    if (counter <= 1)
                    {
                        continue;
                    }

                    var sepa = line.Split(',');

                    var code = sepa[0].Split('-');
                    if (code[0].Length != 4)
                    {
                        continue;
                        throw new Exception("不明な銘柄コードが検出されました。\r\n" + "銘柄コード:" + code);
                    }

                    int 銘柄コードout;
                    int 銘柄コード;
                    if (int.TryParse(code[0], out 銘柄コードout) == false)
                    {
                        throw new Exception("不明な銘柄コードが検出されました。\r\n" + "銘柄コード:" + sepa[0]);
                        //continue;
                    }
                    else
                    {
                        銘柄コード = 銘柄コードout;
                    }

                    // 1301未満10000以上は不要
                    if (銘柄コード < 1301 || 9999 < 銘柄コード)
                    {
                        continue;
                    }

                    //string 市場名称 = sepa[1]; // [2]
                    //string 銘柄名 = sepa[2]; // [1]
                    string 銘柄名 = sepa[1]; // [1]
                    string 市場名称 = sepa[2]; // [2]

                    string 市場コード = null;
                    if (市場名称.Contains("東証"))
                    {
                        市場コード = "T";
                    }
                    else if (市場名称.Contains("JQ"))
                    {
                        市場コード = "Q";
                    }
                    else if (市場名称.Contains("大証"))
                    {
                        市場コード = "OS";
                    }
                    else if (市場名称.Contains("福証"))
                    {
                        市場コード = "F";
                    }
                    else if (市場名称.Contains("札証"))
                    {
                        市場コード = "S";
                    }
                    else
                    {
                        //continue;
                        throw new Exception("不明な市場名称が検出されました。\r\n" + "市場名称:" + 市場名称);
                    }

                    // string 業種 = sepa[3]; // [3]

                    double 始値out;
                    double? 始値;
                    if (double.TryParse(sepa[3], out 始値out) == false)
                    {
                        始値 = null;
                    }
                    else
                    {
                        始値 = 始値out;
                    }

                    double 高値out;
                    double? 高値;
                    if (double.TryParse(sepa[4], out 高値out) == false)
                    {
                        高値 = null;
                    }
                    else
                    {
                        高値 = 高値out;
                    }

                    double 安値out;
                    double? 安値;
                    if (double.TryParse(sepa[5], out 安値out) == false)
                    {
                        安値 = null;
                    }
                    else
                    {
                        安値 = 安値out;
                    }

                    double 終値out;
                    double? 終値;
                    if (double.TryParse(sepa[6], out 終値out) == false)
                    {
                        終値 = null;
                    }
                    else
                    {
                        終値 = 終値out;
                    }


                    double 出来高out;
                    double? 出来高;
                    if (double.TryParse(sepa[7], out 出来高out) == false)
                    {
                        出来高 = null;
                    }
                    else
                    {
                        出来高 = 出来高out;
                    }


                    double 売買代金out;
                    double? 売買代金;
                    if (double.TryParse(sepa[8], out 売買代金out) == false)
                    {
                        売買代金 = null;
                    }
                    else
                    {
                        売買代金 = 売買代金out / 1000;
                    }

                    StartInsert(date, 銘柄コード, 市場コード, 銘柄名, null, null, null, (DateTime?)date, 終値, null, null, null, null, 出来高, 売買代金, null, 始値, 高値, 安値, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                    System.Diagnostics.Debug.WriteLine(string.Format("銘柄:{0}.{1}  {2}  現在値:{3}", 銘柄コード, 市場コード, 銘柄名, 終値));
                }
            }

            string destipath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Path.Combine(HSTStockDataStream.Properties.Settings.Default.InsertCompleteFolderPathFromDesktop, filename));
            File.Move(path, destipath);
            StockDataデータ挿入履歴Inserter(date, DateTime.Now);


        }


        /// <summary>
        /// 無尽蔵から取得したデータの挿入
        /// </summary>
        /// <param name="path"></param>
        private void MUJINZOUInserter(string path)
        {
            var sepapath = path.Split('\\');

            if (sepapath.Count() < 1)
            {
                return;
            }

            string filename = sepapath[sepapath.Count() - 1];
            DateTime date;
            string datestr = filename.Substring(0, 12);
            try
            {
                date = new DateTime(int.Parse(datestr.Substring(0, 4)), int.Parse(datestr.Substring(4, 2)), int.Parse(datestr.Substring(6, 2)), int.Parse(datestr.Substring(8, 2)), int.Parse(datestr.Substring(10, 2)), 0);
            }
            catch
            {
                return;
            }

            using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding(932)))
            {
                double counter = 0;
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    counter++;

                    //[0] 2014/7/23
                    //[1] 1001
                    //[2] 11
                    //[3] 1001 日経２２５
                    //[4] 15367
                    //[5] 15376
                    //[6] 15317
                    //[7] 15329
                    //[8] 1785250000
                    //[9] 東証１部

                    var sepa = line.Split(',');

                    if (sepa[1].Length != 4)
                    {
                        continue;
                        //throw new Exception("不明な銘柄コードが検出されました。\r\n" + "銘柄コード:" + code);
                    }

                    int 銘柄コードout;
                    int 銘柄コード;
                    if (int.TryParse(sepa[1], out 銘柄コードout) == false)
                    {
                        throw new Exception("不明な銘柄コードが検出されました。\r\n" + "銘柄コード:" + sepa[1]);
                        //continue;
                    }
                    else
                    {
                        銘柄コード = 銘柄コードout;
                    }

                    // 1301未満10000以上は不要
                    if (銘柄コード < 1301 || 9999 < 銘柄コード)
                    {
                        continue;
                    }

                    string 市場名称 = sepa[9];
                    string 銘柄名 = sepa[3].Replace(銘柄コード.ToString(), "").Replace(" ", "");

                    string 市場コード = null;
                    if (市場名称.Contains("東証"))
                    {
                        市場コード = "T";
                    }
                    else if (市場名称.Contains("ＪＡＱ"))
                    {
                        市場コード = "Q";
                    }
                    else if (市場名称.Contains("大証"))
                    {
                        市場コード = "OS";
                    }
                    else if (市場名称.Contains("福証"))
                    {
                        市場コード = "F";
                    }
                    else if (市場名称.Contains("札証"))
                    {
                        市場コード = "S";
                    }
                    else
                    {
                        continue;
                        throw new Exception("不明な市場名称が検出されました。\r\n" + "市場名称:" + 市場名称);
                    }


                    double 始値out;
                    double? 始値;
                    if (double.TryParse(sepa[4], out 始値out) == false)
                    {
                        始値 = null;
                    }
                    else
                    {
                        始値 = 始値out;
                    }

                    double 高値out;
                    double? 高値;
                    if (double.TryParse(sepa[5], out 高値out) == false)
                    {
                        高値 = null;
                    }
                    else
                    {
                        高値 = 高値out;
                    }

                    double 安値out;
                    double? 安値;
                    if (double.TryParse(sepa[6], out 安値out) == false)
                    {
                        安値 = null;
                    }
                    else
                    {
                        安値 = 安値out;
                    }

                    double 終値out;
                    double? 終値;
                    if (double.TryParse(sepa[7], out 終値out) == false)
                    {
                        終値 = null;
                    }
                    else
                    {
                        終値 = 終値out;
                    }


                    double 出来高out;
                    double? 出来高;
                    if (double.TryParse(sepa[8], out 出来高out) == false)
                    {
                        出来高 = null;
                    }
                    else
                    {
                        出来高 = 出来高out;
                    }


                    double? 売買代金 = null;

                    StartInsert(date, 銘柄コード, 市場コード, 銘柄名, null, null, null, (DateTime?)date, 終値, null, null, null, null, 出来高, 売買代金, null, 始値, 高値, 安値, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                    System.Diagnostics.Debug.WriteLine(string.Format("銘柄:{0}.{1}  {2}  現在値:{3}", 銘柄コード, 市場コード, 銘柄名, 終値));
                }
            }

            string destipath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Path.Combine(HSTStockDataStream.Properties.Settings.Default.InsertCompleteFolderPathFromDesktop, filename));
            File.Move(path, destipath);
            StockDataデータ挿入履歴Inserter(date, DateTime.Now);


        }


        /// <summary>
        /// データ挿入完了したら履歴に反映
        /// </summary>
        /// <param name="date"></param>
        private void StockDataデータ挿入履歴Inserter(DateTime 株価日付, DateTime 挿入日付)
        {
            using (StockDataFull挿入済株価データ日付TableAdapter ta = new StockDataFull挿入済株価データ日付TableAdapter())
            {
                ta.Insert(株価日付, 挿入日付);
            }
        }





    }


}
