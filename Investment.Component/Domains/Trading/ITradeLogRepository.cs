using System.Collections.Generic;

namespace Investment.Component.Domains.Trading
{
    public interface ITradeLogRepository
    {
        IEnumerable<TradeLogRecord> GetPortfolioTradeLog(int portfolioId);
    }
}