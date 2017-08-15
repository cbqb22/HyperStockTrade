using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using HSTDB.StockData;
using HSTDB.StockData.StockDataFullTableAdapters;
using NDde;
using NDde.Client;
using NDde.Server;

namespace HSTMarketSpeed.Routine
{
    public static class MSStockDataDownloader
    {
        /// <summary>
        /// MSダウンロード開始する為のリセッター
        /// </summary>
        public static void ResetMSandRSS()
        {
            MSLoginLogout.Exit();
            MSRSS.MarketSpeedRSSShutdown();
            MSLoginLogout.LoginWork("NKUS7998", "cbqb22");
            MSRSS.MarketSpeedRSSStart();
        }


        public static string GetStockData(string topic, string item)
        {
            ResetMSandRSS();

            string name = null;

            NDde.DdeException ddeex = null;
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    using (DdeClient client = new DdeClient("RSS", topic))
                    {
                        name = Request(topic, item,client);
                    }
                }
                catch (NDde.DdeException e)
                {
                    if (e.Message.Contains("The client failed to request") || e.Message.Contains("The client failed to connect"))
                    {
                        // 最後は例外入れる
                        if (i == 4)
                        {
                            ddeex = e;
                            break;
                        }
                        continue;
                    }
                    else
                    {
                        ddeex = e;
                        break;
                    }
                }

                break;
            }

            if (ddeex != null)
            {
                throw ddeex;
            }

            if (name == null)
            {
                throw new Exception("Request中に予期せぬ例外が発生しました。");
            }

