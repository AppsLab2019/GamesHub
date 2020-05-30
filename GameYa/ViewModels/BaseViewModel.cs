using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace GameYa.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public static ResourceDictionary Resources =>
            Application.Current.Resources;

        #region INotifyPropertyChanged
        
        protected void RaiseAllPropertiesChanged() =>
            OnPropertyChanged(null);

        public event PropertyChangedEventHandler PropertyChanged; 
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region MaterialDialogConfiguration

        public MaterialAlertDialogConfiguration PlayerRedWinDialog = new MaterialAlertDialogConfiguration
        {
            BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
            TitleTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY),
            TitleFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OxygenBold"),
            MessageTextColor = Color.FromHex("#FF5252"),
            MessageFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OxygenBold"),
            TintColor = Color.White,
            ButtonFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OxygenBold"),
            CornerRadius = 8,
            ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
            ButtonAllCaps = false
        };

        public MaterialAlertDialogConfiguration PlayerBlueWinDialog = new MaterialAlertDialogConfiguration
        {
            BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
            TitleTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY),
            TitleFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OxygenBold"),
            MessageTextColor = Color.FromHex("#448AFF"),
            MessageFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OxygenBold"),
            TintColor = Color.White,
            ButtonFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OxygenBold"),
            CornerRadius = 8,
            ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
            ButtonAllCaps = false
        };

        public MaterialAlertDialogConfiguration BasicDialog = new MaterialAlertDialogConfiguration
        {
            BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
            TitleTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY),
            TitleFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OxygenBold"),
            MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_SURFACE),
            MessageFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OxygenBold"),
            TintColor = Color.White,
            ButtonFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.OxygenBold"),
            CornerRadius = 8,
            ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32),
            ButtonAllCaps = false
        };
        #endregion
    }
}
