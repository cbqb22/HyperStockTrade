using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HSTDB.StockData;
using HSTDB.StockData.StockDataFullTableAdapters;

namespace HSTMarketSpeed.Routine
{
    public static class StockDataConverter
    {

        // TODO: MarketSpeedで取得したデータでイレギュラーデータの形式を確認
        public static void ConvertToDBFormat(string topic, string item, string value, StockDataFull.StockDataFullRow row)
        {
            StockDataFull.StockDataFullDataTable dt = new StockDataFull.StockDataFullDataTable();

            var t = dt.Columns[item].DataType;

            try
            {
                //始値時刻などが09:00の様な時刻形式であることを確認
                //パターンは"\d\d-\d\d"とも書ける
                if (Regex.IsMatch(value,@"^\d\d:\d\d$") && t == typeof(DateTime))
                {
                    row[item] = new DateTime(row.現在日付.Year,row.現在日付.Month,row.現在日付.Day,int.Parse(value.Split(':')[0]),int.Parse(value.Split(':')[1]),0);
                }
                else if (value == "  -  -  " && t == typeof(DateTime))
                {
                    row[item] = new DateTime(1900, 1, 1);
                }
                // 現在値、始値などが"-"の場合
                else if ((value == "-") && t == typeof(Double))
                {
                    row[item] = DBNull.Value;
                }
                else
                {
                    var val = Convert.ChangeType(value, t);
                    row[item] = val;
                }
                
            }
            catch(Exception e)
            {
                if (t == typeof(DateTime))
                {
                    row[item] = new DateTime(1900,1,1);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Error! \r\ntopic:{0}  item:{1} value:{2}\r\n{3}\r\n{4}", topic, item, value, e.Message, e.StackTrace));
                    row[item] = DBNull.Value;
                }
            }

            #region スニペット
            //if (item == "銘柄コード")
            //{
            //}
            //else if (item == "市場コード")
            //{
            //}
            //else if (item == "銘柄名称")
            //{
            //}
            //else if (item == "市場名称")
            //{
            //}
            //else if (item == "市場部名称")
            //{
            //}
            //else if (item == "市場部略称")
            //{
            //}
            //else if (item == "現在日付")
            //{
            //}
            //else if (item == "現在値")
            //{
            //}
            //else if (item == "前日比")
            //{
            //}
            //else if (item == "前日比率")
            //{
            //}
            //else if (item == "前日終値")
            //{
            //}
            //else if (item == "前日日付")
            //{
            //}
            //else if (item == "出来高")
            //{
            //}
            //else if (item == "売買代金")
            //{
            //}
            //else if (item == "出来高加重平均")
            //{
            //}
            //else if (item == "始値")
            //{
            //}
            //else if (item == "高値")
            //{
            //}
            //else if (item == "安値")
            //{
            //}
            //else if (item == "始値時刻")
            //{
            //}
            //else if (item == "高値時刻")
            //{
            //}
            //else if (item == "安値時刻")
            //{
            //}
            //else if (item == "前場出来高")
            //{
            //}
            //else if (item == "信用貸借区分")
            //{
            //}
            //else if (item == "逆日歩")
            //{
            //}
            //else if (item == "逆日歩更新日付")
            //{
            //}
            //else if (item == "信用売残")
            //{
            //}
            //else if (item == "信用売残前週比")
            //{
            //}
            //else if (item == "信用買残")
            //{
            //}
            //else if (item == "信用買残前週比")
            //{
            //}
            //else if (item == "信用倍率")
            //{
            //}
            //else if (item == "証金コード")
            //{
            //}
            //else if (item == "証金残更新日付")
            //{
            //}
            //else if (item == "新規貸株")
            //{
            //}
            //else if (item == "新規融資")
            //{
            //}
            //else if (item == "返済貸株")
            //{
            //}
            //else if (item == "返済融資")
            //{
            //}
            //else if (item == "残高貸株")
            //{
            //}
            //else if (item == "残高融資")
            //{
            //}
            //else if (item == "残高差引")
            //{
            //}
            //else if (item == "前日比貸株")
            //{
            //}
            //else if (item == "前日比融資")
            //{
            //}
            //else if (item == "前日比差引")
            //{
            //}
            //else if (item == "回転日数")
            //{
            //}
            //else if (item == "貸借倍率")
            //{
            //}
            //else if (item == "単位株数")
            //{
            //}
            //else if (item == "配当")
            //{
            //}
            //else if (item == "配当落日")
            //{
            //}
            //else if (item == "権利落日")
            //{
            //}
            //else if (item == "ＰＥＲ")
            //{
            //}
            //else if (item == "ＰＢＲ")
            //{
            //}
            //else if (item == "年初来高値")
            //{
            //}
            //else if (item == "年初来安値")
            //{
            //}
            //else if (item == "年初来高値日付")
            //{
            //}
            //else if (item == "年初来安値日付")
            //{
            //}
            //else if (item == "上場来高値")
            //{
            //}
            //else if (item == "上場来安値")
            //{
            //}
            //else if (item == "上場来高値日付")
            //{
            //}
            //else if (item == "上場来安値日付")
            //{
            //}
            //else
            //{
            //    throw new Exception("存在しないitemの為、処理を中断します。");
            //}

            #endregion
        }
    }
}
