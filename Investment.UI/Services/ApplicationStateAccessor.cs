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

            _changePortfolioEvents.OnStart?.Invoke(arguments);
            _changePortfolioEvents.OnRunSync?.Invoke(arguments);

            if (_changePortfolioEvents.OnRunAsync != null)
                await _changePortfolioEvents.OnRunAsync(arguments);

            _changePortfolioEvents.OnDone?.Invoke(arguments);
            _activePortfolioId = newValue;
        }

        public void RegisterChangePortfolioEventSet(EventSet<PortfolioChangeEventArgs> eventSet)
        {
            _changePortfolioEvents.Combine(eventSet);
        }

    }
}