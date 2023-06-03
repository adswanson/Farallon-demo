using Investment.Component.Presenters;
using Investment.Component.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Investment.UI
{
    public partial class MainForm : Form, IPortfolioHistoryView
    {
        private readonly IPortfolioHistoryPresenter _presenter;

        private int? _activePortfolioId;
        public int? ActivePortfolioId
        {
            get
            {
                return _activePortfolioId;
            }
            set
            {
                // todo- change portfolio logic
                _activePortfolioId = value;
                _presenter.ChangeActivePortfolio(value);
            }
        }

        public MainForm(IPortfolioHistoryPresenter portfolioHistoryPresenter)
        {
            InitializeComponent();

            _presenter = portfolioHistoryPresenter;
            portfolioHistoryPresenter.Initialize(this);

            //todo - portofolio selector component
            portfolioHistoryPresenter.ChangeActivePortfolio(1);
        }

        public void SetTransactionHistory(IEnumerable<PortfolioTransactionModel> transactions)
        {
            dgPortfolioTradeHistory.DataSource = transactions.ToDataTable();
        }
    }

    public static class ModelExtensions
    {
        public static DataTable ToDataTable(this IEnumerable<PortfolioTransactionModel> model)
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
                    transaction.TransactionType,
                    transaction.PurchaseAmount,
                    transaction.PurchasePrice,
                    transaction.TotalAmount
                );
            }

            return dataTable;
        }

        public static string ToTradeTypeDescription(this TradeTypeModel tradeTypeModel)
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
