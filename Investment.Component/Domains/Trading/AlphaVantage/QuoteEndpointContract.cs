using System;
using System.Collections.Generic;
using System.Text;

namespace Investment.Component.Domains.Trading.AlphaVantage
{
    internal sealed class AlphaVantageQuoteEndpointContract: Dictionary<string, Dictionary<string, string>>
    {
        internal class Properties
        {
            public const string Symbol = "01. symbol";
            public const string Price = "05. price";
            public const string PreviousClose = "08. previous close";
            public const string LatestTradingDay = "07. latest trading day";
        }

        private const string RootPropertyName = "Global Quote";

        internal string GetValue(string propertyName)
        {
            if (!TryGetValue(RootPropertyName, out var properties))
                return null;

            if (!properties.TryGetValue(propertyName, out var value))
                return null;

            return value;
        }
    }
}
