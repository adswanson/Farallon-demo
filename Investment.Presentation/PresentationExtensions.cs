using Investment.Presentation.Presenters;
using Utilities.DependencyInjection;

namespace Investment.Presentation
{
    public static class PresentationExtensions
    {
        public static ContainerBuilder AddPresentationServices(this ContainerBuilder builder)
        {
            builder.RegisterTransient<IPortfolioHistoryPresenter, PortfolioHistoryPresenter>()
                .RegisterTransient<IPortfoliosPresenter, PortfoliosPresenter>()
                .RegisterTransient<IProfitsAndLossesPresenter, ProfitsAndLossesPresenter>();

            return builder;
        }
    }
}
