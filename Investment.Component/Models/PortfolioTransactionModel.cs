using System;

namespace Investment.Component.Models
{
    public class PortfolioTransactionModel
    {
        public string SymbolName { get; set; }
        public TradeTypeModel TransactionType { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTimeOffset TradeDate { get; set; }
    }
}
