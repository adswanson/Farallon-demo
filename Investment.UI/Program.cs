using Investment.Component;
using Investment.Component.Domains.Trading;
using Investment.Presentation.Views;
using Investment.UI.Controls;
using Investment.UI.Services;
using System;
using System.Windows.Forms;
using Utilities.DependencyInjection;
using Utilities.Http;
using Utilities.Json;

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
                .AddHttpClients()
                .AddOptions()
                .AddApplicationDependencies()
                .AddJsonSerializer()
                .AddInvestmentDependencies<LocalXmlDataContextAccessor>();

            using(var container = containerBuilder.Build())
            {
                var mainForm = container.Resolve<MainForm>();
                
                Application.Run(mainForm);
            }
        }

        private static ContainerBuilder AddHttpClients(this ContainerBuilder builder)
        {
            var httpClientBuilder = new HttpClientBuilder();

            httpClientBuilder.AddClient(nameof(IQuoteService), new HttpClientOptions
            {
                BaseAddress = new Uri("https://www.alphavantage.co/")
            });

            builder.AddHttpClientFactory(httpClientBuilder);

            return builder;
        }

        private static ContainerBuilder AddOptions(this ContainerBuilder builder)
        {
            builder.RegisterSingleton(new RemoteQuoteOptions
            {
                ApiKey = "this is an API key"
            });

            return builder;
        }

        private static ContainerBuilder AddApplicationDependencies(this ContainerBuilder builder)
        {
            builder.RegisterSingleton<IApplicationStateAccessor, ApplicationStateAccessor>();
            builder.RegisterSingleton<IPortfoliosView, PortfolioPicker>();
            builder.RegisterSingleton<PortfolioPicker, PortfolioPicker>();
            builder.RegisterSingleton<MainForm, MainForm>();
            builder.RegisterSingleton<TradeHistory, TradeHistory>();
            builder.RegisterSingleton<ProfitsAndLossesReport, ProfitsAndLossesReport>();

            return builder;
        }
    }
}
