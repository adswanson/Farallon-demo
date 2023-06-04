using Investment.Component.Models;
using Investment.Component.Presenters;
using Investment.UI.Eventing;
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
    public partial class ProfitsAndLossesReport : UserControl, IProfitsAndLossesReportView
    {
        private readonly IProfitsAndLossesPresenter _presenter;
        private readonly IApplicationContextAccessor _applicationContextAccessor;
        private readonly BackgroundWorker _changeActivePortfolioWorker;

        private DataTable _reportTable;

        private ProfitsAndLossesReport(IProfitsAndLossesPresenter profitsAndLossesPresenter,
            IApplicationContextAccessor applicationContextAccessor,
            BackgroundWorker changeActivePortfolioWorker)
        {
            _presenter = profitsAndLossesPresenter;
            _applicationContextAccessor = applicationContextAccessor;
            _changeActivePortfolioWorker = changeActivePortfolioWorker;

            _changeActivePortfolioWorker.DoWork += changeActivePortfolioWorker_DoWork;
            _changeActivePortfolioWorker.RunWorkerCompleted += changeActivePortfolioWorker_RunWorkerCompleted;

            InitializeComponent();
        }

        private void ProfitsAndLossesReport_Load(object sender, EventArgs e)
        {
            _presenter.Initialize(this);
            //_applicationContextAccessor.OnActivePortfolioChanged += _changeActivePortfolioWorker.RunWorkerAsync;

            _applicationContextAccessor.OnActivePortfolioChanged += (args) =>
            {
                dgProfitsAndLosses.DataSource = _reportTable;
            };

            _applicationContextAccessor.OnActivePortfolioChangedAsync += async (args) =>
            {
                await _presenter.ChangeActivePortfolio(args.New);
            };
        }

        private void changeActivePortfolioWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgProfitsAndLosses.DataSource = _reportTable;
        }
        private void changeActivePortfolioWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is PortfolioChangeEventArgs changeEventArgs)
            {
                _presenter.ChangeActivePortfolio(changeEventArgs.New);
            }
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
