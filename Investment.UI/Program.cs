using Investment.Component;
using Investment.Component.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities.DependencyInjection;

namespace Investment.UI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var containerBuilder = new ContainerBuilder()
                .RegisterTransient<IPortfolioHistoryView, MainForm>()
                .RegisterTransient<IPortfolioHistoryPresenter, PortfolioHistoryPresenter>()
                .AddInvestmentDependencies();

            using(var container = containerBuilder.Build())
            {
                var presenter = container.Resolve<IPortfolioHistoryPresenter>();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // todo - dependency injection solution
                Application.Run(new MainForm(presenter));
            }


        }
    }
}
