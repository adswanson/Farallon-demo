using Investment.Presentation.Views;
using System.Threading.Tasks;

namespace Investment.Presentation.Presenters
{
    public interface IProfitsAndLossesPresenter
    {
        Task ChangeActivePortfolio(int? portfolioId);
        void Initialize(IProfitsAndLossesReportView view);
    }
}