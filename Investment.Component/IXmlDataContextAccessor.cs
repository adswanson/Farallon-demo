namespace Investment.Component
{
    public interface IXmlDataContextAccessor
    {
        string PortfolioXml { get; set; }
        string SymbolXml { get; set; }
        string TradeLogXml { get; set; }
    }
}