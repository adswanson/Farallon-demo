using Investment.Component.Domains.Trading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Component.UnitTests.Mocks
{
    internal class MockTradeLogRepository : ITradeLogRepository
    {
        public Func<int, List<TradeLogRecord>> MockedLog;

        public IEnumerable<TradeLogRecord> GetPortfolioTradeLog(int portfolioId) => MockedLog?.Invoke(portfolioId);
    }
}
