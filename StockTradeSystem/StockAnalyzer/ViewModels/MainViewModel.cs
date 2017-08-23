using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StockAnalyzer.Services.Interfaces;
using System.Windows.Input;
using System;
using StockAnalyzer.Models;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace StockAnalyzer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Services

        private readonly IStockAnalyzeService _analyzeService;
        private readonly ICsvService _csvService;



        #endregion

        #region Commands

        public ICommand AnalyzeCommand { get; set; }
        public ICommand CsvCommand { get; set; }

        #endregion

        #region Properties

        private IEnumerable<PickedStockData> _pickedStockDataList;
        public IEnumerable<PickedStockData> PickedStockDataList { get { return _pickedStockDataList; } set { Set(ref _pickedStockDataList, value); } }



        #endregion

        /// <summary>
        /// 
        /// </summary>
        public MainViewModel(IStockAnalyzeService analyzeService,
                             ICsvService csvService)
        {
            _analyzeService = analyzeService;
            _csvService = csvService;

            AnalyzeCommand = new RelayCommand(Analyze);
            CsvCommand = new RelayCommand(WriteToCsv);

            PickedStockDataList = new List<PickedStockData>();
        }

        private void WriteToCsv()
        {
            var path = Path.Combine(@"C:\Users\poohace\Desktop\SBIマクロ\自動表示マクロ", DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv");
            if (PickedStockDataList != null && PickedStockDataList.Any())
                _csvService.Write(path, PickedStockDataList);
        }

        private void Analyze()
        {
            var start = new DateTime(2017, 01, 01);
            var end = new DateTime(2017, 8, 19);
            PickedStockDataList = _analyzeService.Analyze(start, end);
        }
    }
}