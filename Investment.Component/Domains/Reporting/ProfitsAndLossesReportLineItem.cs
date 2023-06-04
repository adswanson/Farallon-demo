using System;

namespace Investment.Component.Domains.Reporting
{
    public sealed class ProfitsAndLossesReportLineItem
    {
        public string Symbol { get; set; }
        public decimal Cost { get; set; }
        public decimal RealizedGains { get; set; }
        public decimal Quantity { get; set; }
        public DateTime LastTradeDate { get; set; }
        public decimal? Price { get; set; }
        public decimal MarketValue { get; set; }
        public decimal PreviousClose { get; set; }
        public decimal InceptionProfitsAndLosses { get; set; }
        public decimal DailyProfitsAndLosses { get; set; }
    }
}
