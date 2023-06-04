using System.Windows.Forms;
using Investment.UI.Controls;

namespace Investment.UI
{
    public partial class MainForm : Form
    {
        public MainForm(PortfolioPicker portfolioPicker,
            TradeHistory tradeHistoryControl,
            ProfitsAndLossesReport profitsAndLossesReport)
        {
            InitializeComponent();

            tradeHistoryControl.Dock = DockStyle.Fill;
            profitsAndLossesReport.Dock = DockStyle.Fill;

            pnlPortfolioPicker.Controls.Add(portfolioPicker);
            tpTradeHistory.Controls.Add(tradeHistoryControl);
            tpProfitsAndLossesReport.Controls.Add(profitsAndLossesReport);
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            // Workaround for tab control lazy-loading tab pages
            for(var i = 0; i < tcSubPages.TabPages.Count; i++)
            {
                tcSubPages.SelectedIndex = i;
            }

            tcSubPages.SelectedIndex = 0;
        }
    }
}
