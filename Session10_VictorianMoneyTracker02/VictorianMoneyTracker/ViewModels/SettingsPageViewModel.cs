using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace VictorianMoneyTracker.ViewModels
{
    class SettingsPageViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isSoundOn = false;
        private bool _isDarkMode = false;

        private Color _BackgroundColour;
        private Color _TextColour;
        private Color _BorderColour;   

        public Command CloseSettingsCommand { get; }


        public SettingsPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
            CloseSettingsCommand = new Command(CloseSettings);
            LoadProperties();            
        }

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
               
        public bool IsSoundOn
        {
            get { return _isSoundOn; }
            set 
            {
                _isSoundOn = value;
                Application.Current.Properties["soundOn"] = _isSoundOn;
                SoundEffects.PlayClickSound();
            }
        }       
        public bool IsDarkMode
        {
            get { return _isDarkMode; }
            set
            {
                _isDarkMode = value;
                Application.Current.Properties["darkMode"] = _isDarkMode;
                SoundEffects.PlayClickSound();
                SetDarkMode(_isDarkMode);
            }
        }
        public Color BackgroundColour
        {
            get { return _BackgroundColour; }
            set
            {
                _BackgroundColour = value;
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


        private void LoadProperties()
        {
            if (Application.Current.Properties.ContainsKey("darkMode"))
            {
                IsDarkMode = (bool)Application.Current.Properties["darkMode"];
            }
            if (Application.Current.Properties.ContainsKey("soundOn"))
            {
                IsSoundOn = (bool)Application.Current.Properties["soundOn"];
            }
        }

        private void SetDarkMode(bool darkMode)
        {
            BackgroundColour = darkMode ? Color.FromHex("#585858") : Color.White;
            TextColour = darkMode ? Color.White : Color.Black;
            BorderColour = darkMode ? Color.Gray : Color.Black;          
        }

        private void CloseSettings()
        {
            SoundEffects.PlayClickSound();
            _navigation.PopModalAsync();
        }                
    }
}
