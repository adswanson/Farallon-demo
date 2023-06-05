using System;
using System.Threading.Tasks;
using Utilities.Http;
using Utilities.Json;

namespace Investment.Component.Domains.Trading.AlphaVantage
{
    /// <summary>
    /// Calls the AlphaVantage REST API for symbol quote information
    /// </summary>
    internal sealed class AlphaVantageQuoteService : IQuoteService
    {
        private const string GlobalQuote = "GLOBAL_QUOTE";
        private const string TimeSeriesDailyAdjusted = "TIME_SERIES_DAILY_ADJUSTED";

        private readonly IHttpClient _httpClient;
        private readonly RemoteQuoteOptions _remoteQuoteOptions;
        private readonly IJsonSerializationService _jsonSerializationService;

        private AlphaVantageQuoteService(IHttpClientFactory httpClientFactory, RemoteQuoteOptions options,
            IJsonSerializationService jsonSerializationService)
        {
            _httpClient = httpClientFactory.Get(nameof(IQuoteService));
            _remoteQuoteOptions = options;
            _jsonSerializationService = jsonSerializationService;
        }

        public async Task<QuoteRecord> GetQuote(string symbol)
        {
            var endpoint = ConstructEndpoint(GlobalQuote, symbol);

            var quote = await _httpClient.Get(endpoint);
            if (quote == null)
                return null;

            var body = _jsonSerializationService.Deserialize<AlphaVantageQuoteEndpointContract>(quote);

            return TryConvert(body);
        }

        public QuoteRecord TryConvert(AlphaVantageQuoteEndpointContract contract)
        {
            try
            {
                return new QuoteRecord
                {
                    LastTradingDay = Convert.ToDateTime(contract.GetValue(AlphaVantageQuoteEndpointContract.Properties.LatestTradingDay)),
                    PreviousClose = Convert.ToDecimal(contract.GetValue(AlphaVantageQuoteEndpointContract.Properties.PreviousClose)),
                    Price = Convert.ToDecimal(contract.GetValue(AlphaVantageQuoteEndpointContract.Properties.Price)),
                    Symbol = contract.GetValue(AlphaVantageQuoteEndpointContract.Properties.Symbol)
                };
            }
            catch
            {
                return null;
            }
        }

        private string ConstructEndpoint(string operation, string symbol)
        {
            var queryStringParameters = string.Join("&",
                $"function={operation}",
                $"symbol={symbol}",
                $"apikey={_remoteQuoteOptions.ApiKey}");

            return $"query?{queryStringParameters}";
        }
    }
}
