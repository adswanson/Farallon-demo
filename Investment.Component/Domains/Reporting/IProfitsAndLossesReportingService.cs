using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investment.Component.Domains.Reporting
{
    public interface IProfitsAndLossesReportingService
    {
        Task<IEnumerable<ProfitsAndLossesReportLineItem>> Calculate(int portfolioId, DateTime? startDate = null, DateTime? endDate = null);
    }
}