using Investment.Presentation.Models;
using Investment.Presentation.Presenters;
using Investment.Presentation.Views;
using Investment.UI.Eventing;
using Investment.UI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Investment.UI.Controls
{
    public partial class TradeHistory : UserControl, IPortfolioHistoryView
    {
        private readonly IPortfolioHistoryPresenter _presenter;
        private readonly IApplicationStateAccessor _applicationStateAccessor;

        private DataTable _tradeHistory;

        private TradeHistory(IPortfolioHistoryPresenter presenter, IApplicationStateAccessor applicationStateAccessor)
        {
            _presenter = presenter;
            _applicationStateAccessor = applicationStateAccessor;

            InitializeComponent();
        }

        private void TradeHistory_Load(object sender, EventArgs e)
        {
            _presenter.Initialize(this);

            _applicationStateAccessor.RegisterChangePortfolioEventSet(new EventSet<PortfolioChangeEventArgs>
            {
                OnRunAsync = OnPortfolioChangeAsync,
                OnDone = (args) => dgPortfolioTradeHistory.DataSource = _tradeHistory
            });
        }

        private async Task OnPortfolioChangeAsync(PortfolioChangeEventArgs args)
        {
            await Task.Run(() =>
            {
                _presenter.ChangeActivePortfolio(args.New);
            });
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
