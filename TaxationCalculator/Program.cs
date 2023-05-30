using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaxationCalculator;
using TaxationServices.Interfaces;

Startup.Configure();
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => Startup.ConfigureServices(services))
    .Build();

var taxationCalculator = host.Services.GetService<ITaxationCalculator>()
                         ?? throw new ArgumentNullException(nameof(ITaxationCalculator));

taxationCalculator.Start();