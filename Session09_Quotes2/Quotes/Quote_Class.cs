using System;
using System.Collections.Generic;
using System.Text;

namespace Quotes
{
    public class Quote_Class
    {
        public bool Favourite { get; set; }

        private string _quote;
        public string Quote
        {
            get { return _quote; }
            set {  _quote = value; }
        }

        private string _author;
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }
    }
}
