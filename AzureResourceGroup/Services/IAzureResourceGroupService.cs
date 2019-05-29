using Microsoft.Azure.Management.ResourceManager.Fluent;
using System.Collections.Generic;

namespace AzureResourceGroup.Services
{
    public interface IAzureResourceGroupService
    {
        IEnumerable<IResourceGroup> GetAzureResourceGroups(AzureAdOptions azureAdOptions);

        IResourceGroup GetAzureResourceGroup(AzureAdOptions azureAdOptions, string rgName);

        void CreateAzureResourceGroup(AzureAdOptions azureAdOptions, string resourceGroupName, string regionName, Dictionary<string, string> tags = null);

        void DeleteResourceGroup(AzureAdOptions azureAdOptions, string resourceGroupName);

        void CreateResourceGroupTag(AzureAdOptions azureAdOptions, string resourceGroupName, string tagKey, string tagValue);

        void UpdateResourceGroupTag(AzureAdOptions azureAdOptions, string resourceGroupName, string tagKey, string tagValue);

        void DeleteResourceGroupTag(AzureAdOptions azureAdOptions, string resourceGroupName, string tagKey, string tagValue);
    }
}
