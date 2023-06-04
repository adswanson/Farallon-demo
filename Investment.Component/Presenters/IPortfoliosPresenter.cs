using Investment.Component.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investment.Component.Presenters
{
    public interface IPortfoliosPresenter
    {
        void Initialize(IPortfoliosView portfoliosView);
        void UpdatePortfoliosList();
        Task UpdateActivePortfolio(int? portfolioId);
    }
}