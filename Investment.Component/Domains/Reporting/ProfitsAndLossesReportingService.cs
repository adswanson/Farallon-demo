using Investment.Component.Domains.Trading;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investment.Component.Domains.Reporting
{
    internal class ProfitsAndLossesReportingService : IProfitsAndLossesReportingService
    {
        private readonly ITradeLogRepository _tradeLogRepository;
        private readonly IQuoteService _quoteService;

        public ProfitsAndLossesReportingService(ITradeLogRepository tradeLogRepository, IQuoteService quoteService)
        {
            _tradeLogRepository = tradeLogRepository;
            _quoteService = quoteService;
        }

        public async Task<IEnumerable<ProfitsAndLossesReportLineItem>> Calculate(int portfolioId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var symbolGroups = _tradeLogRepository
                .GetPortfolioTradeLog(portfolioId)
                .Where(t => t.TransactionDate >= (startDate ?? DateTime.MinValue) && t.TransactionDate <= (endDate ?? DateTime.MaxValue))
                .GroupBy(t => t.SymbolName);

            var lineItems = new ConcurrentDictionary<string, ProfitsAndLossesReportLineItem>();

            foreach(var group in symbolGroups)
            {
                var quote = await _quoteService.GetQuote(group.Key);
                if (quote == null)
                {
                    // todo - log
                    continue;
                }

                decimal realizedGains = 0;
                decimal unitsOnHand = 0;
                decimal cost = 0;

                foreach (var trade in group.OrderBy(t => t.TransactionDate))
                {
                    if (trade.TradeType == TradeType.Buy)
                    {
                        unitsOnHand += trade.UnitAmount;
                        cost += trade.UnitAmount * trade.Price;
                    }
                    else if (trade.TradeType == TradeType.Sell)
                    {
                        unitsOnHand -= trade.UnitAmount;
                        realizedGains += trade.UnitAmount * trade.Price;
                    }
                }

                lineItems[group.Key] = CalculateLineItem(unitsOnHand, cost,realizedGains,quote);
            }

            return lineItems.Values;
        }

        private ProfitsAndLossesReportLineItem CalculateLineItem(decimal unitsOnHand, decimal cost,
            decimal realizedGains, QuoteRecord quote)
        {
            var marketValue = unitsOnHand * quote.Price;
            var previousDayMarketValue = unitsOnHand * quote.PreviousClose;

            var dailyPandL = marketValue - previousDayMarketValue;
            var inceptionPandL = marketValue + realizedGains - cost;

            return new ProfitsAndLossesReportLineItem
            {
                Cost = cost,
                Quantity = unitsOnHand,
                RealizedGains = realizedGains,
                Price = quote.Price,
                LastTradeDate = quote.LastTradingDay,
                InceptionProfitsAndLosses = inceptionPandL,
                DailyProfitsAndLosses = dailyPandL,
                PreviousClose = quote.PreviousClose,
                MarketValue = marketValue,
                Symbol = quote.Symbol,
            };
        }
    }
}
