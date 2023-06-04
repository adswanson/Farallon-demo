using Investment.Component.Models;
using Investment.Component.Presenters;
using Investment.UI.Services;
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
        private readonly IApplicationStateAccessor _applicationStateAccessor;

        internal PortfolioPicker(IPortfoliosPresenter presenter, IApplicationStateAccessor applicationContextAccessor)
        {
            _presenter = presenter;
            _applicationStateAccessor = applicationContextAccessor;

            InitializeComponent();           
        }
        private void PortfolioPicker_Load(object sender, EventArgs e)
        {
            _presenter.Initialize(this);
        }

        public async Task SetActivePortfolio(int? portfolioId)
        {
            await _applicationStateAccessor.ChangeActivePortfolio(portfolioId);
        }

        public void SetPortfoliosList(IEnumerable<PortfoliosListItemModel> listItems)
        {
            var portfoliosList = listItems
                .ToList();

            portfoliosList.Insert(0, PortfoliosListItemModel.Empty);

            ddlPortfolios.DataSource = portfoliosList;
        }

        private async void ddlPortfolios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sender is ComboBox comboBox)
            {
                var value = comboBox.SelectedValue as PortfoliosListItemModel;
                await _presenter.UpdateActivePortfolio(value?.PortfolioId);
            }
        }
    }
}
