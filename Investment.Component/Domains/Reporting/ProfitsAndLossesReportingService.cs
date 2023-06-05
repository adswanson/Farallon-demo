using Investment.Component.Domains.Trading;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investment.Component.Domains.Reporting
{
    ///<inheritdoc cref="IProfitsAndLossesReportingService"/>
    internal sealed class ProfitsAndLossesReportingService : IProfitsAndLossesReportingService
    {
        private readonly ITradeLogRepository _tradeLogRepository;
        private readonly IQuoteService _quoteService;

        internal ProfitsAndLossesReportingService(ITradeLogRepository tradeLogRepository, IQuoteService quoteService)
        {
            _tradeLogRepository = tradeLogRepository;
            _quoteService = quoteService;
        }

        public async Task<IEnumerable<ProfitsAndLossesReportLineItem>> Calculate(int portfolioId)
        {
            var symbolGroups = _tradeLogRepository
                .GetPortfolioTradeLog(portfolioId)
                .GroupBy(t => t.SymbolName);

            var lineItems = new ConcurrentDictionary<string, ProfitsAndLossesReportLineItem>();

            foreach (var group in symbolGroups)
            {
                lineItems[group.Key] = await AggregateSymbolTrades(group.Key, group);
            }

            return lineItems.Values;
        }

        private async Task<ProfitsAndLossesReportLineItem> AggregateSymbolTrades(string symbol, IEnumerable<TradeLogRecord> trades)
        {
            var quote = await _quoteService.GetQuote(symbol);

            if (quote == null)
            {
                throw new Exception($"Unable to retrieve quote for {symbol}");
            }

            decimal realizedGains = 0;
            decimal unitsOnHand = 0;
            decimal cost = 0;
            decimal purchasedToday = 0;

            foreach (var trade in trades.OrderBy(t => t.TransactionDate))
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

                if (trade.TransactionDate > DateTime.Now.Date)
                {
                    purchasedToday += trade.UnitAmount;
                }
            }

            decimal marketValue = unitsOnHand * quote.Price;
            decimal previousDayMarketValue = (unitsOnHand - purchasedToday) * quote.PreviousClose;
            decimal dailyPandL = marketValue - previousDayMarketValue;
            decimal inceptionPandL = marketValue + realizedGains - cost;

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
