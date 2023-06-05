using System;

namespace Investment.Presentation.Models
{
    public sealed class ProfitsAndLossesModel
    {
        public string Symbol { get; set; }
        public string Cost { get; set; }
        public string RealizedGains { get; set; }
        public decimal Quantity { get; set; }
        public string LastTradeDate { get; set; }
        public string Price { get; set; }
        public string MarketValue { get; set; }
        public string PreviousClose { get; set; }
        public string InceptionProfitsAndLosses { get; set; }
        public string DailyProfitsAndLosses { get; set; }
    }
}
