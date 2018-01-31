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
        #region Properties

        private DateTime _startDate = DateTime.Now.AddDays(-1);
        public DateTime StartDate { get { return _startDate; } set { Set(ref _startDate, value); } }


        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate { get { return _endDate; } set { Set(ref _endDate, value); } }

        #endregion

        #region Services

        private readonly IStockDataDownloadService _downloadService;
        private readonly IStockDataImportService _importService;
        private readonly IStockDataBackupService _backupService;

        #endregion

        #region Commands

        public ICommand DownloadCommand { get; set; }

        #endregion


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IStockDataDownloadService downloadService,
                             IStockDataImportService importService,
                             IStockDataBackupService backupService)
        {
            _downloadService = downloadService;
            _importService = importService;
            _backupService = backupService;

            DownloadCommand = new RelayCommand(Download);
        }

        private async void Download()
        {
            var current = _startDate;
            var end = _endDate;

            while (current <= end)
            {
                await _backupService.BackupAsync();
                await _downloadService.DownloadAsync(current);
                await _importService.ImportAsync(_downloadService.OutputPath);

                current = current.AddDays(1);
            }
        }
    }
}