using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace FaceBattleUWP.ViewModel
{
    public enum LoginMode
    {
        Login,
        Register
    }

    public class StartViewModel : ViewModelBase
    {
        private string SWITCH_TO_LOGIN_TEXT => "OR LOGIN NOW";
        private string SWITCH_TO_REGISTER_TEXT => "OR REGISTER NOW";

        private string _username;
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    RaisePropertyChanged(() => UserName);
                }
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    RaisePropertyChanged(() => Password);
                }
            }
        }

        private string _confirmedPassword;
        public string ConfirmedPassword
        {
            get
            {
                return _confirmedPassword;
            }
            set
            {
                if (_confirmedPassword != value)
                {
                    _confirmedPassword = value;
                    RaisePropertyChanged(() => ConfirmedPassword);
                }
            }
        }

        private string _switchHintText;
        public string SwitchHintText
        {
            get
            {
                return _switchHintText;
            }
            set
            {
                if (_switchHintText != value)
                {
                    _switchHintText = value;
                    RaisePropertyChanged(() => SwitchHintText);
                }
            }
        }

        private string _nextBtnText;
        public string NextBtnText
        {
            get
            {
                return _nextBtnText;
            }
            set
            {
                if (_nextBtnText != value)
                {
                    _nextBtnText = value;
                    RaisePropertyChanged(() => NextBtnText);
                }
            }
        }

        private Visibility _showConfirmedPassword;
        public Visibility ShowConfirmedPassword
        {
            get
            {
                return _showConfirmedPassword;
            }
            set
            {
                if (_showConfirmedPassword != value)
                {
                    _showConfirmedPassword = value;
                    RaisePropertyChanged(() => ShowConfirmedPassword);
                }
            }
        }

        private RelayCommand _switchModeCommand;
        public RelayCommand SwitchModeCommand
        {
            get
            {
                if (_switchModeCommand != null) return _switchModeCommand;
                return _switchModeCommand = new RelayCommand(() =>
                  {
                      if (LoginMode == LoginMode.Login)
                      {
                          LoginMode = LoginMode.Register;
                      }
                      else
                      {
                          LoginMode = LoginMode.Login;
                      }
                  });
            }
        }

        private RelayCommand _nextCommand;
        public RelayCommand NextCommand
        {
            get
            {
                if (_nextCommand != null) return _nextCommand;
                return _nextCommand = new RelayCommand(() =>
                  {

                  });
            }
        }

        private LoginMode _loginMode;

        public LoginMode LoginMode
        {
            get { return _loginMode; }
            set
            {
                _loginMode = value;
                if (value == LoginMode.Login)
                {
                    ShowConfirmedPassword = Visibility.Collapsed;
                    NextBtnText = "LOGIN";
                    SwitchHintText = SWITCH_TO_REGISTER_TEXT;
                }
                else
                {
                    ShowConfirmedPassword = Visibility.Visible;
                    NextBtnText = "REGISTER";
                    SwitchHintText = SWITCH_TO_LOGIN_TEXT;
                }
            }
        }


        public StartViewModel()
        {
            LoginMode = LoginMode.Login;
            ShowConfirmedPassword = Visibility.Collapsed;
            SwitchHintText = SWITCH_TO_REGISTER_TEXT;
        }
    }
}
