namespace Investment.Component
{
    public interface IXmlDataProviderOptions
    {
        string PortfolioXml { get; set; }
        string SymbolXml { get; set; }
        string TradeLogXml { get; set; }
    }
}