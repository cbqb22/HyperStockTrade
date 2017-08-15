using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HSTDB.StockData;
namespace HSTStockDataStream.Routine.FileStream
{
    public static class StockDataWriter
    {

        public static void MarketSpeedDataWriteToCSV(DateTime date,StockDataFull.StockDataFullDataTable dt)
        {
            string SAVE_DESTINATION = string.Format(@"{0}\{1}_StockAllData_MS.csv", Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), HSTStockDataStream.Properties.Settings.Default.DownloadFolderPathFromDesktop), date.ToString("yyyyMMddHHmm"));

            using (StreamWriter sw = new StreamWriter(SAVE_DESTINATION, false, Encoding.GetEncoding(932)))
            {
                // ヘッダー
                sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53},{54},{55},{56},{57}",
                                "銘柄コード","市場コード","銘柄名称","市場名称","市場部名称","市場部略称","現在日付","現在値","前日比","前日比率","前日終値","前日日付","出来高","売買代金","出来高加重平均","始値","高値","安値","始値時刻","高値時刻","安値時刻","前場出来高","信用貸借区分","逆日歩","逆日歩更新日付","信用売残","信用売残前週比","信用買残","信用買残前週比","信用倍率","証金コード","証金残更新日付","新規貸株","新規融資","返済貸株","返済融資","残高貸株","残高融資","残高差引","前日比貸株","前日比融資","前日比差引","回転日数","貸借倍率","単位株数","配当","配当落日","権利落日","ＰＥＲ","ＰＢＲ","年初来高値","年初来安値","年初来高値日付","年初来安値日付","上場来高値","上場来安値","上場来高値日付","上場来安値日付"
                            );
                foreach (var row in dt)
                {
                    sw.WriteLine
                        (
                            "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53},{54},{55},{56},{57}",
                            row.銘柄コード.ToString(),
                            row.市場コード.ToString(),
                            row.Is銘柄名称Null() ? "-" : row.銘柄名称.ToString(),
                            row.Is市場名称Null() ? "-" : row.市場名称.ToString(),
                            row.Is市場部名称Null() ? "-" : row.市場部名称.ToString(),
                            row.Is市場部略称Null() ? "-" : row.市場部略称.ToString(),
                            row.現在日付.ToString(),
                            row.Is現在値Null() ? "-" : row.現在値.ToString(),
                            row.Is前日比Null() ? "-" : row.前日比.ToString(),
                            row.Is前日比率Null() ? "-" : row.前日比率.ToString(),
                            row.Is前日終値Null() ? "-" : row.前日終値.ToString(),
                            row.Is前日日付Null() ? "-" : row.前日日付.ToString(),
                            row.Is出来高Null() ? "-" : row.出来高.ToString(),
                            row.Is売買代金Null() ? "-" : row.売買代金.ToString(),
                            row.Is出来高加重平均Null() ? "-" : row.出来高加重平均.ToString(),
                            row.Is始値Null() ? "-" : row.始値.ToString(),
                            row.Is高値Null() ? "-" : row.高値.ToString(),
                            row.Is安値Null() ? "-" : row.安値.ToString(),
                            row.Is始値時刻Null() ? "-" : row.始値時刻.ToString(),
                            row.Is高値時刻Null() ? "-" : row.高値時刻.ToString(),
                            row.Is安値時刻Null() ? "-" : row.安値時刻.ToString(),
                            row.Is前場出来高Null() ? "-" : row.前場出来高.ToString(),
                            row.Is信用貸借区分Null() ? "-" : row.信用貸借区分.ToString(),
                            row.Is逆日歩Null() ? "-" : row.逆日歩.ToString(),
                            row.Is逆日歩更新日付Null() ? "-" : row.逆日歩更新日付.ToString(),
                            row.Is信用売残Null() ? "-" : row.信用売残.ToString(),
                            row.Is信用売残前週比Null() ? "-" : row.信用売残前週比.ToString(),
                            row.Is信用買残Null() ? "-" : row.信用買残.ToString(),
                            row.Is信用買残前週比Null() ? "-" : row.信用買残前週比.ToString(),
                            row.Is信用倍率Null() ? "-" : row.信用倍率.ToString(),
                            row.Is証金コードNull() ? "-" : row.証金コード.ToString(),
                            row.Is証金残更新日付Null() ? "-" : row.証金残更新日付.ToString(),
                            row.Is新規貸株Null() ? "-" : row.新規貸株.ToString(),
                            row.Is新規融資Null() ? "-" : row.新規融資.ToString(),
                            row.Is返済貸株Null() ? "-" : row.返済貸株.ToString(),
                            row.Is返済融資Null() ? "-" : row.返済融資.ToString(),
                            row.Is残高貸株Null() ? "-" : row.残高貸株.ToString(),
                            row.Is残高融資Null() ? "-" : row.残高融資.ToString(),
                            row.Is残高差引Null() ? "-" : row.残高差引.ToString(),
                            row.Is前日比貸株Null() ? "-" : row.前日比貸株.ToString(),
                            row.Is前日比融資Null() ? "-" : row.前日比融資.ToString(),
                            row.Is前日比差引Null() ? "-" : row.前日比差引.ToString(),
                            row.Is回転日数Null() ? "-" : row.回転日数.ToString(),
                            row.Is貸借倍率Null() ? "-" : row.貸借倍率.ToString(),
                            row.Is単位株数Null() ? "-" : row.単位株数.ToString(),
                            row.Is配当Null() ? "-" : row.配当.ToString(),
                            row.Is配当落日Null() ? "-" : row.配当落日.ToString(),
                            row.Is権利落日Null() ? "-" : row.権利落日.ToString(),
                            row.IsＰＥＲNull() ? "-" : row.ＰＥＲ.ToString(),
                            row.IsＰＢＲNull() ? "-" : row.ＰＢＲ.ToString(),
                            row.Is年初来高値Null() ? "-" : row.年初来高値.ToString(),
                            row.Is年初来安値Null() ? "-" : row.年初来安値.ToString(),
                            row.Is年初来高値日付Null() ? "-" : row.年初来高値日付.ToString(),
                            row.Is年初来安値日付Null() ? "-" : row.年初来安値日付.ToString(),
                            row.Is上場来高値Null() ? "-" : row.上場来高値.ToString(),
                            row.Is上場来安値Null() ? "-" : row.上場来安値.ToString(),
                            row.Is上場来高値日付Null() ? "-" : row.上場来高値日付.ToString(),
                            row.Is上場来安値日付Null() ? "-" : row.上場来安値日付.ToString()
                        );
                }

            }
        }

    }
}
