using System.Threading.Tasks;

namespace Investment.Component.Presenters
{
    public interface IProfitsAndLossesPresenter
    {
        Task ChangeActivePortfolio(int? portfolioId);
        void Initialize(IProfitsAndLossesReportView view);
    }
}