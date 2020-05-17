using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GamesHub.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        
        protected void RaiseAllPropertiesChanged() =>
            OnPropertyChanged(null);

        public event PropertyChangedEventHandler PropertyChanged; 
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
