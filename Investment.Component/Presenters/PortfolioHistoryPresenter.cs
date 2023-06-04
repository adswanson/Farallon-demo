using Investment.Component.Domain.Symbols;
using Investment.Component.Domains.Portfolio;
using Investment.Component.Domains.Trading;
using Investment.Component.Models;
using Investment.Component.Views;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Investment.Component.Presenters
{
    public class PortfolioHistoryPresenter : IPortfolioHistoryPresenter
    {
        private readonly ITradeLogRepository _tradeLogRepository;
        private readonly ISymbolRepository _symbolRepository;

        private IPortfolioHistoryView _view;

        private PortfolioHistoryPresenter(ITradeLogRepository tradeLogRepository, ISymbolRepository symbolRepository)
        {
            _tradeLogRepository = tradeLogRepository;
            _symbolRepository = symbolRepository;
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
            var trades = _tradeLogRepository
                .GetPortfolioTradeLog(portfolioId)
                .OrderByDescending(t => t.TransactionDate)
                .Select(SelectTransactionModel);

            _view?.SetTransactionHistory(trades);
        }

        private PortfolioTransactionModel SelectTransactionModel(TradeLogRecord trade)
        {
            var symbol = _symbolRepository.GetSymbol(trade.SymbolId);

            return new PortfolioTransactionModel
            {
                TransactionType = ToTradeTypeModel(trade.TradeType),
                PurchaseAmount = trade.UnitAmount,
                PurchasePrice = trade.Price,
                SymbolName = symbol?.SymbolName ?? null,
                TradeDate = trade.TransactionDate,
                TotalAmount = trade.UnitAmount * trade.Price,
            };
        }

        public TradeTypeModel ToTradeTypeModel(TradeType tradeType)
        {
            switch (tradeType)
            {
                default:
                case TradeType.Unknown:
                    return TradeTypeModel.Unknown;
                case TradeType.Buy:
                    return TradeTypeModel.Buy;
                case TradeType.Sell:
                    return TradeTypeModel.Sell;
            }
        }

        private void ClearActivePortfolio()
        {
            _view.SetTransactionHistory(Enumerable.Empty<PortfolioTransactionModel>());
        }
    }
}