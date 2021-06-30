using System;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;

namespace FileExplorer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
            CloudFileShare shareFile = fileClient.GetShareReference("unit name or directory");

            if (shareFile.Exists())
            {
                CloudFileDirectory rootDirectory = shareFile.GetRootDirectoryReference();
                CloudFileDirectory directory = rootDirectory.GetDirectoryReference("especific directory");

                if (directory.Exists())
                {
                    CloudFile file = directory.GetFileReference("file_name.extension");

                    if (file.Exists())
                    {
                        Console.WriteLine(file.DownloadTextAsync().Result);
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}
