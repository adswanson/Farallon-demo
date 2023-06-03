using System.Collections.Generic;

namespace Investment.Component.Domains.Portfolio
{
    public interface IPortfolioRepository
    {
        PortfolioRecord GetPortfolio(int id);
        IEnumerable<PortfolioRecord> GetPortfolios();
    }
}