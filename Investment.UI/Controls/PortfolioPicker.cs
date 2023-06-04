using Investment.Component.Models;
using Investment.Component.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Investment.UI.Controls
{
    public partial class PortfolioPicker : UserControl, IPortfoliosView
    {
        private readonly IPortfoliosPresenter _presenter;

        public PortfolioPicker(IPortfoliosPresenter presenter)
        {
            _presenter = presenter;
            InitializeComponent();
            _presenter.Initialize(this);
        }

        public void SetActivePortfolio(int? portfolioId)
        {
            //throw new NotImplementedException();
        }

        public void SetPortfoliosList(IEnumerable<PortfoliosListItemModel> listItems)
        {
            var portfoliosList = listItems
                .ToList();

            portfoliosList.Insert(0, PortfoliosListItemModel.Empty);

            ddlPortfolios.DataSource = portfoliosList;
        }

        private void PortfolioPicker_Load(object sender, EventArgs e)
        {
            _presenter.UpdatePortfoliosList();
        }

        private void ddlPortfolios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sender is ComboBox comboBox)
            {
                var value = comboBox.SelectedValue as PortfoliosListItemModel;
                _presenter.UpdateActivePortfolio(value?.PortfolioId);
            }
        }
    }
}
