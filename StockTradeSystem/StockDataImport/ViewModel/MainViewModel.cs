using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StockDataImport.Services.Interfaces;
using System;
using System.Windows.Input;

namespace StockDataImport.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        #region Services

        private readonly IStockDataDownloadService _downloadService;
        private readonly IStockDataImportService _importService;

        #endregion

        #region Commands

        public ICommand DownloadCommand { get; set; }

        #endregion


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IStockDataDownloadService downloadService,
                             IStockDataImportService importService)
        {
            _downloadService = downloadService;
            _importService = importService;

            DownloadCommand = new RelayCommand(Download);
        }

        private async void Download()
        {
            var current = new DateTime(2017, 11, 09);
            var end = DateTime.Now;

            while (current <= end)
            {
                await _downloadService.DownloadAsync(current);
                await _importService.ImportAsync(_downloadService.OutputPath);

                current = current.AddDays(1);
            }
        }
    }
}