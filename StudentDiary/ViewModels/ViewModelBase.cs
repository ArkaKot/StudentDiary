using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentDiary.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) //Widoczna tylko w tej klasie i pochodnych, virtual -  może być nadpisana
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // jeżeli event nie jest null to zostanie wyzwolony i przekaże nazwę właściwości
        }
    }
}
