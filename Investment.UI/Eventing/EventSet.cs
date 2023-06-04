using System;
using System.Threading.Tasks;

namespace Investment.UI.Eventing
{
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
}
