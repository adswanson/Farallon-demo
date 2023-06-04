using Investment.Presentation.Models;
using Investment.Presentation.Presenters;
using Investment.Presentation.Views;
using Investment.UI.Eventing;
using Investment.UI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Investment.UI.Controls
{
    public partial class ProfitsAndLossesReport : UserControl, IProfitsAndLossesReportView
    {
        private readonly IProfitsAndLossesPresenter _presenter;
        private readonly IApplicationStateAccessor _applicationStateAccessor;

        private DataTable _reportTable;

        private ProfitsAndLossesReport(IProfitsAndLossesPresenter profitsAndLossesPresenter,
            IApplicationStateAccessor applicationStateAccessor)
        {
            _presenter = profitsAndLossesPresenter;
            _applicationStateAccessor = applicationStateAccessor;

            InitializeComponent();
        }

        private void ProfitsAndLossesReport_Load(object sender, EventArgs e)
        {
            _presenter.Initialize(this);

            _applicationStateAccessor.RegisterChangePortfolioEventSet(new EventSet<PortfolioChangeEventArgs>
            {
                OnRunAsync = async (args) => await _presenter.ChangeActivePortfolio(args.New),
                OnDone = (args) => dgProfitsAndLosses.DataSource = _reportTable
            });
        }

        public void SetReport(IEnumerable<ProfitsAndLossesModel> model)
        {
            _reportTable = ToDataTable(model);
        }

        private DataTable ToDataTable(IEnumerable<ProfitsAndLossesModel> model)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Ticker", typeof(string));
            dataTable.Columns.Add("As of Date", typeof(string));
            dataTable.Columns.Add("Cost", typeof(string));
            dataTable.Columns.Add("Quantity", typeof(string));
            dataTable.Columns.Add("Price", typeof(string));
            dataTable.Columns.Add("Market Value", typeof(string));
            dataTable.Columns.Add("Prev. Close", typeof(string));
            dataTable.Columns.Add("Daily P&L", typeof(string));
            dataTable.Columns.Add("Inception P&L", typeof(string));
            dataTable.Columns.Add("Revenue", typeof(string));

            foreach (var reportLineItem in model)
            {
                dataTable.Rows.Add(
                    reportLineItem.Symbol,
                    reportLineItem.LastTradeDate.ToString(),
                    reportLineItem.Cost,
                    reportLineItem.Quantity,
                    reportLineItem.Price,
                    reportLineItem.MarketValue,
                    reportLineItem.PreviousClose,
                    reportLineItem.DailyProfitsAndLosses,
                    reportLineItem.InceptionProfitsAndLosses,
                    reportLineItem.RealizedGains
                );
            }

            return dataTable;
        }
    }
}
