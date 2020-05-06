using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
    public class currency_Model : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public void ConvertPoundDown()
        {
            if (_pounds >= 1)
            {
                Pounds--;
                Crowns = Crowns + 4;
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
                Crowns = Crowns - 4;
                Pounds++;                
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
                Crowns--;
                Shillings = Shillings + 5;                
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
                Shillings = Shillings - 5;
                Crowns++;
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
                Shillings--;
                Pence = Pence + 12;
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
                Pence = Pence - 12;
                Shillings++;
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
                Pence--;
                Farthings = Farthings + 4;
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
                Farthings = Farthings - 4;
                Pence++;
            }
            else
            {
                Console.WriteLine("Not enough farthings to convert up to pences");
            }
        }
    }
}
