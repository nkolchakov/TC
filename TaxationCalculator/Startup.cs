using Microsoft.Extensions.DependencyInjection;
using TaxationServices.Handlers;
using TaxationServices.Interfaces;
using TaxationServices;
using System.Globalization;
using Microsoft.Extensions.Logging;
using TaxationServices.IO.Interfaces;
using TaxationServices.IO;

namespace TaxationCalculator
{
    /// <summary>
    /// Use this class to boostrap some configurations.
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// Bootstrap with custom configuration
        /// </summary>
        public static void Configure()
        {
            var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.CurrencySymbol = TaxMessages.CURRENCY_SYMBOL;
            CultureInfo.DefaultThreadCurrentCulture = culture;
        }

        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITaxationService, TaxationService>();
            services.AddSingleton<ITaxUtils, TaxUtils>();

            // handlers 
            services.AddSingleton<IReader, ConsoleReader>();
            services.AddSingleton<IncomeTaxHandler>();
            services.AddSingleton<SocialContributionsHandler>();
            services.AddSingleton<TaxHandler, TaxationAggregator>(sp =>
                new TaxationAggregator(new List<TaxHandler>() {
                    // chain the handlers
                    sp.GetService<IncomeTaxHandler>(),
                    sp.GetService<SocialContributionsHandler>()
                },
                sp.GetService<ITaxUtils>()));

            services.AddSingleton<ITaxationCalculator, TaxationServices.TaxationCalculator>();

            return services;
        }
    }
}
