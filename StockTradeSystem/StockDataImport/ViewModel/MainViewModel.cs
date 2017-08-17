using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StockDataImport.Services.Interfaces;
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

        private readonly IStockDataDownloader _stockDataDownloader;

        #endregion

        #region Commands

        public ICommand DownloadCommand { get; set; }

        #endregion


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IStockDataDownloader stockDataDownloader)
        {
            _stockDataDownloader = stockDataDownloader;

            DownloadCommand = new RelayCommand(Download);
        }

        private void Download()
        {
            _stockDataDownloader.DownloadAsync();
        }
    }
}