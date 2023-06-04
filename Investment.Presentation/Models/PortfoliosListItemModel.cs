
namespace Investment.Presentation.Models
{
    public sealed class PortfoliosListItemModel
    {
        public int? PortfolioId { get; set; }
        public string Name { get; set; }

        public static PortfoliosListItemModel Empty => new PortfoliosListItemModel
        {
            Name = "",
            PortfolioId = null
        };
    }
}
