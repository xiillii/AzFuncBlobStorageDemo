using AzFuncBlobStorageDemo.Helpers;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AzFuncBlobStorageDemo
{
    public class FileProcessor
    {
        private readonly AzureStorageBlobOptions azureBlobStorageBlobOptions;
        private readonly AzureStorageBlobOptionsTokenGenerator azureStorageBlobTokenGenerator;

        public FileProcessor(AzureStorageBlobOptions opts, AzureStorageBlobOptionsTokenGenerator tokenGenerator)
        {
            azureBlobStorageBlobOptions = opts;
            azureStorageBlobTokenGenerator = tokenGenerator;
        }

        [FunctionName("FileProcessor")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"FileProcessor Timer trigger function executed at: {DateTime.Now}");

            // find files
            var sasTokoken = azureStorageBlobTokenGenerator.GenerateSasToken(azureBlobStorageBlobOptions.Container);
            var storageCredentials = new StorageCredentials(sasTokoken);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, azureBlobStorageBlobOptions.Accountname, null, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(azureBlobStorageBlobOptions.Container);
            var uploadDirectory = cloudBlobContainer.GetDirectoryReference(azureBlobStorageBlobOptions.PathUpload);

            var processedDirectory = cloudBlobContainer.GetDirectoryReference(azureBlobStorageBlobOptions.PathProcessed);


            


            var manager = new AzureStorageBlobManager();

            var files = await manager.GetBlockBlobsAsync(uploadDirectory);
            foreach (var file in files)
            {
                log.LogInformation($"{file.Uri}");


                

                var ms = new MemoryStream();
                
                await file.DownloadToStreamAsync(ms);

                var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.Name);

                var targetBlob = cloudBlobContainer.GetBlockBlobReference("processed/" + filename);
                targetBlob.Properties.ContentType = file.Properties.ContentType;
                ms.Position = 0;
                await targetBlob.UploadFromStreamAsync(ms);
                await file.DeleteIfExistsAsync();
                
            }

        }
    }
}
