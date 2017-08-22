using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StockAnalyzer.Services.Interfaces;
using System.Windows.Input;
using System;
using System.Collections.ObjectModel;
using StockAnalyzer.Models;
using System.Collections.Generic;

namespace StockAnalyzer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Services

        private readonly IStockAnalyzeService _analyzeService;

        #endregion

        #region Commands

        public ICommand AnalyzeCommand { get; set; }

        #endregion

        #region Properties

        private IEnumerable<PickedStockData> _pickedStockDataList;
        public IEnumerable<PickedStockData> PickedStockDataList { get { return _pickedStockDataList; } set { Set(ref _pickedStockDataList, value); } }



        #endregion

        /// <summary>
        /// 
        /// </summary>
        public MainViewModel(IStockAnalyzeService analyzeService)
        {
            _analyzeService = analyzeService;
            AnalyzeCommand = new RelayCommand(Analyze);

            PickedStockDataList = new List<PickedStockData>();
        }

        private void Analyze()
        {
            var start = new DateTime(2017, 01, 01);
            var end = new DateTime(2017, 8, 19);
            PickedStockDataList = _analyzeService.Analyze(start, end);
        }
    }
}