using Investment.Component.Domain.Symbols;
using Investment.Component.Domains.Trading;
using Investment.Component.Presenters;
using System;
using Utilities.DependencyInjection;

namespace Investment.Component
{
    public static class DependencyExtensions
    {
        public static ContainerBuilder AddInvestmentDependencies(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterTransient<ITradeLogRepository, XmlFileTradeLogRepository>();

            return containerBuilder;
        }

        internal static ITradeLogRepository TradeLogRepository => new XmlFileTradeLogRepository();
        internal static ISymbolRepository SymbolRepository => new XmlFileSymbolRepository();

        public static IPortfolioHistoryPresenter GetPortfolioHistoryPresenter => 
            new PortfolioHistoryPresenter(TradeLogRepository, SymbolRepository);
    }
}

