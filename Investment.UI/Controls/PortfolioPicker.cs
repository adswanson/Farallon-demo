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
        private readonly IApplicationContextAccessor _applicationContextAccessor;

        internal PortfolioPicker(IPortfoliosPresenter presenter, IApplicationContextAccessor applicationContextAccessor)
        {
            _presenter = presenter;
            _applicationContextAccessor = applicationContextAccessor;

            InitializeComponent();           
        }
        private void PortfolioPicker_Load(object sender, EventArgs e)
        {
            _presenter.Initialize(this);
            _presenter.UpdatePortfoliosList();
        }

        public void SetActivePortfolio(int? portfolioId)
        {
            _applicationContextAccessor.ActivePortfolioId = portfolioId;
        }

        public void SetPortfoliosList(IEnumerable<PortfoliosListItemModel> listItems)
        {
            var portfoliosList = listItems
                .ToList();

            portfoliosList.Insert(0, PortfoliosListItemModel.Empty);

            ddlPortfolios.DataSource = portfoliosList;
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
