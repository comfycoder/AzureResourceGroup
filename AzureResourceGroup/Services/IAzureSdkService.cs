using Microsoft.Azure.Management.Fluent;
using static Microsoft.Azure.Management.Fluent.Azure;

namespace AzureResourceGroup.Services
{
    public interface IAzureSdkService
    {
        IAzure GetAzureSdk(AzureAdOptions azureAdOptions);

        IAzure GetAzureSdk(string clientId, string clientSecret, string tenantId, string subscriptionId);

        IAuthenticated GetAuthenticatedSdk(AzureAdOptions azureAdOptions);

        IAuthenticated GetAuthenticatedSdk(string clientId, string clientSecret, string tenantId, string subscriptionId);
    }
}
