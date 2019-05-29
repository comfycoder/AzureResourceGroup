using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureResourceGroup.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AzureResourceGroup
{
    public class HttpTrigger
    {
        readonly IConfiguration _config;
        readonly AzureAdOptions _azureAdOptions;
        IAzureResourceGroupService _azureResourceGroupService;

        public HttpTrigger(
            IConfiguration config,
            IOptions<AzureAdOptions> azureAdOptionsAccessor,
            IAzureResourceGroupService azureResourceGroupService)
        {
            _config = config;
            _azureAdOptions = azureAdOptionsAccessor.Value;
            _azureResourceGroupService = azureResourceGroupService;
        }

        [FunctionName("HttpTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var resourceGroupName = "RG-MM-MyWebApp";

            var resourceGroup = _azureResourceGroupService.GetAzureResourceGroup(_azureAdOptions, resourceGroupName);

            if (resourceGroup != null)
            {
                return new OkObjectResult(resourceGroup);
            }
            else
            {
                return new OkObjectResult("");
            }            

            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //return name != null
            //    ? (ActionResult)new OkObjectResult($"Hello, {name}")
            //    : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
