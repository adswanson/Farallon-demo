using Investment.Component.Domains.Reporting;
using Investment.Component.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investment.Component.Presenters
{
    public interface IProfitsAndLossesReportView
    {
        void SetReport(IEnumerable<ProfitsAndLossesModel> model);
    }

    public class ProfitsAndLossesPresenter : IProfitsAndLossesPresenter
    {
        private readonly IProfitsAndLossesReportingService _profitsAndLossesReportingService;

        private IProfitsAndLossesReportView _view;

        public ProfitsAndLossesPresenter(IProfitsAndLossesReportingService profitsAndLossesReportingService)
        {
            _profitsAndLossesReportingService = profitsAndLossesReportingService;
        }

        public void Initialize(IProfitsAndLossesReportView view)
        {
            _view = view;
        }

        public async Task ChangeActivePortfolio(int? portfolioId)
        {
            if (portfolioId == null)
            {
                ClearProfitsAndLossesReport();
                return;
            }

            await UpdateProfitsAndLossesReport(portfolioId.Value);
        }

        private void ClearProfitsAndLossesReport()
        {
            _view.SetReport(Enumerable.Empty<ProfitsAndLossesModel>());
        }

        private async Task UpdateProfitsAndLossesReport(int portfolioId)
        {
            var reportLineItems = await _profitsAndLossesReportingService.Calculate(portfolioId);

            _view.SetReport(
                reportLineItems
                    .Select(SelectProfitsAndLossesModel)
                    .OrderBy(rli => rli.Symbol));
        }

        private ProfitsAndLossesModel SelectProfitsAndLossesModel(ProfitsAndLossesReportLineItem lineItem)
        {
            return new ProfitsAndLossesModel
            {
                DailyProfitsAndLosses = lineItem.DailyProfitsAndLosses,
                Cost = lineItem.Cost,
                InceptionProfitsAndLosses = lineItem.InceptionProfitsAndLosses,
                LastTradeDate = lineItem.LastTradeDate,
                MarketValue = lineItem.MarketValue,
                PreviousClose = lineItem.PreviousClose,
                Price = lineItem.Price,
                Quantity = lineItem.Quantity,
                RealizedGains = lineItem.RealizedGains,
                Symbol = lineItem.Symbol
            };
        }
    }
}

