using System.Collections.Generic;

namespace Investment.Component.Domains.Trading
{
    /// <summary>
    /// Data access for trade log information
    /// </summary>
    public interface ITradeLogRepository
    {
        /// <summary>
        /// Returns a list of <seealso cref="TradeLogRecord"/> records for a given portfolio ID
        /// </summary>
        IEnumerable<TradeLogRecord> GetPortfolioTradeLog(int portfolioId);
    }
}