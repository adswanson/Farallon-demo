using Investment.Component;
using Investment.Component.Domains.Trading;
using Investment.Presentation.Models;
using Investment.Presentation.Views;
using System.Collections.Generic;
using System.Linq;

namespace Investment.Presentation.Presenters
{
    public class PortfolioHistoryPresenter : IPortfolioHistoryPresenter
    {
        private readonly ITradeLogRepository _tradeLogRepository;

        private IPortfolioHistoryView _view;

        private PortfolioHistoryPresenter(ITradeLogRepository tradeLogRepository)
        {
            _tradeLogRepository = tradeLogRepository;
        }

        public void Initialize(IPortfolioHistoryView view)
        {
            _view = view;
        }

        public void ChangeActivePortfolio(int? portfolioId)
        {
            if (!portfolioId.HasValue)
            {
                ClearActivePortfolio();
                return;
            }

            UpdateTransactionHistory(portfolioId.Value);
        }

        private void UpdateTransactionHistory(int portfolioId)
        {
            var result = Result<IEnumerable<TradeLogRecord>>.Try(
                () =>_tradeLogRepository.GetPortfolioTradeLog(portfolioId));

            if(!result.IsSuccess)
            {
                _view.SetTransactionHistory(Enumerable.Empty<PortfolioTransactionModel>());
                return;
            }

            var trades = result
                .Unwrap()
                .OrderByDescending(t => t.TransactionDate)
                .Select(SelectTransactionModel);

            _view?.SetTransactionHistory(trades);
        }

        private PortfolioTransactionModel SelectTransactionModel(TradeLogRecord trade)
        {
            return new PortfolioTransactionModel
            {
                TransactionType = ToTradeDescription(trade.TradeType),
                PurchaseAmount = string.Format("{0:C2}", trade.UnitAmount),
                PurchasePrice = string.Format("{0:C2}", trade.Price),
                SymbolName = trade.SymbolName,
                TradeDate = trade.TransactionDate.ToString("M/dd/yyyy"),
                TotalAmount = string.Format("{0:C2}", trade.UnitAmount * trade.Price)
            };
        }

        private string ToTradeDescription(TradeType tradeType)
        {
            switch (tradeType)
            {
                default:
                case TradeType.Unknown:
                    return "Unknown";
                case TradeType.Buy:
                    return "Buy";
                case TradeType.Sell:
                    return "Sell";
            }
        }

        private void ClearActivePortfolio()
        {
            _view.SetTransactionHistory(Enumerable.Empty<PortfolioTransactionModel>());
        }
    }
}