using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net.Http;
using System.Net;
using System;

namespace BenneIO.ExternalHttpProxy
{
    public static class OutboundHttpTrigger
    {
        [FunctionName("OutboundHttpTrigger")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "outbound/{endpoint?}")] HttpRequestMessage req,
            string endpoint)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable cloudTable = tableClient.GetTableReference("data");

            TableOperation retrieveOperation = TableOperation.Retrieve<DataEntity>(req.RequestUri.Host, endpoint ?? "endpoint");
            TableResult retrieveResult = await cloudTable.ExecuteAsync(retrieveOperation);

            if (retrieveResult.Result == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            DataEntity dataEntity = (DataEntity)retrieveResult.Result;
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(dataEntity.Data)
            };

            TableOperation deleteOperation = TableOperation.Delete(dataEntity);
            await cloudTable.ExecuteAsync(deleteOperation);

            return responseMessage;
        }
    }
}
