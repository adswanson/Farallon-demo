using System.Collections.Generic;

namespace Investment.Component.Domains.Reporting
{
    public interface IProfitsAndLossesReportingService
    {
        IEnumerable<ProfitsAndLossesReportLineItem> Calculate(int portfolioId);
    }
}