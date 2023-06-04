using Investment.Component.Models;
using Investment.Component.Presenters;
using Investment.Component.Views;
using Investment.UI.Eventing;
using Investment.UI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Investment.UI.Controls
{
    public partial class TradeHistory : UserControl, IPortfolioHistoryView
    {
        private readonly IPortfolioHistoryPresenter _presenter;
        private readonly IApplicationContextAccessor _applicationContextAccessor;
        private readonly BackgroundWorker _changeActivePortfolioWorker;

        private DataTable _tradeHistory;

        private TradeHistory(IPortfolioHistoryPresenter presenter, IApplicationContextAccessor applicationContextAccessor,
            BackgroundWorker changeActivePortfolioWorker)
        {
            _presenter = presenter;
            _applicationContextAccessor = applicationContextAccessor;
            _changeActivePortfolioWorker = changeActivePortfolioWorker;

            _changeActivePortfolioWorker.DoWork += changeActivePortfolioWorker_DoWork;
            _changeActivePortfolioWorker.RunWorkerCompleted += changeActivePortfolioWorker_RunWorkerCompleted;

            InitializeComponent();
        }

        private void TradeHistory_Load(object sender, EventArgs e)
        {
            _presenter.Initialize(this);

            //_applicationContextAccessor.OnActivePortfolioChanged += _changeActivePortfolioWorker.RunWorkerAsync;
            _applicationContextAccessor.RegisterChangePortfolioEventSet(new EventSet<PortfolioChangeEventArgs>
            {
                OnRunAsync = RunAsync,
                //OnRunSync = (args) => _presenter.ChangeActivePortfolio(args.New),
                OnDone = (args) => dgPortfolioTradeHistory.DataSource = _tradeHistory
            });
        }

        private async Task RunAsync(PortfolioChangeEventArgs args)
        {
            await Task.Run(() =>
            {
                _presenter.ChangeActivePortfolio(args.New);
            });
        }

        private void changeActivePortfolioWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgPortfolioTradeHistory.DataSource = _tradeHistory;
        }
        private void changeActivePortfolioWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if(e.Argument is PortfolioChangeEventArgs changeEventArgs)
            {
                _presenter.ChangeActivePortfolio(changeEventArgs.New);
            }          
        }

        public void SetTransactionHistory(IEnumerable<PortfolioTransactionModel> transactions)
        {
            _tradeHistory = ToDataTable(transactions);
        }

        private DataTable ToDataTable(IEnumerable<PortfolioTransactionModel> model)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Ticker", typeof(string));
            dataTable.Columns.Add("Trade Date", typeof(string));
            dataTable.Columns.Add("Buy/Sell", typeof(string));
            dataTable.Columns.Add("Quantity", typeof(string));
            dataTable.Columns.Add("Price", typeof(string));
            dataTable.Columns.Add("Cost", typeof(string));

            foreach (var transaction in model)
            {
                dataTable.Rows.Add(
                    transaction.SymbolName ?? "Undefined",
                    transaction.TradeDate.Date.ToString(),
                    ToTradeTypeDescription(transaction.TransactionType),
                    transaction.PurchaseAmount,
                    transaction.PurchasePrice,
                    transaction.TotalAmount
                );
            }

            return dataTable;
        }

        private string ToTradeTypeDescription(TradeTypeModel tradeTypeModel)
        {
            switch (tradeTypeModel)
            {
                default:
                case TradeTypeModel.Unknown:
                    return "Undefined";
                case TradeTypeModel.Buy:
                    return "Buy";
                case TradeTypeModel.Sell:
                    return "Sell";
            }
        }
    }
}
