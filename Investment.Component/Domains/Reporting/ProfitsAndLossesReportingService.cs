using Investment.Component.Domains.Trading;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investment.Component.Domains.Reporting
{
    public class ProfitsAndLossesReportingService : IProfitsAndLossesReportingService
    {
        private readonly ITradeLogRepository _tradeLogRepository;

        public ProfitsAndLossesReportingService(ITradeLogRepository tradeLogRepository)
        {
            _tradeLogRepository = tradeLogRepository;
        }

        public IEnumerable<ProfitsAndLossesReportLineItem> Calculate(int portfolioId)
        {
            var result = Enumerable.Empty<ProfitsAndLossesReportLineItem>();

            var symbolGroups = _tradeLogRepository
                .GetPortfolioTradeLog(portfolioId)
                .GroupBy(t => t.SymbolId);

            var lineItems = new ConcurrentDictionary<int, ProfitsAndLossesReportLineItem>();

            Parallel.ForEach(symbolGroups, group =>
            {
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
            });

            return result;
        }
    }
}
