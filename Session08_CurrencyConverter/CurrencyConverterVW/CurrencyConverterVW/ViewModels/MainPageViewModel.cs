using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CurrencyConverterVW.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        HttpClient client = new HttpClient();

        public event PropertyChangedEventHandler PropertyChanged;

        public Command ButtonClickedCommand { get; }

        public ObservableCollection<Currency> CurrencyList { get; private set; }

        private string _SelectedCurrencyIndexID;
        private string _InputAmount;
        private string _OutputAmount;
        private string _currencyID;

        public string OutputAmount
        {
            get { return _OutputAmount; }
            set 
            { 
                _OutputAmount = value;
                OnPropertyChanged();
            }
        }
        public string SelectedCurrencyIndexID
        {
            get 
            { return _SelectedCurrencyIndexID; } 
            set 
            {
                _SelectedCurrencyIndexID = value;
                _currencyID = CurrencyList[int.Parse(_SelectedCurrencyIndexID)].id;
                Convert();
            }
        }
        public string InputAmount
        {
            get { return _InputAmount; }
            set
            {
                decimal _temp;
                string _tempStr = InputAmount + value;

                if (value == "c") { _InputAmount = ""; }
                else if (_tempStr.Contains(".") && value != ".")
                {
                    int length = _tempStr.Substring(_tempStr.IndexOf(".")).Length;
                    if (length > 3) { return; }
                }
                if (value == ".")
                {
                    if (InputAmount.Contains(".")) { return; }
                    else { _InputAmount = _tempStr; }
                }
                else if (Decimal.TryParse(_tempStr, NumberStyles.Currency, CultureInfo.CurrentCulture, out _temp))
                {
                    _InputAmount = _temp.ToString("#.##");
                }
                OnPropertyChanged();
            }
        }

        public MainPageViewModel()
        {
            CurrencyList = new ObservableCollection<Currency>();
            ButtonClickedCommand = new Command<Button>(ButtonClicked);
            LoadCurrenciesFromAPI();
        }

        private async void Convert()
        {
            if(string.IsNullOrEmpty(_currencyID)) { return; }
            if(string.IsNullOrEmpty(InputAmount)) { return; }

            string currencyID = _currencyID;
            decimal amount = decimal.Parse(InputAmount);
            decimal exchangeRate = await GetExchangeRateAsync(currencyID);
            if(exchangeRate == -1) { return; }
            decimal convertedAmount = amount * exchangeRate;

            OutputAmount = convertedAmount.ToString("#.##");
        }

        private async Task<decimal> GetExchangeRateAsync(string currID)
        {
            decimal exRate;

            StringBuilder sb = new StringBuilder();
            sb.Append("https://free.currconv.com/api/v7/convert?q=AUD_");
            sb.Append(currID);
            sb.Append("&compact=ultra&apiKey=0ab72b2279d01f6e5a76");

            try
            {
                HttpResponseMessage response = await client.GetAsync(sb.ToString());
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                IList<JToken> obj = JObject.Parse(responseBody);
                string xr = ((JProperty)obj[0]).Value.ToString();
                decimal.TryParse(xr, out exRate);
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.Write(ea.Message);
                exRate = -1;
            }            
            return exRate;
        }

        private async void LoadCurrenciesFromAPI()
        {
            List<Currency> cList = new List<Currency>();
            cList = await LoadCurrencyListResultsAsync();
            foreach(Currency c in cList)
            {
                CurrencyList.Add(c);
            }            
        }

        public async Task<List<Currency>> LoadCurrencyListResultsAsync()
        {
            string urlAPI = "https://free.currconv.com/api/v7/currencies?apiKey=0ab72b2279d01f6e5a76";                        
            List<Currency> cList = new List<Currency>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(urlAPI);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(responseBody);
                var children = jo.SelectToken("results").Children();
                foreach (var child in children)
                {
                    var childrenOfChild = child.Children();
                    foreach (var c in childrenOfChild)
                    {
                        cList.Add(JsonConvert.DeserializeObject<Currency>(JsonConvert.SerializeObject(c)));
                    }
                }
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.Write(ea.Message);
                cList.Add(new Currency { currencyName = "Error Loading Currencies." });
            }            
            return cList;
        }    

        private void ButtonClicked(Button btn)
        {
            string btnName= btn.ClassId.ToString();

            switch (btnName)
            {
                case "btn_7":
                    InputAmount = "7";
                    break;
                case "btn_8":
                    InputAmount = "9";
                    break;
                case "btn_9":
                    InputAmount = "0";
                    break;
                case "btn_4":
                    InputAmount = "4";
                    break;
                case "btn_5":
                    InputAmount = "5";
                    break;
                case "btn_6":
                    InputAmount = "6";
                    break;
                case "btn_1":
                    InputAmount = "1";
                    break;
                case "btn_2":
                    InputAmount = "2";
                    break;
                case "btn_3":
                    InputAmount = "3";
                    break;
                case "btn_0":
                    InputAmount = "0";
                    break;
                case "btn_dot":
                    InputAmount = ".";
                    break;
                case "btn_c":
                    InputAmount = "c";
                    break;
            }
            Convert();
        }

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
