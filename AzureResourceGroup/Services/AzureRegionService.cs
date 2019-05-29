using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace AzureResourceGroup.Services
{
    public static class AzureRegionService
    {
        public static Region GetRegion(string regionName)
        {
            Region region = null;

            if (!string.IsNullOrWhiteSpace(regionName))
            {
                regionName = regionName.Replace(" ", "").ToLower();
            }

            switch (regionName)
            {
                case "centralus":
                    region = Region.USCentral;
                    break;

                case "eastus":
                    region = Region.USEast;
                    break;

                case "eastus2":
                    region = Region.USEast2;
                    break;

                case "northcentralus":
                    region = Region.USNorthCentral;
                    break;

                case "southcentralus":
                    region = Region.USSouthCentral;
                    break;

                case "westus":
                    region = Region.USWest;
                    break;

                case "westus2":
                    region = Region.USWest2;
                    break;

                case "westcentralus":
                    region = Region.USWestCentral;
                    break;
            }

            return region;
        }
    }
}
