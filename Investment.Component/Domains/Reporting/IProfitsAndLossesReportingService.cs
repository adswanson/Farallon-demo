using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investment.Component.Domains.Reporting
{
    /// <summary>
    /// Service for calculating Profit and Loss reports
    /// </summary>
    public interface IProfitsAndLossesReportingService
    {
        Task<IEnumerable<ProfitsAndLossesReportLineItem>> Calculate(int portfolioId);
    }
}