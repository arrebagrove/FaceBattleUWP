using FaceBattleUWP.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using JP.Utils.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace FaceBattleUWP.ViewModel
{
    public class UploadAnalysisViewModel : ViewModelBase, INavigable
    {
        private BitmapImage _imageBitmap;
        public BitmapImage ImageBitmap
        {
            get
            {
                return _imageBitmap;
            }
            set
            {
                if (_imageBitmap != value)
                {
                    _imageBitmap = value;
                    RaisePropertyChanged(() => ImageBitmap);
                }
            }
        }

        private Visibility _showLoading;
        public Visibility ShowLoading
        {
            get
            {
                return _showLoading;
            }
            set
            {
                if (_showLoading != value)
                {
                    _showLoading = value;
                    RaisePropertyChanged(() => ShowLoading);
                }
            }
        }

        private RelayCommand _confirmLoadingCommand;
        public RelayCommand ConfirmLoadingCommand
        {
            get
            {
                if (_confirmLoadingCommand != null) return _confirmLoadingCommand;
                return _confirmLoadingCommand = new RelayCommand(() =>
                  {
                      ShowLoading = Visibility.Visible;
                      ShowConfirmGrid = false;
                  });
            }
        }

        private RelayCommand _cancelLoadingCommand;
        public RelayCommand CancelLoadingCommand
        {
            get
            {
                if (_cancelLoadingCommand != null) return _cancelLoadingCommand;
                return _cancelLoadingCommand = new RelayCommand(() =>
                  {
                      try
                      {
                          _cts?.Cancel();
                      }
                      catch (Exception)
                      {

                      }
                      finally
                      {
                          ShowLoading = Visibility.Collapsed;
                          ShowConfirmGrid = true;
                      }
                  });
            }
        }

        private RelayCommand _backToCaptureCommand;
        public RelayCommand BackToCaptureCommand
        {
            get
            {
                if (_backToCaptureCommand != null) return _backToCaptureCommand;
                return _backToCaptureCommand = new RelayCommand(() =>
                  {
                      NavigationService.GoBack();
                  });
            }
        }

        private bool _showConfirmGrid;
        public bool ShowConfirmGrid
        {
            get
            {
                return _showConfirmGrid;
            }
            set
            {
                if (_showConfirmGrid != value)
                {
                    _showConfirmGrid = value;
                    RaisePropertyChanged(() => ShowConfirmGrid);
                }
            }
        }

        private CancellationTokenSource _cts;

        public bool IsFirstActived { get; set; }

        public bool IsInView { get; set; }

        public UploadAnalysisViewModel()
        {
            ShowLoading = Visibility.Collapsed;
        }

        private async Task DisplayImage(StorageFile file)
        {
            using (var fs = await file.OpenReadAsync())
            {
                ImageBitmap = new BitmapImage();
                await ImageBitmap.SetSourceAsync(fs);
            }
        }

        public async void Activate(object param)
        {
            if (param is StorageFile)
            {
                var file = param as StorageFile;
                await DisplayImage(file);
            }
        }

        public void Deactivate(object param)
        {

        }

        public void OnLoaded()
        {

        }
    }
}
