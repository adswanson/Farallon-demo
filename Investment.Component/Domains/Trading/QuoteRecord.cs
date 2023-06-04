using System;
using System.Collections.Generic;
using System.Text;

namespace Investment.Component.Domains.Trading
{
    public sealed class QuoteRecord
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal PreviousClose { get; set; }
        public DateTime LastTradingDay { get; set; }
    }
}
