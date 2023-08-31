using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;

namespace AzFuncBlobStorageDemo.Helpers
{
    public class AzureStorageBlobOptionsTokenGenerator
    {
        private readonly IOptions<AzureStorageBlobOptions> options;

        public AzureStorageBlobOptionsTokenGenerator(IOptions<AzureStorageBlobOptions> opts)
        {
            options = opts;
        }

        public string GenerateSasToken(string containerName, DateTime expiresOn)
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(options.Value.ConnectionString);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

            var permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.List | SharedAccessBlobPermissions.Delete;



            var shareAccessBlobPolicy = new SharedAccessBlobPolicy
            {
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5),
                SharedAccessExpiryTime = expiresOn,
                Permissions = permissions
            };

            var sasContainerToken = cloudBlobContainer.GetSharedAccessSignature(shareAccessBlobPolicy, null);

            return sasContainerToken;
        }

        public string GenerateSasToken(string containerName) => GenerateSasToken(containerName,
                                                                                 DateTime.UtcNow.AddSeconds(30));
    }
}
