using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;


/* 
 * View Controller for length converter 
 * 
 * Vars:
 * entry01Default           -- Default values for textbox01
 * entry02Default           -- Default values for textbox02
 * formAppeared             -- Bool if form has loaded 
 * 
 * Functions: 
 * OnAppearing();           -- When app has finished loaded an appeared sets formAppeared bool to true and instantiate the Converter_Controller converter class
 * PickerChanged_Event();   -- When measurement type is changed it will pass through the new measurement type to the Converter_Controller, which then will update the coverted amounts, and then call UpdateUI();
 * TextChanged_Event();     -- When the value is changed in a text box it will validate it as a valid decimal, then pass the number to Converter_Controller, which then will update the coverted amounts, and then call UpdateUI();
 * UpdateUI();              -- When called it will update the UI with the values from Converter_Controller.Number01/Number02
 */

namespace Conversion_App
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        decimal entry01Default = 0; // Setting defaults here giving possiblity to impliment saving state.
        decimal entry02Default = 0; // Setting defaults here giving possiblity to impliment saving state.
        bool formAppeared = false;
        bool convertFinished = true;
        

        Converter_Controller converter;
        public MainPage()
        {
            InitializeComponent();            
        }


        override protected void OnAppearing()   // Overrides Xaml class OnAppearing, called when form has loaded an appears
        {
            converter = new Converter_Controller(pickerBox01.SelectedItem.ToString(), pickerBox02.SelectedItem.ToString(), entry01Default, entry02Default); // Instantiate our controller class
            entryBox01.Text = entry01Default.ToString();    // Makes sure our textboxes match our Instantiated controller class
            entryBox02.Text = entry02Default.ToString();    // Makes sure our textboxes match our Instantiated controller class       
            formAppeared = true;                            // Change bool to show app is loaded
        }

        private void PickerChanged_Event(object sender, EventArgs e)
        {
            if (!formAppeared) { return; } // Breaks out of event untill OnAppearing has finished
            Picker picker = (Picker)sender;
            converter.SetConvertTypes(picker.ClassId, picker.SelectedItem.ToString());  // Sends the picker and the selected item to Coverter_Controller.SeteConvertTypes(classID, selectedItem)
            UpdateUI();
        }

        private void TextChanged_Event(object sender, TextChangedEventArgs e)
        {
            if (!formAppeared) { return; }  // Breaks out of event untill OnAppearing has finished
            Entry entry = (Entry)sender;           
            int decimalCount = entry.Text.Split('.').Length - 1;    // Count of '.'s
            decimal number;

            if (String.IsNullOrEmpty(entry.Text))   // If text is cleared set to 0
            {
                entry.Text = "0";
            }
            if (entry.Text.Length > 1 && entry.Text[0].ToString() == "0" && decimalCount < 1)   // When adding a number if first number is 0, remove it unless contains a '.'
            {             
                entry.Text = entry.Text.Remove(0, 1);                              
            }
            if (decimalCount > 1)   // If trying to add more than 1 decimal don't allow
            {
                entry.Text = entry.Text.Remove(entry.Text.Length - 1);
            }
            if (!String.IsNullOrEmpty(entry.Text) && entry.Text[entry.Text.Length - 1].ToString() != ".")   // Don't try to parse if empty or last entered char was '.'
            {
                if (!decimal.TryParse(entry.Text, out number))  // Try parsing input into decimal
                {
                    entry.Text = entry.Text.Remove(entry.Text.Length - 1);  // Remove last entered input
                }
                else
                {
                    converter.SetNumber(entry.ClassId, number);     // Number a valid decimal, pass it to our Converter_Controller.SetNumber(whatbox, number)
                    UpdateUI(); 
                }
            }

        }

        private void UpdateUI() // When called disable the text changed event handlers, update text and then reenable event handlers.
        {
            entryBox01.TextChanged -= TextChanged_Event;
            entryBox01.Text = Math.Round(converter.Number01, 6).ToString();
            entryBox01.TextChanged += TextChanged_Event;

            entryBox02.TextChanged -= TextChanged_Event;
            entryBox02.Text = Math.Round(converter.Number02, 6).ToString();
            entryBox02.TextChanged += TextChanged_Event;
        }
    }
}
