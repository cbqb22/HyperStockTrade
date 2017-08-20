using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StockDataImport.Services.Interfaces;
using System;
using System.Windows.Input;

namespace StockDataImport.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
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
            var current = new DateTime(2017, 01, 01);
            var end = new DateTime(2017, 08, 20); ;

            while (current <= end)
            {
                await _downloadService.DownloadAsync(current);
                await _importService.ImportAsync(_downloadService.OutputPath);

                current = current.AddDays(1);
            }
        }
    }
}