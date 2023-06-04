using Investment.Component.Domains.Portfolio;
using Investment.Presentation.Models;
using Investment.Presentation.Views;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Presentation.Presenters
{

    internal class PortfoliosPresenter : IPortfoliosPresenter
    {
        private IPortfoliosView _view;
        private readonly IPortfolioRepository _portfolioRepository;

        private PortfoliosPresenter(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public void Initialize(IPortfoliosView view)
        {
            _view = view;
            _view.SetActivePortfolio(null);

            UpdatePortfoliosList();
        }

        public async Task UpdateActivePortfolio(int? portfolioId)
        {
            await _view.SetActivePortfolio(portfolioId);
        }

        public void UpdatePortfoliosList()
        {
            _view.SetPortfoliosList(_portfolioRepository
                .GetPortfolios()
                .OrderBy(p => p.Name)
                .Select(SelectPortfoliosListItemModel));
        }

        private PortfoliosListItemModel SelectPortfoliosListItemModel(PortfolioRecord record)
        {
            return new PortfoliosListItemModel
            {
                Name = record.Name,
                PortfolioId = record.Id
            };
        }
    }
}
