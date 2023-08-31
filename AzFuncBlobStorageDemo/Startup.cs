using AzFuncBlobStorageDemo.Helpers;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzFuncBlobStorageDemo.Startup))]
namespace AzFuncBlobStorageDemo
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<AzureStorageBlobOptions, AzureStorageBlobOptions>();
            builder.Services.AddSingleton<AzureStorageBlobOptionsTokenGenerator, AzureStorageBlobOptionsTokenGenerator>();
        }
    }
}
