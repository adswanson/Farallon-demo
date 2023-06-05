using System.Collections.Generic;

namespace Investment.Component.Domains.Portfolio
{
    /// <summary>
    /// Data access class for portfolio-aligned domain concerns
    /// </summary>
    public interface IPortfolioRepository
    {
        /// <summary>
        /// Get a list of <seealso cref="PortfolioRecord"/>
        /// </summary>
        /// <returns></returns>
        IEnumerable<PortfolioRecord> GetPortfolios();
    }
}