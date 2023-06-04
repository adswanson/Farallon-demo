using System;
using System.Collections.Generic;
using System.Text;

namespace Investment.Component.Models
{
    public class PortfoliosListItemModel
    {
        public int? PortfolioId { get; set; }
        public string Name { get; set; }

        public static PortfoliosListItemModel Empty => new PortfoliosListItemModel
        {
            Name = "",
            PortfolioId = null
        };
    }
}
