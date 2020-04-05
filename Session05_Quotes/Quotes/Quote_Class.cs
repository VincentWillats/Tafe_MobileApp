using System;
using System.Collections.Generic;
using System.Text;

namespace Quotes
{
    class Quote_Class
    {
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
