using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GamesHub.Models
{ 
    public abstract  class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingField, value))
                return;

            backingField = value;
            RaisePropertyChanged(propertyName);
        }
    }
}
