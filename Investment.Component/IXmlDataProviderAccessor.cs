namespace Investment.Component
{
    public interface IXmlDataProviderAccessor
    {
        string PortfolioXml { get; set; }
        string SymbolXml { get; set; }
        string TradeLogXml { get; set; }
    }
}