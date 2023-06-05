using Investment.UI.Eventing;
using System.Threading.Tasks;

namespace Investment.UI.Services
{
    internal sealed class ApplicationStateAccessor : IApplicationStateAccessor
    {
        private int? _activePortfolioId;
        private EventSet<PortfolioChangeEventArgs> _changePortfolioEvents
            = new EventSet<PortfolioChangeEventArgs>();

        public async Task ChangeActivePortfolio(int? newValue)
        {
            var arguments = new PortfolioChangeEventArgs
            {
                New = newValue,
                Previous = _activePortfolioId
            };

            await _changePortfolioEvents.Execute(arguments);
            
            _activePortfolioId = newValue;
        }

        public void RegisterChangePortfolioEventSet(EventSet<PortfolioChangeEventArgs> eventSet)
        {
            _changePortfolioEvents.Combine(eventSet);
        }

    }
}