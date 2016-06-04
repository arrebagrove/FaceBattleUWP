using FaceBattleControl;
using FaceBattleUWP.Common;
using FaceBattleUWP.Model;
using FaceBattleUWP.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using JP.Utils.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBattleUWP.ViewModel
{
    public class MainViewModel:ViewModelBase
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
                return _logoutCommand = new RelayCommand(async() =>
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
    }
}
