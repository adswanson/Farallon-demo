
namespace Investment.UI.Eventing
{
    internal class PortfolioChangeEventArgs
    {
        public int? Previous { get; set; }
        public int? New { get; set; }
    }
}
