using System;

namespace Investment.Component.Domains.Trading
{
    public class TradeLogRecord
    {
        public int PortfolioId { get; set; }
        public int TradeLogId { get; set; }
        public int SymbolId { get; set; }
        public TradeType TradeType { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal Price { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
