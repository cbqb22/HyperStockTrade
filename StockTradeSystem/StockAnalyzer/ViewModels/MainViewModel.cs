using GalaSoft.MvvmLight;
using StockAnalyzer.Services.Interfaces;
using System.Windows.Input;
using System;
using StockAnalyzer.Models;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using StockAnalyzer.Models.Interfaces;
using System.Threading.Tasks;
using MIC.Common.Commands.Interfaces;

namespace StockAnalyzer.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private const string BaseUrlFormat = "https://stocks.finance.yahoo.co.jp/stocks/chart/?code={0}.T&ct=z&t={1}&q=c&l=off&z=m&p=m65,m130,s&a=v";
        private const int DefaultSelectSpanIndex = 4;

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

        private DateTime _startDate = DateTime.Now.AddDays(-1);
        public DateTime StartDate { get { return _startDate; } set { Set(ref _startDate, value); } }

        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate { get { return _endDate; } set { Set(ref _endDate, value); } }

        private IEnumerable<PickedStockData> _pickedStockDataList;
        public IEnumerable<PickedStockData> PickedStockDataList { get { return _pickedStockDataList; } set { Set(ref _pickedStockDataList, value); } }

        public string Url { get { return _url; } set { Set(ref _url, value); } }
        private string _url;

        private PickedStockData _selectedItem;
        public PickedStockData SelectedItem { get { return _selectedItem; } set { Set(ref _selectedItem, value); } }

        private List<IComboBoxItem<string>> _spanItems = new List<IComboBoxItem<string>>()
            {
                new ComboBoxItem<string>{ Id = "1m", Name = "‚P‚©ŒŽ"},
                new ComboBoxItem<string>{ Id = "3m", Name = "‚R‚©ŒŽ"},
                new ComboBoxItem<string>{ Id = "6m", Name = "‚U‚©ŒŽ"},
                new ComboBoxItem<string>{ Id = "1y", Name = "‚P”N"},
                new ComboBoxItem<string>{ Id = "2y", Name = "‚Q”N"},
                new ComboBoxItem<string>{ Id = "5y", Name = "‚T”N"},
                new ComboBoxItem<string>{ Id = "ay", Name = "‚P‚O”N"},
            };
        public List<IComboBoxItem<string>> SpanItems { get { return _spanItems; }}

        private IComboBoxItem<string> _selectedSpan;
        public IComboBoxItem<string> SelectedSpan { get { return _selectedSpan; } set { Set(ref _selectedSpan, value); } }

        private int _selectSpanIndex;
        public int SelectSpanIndex { get { return _selectSpanIndex; } set { Set(ref _selectSpanIndex, value); } }


        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public MainViewModel(IStockAnalyzeService analyzeService,
                             ICsvService csvService)
        {
            _analyzeService = analyzeService;
            _csvService = csvService;

            AnalyzeCommand = new AsyncRelayCommand(AnalyzeAsync);
            CsvCommand = new AsyncRelayCommand(WriteToCsvAsync);
            ItemSelectionChangedCommand = new AsyncRelayCommand(ItemSelectionChanged);

            PickedStockDataList = new List<PickedStockData>();
            SelectSpanIndex = DefaultSelectSpanIndex;
            EndDate = DateTime.Now;
            StartDate = EndDate.AddMonths(-6);
        }

        #endregion

        #region Command Handlers

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task ItemSelectionChanged()
        {
            if (SelectedItem == null || SelectedSpan == null)
                return;
            await Task.Run(() => Url = string.Format(BaseUrlFormat, SelectedItem.StockCode, SelectedSpan.Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task WriteToCsvAsync()
        {
            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SBIƒ}ƒNƒ");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv");
            if (PickedStockDataList != null && PickedStockDataList.Any())
                await Task.Run(() => _csvService.Write(filePath, PickedStockDataList));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task AnalyzeAsync()
        {
            if (EndDate <= StartDate)
                return;
            await Task.Run(() => PickedStockDataList = _analyzeService.Analyze(StartDate, EndDate, AnalyzeType.Type1));
        }

        #endregion
    }
}