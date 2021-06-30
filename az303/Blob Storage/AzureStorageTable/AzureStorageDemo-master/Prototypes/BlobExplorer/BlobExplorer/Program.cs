using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using System;

namespace BlobExplorer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var blobClient = storageAccount.CreateCloudBlobClient();
            var cloudContainer = blobClient.GetContainerReference("--nombre del contenedor del blobcontainer--");

            //cloudContainer.CreateIfNotExists();
            //cloudContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            //Para crear un nuevo Blob
            CloudBlockBlob newBlob = cloudContainer.GetBlockBlobReference("foto.jpg");//es el nombre de como se mostrara en el explorer

            using (var fileStream = System.IO.File.OpenRead("C:\\fotoUp.jpg")) //foto a subir
            {
                newBlob.UploadFromStream(fileStream);
            }

            Console.WriteLine("Container ready");
            Console.ReadLine();
        }
    }
}
