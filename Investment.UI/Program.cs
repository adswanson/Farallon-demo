using Investment.Component;
using Investment.Component.Presenters;
using Investment.Component.Views;
using Investment.UI.Controls;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var containerBuilder = new ContainerBuilder()
                .AddApplicationDependencies()
                .AddInvestmentDependencies<LocalXmlDataContextAccessor>();

            using(var container = containerBuilder.Build())
            {
                var mainForm = container.Resolve<MainForm>();
                var presenter = container.Resolve<IPortfolioHistoryPresenter>();

                Application.Run(mainForm);
            }
        }

        private static ContainerBuilder AddApplicationDependencies(this ContainerBuilder builder)
        {
            builder.RegisterSingleton<IPortfolioHistoryView, MainForm>();
            builder.RegisterSingleton<IPortfoliosView, PortfolioPicker>();
            builder.RegisterSingleton<PortfolioPicker, PortfolioPicker>();
            builder.RegisterSingleton<MainForm, MainForm>();

            return builder;
        }
    }
}
