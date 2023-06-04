using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.UI.Eventing
{
    internal class PortfolioChangeEventArgs
    {
        public int? Previous { get; set; }
        public int? New { get; set; }
    }
}
