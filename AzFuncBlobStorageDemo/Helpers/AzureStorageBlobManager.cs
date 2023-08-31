using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzFuncBlobStorageDemo.Helpers
{
    public class AzureStorageBlobManager
    {
        

        public async Task< List<CloudBlockBlob>> GetBlockBlobsAsync(CloudBlobDirectory dir)
        {
            var result = new List<CloudBlockBlob>();
            var resultSegment = await dir.ListBlobsSegmentedAsync(currentToken: null);

            var blobItems = resultSegment.Results;
            result.AddRange(from blobItem in blobItems
                            where blobItem is CloudBlockBlob
                            select blobItem as CloudBlockBlob);
            return result;
        }
    }
}
