using System;
using System.Collections.Generic;
using System.Text;

namespace Investment.Component
{
    public sealed class XmlDataProviderOptions : IXmlDataProviderOptions
    {
        public string PortfolioXml { get; set; }
        public string TradeLogXml { get; set; }
        public string SymbolXml { get; set; }
    }
}
