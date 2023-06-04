using Investment.Presentation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investment.Presentation.Views
{
    public interface IPortfoliosView
    {
        void SetPortfoliosList(IEnumerable<PortfoliosListItemModel> listItems);
        Task SetActivePortfolio(int? portfolioId);
    }
}
