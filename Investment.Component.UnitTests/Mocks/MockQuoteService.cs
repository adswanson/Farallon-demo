using Investment.Component.Domains.Trading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Component.UnitTests.Mocks
{
    internal class MockQuoteService : IQuoteService
    {
        public Func<string, QuoteRecord> MockedQuote { get; set; }

        public Task<QuoteRecord> GetQuote(string symbol)
        {
            var result = MockedQuote(symbol);
            return Task.FromResult(result);
        }
    }
}
