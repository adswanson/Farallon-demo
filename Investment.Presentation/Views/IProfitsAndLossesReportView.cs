using Investment.Presentation.Models;
using System.Collections.Generic;

namespace Investment.Presentation.Views
{
    public interface IProfitsAndLossesReportView
    {
        void SetReport(IEnumerable<ProfitsAndLossesModel> model);
    }
}

