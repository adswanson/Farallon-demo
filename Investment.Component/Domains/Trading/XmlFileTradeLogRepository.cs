using System;
using System.Collections.Generic;
using System.Linq;

namespace Investment.Component.Domains.Trading
{
    public class XmlFileTradeLogRepository : ITradeLogRepository
    {
        private static Dictionary<int, TradeLogRecord> _mockTable =
            new Dictionary<int, TradeLogRecord>
            {
                { 0, new TradeLogRecord { PortfolioId = 1, TradeLogId = 0, SymbolId = 0, Price = 30.00M, TradeType = TradeType.Buy, TransactionDateUtc = DateTimeOffset.UtcNow,UnitAmount = 10 } }
            };

        public IEnumerable<TradeLogRecord> GetPortfolioTradeLog(int portfolioId)
        {
            return _mockTable
                .Values
                .Where(tlr => tlr.PortfolioId == portfolioId);
        }
    }
}
