using AzureResourceGroup.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

[assembly: FunctionsStartup(typeof(AzureResourceGroup.Startup))]
namespace AzureResourceGroup
{
    // Use dependency injection in .NET Azure Functions
    // https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddHttpClient();

            //builder.Services.AddSingleton((s) => {
            //    return new CosmosClient(Environment.GetEnvironmentVariable("COSMOSDB_CONNECTIONSTRING"));
            //});

            builder.Services.AddOptions();

            // https://blog.jongallant.com/2018/01/azure-function-config/

            var basePath = Directory.GetCurrentDirectory();

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var configSection = config.GetSection("AzureAd");

            builder.Services.Configure<AzureAdOptions>(configSection);

            builder.Services.AddSingleton<IAzureSdkService, AzureSdkService>();

            builder.Services.AddSingleton<IAzureResourceGroupService, AzureResourceGroupService>();
        }
    }
}
