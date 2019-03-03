using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net.Http;
using System;

namespace BenneIO.ExternalHttpProxy
{
    public static class InboundHttpTrigger
    {
        [FunctionName("InboundHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "inbound/{endpoint?}")] HttpRequestMessage req,
            string endpoint)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable cloudTable = tableClient.GetTableReference("data");

            HttpMessageContent content = new HttpMessageContent(req);
            byte[] data;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				await content.CopyToAsync(memoryStream);
				data = memoryStream.ToArray();
			}

            DataEntity dataEntity = new DataEntity(req.RequestUri.Host, endpoint ?? "endpoint")
            {
                Data = data
            };

            TableOperation insertOperation = TableOperation.InsertOrReplace(dataEntity);
            await cloudTable.ExecuteAsync(insertOperation);

            return new OkResult();
        }
    }
}
