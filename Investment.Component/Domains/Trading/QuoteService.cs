using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Component.Domains.Trading
{
    public interface IQuoteService
    {
        Task<QuoteRecord> GetHistoricalQuote(string symbol);
        Task<QuoteRecord> GetQuote(string symbol);
    }

    public class RemoteQuoteOptions
    {
        public string ApiKey { get; set; }
    }
}
