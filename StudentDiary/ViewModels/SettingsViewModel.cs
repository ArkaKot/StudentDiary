using StudentDiary.Commands;
using StudentDiary.Models;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace StudentDiary.ViewModels
{

    public class SettingsViewModel : ViewModelBase
    {
        private SettingConnectDataBase _settingConnectDataBase = new SettingConnectDataBase();

        public SettingsViewModel()
        {
            CloseCommand = new RelayCommand(Close);

            ConfirmCommand = new RelayCommand(confirm);

            _settingConnectDataBase = new SettingConnectDataBase();

        }

        public SettingConnectDataBase SettingConnectDataBase
        {
            get
            {
                return _settingConnectDataBase;
            }

            set
            {
                _settingConnectDataBase = value;
                OnPropertyChanged();
            }
        }

        private void confirm(object obj)
        {
            if (!SettingConnectDataBase.IsValid)
            {
                return;
            }

            SettingConnectDataBase.saveSettings();

            WindowClose((Window)obj);

            RestartApplication();
        }

        private void Close(object obj)
        {
            WindowClose(obj as Window);
        }

        private void WindowClose(Window window)
        {
            window.Close();
        }

        private void RestartApplication()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        public ICommand CloseCommand { get; set; }

        public ICommand ConfirmCommand { get; set; }
    }
}
