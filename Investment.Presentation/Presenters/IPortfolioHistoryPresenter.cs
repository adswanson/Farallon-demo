using Investment.Presentation.Models;
using Investment.Presentation.Views;

namespace Investment.Presentation.Presenters
{
    public interface IPortfolioHistoryPresenter
    {
        void ChangeActivePortfolio(int? portfolioId);
        void Initialize(IPortfolioHistoryView view);
    }
}