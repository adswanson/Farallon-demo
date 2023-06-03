using Investment.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.UI.Services
{
    internal class LocalXmlDataContextAccessor : IXmlDataContextAccessor
    {
        public string PortfolioXml { get; set; } = "resources/data/Portfolios.xml";
        public string SymbolXml { get; set; } = "resources/data/Symbols.xml";
        public string TradeLogXml { get; set; } = "resources/data/TradeLog.xml";
    }
}
