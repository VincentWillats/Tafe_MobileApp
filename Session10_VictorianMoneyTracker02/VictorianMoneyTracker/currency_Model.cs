using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

/**
 * 
 * Currency Data Model
 * Holds current currency and functions get/set and to convert between.
 * 
 * Data validation to stop currency going negative held in set functions
 *  
 **/



namespace VictorianMoneyTracker
{
    class currency_Model
    {
        private int _pounds;
        public int Pounds
        {
            get { return _pounds; }
            set 
            {
                _pounds = value;
                if (!(_pounds >= 0)) 
                { _pounds = 0; }
                Application.Current.Properties["pounds"] = _pounds;
            }
        }

        private int _crowns;
        public int Crowns
        {
            get { return _crowns; }
            set 
            { 
                _crowns = value;
                if (!(_crowns >= 0))
                { _crowns = 0; }
                Application.Current.Properties["crowns"] = _crowns;
            }
        }

        private int _shillings;
        public int Shillings
        {
            get { return _shillings; }
            set 
            { 
                _shillings = value;
                if (!(_shillings >= 0))
                { _shillings = 0; }
                Application.Current.Properties["shillings"] = _shillings;
            }
        }

        private int _pence;
        public int Pence
        {
            get { return _pence; }
            set 
            { 
                _pence = value;
                if (!(_pence >= 0))
                { _pence = 0; }
                Application.Current.Properties["pence"] = _pence;

            }
        }

        private int _farthings;
        public int Farthings
        {
            get { return _farthings; }
            set 
            { 
                _farthings = value;
                if (!(_farthings >= 0))
                { _farthings = 0; }
                Application.Current.Properties["farthings"] = _farthings;
            }
        }

        public void ConvertPoundDown()
        {
            if (_pounds >= 1)
            {
                _pounds--;
                _crowns = _crowns + 4;
            }        
            else
            {
                Console.WriteLine("Not enough pounds to convert down to crowns");                
            }
        }

        public void ConvertCrownUp()
        {
        
            if (_crowns >= 4)
            {
                _crowns = _crowns - 4;
                _pounds++;                
            }
            else
            {
                Console.WriteLine("Not enough crowns to convert up to pounds");
            }
        }

        public void ConvertCrownDown()
        {
            if (_crowns >= 1)
            {
                _crowns--;
                _shillings = _shillings + 5;                
            }
            else
            {
                Console.WriteLine("Not enough crowns to convert down to shillings");
            }
        }

        public void ConvertShillingUp()
        {
            if (_shillings >= 5)
            {
                _shillings = _shillings - 5;
                _crowns++;
            }
            else
            {
                Console.WriteLine("Not enough shillings to convert up to crowns");
            }
        }

        public void ConvertShillingDown()
        {
            if (_shillings >= 1)
            {
                _shillings--;
                _pence = _pence + 12;
            }
            else
            {
                Console.WriteLine("Not enough shillings to convert down to pence");
            }
        }

        public void ConvertPenceUp()
        {
            if (_pence >= 12)
            {
                _pence = _pence - 12;
                _shillings++;
            }
            else
            {
                Console.WriteLine("Not enough pence to convert up to shillings");
            }
        }

        public void ConvertPenceDown()
        {
            if (_pence >= 1)
            {
                _pence--;
                _farthings = _farthings + 4;
            }
            else
            {
                Console.WriteLine("Not enough pence to convert down to farthing");
            }
        }

        public void ConvertFarthingUp()
        {
            if (_farthings >= 4)
            {
                _farthings = _farthings - 4;
                _pence++;
            }
            else
            {
                Console.WriteLine("Not enough farthings to convert up to pences");
            }
        }
    }
}
