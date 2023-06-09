﻿using Investment.Component.Services;
using System.Collections.Generic;
using System.Linq;

namespace Investment.Component.Domains.Trading
{
    /// <inheritdoc cref="ITradeLogRepository"/>
    internal sealed class XmlFileTradeLogRepository : ITradeLogRepository
    {
        private readonly IXmlFileDataProvider _xmlFileDataProvider;
        private readonly IXmlDataContextAccessor _xmlDataContextAccessor;

        private XmlTradeLog TradeLog => _xmlFileDataProvider.TryDeserialize<XmlTradeLog>(_xmlDataContextAccessor.TradeLogXml);

        private XmlFileTradeLogRepository(IXmlFileDataProvider xmlFileDataProvider, IXmlDataContextAccessor xmlDataContextAccessor)
        {
            _xmlFileDataProvider = xmlFileDataProvider;
            _xmlDataContextAccessor = xmlDataContextAccessor;
        }

        public IEnumerable<TradeLogRecord> GetPortfolioTradeLog(int portfolioId)
        {
            return TradeLog?
                .TradeLogEntries?
                .Where(tlr => tlr.PortfolioId == portfolioId)
                ?? Enumerable.Empty<TradeLogRecord>();
        }
    }
}
