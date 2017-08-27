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
        #region Fields

        private const string BaseUrlFormat = "https://stocks.finance.yahoo.co.jp/stocks/chart/?code={0}.T&ct=z&t=5y&q=c&l=off&z=m&p=m65,m130,s&a=v";

        #endregion

        #region Services

        private readonly IStockAnalyzeService _analyzeService;
        private readonly ICsvService _csvService;



        #endregion

        #region Commands

        public ICommand AnalyzeCommand { get; set; }
        public ICommand CsvCommand { get; set; }
        public ICommand ItemSelectionChangedCommand { get; set; }
        
        #endregion

        #region Properties

        private IEnumerable<PickedStockData> _pickedStockDataList;
        public IEnumerable<PickedStockData> PickedStockDataList { get { return _pickedStockDataList; } set { Set(ref _pickedStockDataList, value); } }

        public string Url { get { return _url; } set { Set(ref _url, value); } }
        private string _url;

        private PickedStockData _selectedItem;
        public PickedStockData SelectedItem { get { return _selectedItem; } set { Set(ref _selectedItem, value); } }



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
            ItemSelectionChangedCommand = new RelayCommand(ItemSelectionChanged);

            PickedStockDataList = new List<PickedStockData>();
        }

        private void ItemSelectionChanged()
        {
            if (SelectedItem == null)
                return;
            Url = string.Format(BaseUrlFormat, SelectedItem.StockCode);
        }

        private void WriteToCsv()
        {
            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),  "SBIÉ}ÉNÉç");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv");
            if (PickedStockDataList != null && PickedStockDataList.Any())
                _csvService.Write(filePath, PickedStockDataList);
        }

        private void Analyze()
        {
            var start = new DateTime(2017, 01, 01);
            var end = new DateTime(2017, 8, 19);
            PickedStockDataList = _analyzeService.Analyze(start, end);
        }
    }
}