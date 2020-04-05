using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tip_calculator
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        
        bool hasStarted = false;
        bool hasDecimal = false;
        int numOfDecimalPlaces = 0;

        public MainPage()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {             
            UpdateUI();
        }

        private void UpdateUI()
        {
            double bill, tip, total;
            double _noOfDiners = _Stepper.Value;

            if (!double.TryParse(billAmountLabel.Text.Remove(0, 1), out bill))
            {
                return;
            } 
                        
            double tipPercent = (int)TipSlider.Value / (double)100;
            tip = bill * tipPercent;
            total = bill + tip;

            percentageLabel.Text = (tipPercent * 100).ToString() + "%";
            TipAmountLabel.Text = tip.ToString("c");
            TotalAmountLabel.Text = total.ToString("c");
            costPerDiners.Text = (total / _noOfDiners).ToString("c");
        }

        private void Calculator_Button_Clicked(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;        
            string value = clickedButton.Text;  
                       

            // Reset if C pressed
            if(value == "C")
            {
                billAmountLabel.Text = "$0";
                hasDecimal = false;
                hasStarted = false;
                numOfDecimalPlaces = 0;
                TipAmountLabel.Text = "$0.00";
                TotalAmountLabel.Text = "$0.00";
                _Stepper.Value = 1;
                TipSlider.Value = 0;
            }
            // if not started get bool to started
            else if (!hasStarted)
            {
                hasStarted = true;
                // if not started and first value 0 do nothing
                if (value == "0")
                {
                    hasStarted = false;
                    return;
                }
                if (value == ".")
                {
                    hasDecimal = true;
                    billAmountLabel.Text = "$0.";
                }
                else
                {
                    billAmountLabel.Text = "$"; // remove the 0 before adding the rest
                }
            }
               
            // if hasDecimal false and . pressed then add . and change hasDecimal bool to true
            if(value == "." && !hasDecimal)
            {      
                hasDecimal = true;
                billAmountLabel.Text = billAmountLabel.Text + ".";
           
            }

            // if num of decimal places is less than 2 and value NOT . or C 
            else if (numOfDecimalPlaces <2 && !(value == ".") && !(value == "C"))
            {            
                // if decimal increase num of decimal places and add number
                if (hasDecimal)
                {
                    numOfDecimalPlaces += 1;                    
                }                
                billAmountLabel.Text = billAmountLabel.Text + value;                
            }

            UpdateUI();
        }

        private void _Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateUI();
        }
    }
}
