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
        int? ActivePortfolioId { get; set; }
        Action<PortfolioChangeEventArgs> OnActivePortfolioChanged { get; set; }
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
    }
}
