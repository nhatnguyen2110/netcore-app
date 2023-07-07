using Azure.Storage.Blobs;

namespace AzureSupportService
{
    public static class AzureBlobStorageSupport
    {
        public static string CleanFilePath(string filename)
        {
            string cleanRef = filename.Replace("\\", "-");

            cleanRef = cleanRef.Replace("/", "-");
            cleanRef = cleanRef.Replace("<", "(");
            cleanRef = cleanRef.Replace(">", ")");
            cleanRef = cleanRef.Replace("|", "-");
            cleanRef = cleanRef.Replace("?", "-");
            cleanRef = cleanRef.Replace("*", "-");
            cleanRef = cleanRef.Replace(":", "-");
            cleanRef = cleanRef.Replace("\"", "'");
            cleanRef = cleanRef.Replace("\t", "'");

            return cleanRef;
        }
        /// <summary>
        /// Move file to new folder, return new file name if duplicate
        /// </summary>
        /// <param name="ConnectionStr"></param>
        /// <param name="ContainerNameFrom"></param>
        /// <param name="FileNameFrom"></param>
        /// <param name="ContainerNameTo"></param>
        /// <param name="FileNameTo"></param>
        /// <returns></returns>
        public static async Task<string> DuplicateFileInAzureAsync(String ConnectionStr, String ContainerNameFrom, String FileNameFrom, String ContainerNameTo, String FileNameTo)
        {
            var origTarget = FileNameTo;
            var optionalSequence = 1;
            var ext = "";

            if (FileExists(ConnectionStr, ContainerNameFrom, FileNameFrom).Result == true)
            {
                while (FileExists(ConnectionStr, ContainerNameTo, FileNameTo).Result == true)
                {
                    // The target already exists, need to change the name somehow.
                    if (FileNameTo.LastIndexOf(".") != -1)
                    {
                        ext = FileNameTo.Split(".")[FileNameTo.Split(".").Length - 1];
                        FileNameTo = FileNameTo.Substring(0, FileNameTo.LastIndexOf(".")) + optionalSequence.ToString() + "." + ext;
                    }
                    else
                    {
                        FileNameTo += optionalSequence.ToString();
                    }
                    optionalSequence++;
                }

                await UploadFileToAzureAsync(ConnectionStr, ContainerNameTo, FileNameTo, DownloadFileFromAzure(ConnectionStr, ContainerNameFrom, FileNameFrom).Result);
            }

            return FileNameTo;
        }

        public static async Task UploadFileToAzureAsync(String ConnectionStr, String ContainerName, String FileName, Stream FileData)
        {
            // Get a reference to a container and then create it
            BlobContainerClient container = new BlobContainerClient(ConnectionStr, ContainerName);

            // Get a reference to a blob named "sample-file" in a container named "sample-container"
            BlobClient blob = container.GetBlobClient(FileName);

            // Upload file from stream
            await blob.UploadAsync(FileData);
        }

        public static void UploadBase64ImageToAzure(string ConnectionStr, string ContainerName, string FileName, string Base64Image)
        {
            // Get a reference to a container and then create it
            BlobContainerClient container = new BlobContainerClient(ConnectionStr, ContainerName);

            // Get a reference to a blob named "sample-file" in a container named "sample-container"
            BlobClient blob = container.GetBlobClient(FileName);

            //Convert from string to memory stream
            byte[] imageBytes = Convert.FromBase64String(Base64Image);
            Stream imageStream = new MemoryStream(imageBytes);
            imageStream.Seek(0, SeekOrigin.Begin);

            // Upload file from stream
            blob.Upload(imageStream);
        }

        public static async Task<MemoryStream> DownloadFileFromAzure(String ConnectionStr, String ContainerName, String FileName)
        {
            // Get a reference to a container
            BlobContainerClient container = new BlobContainerClient(ConnectionStr, ContainerName);

            // Get reference to blob
            var client = container.GetBlobClient(FileName);

            // Download to Memory Stream
            MemoryStream ms = new MemoryStream();
            await client.DownloadToAsync(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
        public static async void DeleteFileFromAzure(String ConnectionStr, String ContainerName, String FileName)
        {
            // Get a reference to a container
            BlobContainerClient container = new BlobContainerClient(ConnectionStr, ContainerName);

            // Get reference to blob
            var client = container.GetBlobClient(FileName);

            // Delete blob
             await client.DeleteIfExistsAsync();  
        }

        public static void DownloadFileFromAzure(String ConnectionStr, String ContainerName, String FileName, Stream ms)
        {
            // Get a reference to a container
            BlobContainerClient container = new BlobContainerClient(ConnectionStr, ContainerName);

            // Get reference to blob
            var client = container.GetBlobClient(FileName);

            // Download to Memory Stream
            client.DownloadTo(ms);
            ms.Seek(0, SeekOrigin.Begin);
        }


        public static async Task<MemoryStream> DownloadFileFromAzureAsync(String ConnectionStr, String ContainerName, String FileName)
        {

            MemoryStream ms = new MemoryStream();

            BlobContainerClient container = new BlobContainerClient(ConnectionStr, ContainerName);

            // Get reference to blob
            var client = container.GetBlobClient(FileName);

            // Download to Memory Stream
            await client.DownloadToAsync(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public static async Task DownloadFileFromAzureAsync(String ConnectionStr, String ContainerName, String FileName, Stream ms)
        {
            BlobContainerClient container = new BlobContainerClient(ConnectionStr, ContainerName);

            // Get reference to blob
            var client = container.GetBlobClient(FileName);

            // Download to Memory Stream
            await client.DownloadToAsync(ms);
            ms.Seek(0, SeekOrigin.Begin);
        }

        public static async Task<bool> FileExists(String ConnectionStr, String ContainerName, String FileName)
        {
            try
            {
                BlobContainerClient container = new BlobContainerClient(ConnectionStr, ContainerName);

                // Get reference to blob
                var client = container.GetBlobClient(FileName);

                return await client.ExistsAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
