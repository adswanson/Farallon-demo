using Investment.Component.Presenters;
using Investment.Component.Models;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Investment.Component.Views;
using Investment.UI.Controls;

namespace Investment.UI
{
    public partial class MainForm : Form
    {
        public MainForm(PortfolioPicker portfolioPicker, TradeHistory tradeHistoryControl)
        {
            InitializeComponent();

            tradeHistoryControl.Dock = DockStyle.Fill;

            pnlPortfolioPicker.Controls.Add(portfolioPicker);
            tpTradeHistory.Controls.Add(tradeHistoryControl);
        }
    }
}
