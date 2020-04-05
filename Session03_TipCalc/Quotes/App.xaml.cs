using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quotes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            MessagingCenter.Send<object>(this, "Sleeping"); // Send message "Sleeping" too mainpage listener
        }

        protected override void OnResume()
        {
        }

    }
}
