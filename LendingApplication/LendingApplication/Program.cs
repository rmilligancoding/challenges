using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LendingApplication;
using LendingApplication.Validators;
using LendingApplication.Loans;
using LendingApplication.Data;
using LendingApplication.Metrics;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton<ILoanValidator, LoanValidator>();
        services.AddSingleton<ILoanStore, LoanStore>();
        services.AddSingleton<ILoanMetricsHelper, LoanMetricsHelper>();
    })
    .Build();

var svc = ActivatorUtilities.CreateInstance<LendingPlatform>(host.Services);

// set up loans
var loanApplications = new LoanApplication[]
{
    new LoanApplication { Amount = 80000, AssetValue = 400000, CreditScore = 500 },
    new LoanApplication { Amount = 1200000, AssetValue = 1400000, CreditScore = 1000 },
    new LoanApplication { Amount = 500000, AssetValue = 1200000, CreditScore = 600 },
    new LoanApplication { Amount = 400000, AssetValue = 1100000, CreditScore = 800 }
};

// process loans
foreach (var loanApplication in loanApplications)
{
    // add loan application and display metrics
    var loanMetrics = svc.WriteLoan(loanApplication);
    Console.WriteLine(loanMetrics.Summmary);
}

await host.RunAsync();
