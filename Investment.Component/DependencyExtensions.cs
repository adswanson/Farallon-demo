﻿using Investment.Component.Domains.Portfolio;
using Investment.Component.Domains.Reporting;
using Investment.Component.Domains.Trading;
using Investment.Component.Domains.Trading.AlphaVantage;
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
                .RegisterTransient<IPortfolioRepository, XmlFilePortfolioRepository>()
                .RegisterTransient<ITradeLogRepository, XmlFileTradeLogRepository>()
                .RegisterSingleton<IXmlDataContextAccessor, TXmlDataContextAccessor>()
                .RegisterTransient<IXmlFileDataProvider, XmlFileDataProvider>()
                .RegisterTransient<IQuoteService, AlphaVantageQuoteService>()
                .RegisterTransient<IProfitsAndLossesReportingService, ProfitsAndLossesReportingService>();

            return containerBuilder;
        }
    }
}

