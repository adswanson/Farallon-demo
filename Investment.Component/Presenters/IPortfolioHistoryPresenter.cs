using Investment.Component.Domains.Trading;
using Investment.Component.Models;
using Investment.Component.Views;

namespace Investment.Component.Presenters
{
    public interface IPortfolioHistoryPresenter
    {
        void ChangeActivePortfolio(int? portfolioId);
        void Initialize(IPortfolioHistoryView view);
        TradeTypeModel ToTradeTypeModel(TradeType tradeType);
    }
}