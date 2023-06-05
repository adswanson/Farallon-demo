using System;

namespace Investment.Presentation.Models
{
    public sealed class PortfolioTransactionModel
    {
        public string SymbolName { get; set; }
        public string TransactionType { get; set; }
        public string PurchasePrice { get; set; }
        public string PurchaseAmount { get; set; }
        public string TotalAmount { get; set; }
        public string TradeDate { get; set; }
    }
}
