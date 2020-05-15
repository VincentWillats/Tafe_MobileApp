using System;
using System.ComponentModel;
using VictorianMoneyTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VictorianMoneyTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage, INotifyPropertyChanged
    {
        public SettingsPage()
        {
            InitializeComponent();            
            BindingContext = new SettingsPageViewModel(this.Navigation);
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {

        }
    }
}