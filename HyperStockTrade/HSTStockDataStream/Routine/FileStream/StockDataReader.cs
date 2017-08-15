using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSTDB.StockData;
using System.IO;

namespace HSTStockDataStream.Routine.FileStream
{
    public static class StockDataReader
    {
        public static List<string> ReadStockDataItemList()
        {
            List<string> itemlist = new List<string>();
            string itemlistpath = Path.Combine(System.IO.Directory.GetCurrentDirectory(),"StockDataItemList.csv");
            using (StreamReader sr = new StreamReader(itemlistpath, Encoding.GetEncoding(932)))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    itemlist.Add(line);
                }
            }

            return itemlist;

        }
    }
}
