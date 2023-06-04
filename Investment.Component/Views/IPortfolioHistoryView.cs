using Investment.Component.Models;
using System.Collections.Generic;

namespace Investment.Component.Views
{
    public interface IPortfolioHistoryView
    {
        void SetTransactionHistory(IEnumerable<PortfolioTransactionModel> transactions);
    }
}