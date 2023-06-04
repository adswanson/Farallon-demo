using Investment.Presentation.Views;
using System.Threading.Tasks;

namespace Investment.Presentation.Presenters
{
    public interface IPortfoliosPresenter
    {
        void Initialize(IPortfoliosView portfoliosView);
        void UpdatePortfoliosList();
        Task UpdateActivePortfolio(int? portfolioId);
    }
}