            return name;


        }

        private static string Request(string topic, string item, DdeClient client)
        {
            string name = null;


            try
            {
                DateTime start = DateTime.Now;

                // 楽天RSSサービスに接続する  
                if (client.IsConnected == false)
                {
                    client.Connect();
                }


                // リクエストを出して値を取得する  
                // 第1引数は取得したいアイテム名  
                // 第2引数は内部的にDdeClientTransaction()をコールしており  
                // wFmtの[CF_TEXT(文字列)]と等価である1を指定する  
                // 第3引数はタイムアウトする時間をミリ秒単位で指定する  
                DateTime start2 = DateTime.Now;

                byte[] data = client.Request(item, 1, 500);



                name = Encoding.Default.GetString(data).Replace("\0", "");
            }
            catch (NDde.DdeException ddex)
            {
                throw ddex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return name;


        }

        public static StockDataFull.StockDataFullDataTable GetStockDatas(List<string> topicslist, List<string> itemslist)
        {

            ResetMSandRSS();
            StockDataFull.StockDataFullDataTable dt = new StockDataFull.StockDataFullDataTable();
            int counter = 0;
            try
            {

                foreach (var topic in topicslist)
                {
                    // DateTime start = DateTime.Now;

                    int errorcounter = 0;
                    StockDataFull.StockDataFullRow row = dt.NewRow() as StockDataFull.StockDataFullRow;
                    using (DdeClient client = new DdeClient("RSS", topic))
                    {

                        foreach (var item in itemslist)
                        {
                            // System.Diagnostics.Debug.WriteLine(item);
                            counter++;
                            string name = null;
                            NDde.DdeException ddeex = null;

                            for (int i = 0; i < 5; i++)
                            {
                                try
                                {
                                    name = Request(topic, item, client);
                                }
                                catch (NDde.DdeException e)
                                {
                                    errorcounter++;

                                    if (errorcounter == 5)
                                    {
                                        ddeex = e;
                                        //continue;
                                         break;
                                    }

                                    if (e.Message.Contains("The client failed to request") || e.Message.Contains("The client failed to connect"))
                                    {
                                        // 最後は例外入れる
                                        if (i == 4)
                                        {
                                            ddeex = e;
                                            // continue;
                                            break;
                                        }
                                        continue;
                                    }
                                    else
                                    {
                                        ddeex = e;
                                        //continue;
                                        break;
                                    }
                                }

                                break;
                            }

                            if (ddeex != null)
                            {
                                // throw ddeex;
                                break;
                            }

                            if (name == null)
                            {
                                //continue;
                                break;
                                // throw new Exception("Request中に予期せぬ例外が発生しました。");
                            }

                            StockDataConverter.ConvertToDBFormat(topic, item, name, row);
                        }
                    }

                    try
                    {
                        dt.AddStockDataFullRow(row);
                    }
                    catch
                    {
                        continue;
                    }


                    #region DebugWriteline
                    System.Diagnostics.Debug.WriteLine(
                        string.Format
                        (
                            "count:{58} 銘柄コード:{0},市場コード:{1},銘柄名称:{2},市場名称:{3},市場部名称:{4},市場部略称:{5},現在日付:{6},現在値:{7},前日比:{8},前日比率:{9},前日終値:{10},前日日付:{11},出来高:{12},売買代金:{13},出来高加重平均:{14},始値:{15},高値:{16},安値:{17},始値時刻:{18},高値時刻:{19},安値時刻:{20},前場出来高:{21},信用貸借区分:{22},逆日歩:{23},逆日歩更新日付:{24},信用売残:{25},信用売残前週比:{26},信用買残:{27},信用買残前週比:{28},信用倍率:{29},証金コード:{30},証金残更新日付:{31},新規貸株:{32},新規融資:{33},返済貸株:{34},返済融資:{35},残高貸株:{36},残高融資:{37},残高差引:{38},前日比貸株:{39},前日比融資:{40},前日比差引:{41},回転日数:{42},貸借倍率:{43},単位株数:{44},配当:{45},配当落日:{46},権利落日:{47},ＰＥＲ:{48},ＰＢＲ:{49},年初来高値:{50},年初来安値:{51},年初来高値日付:{52},年初来安値日付:{53},上場来高値:{54},上場来安値:{55},上場来高値日付:{56},上場来安値日付{57}",
                            row.銘柄コード.ToString(),
                            row.市場コード.ToString(),
                            row.Is銘柄名称Null() ? "DBnull" : row.銘柄名称.ToString(),
                            row.Is市場名称Null() ? "DBnull" : row.市場名称.ToString(),
                            row.Is市場部名称Null() ? "DBnull" : row.市場部名称.ToString(),
                            row.Is市場部略称Null() ? "DBnull" : row.市場部略称.ToString(),
                            row.現在日付.ToString(),
                            row.Is現在値Null() ? "DBnull" : row.現在値.ToString(),
                            row.Is前日比Null() ? "DBnull" : row.前日比.ToString(),
                            row.Is前日比率Null() ? "DBnull" : row.前日比率.ToString(),
                            row.Is前日終値Null() ? "DBnull" : row.前日終値.ToString(),
                            row.Is前日日付Null() ? "DBnull" : row.前日日付.ToString(),
                            row.Is出来高Null() ? "DBnull" : row.出来高.ToString(),
                            row.Is売買代金Null() ? "DBnull" : row.売買代金.ToString(),
                            row.Is出来高加重平均Null() ? "DBnull" : row.出来高加重平均.ToString(),
                            row.Is始値Null() ? "DBnull" : row.始値.ToString(),
                            row.Is高値Null() ? "DBnull" : row.高値.ToString(),
                            row.Is安値Null() ? "DBnull" : row.安値.ToString(),
                            row.Is始値時刻Null() ? "DBnull" : row.始値時刻.ToString(),
                            row.Is高値時刻Null() ? "DBnull" : row.高値時刻.ToString(),
                            row.Is安値時刻Null() ? "DBnull" : row.安値時刻.ToString(),
                            row.Is前場出来高Null() ? "DBnull" : row.前場出来高.ToString(),
                            row.Is信用貸借区分Null() ? "DBnull" : row.信用貸借区分.ToString(),
                            row.Is逆日歩Null() ? "DBnull" : row.逆日歩.ToString(),
                            row.Is逆日歩更新日付Null() ? "DBnull" : row.逆日歩更新日付.ToString(),
                            row.Is信用売残Null() ? "DBnull" : row.信用売残.ToString(),
                            row.Is信用売残前週比Null() ? "DBnull" : row.信用売残前週比.ToString(),
                            row.Is信用買残Null() ? "DBnull" : row.信用買残.ToString(),
                            row.Is信用買残前週比Null() ? "DBnull" : row.信用買残前週比.ToString(),
                            row.Is信用倍率Null() ? "DBnull" : row.信用倍率.ToString(),
                            row.Is証金コードNull() ? "DBnull" : row.証金コード.ToString(),
                            row.Is証金残更新日付Null() ? "DBnull" : row.証金残更新日付.ToString(),
                            row.Is新規貸株Null() ? "DBnull" : row.新規貸株.ToString(),
                            row.Is新規融資Null() ? "DBnull" : row.新規融資.ToString(),
                            row.Is返済貸株Null() ? "DBnull" : row.返済貸株.ToString(),
                            row.Is返済融資Null() ? "DBnull" : row.返済融資.ToString(),
                            row.Is残高貸株Null() ? "DBnull" : row.残高貸株.ToString(),
                            row.Is残高融資Null() ? "DBnull" : row.残高融資.ToString(),
                            row.Is残高差引Null() ? "DBnull" : row.残高差引.ToString(),
                            row.Is前日比貸株Null() ? "DBnull" : row.前日比貸株.ToString(),
                            row.Is前日比融資Null() ? "DBnull" : row.前日比融資.ToString(),
                            row.Is前日比差引Null() ? "DBnull" : row.前日比差引.ToString(),
                            row.Is回転日数Null() ? "DBnull" : row.回転日数.ToString(),
                            row.Is貸借倍率Null() ? "DBnull" : row.貸借倍率.ToString(),
                            row.Is単位株数Null() ? "DBnull" : row.単位株数.ToString(),
                            row.Is配当Null() ? "DBnull" : row.配当.ToString(),
                            row.Is配当落日Null() ? "DBnull" : row.配当落日.ToString(),
                            row.Is権利落日Null() ? "DBnull" : row.権利落日.ToString(),
                            row.IsＰＥＲNull() ? "DBnull" : row.ＰＥＲ.ToString(),
                            row.IsＰＢＲNull() ? "DBnull" : row.ＰＢＲ.ToString(),
                            row.Is年初来高値Null() ? "DBnull" : row.年初来高値.ToString(),
                            row.Is年初来安値Null() ? "DBnull" : row.年初来安値.ToString(),
                            row.Is年初来高値日付Null() ? "DBnull" : row.年初来高値日付.ToString(),
                            row.Is年初来安値日付Null() ? "DBnull" : row.年初来安値日付.ToString(),
                            row.Is上場来高値Null() ? "DBnull" : row.上場来高値.ToString(),
                            row.Is上場来安値Null() ? "DBnull" : row.上場来安値.ToString(),
                            row.Is上場来高値日付Null() ? "DBnull" : row.上場来高値日付.ToString(),
                            row.Is上場来安値日付Null() ? "DBnull" : row.上場来安値日付.ToString(),
                            counter
                        )


                        );

                    #endregion


                    //DateTime end = DateTime.Now;
                    //var span = end - start;
                    //System.Diagnostics.Debug.WriteLine(span.TotalSeconds);


                }



            }
            catch (Exception e)
            {
                throw e;
            }

            return dt;





        }



        //public static StockDataFull.StockDataFullDataTable GetStockDatas(List<string> topicslist, List<string> itemslist)
        //{

        //    ResetMSandRSS();

        //    StockDataFull.StockDataFullDataTable dt = new StockDataFull.StockDataFullDataTable();

        //    foreach (var topic in topicslist)
        //    {

        //        StockDataFull.StockDataFullRow row = dt.NewRow() as StockDataFull.StockDataFullRow;

        //        foreach (var item in itemslist)
        //        {
        //            string name = null;
        //            using (DdeClient client = new DdeClient("RSS", topic))
        //            {
        //                // 楽天RSSサービスに接続する  
        //                client.Connect();

        //                // リクエストを出して値を取得する  
        //                // 第1引数は取得したいアイテム名  
        //                // 第2引数は内部的にDdeClientTransaction()をコールしており  
        //                // wFmtの[CF_TEXT(文字列)]と等価である1を指定する  
        //                // 第3引数はタイムアウトする時間をミリ秒単位で指定する  
        //                byte[] data = client.Request(item, 1, 1000);

        //                name = Encoding.Default.GetString(data).Replace("\0", "");
        //            }

        //            StockDataConverter.ConvertToDBFormat(item, name, row);
        //        }

        //        dt.AddStockDataFullRow(row);


        //    }

        //    return dt;





        //}


    }
}
