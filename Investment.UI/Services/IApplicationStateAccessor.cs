using Investment.UI.Eventing;
using System.Threading.Tasks;

namespace Investment.UI.Services
{
    internal interface IApplicationStateAccessor
    {
        Task ChangeActivePortfolio(int? newValue);
        void RegisterChangePortfolioEventSet(EventSet<PortfolioChangeEventArgs> eventSet);
    }
}
