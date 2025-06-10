using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using StudentDiary.Commands;
using StudentDiary.Models.Domains;
using StudentDiary.Models.Wrappers;
using StudentDiary.Properties;
using StudentDiary.Views;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentDiary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {


        private Repository _repository = new Repository();


        private static string _connectingData = $@"
            server = {Settings.Default.serverAdress}\{Settings.Default.serverName};
            Database = {Settings.Default.nameDataBase};
            User Id = {Settings.Default.userDataBase};
            Password = {Settings.Default.passwordDataBase};";

        public MainViewModel()
        {
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudensCommand = new RelayCommand(RefreshStudents);
            SettingsCommands = new RelayCommand(EditConnection);

            VerifyConnectionCommand = new AsyncRelayCommand(VerifyConnection);

            VerifyConnection(null);

          

        }

        public async Task VerifyConnection(object obj)
        {
            if (!VerifyConnectionToSQL())
            {
                var metroWindow = Application.Current.MainWindow as MetroWindow;
                var dialog = await metroWindow.ShowMessageAsync("Błąd połączenia", "Nie udało się połączyć z bazą danych, czy chcesz zmienić ustawienia?", MessageDialogStyle.AffirmativeAndNegative);

                if (dialog == MessageDialogResult.Affirmative)
                    EditConnection(null);
                else
                    Application.Current.Shutdown();
            }

            RefreshDiary();

            InitGroups();
        }

        private StudentWrapper _selectedStudent;

        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();

            }
        }

        private ObservableCollection<StudentWrapper> _students;

        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _group;

        public ObservableCollection<Group> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudensCommand { get; set; }
        public ICommand SettingsCommands { get; set; }
        public ICommand VerifyConnectionCommand { get; }


        public static bool VerifyConnectionToSQL()
        {
            try
            {
                using (var connectToSQL = new SqlConnection(_connectingData))
                {
                    connectToSQL.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();

            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = 0;
        }

        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }

        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Usuwanie ucznia",
                $"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName} " +
                $"{SelectedStudent.LastName} ?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.DeleteStudent(SelectedStudent.Id);

            RefreshDiary();
        }

        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void AddEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>(_repository.GetStudents(SelectedGroupId));
        }

        private void EditConnection(object obj)
        {
            var settingsView = new SettingsView();
            settingsView.Closed += SettingsView_Closed;
            settingsView.ShowDialog();
        }

        private void SettingsView_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }
    }
}