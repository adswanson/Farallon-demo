using Investment.UI.Eventing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.UI.Services
{
    internal interface IApplicationContextAccessor
    {
        //int? ActivePortfolioId { get; set; }
        Action<PortfolioChangeEventArgs> OnActivePortfolioChanged { get; set; }
        Func<PortfolioChangeEventArgs, Task> OnActivePortfolioChangedAsync { get; set; }
        Task ChangeActivePortfolio(int? newValue);
        void RegisterChangePortfolioEventSet(EventSet<PortfolioChangeEventArgs> eventSet);
    }

    public class EventSet<TArgs>
    {
        public Action<TArgs> OnStart { get; set; }
        public Func<TArgs, Task> OnRunAsync { get; set; }
        public Action<TArgs> OnRunSync { get; set; }
        public Action<TArgs> OnDone { get; set; }

        public void Combine(EventSet<TArgs> eventSet)
        {
            OnStart += eventSet.OnStart;
            OnRunAsync += eventSet.OnRunAsync;
            OnRunSync += eventSet.OnRunSync;
            OnDone += eventSet.OnDone;
        }
    }

    internal sealed class ApplicationContextAccessor : IApplicationContextAccessor
    {
        private int? _activePortfolioId;
        public int? ActivePortfolioId
        {
            get => _activePortfolioId;
            set
            {
                OnActivePortfolioChanged?.Invoke(new PortfolioChangeEventArgs
                {
                    New = value,
                    Previous = _activePortfolioId,
                });

                _activePortfolioId = value;
            }
        }
        public Action<PortfolioChangeEventArgs> OnActivePortfolioChanged { get; set; }
        public Func<PortfolioChangeEventArgs, Task> OnActivePortfolioChangedAsync { get; set; }

        public void RegisterChangePortfolioEventSet(EventSet<PortfolioChangeEventArgs> eventSet)
        {
            _changePortfolioEvents.Combine(eventSet);
        }

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

            if(_changePortfolioEvents.OnRunAsync != null)
                await _changePortfolioEvents.OnRunAsync(arguments);

            _changePortfolioEvents.OnDone?.Invoke(arguments);
        }

    }
}
