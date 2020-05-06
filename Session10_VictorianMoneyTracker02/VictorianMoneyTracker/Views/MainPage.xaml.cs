using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VictorianMoneyTracker.ViewModels;
using Xamarin.Forms;

/**
 * Victorian Money Tracker View 
 * 
 * 06/05/2020 - Changed to View Model
 * 04/05/2020 - Added click sound
 **/

namespace VictorianMoneyTracker
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {       
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(this.Navigation);
        }    
    }
}


