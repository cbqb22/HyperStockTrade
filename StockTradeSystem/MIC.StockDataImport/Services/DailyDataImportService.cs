using CsvHelper.Configuration;
using MIC.Database.Commons.Enums;
using MIC.Database.Connection.DataContexts;
using MIC.Database.Connection.Services.Interfaces;
using MIC.Database.Models;
using MIC.StockDataImport.Models.Csv;
using MIC.StockDataImport.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MIC.StockDataImport.Services
{
    public class DailyDataImportService : IStockDataImportService
    {
        #region Fields

        private readonly IDataContextFactory<DataContext> _dataContextFactory;

        #endregion

        #region Constractor

        public DailyDataImportService(IDataContextFactory<DataContext> factory)
        {
            _dataContextFactory = factory;
        }
        #endregion

        public Task<bool> ImportAsync(string filePath)
        {
            return Task.Run(() =>
            {
                if (!File.Exists(filePath))
                    return false;

                try
                {
                    var fileName = Path.GetFileNameWithoutExtension(filePath);
                    var dealDate = new DateTime(int.Parse(fileName.Substring(0, 4)), int.Parse(fileName.Substring(4, 2)), int.Parse(fileName.Substring(6, 2)));

                    var config = new CsvConfiguration() { Encoding = Encoding.GetEncoding(932), HasHeaderRecord = true };
                    config.RegisterClassMap<DailyDataMap>();

                    //using (var csv = new CsvReader(File.OpenText(FilePath), config))
                    using (var sr = new StreamReader(filePath, Encoding.GetEncoding(932)))
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
                                StockMarketCode = sepa[0],
                                CompanyName = sepa[1],
                                MarketName = sepa[2],
                                OpeningPrice = string.IsNullOrWhiteSpace(sepa[3]) ? null : (double?)double.Parse(sepa[3]),
                                HighPrice = string.IsNullOrWhiteSpace(sepa[4]) ? null : (double?)double.Parse(sepa[4]),
                                LowPrice = string.IsNullOrWhiteSpace(sepa[5]) ? null : (double?)double.Parse(sepa[5]),
                                ClosingPrice = string.IsNullOrWhiteSpace(sepa[6]) ? null : (double?)double.Parse(sepa[6]),
                                Volume = string.IsNullOrWhiteSpace(sepa[7]) ? 0 : double.Parse(sepa[7]),
                                Turnover = string.IsNullOrWhiteSpace(sepa[8]) ? 0 : double.Parse(sepa[8]),
                            });
                        }

                        var companies = context.StockCompany.ToList().Select(x => x.StockCode + "_" + x.MarketCode);
                        var companyTargets = targets.Where(x => !companies.Contains(x.StockMarketCode.Substring(0, 4) + "_" + MarketCodeExtension.GetMarketCodeByMarketName(x.MarketName))).ToList();

                        if (companyTargets.Any())
                        {
                            context.StockCompany.AddRange(companyTargets.Select(x => new StockCompany()
                            {
                                CompanyName = x.CompanyName,
                                StockCode = x.StockMarketCode.Substring(0, 4),
                                MarketCode = MarketCodeExtension.GetMarketCodeByMarketName(x.MarketName),
                            }));
                        }

                        context.SaveChanges();

                        var companies2 = context.StockCompany.Select(x => new { x.StockCompanyId, x.StockCode, x.MarketCode }).ToList();

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
                catch(Exception ex)
                {
                    Debug.WriteLine(string.Format("インポート中にエラーが発生しました。\r\n File:{0} \r\n Message:{1}\r\n Error:{2}", filePath, ex.Message, ex.ToString()));
                    return false;
                }

                return true;
            });
        }
    }
}
