using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using static Microsoft.Azure.Management.Fluent.Azure;

namespace AzureResourceGroup.Services
{
    public class AzureSdkService : IAzureSdkService
    {
        public IAzure GetAzureSdk(AzureAdOptions azureAdOptions)
        {
            return GetAzureSdk(azureAdOptions.ClientId, azureAdOptions.ClientSecret,
                azureAdOptions.TenantId, azureAdOptions.SubscriptionId);
        }

        public IAzure GetAzureSdk(string clientId, string clientSecret, string tenantId, string subscriptionId)
        {
            var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(
                clientId, clientSecret, tenantId, AzureEnvironment.AzureGlobalCloud);

            var azure = Azure
                .Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithSubscription(subscriptionId);

            return azure;
        }

        public IAuthenticated GetAuthenticatedSdk(AzureAdOptions azureAdOptions)
        {
            return GetAuthenticatedSdk(azureAdOptions.ClientId, azureAdOptions.ClientSecret,
                azureAdOptions.TenantId, azureAdOptions.SubscriptionId);
        }

        public IAuthenticated GetAuthenticatedSdk(string clientId, string clientSecret, string tenantId, string subscriptionId)
        {
            var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(
                clientId, clientSecret, tenantId, AzureEnvironment.AzureGlobalCloud);

            var azure = Azure
                .Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials);

            return azure as IAuthenticated;
        }
    }
}
