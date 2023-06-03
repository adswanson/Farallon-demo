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
            containerBuilder
                .RegisterTransient<IPortfolioHistoryPresenter, PortfolioHistoryPresenter>()
                .RegisterTransient<ITradeLogRepository, XmlFileTradeLogRepository>()
                .RegisterTransient<ISymbolRepository, XmlFileSymbolRepository>()
                .RegisterSingleton<IXmlDataProviderAccessor, XmlDataProviderAccessor>();

            return containerBuilder;
        }
    }
}

