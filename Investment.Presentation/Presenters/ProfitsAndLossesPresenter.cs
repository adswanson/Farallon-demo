using Investment.Component;
using Investment.Component.Domains.Reporting;
using Investment.Presentation.Models;
using Investment.Presentation.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investment.Presentation.Presenters
{

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
            var reportLineItems = await Result<IEnumerable<ProfitsAndLossesReportLineItem>>
                .TryAsync(async () => await _profitsAndLossesReportingService.Calculate(portfolioId));

            if(!reportLineItems.IsSuccess)
            {
                _view.SetReport(Enumerable.Empty<ProfitsAndLossesModel>());
                return;
            }

            _view.SetReport(
                reportLineItems
                    .Unwrap()
                    .Select(SelectProfitsAndLossesModel)
                    .OrderBy(rli => rli.Symbol));
        }

        private ProfitsAndLossesModel SelectProfitsAndLossesModel(ProfitsAndLossesReportLineItem lineItem)
        {
            return new ProfitsAndLossesModel
            {
                DailyProfitsAndLosses = string.Format("{0:C2}", lineItem.DailyProfitsAndLosses),
                Cost = string.Format("{0:C2}", lineItem.Cost),
                InceptionProfitsAndLosses = string.Format("{0:C2}", lineItem.InceptionProfitsAndLosses),
                LastTradeDate = lineItem.LastTradeDate.ToString("M/dd/yyyy"),
                MarketValue = string.Format("{0:C2}", lineItem.MarketValue),
                PreviousClose = string.Format("{0:C2}", lineItem.PreviousClose),
                Price = string.Format("{0:C2}", lineItem.Price),
                Quantity = lineItem.Quantity,
                RealizedGains = string.Format("{0:C2}", lineItem.RealizedGains),
                Symbol = lineItem.Symbol
            };
        }
    }
}

