using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Component.Domains.Trading
{
    /// <summary>
    /// Service for retrieving symbol quote information
    /// </summary>
    public interface IQuoteService
    {
        Task<QuoteRecord> GetQuote(string symbol);
    }
}
