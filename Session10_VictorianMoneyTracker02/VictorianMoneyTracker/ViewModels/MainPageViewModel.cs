using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace VictorianMoneyTracker.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;
        bool isSoundOn = true;

        private Color _BackGroundColor;
        private Color _TextColour;
        private Color _BorderColour;

        public Command PageAppearingCommand { get; }
        public Command ConvertCurrencyCommand { get; }
        public Command AdjustCurrencyCommand { get; }
        public Command OpenSettingsCommand { get; }

        public MainPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
            Currency = new currency_Model();
            AdjustCurrencyCommand = new Command<string>(AdjustCurrency);
            ConvertCurrencyCommand = new Command<string>(ConvertCurrency);
            PageAppearingCommand = new Command(PageAppearing);
            OpenSettingsCommand = new Command(OpenSettings);
            LoadProperties();
        }       

        public currency_Model Currency { get; set; }
        
        public Color BackgroundColour //{ get; set; }
        {
            get { return _BackGroundColor;  }
            set 
            {   
                _BackGroundColor = value;
                OnPropertyChanged();      
            }
        }        
        public Color TextColour 
        {
            get { return _TextColour; }
            set 
            {
                _TextColour = value;
                OnPropertyChanged();
            }
        }        
        public Color BorderColour
        {
            get { return _BorderColour; }
            set 
            {
                _BorderColour = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void LoadProperties()
        {
            if (Application.Current.Properties.ContainsKey("darkMode"))
            {
                SetDarkMode((bool)Application.Current.Properties["darkMode"]);
            }
            if (Application.Current.Properties.ContainsKey("soundOn"))
            {
                isSoundOn = (bool)Application.Current.Properties["soundOn"];
            }

            if (Application.Current.Properties.ContainsKey("pounds"))
            {
                Currency.Pounds = int.Parse(Application.Current.Properties["pounds"].ToString());
            }
            if (Application.Current.Properties.ContainsKey("crowns"))
            {
                Currency.Crowns = int.Parse(Application.Current.Properties["crowns"].ToString());
            }
            if (Application.Current.Properties.ContainsKey("shillings"))
            {
                Currency.Shillings = int.Parse(Application.Current.Properties["shillings"].ToString());
            }
            if (Application.Current.Properties.ContainsKey("pence"))
            {
                Currency.Pence = int.Parse(Application.Current.Properties["pence"].ToString());
            }
            if (Application.Current.Properties.ContainsKey("farthings"))
            {
                Currency.Farthings = int.Parse(Application.Current.Properties["farthings"].ToString());
            }
        }       

        private void PageAppearing()
        {
            LoadProperties();
        }
        
        private void ConvertCurrency(string whatButton)
        {
            if (isSoundOn) SoundEffects.PlayClickSound();        

            switch (whatButton)
            {
                // Pound
                case "convertDownPoundID":
                    Currency.ConvertPoundDown();
                    break;

                // Crown
                case "convertDownCrownID":
                    Currency.ConvertCrownDown();
                    break;
                case "convertUpCrownID":
                    Currency.ConvertCrownUp();
                    break;

                // Shilling
                case "convertDownShillingID":
                    Currency.ConvertShillingDown();
                    break;
                case "convertUpShillingID":
                    Currency.ConvertShillingUp();
                    break;

                // Pence
                case "convertDownPenceID":
                    Currency.ConvertPenceDown();
                    break;
                case "convertUpPenceID":
                    Currency.ConvertPenceUp();
                    break;

                // Farthing       
                case "convertUpFarthingID":
                    Currency.ConvertFarthingUp();
                    break;
            }    
        }        

        public void AdjustCurrency(string whatButton)
        {
            if (isSoundOn) SoundEffects.PlayClickSound();                                
            switch (whatButton)
            {
                // Pound
                case "lessPoundID":
                    Currency.Pounds--;
                    break;
                case "morePoundID":
                    Currency.Pounds++;
                    break;

                // Crown
                case "lessCrownID":
                    Currency.Crowns--;
                    break;
                case "moreCrownID":
                    Currency.Crowns++;
                    break;

                // Shilling
                case "lessShillingID":
                    Currency.Shillings--;
                    break;
                case "moreShillingID":
                    Currency.Shillings++;
                    break;

                // Pence
                case "lessPenceID":
                    Currency.Pence--;
                    break;
                case "morePenceID":
                    Currency.Pence++;
                    break;

                // Farthing
                case "lessFarthingID":
                    Currency.Farthings--;
                    break;
                case "moreFarthingID":
                    Currency.Farthings++;
                    break;
            }
        }

        private void SetDarkMode(bool darkMode)
        {
            BackgroundColour = darkMode ? Color.FromHex("#585858") : Color.White;
            TextColour = darkMode ? Color.White : Color.Black;
            BorderColour = darkMode ? Color.Gray : Color.Black;
        }
        
        private void OpenSettings()
        {
            if (isSoundOn) SoundEffects.PlayClickSound();
            _navigation.PushModalAsync(new SettingsPage());
        }
    }
}
