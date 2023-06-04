using Investment.Presentation.Models;
using System.Collections.Generic;

namespace Investment.Presentation.Views
{
    public interface IPortfolioHistoryView
    {
        void SetTransactionHistory(IEnumerable<PortfolioTransactionModel> transactions);
    }
}