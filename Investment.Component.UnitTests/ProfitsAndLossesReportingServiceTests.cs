using Investment.Component.Domains.Reporting;
using Investment.Component.Domains.Trading;
using Investment.Component.UnitTests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investment.Component.UnitTests
{
    public class ProfitsAndLossesReportingServiceTests
    {
        MockTradeLogRepository _tradeLogRepository;
        MockQuoteService _quoteService;

        [SetUp]
        public void Setup()
        {
            _tradeLogRepository = new MockTradeLogRepository();
            _quoteService = new MockQuoteService();
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
            _tradeLogRepository.MockedLog = (portfolioId) =>
                new List<TradeLogRecord>
                {
                    new TradeLogRecord
                    {
                        PortfolioId = 1,
                        Price = 10.00M,
                        SymbolName = "TEST",
                        TradeType = TradeType.Buy,
                        UnitAmount = 10,
                        TransactionDate = DateTime.Now
                    }
                };

            var runTime = DateTime.Now;
            _quoteService.MockedQuote = (symbol) =>
                new QuoteRecord
                {
                    Symbol = symbol,
                    Price = 10.00M,
                    PreviousClose = 9.00M,
                    LastTradingDay = runTime.AddDays(-1)
                };

            var service = new ProfitsAndLossesReportingService(_tradeLogRepository, _quoteService);

            // Act
            var report = await service.Calculate(1);

            // Assert
            Assert.IsNotNull(report);

            var reportList = report.ToList();

            Assert.AreEqual(1, reportList.Count);

            var lineItem = reportList.First();
            Assert.AreEqual(100.00M, lineItem.MarketValue);
            Assert.AreEqual(100.00M, lineItem.DailyProfitsAndLosses);
            Assert.AreEqual(0.00M, lineItem.InceptionProfitsAndLosses);
            Assert.AreEqual(0.00M, lineItem.RealizedGains);
            Assert.AreEqual(100.0M, lineItem.Cost);
            Assert.AreEqual("TEST", lineItem.Symbol);
            Assert.AreEqual(10.00, lineItem.Quantity);
            Assert.AreEqual(runTime.AddDays(-1), lineItem.LastTradeDate);
            Assert.AreEqual(9.00M, lineItem.PreviousClose);
            Assert.AreEqual(10.00, lineItem.Price);
        }
    }
}