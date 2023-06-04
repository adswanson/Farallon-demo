using Investment.Component.Models;
using System.Collections.Generic;

namespace Investment.Component.Presenters
{
    public interface IPortfoliosPresenter
    {
        void Initialize(IPortfoliosView portfoliosView);
        void UpdatePortfoliosList();
        void UpdateActivePortfolio(int? portfolioId);
    }
}