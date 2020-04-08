using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quotes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Favourites : ContentPage
    {
        Quote_Controller QuoteController = new Quote_Controller();

        public ObservableCollection<Quote_Class> Quotes { get; private set; }

        public Favourites(Quote_Controller qc)
        {
            QuoteController = qc;
            Quotes = new ObservableCollection<Quote_Class>();
            FillQuotes();
            InitializeComponent();
        }

        private void FillQuotes()
        {
            List<Quote_Class> _quotes = QuoteController.GetTenFavQuotes();
            foreach(Quote_Class quote in _quotes)
            {
                Quotes.Add(quote);
            }
            BindingContext = this;

        }

        private async void BackButton_Pressed(object sender, EventArgs e)
        {
            // await Navigation.PopAsync();
            await Navigation.PopModalAsync();
        }
    }
}