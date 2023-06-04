using Investment.Component;

namespace Investment.UI.Services
{
    internal sealed class LocalXmlDataContextAccessor : IXmlDataContextAccessor
    {
        public string PortfolioXml { get; set; } = "resources/data/Portfolios.xml";
        public string SymbolXml { get; set; } = "resources/data/Symbols.xml";
        public string TradeLogXml { get; set; } = "resources/data/TradeLog.xml";
    }
}
