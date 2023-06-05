using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investment.UI.Eventing
{
    public class EventSet<TArgs>
    {
        public Action<TArgs> OnStart { get; set; }
        public Func<TArgs, Task> OnRunAsync { get; set; }
        public Action<TArgs> OnRunSync { get; set; }
        public Action<TArgs> OnDone { get; set; }

        private List<Func<TArgs, Task>> _internalRunAsync
            = new List<Func<TArgs, Task>>();

        public async Task Execute(TArgs args)
        {
            OnStart?.Invoke(args);
            OnRunSync?.Invoke(args);

            // Multicast delegates seem to risk a race condition
            // when executed asyc in a WinForms application.
            foreach(var action in _internalRunAsync)
            {
                await action(args);
            }

            OnDone?.Invoke(args);
        }

        public void Combine(EventSet<TArgs> eventSet)
        {
            OnStart += eventSet.OnStart;
            OnRunAsync += eventSet.OnRunAsync;
            OnRunSync += eventSet.OnRunSync;
            OnDone += eventSet.OnDone;

            _internalRunAsync.Add(OnRunAsync);
        }
    }
}
