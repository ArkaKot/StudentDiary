using StudentDiary.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDiary.Models
{
    public class SettingConnectDataBase : IDataErrorInfo
    {

        private bool _isserverAddressValid;

        private bool _isserverNameValid;

        private bool _isdatabaseValid;

        private bool _isuserValid;

        private bool _ispasswordValid;

        public string serverAddress
        {
            get
            {
                return Settings.Default.serverAdress;
            }
            set
            {
                Settings.Default.serverAdress = value;
            }
        }

        public string serverName
        {
            get
            {
                return Settings.Default.serverName;
            }
            set
            {
                Settings.Default.serverName = value;
            }
        }

        public string baseName
        {

            get
            {
                return Settings.Default.nameDataBase;
            }
            set
            {
                Settings.Default.nameDataBase = value;
            }
        }

        public string user
        {
            get
            {
                return Settings.Default.userDataBase;
            }
            set
            {
                Settings.Default.userDataBase = value;
            }
        }

        public string password
        {
            get
            {
                return Settings.Default.passwordDataBase;
            }
            set
            {
                Settings.Default.passwordDataBase = value;
            }
        }



        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(serverAddress):
                        if (string.IsNullOrWhiteSpace(serverAddress))
                        {
                            Error = "Pole Adres Serwera nie może być puste";
                            _isserverAddressValid = false;

                        }
                        else
                        {
                            Error = string.Empty;
                            _isserverAddressValid = true;
                        }
                        break;

                    case nameof(serverName):
                        if (string.IsNullOrWhiteSpace(serverName))
                        {
                            Error = "Pole Nazwa Serwera nie może być puste";
                            _isserverNameValid = false;

                        }
                        else
                        {
                            Error = string.Empty;
                            _isserverNameValid = true;
                        }
                        break;

                    case nameof(baseName):
                        if (string.IsNullOrWhiteSpace(baseName))
                        {
                            Error = "Pole Nazwa bazy danych nie może być puste";
                            _isdatabaseValid = false;

                        }
                        else
                        {
                            Error = string.Empty;
                            _isdatabaseValid = true;
                        }
                        break;

                    case nameof(user):
                        if (string.IsNullOrWhiteSpace(user))
                        {
                            Error = "Pole nazwa użytkownika nie może być puste";
                            _isuserValid = false;

                        }
                        else
                        {
                            Error = string.Empty;
                            _isuserValid = true;
                        }
                        break;

                    case nameof(password):
                        if (string.IsNullOrWhiteSpace(password))
                        {
                            Error = "Pole hasło nie może być puste";
                            _ispasswordValid = false;

                        }
                        else
                        {
                            Error = string.Empty;
                            _ispasswordValid = true;
                        }
                        break;

                }

                return Error;
            }
        }

        public string Error { get; set; }

        public bool IsValid
        {
            get
            {
                return _isserverAddressValid && _isserverNameValid &&
                    _isdatabaseValid &&
                    _isuserValid &&
                    _ispasswordValid;
            }

        }

        public void saveSettings()
        {
            Settings.Default.Save();
        }

    }
}
