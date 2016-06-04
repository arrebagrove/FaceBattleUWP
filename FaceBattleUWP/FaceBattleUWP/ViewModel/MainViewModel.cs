using System;
using FaceBattleControl;
using FaceBattleUWP.Common;
using FaceBattleUWP.Model;
using FaceBattleUWP.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using JP.Utils.Data;
using JP.Utils.Framework;

namespace FaceBattleUWP.ViewModel
{
    public class MainViewModel : ViewModelBase,INavigable
    {
        private FBUser _currentUser;
        public FBUser CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    RaisePropertyChanged(() => CurrentUser);
                }
            }
        }

        private RelayCommand _logoutCommand;
        public RelayCommand LogoutCommand
        {
            get
            {
                if (_logoutCommand != null) return _logoutCommand;
                return _logoutCommand = new RelayCommand(async () =>
                  {
                      DialogService ds = new DialogService(DialogKind.PlainText, "ATTENTION", "Confrim to logout?");
                      ds.OnLeftBtnClick += (s) =>
                        {
                            LocalSettingHelper.CleanUpAll();
                            NavigationService.NaivgateToPage(typeof(StartPage));
                        };
                      ds.OnRightBtnClick += () =>
                        {

                        };
                      await ds.ShowAsync();
                  });
            }
        }

        private RelayCommand _enterClassicModeCommand;
        public RelayCommand EnterClassicModeCommand
        {
            get
            {
                if (_enterClassicModeCommand != null) return _enterClassicModeCommand;
                return _enterClassicModeCommand = new RelayCommand(() =>
                  {
                      NavigationService.NaivgateToPage(typeof(CapturePage));
                  });
            }
        }

        private RelayCommand _enterHulkModeCommand;
        public RelayCommand EnterHulkModeCommand
        {
            get
            {
                if (_enterHulkModeCommand != null) return _enterHulkModeCommand;
                return _enterHulkModeCommand = new RelayCommand(() =>
                  {
                      NavigationService.NaivgateToPage(typeof(CapturePage));
                  });
            }
        }

        public bool IsInView { get; set; }

        public bool IsFirstActived { get; set; }

        public MainViewModel()
        {
            InitUser();
        }

        private void InitUser()
        {
            var uid = LocalSettingHelper.GetValue("uid");
            var userName = LocalSettingHelper.GetValue("username");
            var authCode = LocalSettingHelper.GetValue("authcode");

            CurrentUser = new FBUser()
            {
                UID = int.Parse(uid),
                UserName = userName,
                AuthCode = authCode,
            };
        }

        public void Activate(object param)
        {
            
        }

        public void Deactivate(object param)
        {
            
        }

        public void OnLoaded()
        {
            
        }
    }
}
