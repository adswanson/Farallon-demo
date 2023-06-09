﻿using Investment.Component.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Investment.Component.Domains.Portfolio
{
    /// <inheritdoc cref="IPortfolioRepository"/>
    internal sealed class XmlFilePortfolioRepository : IPortfolioRepository
    {
        private readonly IXmlFileDataProvider _xmlFileDataProvider;
        private readonly IXmlDataContextAccessor _xmlDataContextAccessor;

        private XmlPortfolio Portfolio => _xmlFileDataProvider.TryDeserialize<XmlPortfolio>(_xmlDataContextAccessor.PortfolioXml);

        private XmlFilePortfolioRepository(IXmlFileDataProvider xmlFileDataProvider, IXmlDataContextAccessor xmlDataContextAccessor)
        {
            _xmlFileDataProvider = xmlFileDataProvider;
            _xmlDataContextAccessor = xmlDataContextAccessor;
        }

        public IEnumerable<PortfolioRecord> GetPortfolios()
        {
            return Portfolio.Portfolios;
        }
    }
}
