namespace Investment.Component
{
    /// <summary>
    /// Provides context information for where to find XML-based data stores
    /// </summary>
    public interface IXmlDataContextAccessor
    {
        string PortfolioXml { get; set; }
        string SymbolXml { get; set; }
        string TradeLogXml { get; set; }
    }
}