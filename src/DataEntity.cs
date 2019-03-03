using Microsoft.WindowsAzure.Storage.Table;

namespace BenneIO.ExternalHttpProxy
{
    public class DataEntity : TableEntity
    {
        public DataEntity()
        {
        }

        public DataEntity(string partitionKey, string rowKey)
            : base(partitionKey, rowKey)
        {
        }
        public byte[] Data { get; set; }
    }
}