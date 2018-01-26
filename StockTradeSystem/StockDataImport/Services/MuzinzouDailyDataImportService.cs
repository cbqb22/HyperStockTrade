using CsvHelper;
using CsvHelper.Configuration;
using MIC.Database.Commons.Enums;
using MIC.Database.Connection.DataContexts;
using MIC.Database.Connection.Services.Interfaces;
using MIC.Database.Models;
using StockDataImport.Models.Csv;
using StockDataImport.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockDataImport.Services
{
    public class MuzinzouDailyDataImportService : IStockDataImportService
    {
        #region Fields

        private readonly IDataContextFactory<DataContext> _dataContextFactory;

        #endregion

        #region Constractor

        public MuzinzouDailyDataImportService(IDataContextFactory<DataContext> factory)
        {
            _dataContextFactory = factory;
        }
        #endregion

        public async Task<bool> ImportAsync(string FilePath)
        {
            if (!File.Exists(FilePath))
                return false;

            return await Task.Run(() =>
                         {
                             try
                             {
                                 var fileName = Path.GetFileNameWithoutExtension(FilePath);
                                 var dealDate = new DateTime(int.Parse(fileName.Substring(0, 4)), int.Parse(fileName.Substring(4, 2)), int.Parse(fileName.Substring(6, 2)));

                                 var config = new CsvConfiguration() { Encoding = Encoding.GetEncoding(932), HasHeaderRecord = true };
                                 config.RegisterClassMap<DailyDataMap>();

                                 using (var sr = new StreamReader(FilePath, Encoding.GetEncoding(932)))
                                 using (var context = _dataContextFactory.Create())
                                 using (var tran = context.BeginTransaction())
                                 {
                                     var targets = new List<DailyData>();
                                     var line = "";

                                     int counter = 0;
                                     while ((line = sr.ReadLine()) != null)
                                     {
                                         counter++;
                                         if (counter == 1)
                                             continue;

                                         var sepa = line.Split(',');

                                         if (!string.IsNullOrWhiteSpace(sepa[0]) && sepa[0].Length != 6)
                                             continue;

                                         targets.Add(new DailyData
                                         {
                                             StockMarketCode = sepa[1], // 
                                             CompanyName = sepa[3],
                                             MarketName = sepa[2], // MarketCodeからキャストが必要
                                             OpeningPrice = string.IsNullOrWhiteSpace(sepa[4]) ? null : (double?)double.Parse(sepa[4]),
                                             HighPrice = string.IsNullOrWhiteSpace(sepa[5]) ? null : (double?)double.Parse(sepa[5]),
                                             LowPrice = string.IsNullOrWhiteSpace(sepa[6]) ? null : (double?)double.Parse(sepa[6]),
                                             ClosingPrice = string.IsNullOrWhiteSpace(sepa[7]) ? null : (double?)double.Parse(sepa[7]),
                                             Volume = string.IsNullOrWhiteSpace(sepa[8]) ? 0 : double.Parse(sepa[8]),
                                             Turnover = (string.IsNullOrWhiteSpace(sepa[7]) ? 0 : double.Parse(sepa[7])) * (string.IsNullOrWhiteSpace(sepa[8]) ? 0 : double.Parse(sepa[8]))
                                         });
                                     }

                                     var companies = context.StockCompany.ToList().Select(x => x.StockCode + "_" +  x.MarketCode);

                                     //var companyTargets = targets.Where(x => !companies.Contains(x.StockMarketCode.Substring(0, 4) + "_" + MarketCodeExtension.GetMarketCodeByMarketName(x.MarketName)))
                                     //                            .Distinct(new DailyDataEqualityComparer()).ToList();
                                     var companyTargets = targets.Where(x => !companies.Contains(x.StockMarketCode.Substring(0, 4) + "_" + MarketCodeExtension.GetMarketCodeByMarketName(x.MarketName))).ToList();

                                     if (companyTargets.Any())
                                     {
                                         context.StockCompany.AddRange(companyTargets.Select(x => new StockCompany()
                                         {
                                             CompanyName = x.CompanyName,
                                             StockCode = x.StockMarketCode.Substring(0, 4),
                                             MarketCode = MarketCodeExtension.GetMarketCodeByMarketName(x.MarketName),
                                             //DailyPrices = new List<DailyPrice>
                                             //{
                                             //    new DailyPrice
                                             //    {
                                             //       DealDate = dealDate,
                                             //       OpeningPrice = x.OpeningPrice,
                                             //       HighPrice = x.HighPrice,
                                             //       LowPrice = x.LowPrice,
                                             //       ClosingPrice = x.ClosingPrice,
                                             //       Volume = x.Volume,
                                             //       Turnover = x.Turnover
                                             //    }
                                             //}
                                         }));
                                     }

                                     context.SaveChanges();

                                     var companies2 = context.StockCompany.Select(x => new { x.StockCompanyId, x.StockCode, x.MarketCode }).ToList();

                                     //var list = new List<DailyPrice>();

                                     //targets.ForEach(x =>
                                     //{
                                     //    var id = companies2.FirstOrDefault(y => y.StockCode == x.StockMarketCode.Substring(0, 4) && y.MarketCode.GetMarketCode() == MarketCodeExtension.GetMarketCodeByMarketName(x.MarketName).GetMarketCode());

                                     //    if (id == null)
                                     //        return;

                                     //    var dp = new DailyPrice()
                                     //    {
                                     //        StockCompanyId = id.StockCompanyId,
                                     //        DealDate = dealDate,
                                     //        OpeningPrice = x.OpeningPrice,
                                     //        HighPrice = x.HighPrice,
                                     //        LowPrice = x.LowPrice,
                                     //        ClosingPrice = x.ClosingPrice,
                                     //        Volume = x.Volume,
                                     //        Turnover = x.Turnover
                                     //    };


                                     //});

                                     if (targets.Any())
                                     {
                                         context.DailyPrice.AddRange(targets.Select(x => new DailyPrice()
                                         {
                                             StockCompanyId = companies2.First(y => y.StockCode == x.StockMarketCode.Substring(0, 4) && y.MarketCode == MarketCodeExtension.GetMarketCodeByMarketName(x.MarketName)).StockCompanyId,
                                             DealDate = dealDate,
                                             OpeningPrice = x.OpeningPrice,
                                             HighPrice = x.HighPrice,
                                             LowPrice = x.LowPrice,
                                             ClosingPrice = x.ClosingPrice,
                                             Volume = x.Volume,
                                             Turnover = x.Turnover
                                         }));
                                     }

                                     context.SaveChanges();

                                     tran.Commit();
                                 }
                             }
                             catch (Exception ex)
                             {
                                 return false;
                             }

                             return true;
                         });
        }
    }
}
