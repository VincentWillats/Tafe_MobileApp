using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

/**
 * Victorian Money Tracker View Controller
 * 
 * Has two event handles, one for Convert clicks and one for adding/subtracting currency
 * 
 * Has an UpdateUI function which is called in all event handlers and in mainpage constructor
 * UpdateUI updates all labels/text and visability of buttons
 * 
 * 04/05/2020 - Added click sound
 **/

namespace VictorianMoneyTracker
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        currency_Model Currency = new currency_Model();

        bool isSoundOn = true;

        public Xamarin.Forms.Color BackgroundColour { get; set; }
        public Xamarin.Forms.Color TextColour { get; set; }
        public Xamarin.Forms.Color BorderColour { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        public MainPage()
        {
            InitializeComponent();
            
            BindingContext = this;
        }

        override protected void OnAppearing()
        {
            LoadProperties();
            UpdateUI();
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

        private void Currency_Convert_Pressed(object sender, EventArgs e)
        {
            ImageButton pressedButton = (ImageButton)sender;
            string pressedButtonName = pressedButton.ClassId;
            if (isSoundOn) SoundEffects.PlayClickSound();

            switch (pressedButtonName)
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

            UpdateUI();
        }

        private void Currency_Adjust_Pressed(object sender, EventArgs e)
        {
            Button pressedButton = (Button)sender;
            string pressedButtonName = pressedButton.ClassId;
            if (isSoundOn) SoundEffects.PlayClickSound();

            switch (pressedButtonName)
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
            UpdateUI();
        }
        
        private void SetDarkMode(bool darkMode)
        {
            BackgroundColour = darkMode ? Color.FromHex("#585858") : Color.White; 
            TextColour = darkMode ? Color.White : Color.Black; 
            BorderColour = darkMode ? Color.Gray : Color.Black;
            System.Diagnostics.Debug.WriteLine(darkMode.ToString());        

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BackgroundColour"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TextColour"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BorderColour"));            
        }

        private void UpdateUI()
        {        
            totalPoundText.Text = "£" + Currency.Pounds.ToString();
            totalCrownText.Text = Currency.Crowns.ToString() + "c";
            totalShillingText.Text = Currency.Shillings.ToString() + "s";
            totalPenceText.Text = Currency.Pence.ToString() + "d";
            totalFarthingText.Text = Currency.Farthings.ToString() + "f";

            poundText.Text = Currency.Pounds.ToString();
            crownText.Text = Currency.Crowns.ToString();
            shillingText.Text = Currency.Shillings.ToString();
            penceText.Text = Currency.Pence.ToString();
            farthingText.Text = Currency.Farthings.ToString();
            
            _ = Currency.Pounds == 1 ? poundLabel.Text = "pound" : poundLabel.Text = "pounds"; // Changes singlar/plural 
            _ = Currency.Pounds == 0 ? convertDownPound.IsVisible = false : convertDownPound.IsVisible = true; // Sets button visability
                         
            _ = Currency.Crowns == 1 ? crownLabel.Text = "crown" : crownLabel.Text = "crowns"; // Changes singlar/plural 
            _ = Currency.Crowns == 0 ? convertDownCrown.IsVisible = false : convertDownCrown.IsVisible = true; // Sets button visability
            _ = Currency.Crowns < 4 ? convertUpCrown.IsVisible = false : convertUpCrown.IsVisible = true; // Sets button visability

            _ = Currency.Shillings == 1 ? shillingLabel.Text = "shilling" : shillingLabel.Text = "shillings"; // Changes singlar/plural 
            _ = Currency.Shillings == 0 ? convertDownShilling.IsVisible = false : convertDownShilling.IsVisible = true; // Sets button visability
            _ = Currency.Shillings < 5 ? convertUpShilling.IsVisible = false : convertUpShilling.IsVisible = true;  // Sets button visability

            _ = Currency.Pence == 1 ? penceLabel.Text = "penny" : penceLabel.Text = "pence"; // Changes singlar/plural 
            _ = Currency.Pence == 0 ? convertDownPence.IsVisible = false : convertDownPence.IsVisible = true;   // Sets button visability
            _ = Currency.Pence < 12 ? convertUpPence.IsVisible = false : convertUpPence.IsVisible = true;   // Sets button visability

            _ = Currency.Farthings == 1 ? farthingLabel.Text = "farthing" : farthingLabel.Text = "farthings";  // Changes singlar/plural           
            _ = Currency.Farthings < 4 ? convertUpFarthing.IsVisible = false : convertUpFarthing.IsVisible = true;   // Sets button visability        
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(isSoundOn) SoundEffects.PlayClickSound();
            Navigation.PushModalAsync(new SettingsPage());     

            //Application.Current.Properties["darkMode"] = !((bool)Application.Current.Properties["darkMode"]);
        }
    }
}


