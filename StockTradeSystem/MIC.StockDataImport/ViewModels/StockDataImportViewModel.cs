using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MIC.Common.Commands.Interfaces;
using MIC.Common.ViewModelBases;
using MIC.StockDataImport.Services.Interfaces;
using MIC.Common.Dialogs.Extensions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics.CodeAnalysis;

namespace MIC.StockDataImport.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class StockDataImportViewModel : ProgressViewModelBase
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
        public StockDataImportViewModel(IStockDataDownloadService downloadService,
                             IStockDataImportService importService,
                             IStockDataBackupService backupService)
        {
            _downloadService = downloadService;
            _importService = importService;
            _backupService = backupService;

            DownloadCommand = new AsyncRelayCommand(DownloadAsync);
        }

        //[SuppressMessage("Compiler", "CS4014")]
        private async Task DownloadAsync()
        {
            var current = _startDate;
            var end = _endDate;

            try
            {
                using (GetProgress("ダウンロード"))
                {

                    while (current <= end)
                    {
                        await _backupService.BackupAsync();
                        await _downloadService.DownloadAsync(current);
                        await _importService.ImportAsync(_downloadService.OutputPath);

                        current = current.AddDays(1);
                    }
                }
            }
            catch (Exception ex)
            {
#pragma warning disable 4014
                this.RequestShowDialog("Error", ex.Message);
            }
        }
    }
}