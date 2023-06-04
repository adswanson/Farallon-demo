using Investment.Component.Domain.Symbols;
using Investment.Component.Domains.Portfolio;
using Investment.Component.Domains.Trading;
using Investment.Component.Presenters;
using Investment.Component.Services;
using System;
using Utilities.DependencyInjection;

namespace Investment.Component
{
    public static class DependencyExtensions
    {
        public static ContainerBuilder AddInvestmentDependencies<TXmlDataContextAccessor>(this ContainerBuilder containerBuilder)
            where TXmlDataContextAccessor : IXmlDataContextAccessor
        {
            containerBuilder
                .RegisterTransient<IPortfolioHistoryPresenter, PortfolioHistoryPresenter>()
                .RegisterTransient<IPortfoliosPresenter, PortfoliosPresenter>()
                .RegisterTransient<IPortfolioRepository, XmlFilePortfolioRepository>()
                .RegisterTransient<ITradeLogRepository, XmlFileTradeLogRepository>()
                .RegisterTransient<ISymbolRepository, XmlFileSymbolRepository>()
                .RegisterSingleton<IXmlDataContextAccessor, TXmlDataContextAccessor>()
                .RegisterTransient<IXmlFileDataProvider, XmlFileDataProvider>();

            return containerBuilder;
        }
    }
}

