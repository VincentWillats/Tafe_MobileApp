using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

/*
 * Converter_Controller class for length converter 
 * 
 * Vars:
 * typePicker01             -- What measurement type in picker01
 * typePicker02             -- What measurement type in picker02
 * _number01                -- What number in entryBox01
 * _number02                -- What number in entryBox02
 *
 * Functions: 
 * SetConvertTypes()        -- Set the typePicker vars then call Convert(whatbox);
 * SetNumber()              -- Set the _number vars then call Convert(whatbox);
 * Convert()                -- Converts the number and sets the correct _number var
 * ConvertToCM()            -- Converts number to centremeters
 * convertFromCM()          -- Converts number from centremeters
 * convertFromCM()          -- Converts number from centremeters+
 */

namespace Conversion_App
{
    class Converter_Controller
    {
        private string typePicker01;
        private string typePicker02;


        private decimal _number01 = 0;

        public decimal Number01
        {
            get { return _number01; }      
            set { _number01 = value; }
        }

        private decimal _number02 = 0;

        public decimal Number02
        {
            get { return _number02; }
            set { _number02 = value; }
        }       

        public Converter_Controller(string picker01selectedValue, string picker02selectedValue, decimal num01, decimal num02)
        {
            typePicker01 = picker01selectedValue;
            typePicker02 = picker02selectedValue;
            _number01 = num01;
            _number02 = num02;
        }
        
        public void SetConvertTypes(string whatPicker, string whatType)
        {
            if(whatPicker == "pickerBox01")
            {
                typePicker01 = whatType;
                Convert(2);
            }
            else if (whatPicker == "pickerBox02")
            {
                typePicker02 = whatType;
                Convert(1);
            }
            
        }

        public void SetNumber(string whatEntry, decimal number)
        {
            if(whatEntry == "entryBox01")
            {                
                _number01 = number;
                Convert(1);
            }
            else if(whatEntry == "entryBox02")
            {                
                _number02 = number;
                Convert(2);
            }
            
        }

        private void Convert(int whatEntry)
        {
            if(whatEntry == 1)
            {                
                decimal num01InCM = ConvertToCM(whatEntry, _number01);
                _number02 = ConvertFromCM(whatEntry, num01InCM);               
            }
            else if (whatEntry == 2)
            {
                decimal num02InCM = ConvertToCM(whatEntry, _number02);
                _number01 = ConvertFromCM(whatEntry, num02InCM);          
            }
        }


        private decimal ConvertToCM(int whatEntry, decimal numToConvert)
        {
            decimal result = 0;
            decimal numberToConvert = numToConvert;
            string currentUnitType;

            if(whatEntry == 1)
            {
                currentUnitType = typePicker01;
            }
            else
            {
                currentUnitType = typePicker02;
            }

            switch (currentUnitType)
            {
                case "Milimeters (mm)":                   
                    result = numberToConvert / (decimal)10;
                    break;
                case "Centimeters (cm)":
                    result = numberToConvert;
                    break;
                case "Meters (m)":
                    result = numberToConvert * (decimal)100; 
                    break;
                case "Kilometers (km)":
                    result = numberToConvert * (decimal)100000;
                    break;
                case "Feet (ft)":
                    result = numberToConvert * (decimal)30.48;
                    break;
                case "Inches (in)":
                    result = numberToConvert * (decimal)2.54;
                    break;
                case "Decimeter":
                    result = numberToConvert * (decimal)10;
                    break;
                case "Mile (m)":
                    result = numberToConvert * (decimal)160934;
                    break;
                case "Yard":
                    result = numberToConvert * (decimal)91.44;
                    break;
                case "Furlong":
                    result = numberToConvert * (decimal)20117;
                    break;
                case "Hand (horses)":
                    result = numberToConvert * (decimal)10.16;
                    break;
                case "Fathom":
                    result = numberToConvert * (decimal)183;
                    break;
            }
            return result;
        }

        private decimal ConvertFromCM(int whatEntry, decimal numToConvert)
        {
            decimal result = 0;
            decimal numberToConvert = numToConvert;
            string wantedUnitType;

            if (whatEntry == 1)
            {
                wantedUnitType = typePicker02;
            }
            else
            {
                wantedUnitType = typePicker01;
            }

            switch (wantedUnitType)
            {
                case "Milimeters (mm)":
                    result = numberToConvert * (decimal)10;
                    break;
                case "Centimeters (cm)":
                    result = numberToConvert;
                    break;
                case "Meters (m)":
                    result = numberToConvert / (decimal)100;
                    break;
                case "Kilometers (km)":
                    result = numberToConvert / (decimal)100000;
                    break;
                case "Feet (ft)":
                    result = numberToConvert / (decimal)30.48;
                    break;
                case "Inches (in)":
                    result = numberToConvert / (decimal)2.54;
                    break;
                case "Decimeter":
                    result = numberToConvert / (decimal)10;
                    break;
                case "Mile (m)":
                    result = numberToConvert / (decimal)160934;
                    break;
                case "Yard":
                    result = numberToConvert / (decimal)91.44;
                    break;
                case "Furlong":
                    result = numberToConvert / (decimal)20117;
                    break;
                case "Hand (horses)":
                    result = numberToConvert / (decimal)10.16;
                    break;
                case "Fathom":
                    result = numberToConvert / (decimal)183;
                    break;
            }
            return result;
        }
    }
}
