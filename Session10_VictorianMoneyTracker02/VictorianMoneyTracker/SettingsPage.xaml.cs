using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VictorianMoneyTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage, INotifyPropertyChanged
    {

        bool isSoundOn = true;
        bool isDarkMode = true;

        public Xamarin.Forms.Color BackgroundColour { get; set; }
        public Xamarin.Forms.Color TextColour { get; set; }
        public Xamarin.Forms.Color BorderColour { get; set; }

        public string isSoundOnText { get; set; }
        public string isDarkModeOnText { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        public SettingsPage()
        {
            InitializeComponent();
            LoadProperties();
            BindingContext = this;
        }

        private void LoadProperties()
        {
            if (Application.Current.Properties.ContainsKey("darkMode"))
            {
                SetDarkMode((bool)Application.Current.Properties["darkMode"]);

                isDarkMode = (bool)Application.Current.Properties["darkMode"];
                if (isDarkMode)
                {
                    isDarkModeOnText = "Toggle Off";
                }
                else
                {
                    isDarkModeOnText = "Toggle On";
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isDarkModeOnText"));
            }
            else
            {
                Application.Current.Properties["darkMode"] = false;
                SetDarkMode((bool)Application.Current.Properties["darkMode"]);

                isDarkMode = (bool)Application.Current.Properties["darkMode"];
                if (isDarkMode)
                {
                    isDarkModeOnText = "Toggle Off";
                }
                else
                {
                    isDarkModeOnText = "Toggle On";
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isDarkModeOnText"));
            }
            if (Application.Current.Properties.ContainsKey("soundOn"))
            {
                isSoundOn = (bool)Application.Current.Properties["soundOn"];
                if (isSoundOn)
                {
                    isSoundOnText = "Toggle Off";
                }
                else
                {
                    isSoundOnText = "Toggle On";
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isSoundOnText"));
            }
            else
            {
                Application.Current.Properties["soundOn"] = true;
                isSoundOn = (bool)Application.Current.Properties["soundOn"];
                if (isSoundOn)
                {
                    isSoundOnText = "Toggle Off";
                }
                else
                {
                    isSoundOnText = "Toggle On";
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isSoundOnText"));
            }
        }

        private void SetDarkMode(bool darkMode)
        {
            BackgroundColour = darkMode ? Color.FromHex("#585858") : Color.White;
            TextColour = darkMode ? Color.White : Color.Black;
            BorderColour = darkMode ? Color.Gray : Color.Black;

            isDarkMode = darkMode;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BackgroundColour"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TextColour"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BorderColour"));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string btnStr = btn.ClassId;
            if((bool)Application.Current.Properties["soundOn"]) SoundEffects.PlayClickSound();

            switch (btnStr)
            {
                case "Close":
                    Navigation.PopModalAsync();
                    break;
                case "Sound":
                    Application.Current.Properties["soundOn"] = !((bool)Application.Current.Properties["soundOn"]);
                    LoadProperties();
                    break;
                case "Darkmode":
                    Application.Current.Properties["darkMode"] = !((bool)Application.Current.Properties["darkMode"]);
                    LoadProperties();
                    break;
            }
        }
    }
}