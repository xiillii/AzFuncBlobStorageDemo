using System;

namespace AzFuncBlobStorageDemo.Helpers
{
    public class AzureStorageBlobOptions
    {
        public string Accountname { get; set; }
        public string Container { get; set; }
        public string PathOnError { get; set; }
        public string PathUpload { get; set; }
        public string PathProcessed { get; set; }
        public string ConnectionString { get; set; }

        public AzureStorageBlobOptions()
        {
            Accountname = Environment.GetEnvironmentVariable($"{nameof(AzureStorageBlobOptions)}:AccountName");
            ConnectionString = Environment.GetEnvironmentVariable($"{nameof(AzureStorageBlobOptions)}:ConnectionString");
            Container = Environment.GetEnvironmentVariable($"{nameof(AzureStorageBlobOptions)}:Container");
            PathUpload = Environment.GetEnvironmentVariable($"{nameof(AzureStorageBlobOptions)}:PathUpload");
            PathProcessed= Environment.GetEnvironmentVariable($"{nameof(AzureStorageBlobOptions)}:PathProcessed");
            PathOnError = Environment.GetEnvironmentVariable($"{nameof(AzureStorageBlobOptions)}:PathOnError");
        }
    }
}
