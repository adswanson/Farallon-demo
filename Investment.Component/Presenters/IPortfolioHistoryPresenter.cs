using Investment.Component.Domains.Trading;
using Investment.Component.Models;

namespace Investment.Component.Presenters
{
    public interface IPortfolioHistoryPresenter
    {
        void ChangeActivePortfolio(int? portfolioId);
        void Initialize(IPortfolioHistoryView view);
        TradeTypeModel ToTradeTypeModel(TradeType tradeType);
    }
}