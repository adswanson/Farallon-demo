using System;
using System.Collections.Generic;
using System.Text;

namespace Investment.Component.Domains.Trading
{
    public sealed class XmlTradeLog
    {
        public List<TradeLogRecord> TradeLogEntries = new List<TradeLogRecord>();
    }
}
