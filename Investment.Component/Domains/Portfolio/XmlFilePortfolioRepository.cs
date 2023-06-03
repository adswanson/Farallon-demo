using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Investment.Component.Domains.Portfolio
{
    internal sealed class XmlFilePortfolioRepository : IPortfolioRepository
    {
        private readonly XmlDataProviderAccessor _options;
        private readonly XmlSerializer _serializer;

        private IEnumerable<PortfolioRecord> Records
        {
            get
            {
                IEnumerable<PortfolioRecord> result = null;

                try
                {
                    result = TryGetRecords();
                }
                catch (Exception e)
                {
                    // todo - logging
                    return null;
                }

                return result;
            }
        }

        private IEnumerable<PortfolioRecord> TryGetRecords()
        {
            using (var portfolioFile = new FileStream(_options.PortfolioXml, FileMode.Open))
            {
                XmlPortfolio deserializedPortfolio = _serializer.Deserialize(portfolioFile) as XmlPortfolio;

                return deserializedPortfolio != null
                    ? deserializedPortfolio.Portfolios
                    : null;
            }
        }

        internal XmlFilePortfolioRepository(XmlDataProviderAccessor dataProviderOptions)
        {
            _options = dataProviderOptions;
        }

        public IEnumerable<PortfolioRecord> GetPortfolios()
        {
            return Records;
        }

        public PortfolioRecord GetPortfolio(int id)
        {
            return Records
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
