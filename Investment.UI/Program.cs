using Investment.Component;
using Investment.Component.Presenters;
using Investment.UI.Services;
using System;
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
                .AddApplicationDependencies()
                .AddInvestmentDependencies<LocalXmlDataContextAccessor>();

            using(var container = containerBuilder.Build())
            {
                var presenter = container.Resolve<IPortfolioHistoryPresenter>();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(presenter));
            }
        }

        private static ContainerBuilder AddApplicationDependencies(this ContainerBuilder builder)
        {
            builder.RegisterSingleton<IPortfolioHistoryView, MainForm>();

            return builder;
        }
    }
}
