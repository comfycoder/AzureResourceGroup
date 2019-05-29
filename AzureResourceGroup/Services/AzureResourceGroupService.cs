using System;
using System.Collections.Generic;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace AzureResourceGroup.Services
{
    public class AzureResourceGroupService : IAzureResourceGroupService
    {
        readonly IAzureSdkService _azureSdkService;

        public AzureResourceGroupService(
            IAzureSdkService azureSdkService
        )
        {
            _azureSdkService = azureSdkService;
        }

        public IEnumerable<IResourceGroup> GetAzureResourceGroups(AzureAdOptions azureAdOptions)
        {
            IAzure azure = _azureSdkService.GetAzureSdk(azureAdOptions);

            var resourceGroups = azure.ResourceGroups.List();

            return resourceGroups;
        }

        public IResourceGroup GetAzureResourceGroup(AzureAdOptions azureAdOptions,
            string resourceGroupName)
        {
            IAzure azure = _azureSdkService.GetAzureSdk(azureAdOptions);

            IResourceGroup resourceGroup = null;

            if (azure.ResourceGroups.Contain(resourceGroupName))
            {
                resourceGroup = azure.ResourceGroups.GetByName(resourceGroupName);
            }

            return resourceGroup;
        }

        public void CreateAzureResourceGroup(AzureAdOptions azureAdOptions,
            string resourceGroupName, string regionName, Dictionary<string, string> tags = null)
        {
            IAzure azure = _azureSdkService.GetAzureSdk(azureAdOptions);

            if (!azure.ResourceGroups.Contain(resourceGroupName))
            {
                var region = AzureRegionService.GetRegion(regionName);

                if (region == null)
                {
                    throw new ApplicationException($"{regionName}");
                }

                var resourceGroup = azure.ResourceGroups
                    .Define(resourceGroupName)
                    .WithRegion(region)
                    .WithTags(tags)
                    .Create();
            }
        }

        public void DeleteResourceGroup(AzureAdOptions azureAdOptions, string resourceGroupName)
        {
            IAzure azure = _azureSdkService.GetAzureSdk(azureAdOptions);

            if (azure.ResourceGroups.Contain(resourceGroupName))
            {
                azure.ResourceGroups.DeleteByName(resourceGroupName);
            }
        }

        public void CreateResourceGroupTag(AzureAdOptions azureAdOptions, string resourceGroupName, string tagKey, string tagValue)
        {
            IAzure azure = _azureSdkService.GetAzureSdk(azureAdOptions);

            if (azure.ResourceGroups.Contain(resourceGroupName))
            {
                var resourceGroup = azure.ResourceGroups.GetByName(resourceGroupName);

                resourceGroup.Update()
                    .WithTag(tagKey, tagValue)
                    .Apply();
            }
            else
            {
                throw new ApplicationException($"ERROR: Create ResourceGroupTag: Unable to find resource group called '{resourceGroupName}'.");
            }
        }

        public void UpdateResourceGroupTag(AzureAdOptions azureAdOptions, string resourceGroupName, string tagKey, string tagValue)
        {
            IAzure azure = _azureSdkService.GetAzureSdk(azureAdOptions);

            if (azure.ResourceGroups.Contain(resourceGroupName))
            {
                var resourceGroup = azure.ResourceGroups.GetByName(resourceGroupName);

                resourceGroup.Update()
                    .WithoutTag(tagKey)
                    .Apply();

                resourceGroup.Update()
                    .WithTag(tagKey, tagValue)
                    .Apply();
            }
            else
            {
                throw new ApplicationException($"ERROR: UpdateResourceGroupTag: Unable to find resource group called '{resourceGroupName}'.");
            }
        }

        public void DeleteResourceGroupTag(AzureAdOptions azureAdOptions, string resourceGroupName, string tagKey, string tagValue)
        {
            IAzure azure = _azureSdkService.GetAzureSdk(azureAdOptions);

            if (azure.ResourceGroups.Contain(resourceGroupName))
            {
                var resourceGroup = azure.ResourceGroups.GetByName(resourceGroupName);

                resourceGroup.Update()
                    .WithoutTag(tagKey)
                    .Apply();
            }
            else
            {
                throw new ApplicationException($"ERROR: DeleteResourceGroupTag: Unable to find resource group called '{resourceGroupName}'.");
            }
        }
    }
}
