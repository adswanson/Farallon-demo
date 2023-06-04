using Investment.Component.Domains.Portfolio;
using Investment.Component.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Investment.Component.Presenters
{
    public interface IPortfoliosView
    {
        void SetPortfoliosList(IEnumerable<PortfoliosListItemModel> listItems);
        void SetActivePortfolio(int? portfolioId);
    }

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
        }

        public void UpdateActivePortfolio(int? portfolioId)
        {
            _view.SetActivePortfolio(portfolioId);
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
