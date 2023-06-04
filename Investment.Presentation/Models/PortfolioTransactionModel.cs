using System;

namespace Investment.Presentation.Models
{
    public sealed class PortfolioTransactionModel
    {
        public string SymbolName { get; set; }
        public TradeTypeModel TransactionType { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TradeDate { get; set; }
    }
}
