
namespace Investment.Component
{
    internal sealed class XmlDataProviderAccessor : IXmlDataProviderAccessor
    {
        public string PortfolioXml { get; set; }
        public string TradeLogXml { get; set; }
        public string SymbolXml { get; set; }
    }
}
