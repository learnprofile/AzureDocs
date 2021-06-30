using System;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace QueueHandler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("ConnectionString"));

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("--process_queue--");
            CloudQueueMessage peekedMsg = queue.PeekMessage();//get last queue message

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("--contenedor_de_registros--");
            blobContainer.CreateIfNotExists();

            //Can not get all message at once.Need to use batch.
            foreach (CloudQueueMessage item in queue.GetMessages(20, TimeSpan.FromSeconds(100)))
            {
                var path = string.Format(@"c:\\Temporal\log{0}.txt", item.Id);

                using (TextWriter tempFile = File.CreateText(path))
                {
                    var msg = queue.GetMessage().AsString;
                    
                    tempFile.WriteLine(msg);
                    Console.WriteLine("CreatedFile");
                }
                    
                using (var fileStream = File.OpenRead(path))
                {
                    CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(string.Format("log{0}", item.Id));
                    blockBlob.UploadFromStream(fileStream);
                    Console.WriteLine("Blob created");
                }

                queue.DeleteMessage(item);
            }

            Console.ReadLine();
        }
    }
}
