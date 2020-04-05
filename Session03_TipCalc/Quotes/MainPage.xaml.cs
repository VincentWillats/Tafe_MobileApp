using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

/**
 *  MainPage View Controller
 * 
 * Runs QuoteController.LoadQuotes() on start
 * 
 * Listens for button clicks and OnSleep message and runs relevant functions
 */

namespace Quotes
{

    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Quote_Controller QuoteController = new Quote_Controller();

   
        public MainPage()
        {
            InitializeComponent();
            QuoteController.LoadQuotes();

            // When "Sleeping" is sent, run SaveQuotes
            MessagingCenter.Subscribe<object>(this, "Sleeping", (sender) =>
            {
                QuoteController.SaveQuotes(); // Run save function
            });
        }              
               

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button buttonPressed = (Button)sender;

            string buttonName = buttonPressed.ClassId;

            switch (buttonName)
            {
                case "DisplayRandomQuoteButton":
                    Quote_Class _temp = QuoteController.RetrieveRandomQuote();
                    quoteText.Text = _temp.Quote;
                    quoteAuthorText.Text = _temp.Author;
                    break;
                case "AddQuoteButton":
                    if (!QuoteController.AddQuote(newQuoteText.Text, newQuoteAuthor.Text)) // If a text entry is blank
                    {
                        DisplayAlert("Error", "Entry cannot be blank", "OK");
                    }
                    else
                    {
                        newQuoteText.Text = "";
                        newQuoteAuthor.Text = "";
                    }
                    break;
            }
        }        
    }
}
