using System;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace QueueExplorer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("ConnectionString"));

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("--process_queue--");

            queue.CreateIfNotExists();
            
            #region Insert queue

            for (int i = 0; i < 500; i++)
            {
                CloudQueueMessage msg = new CloudQueueMessage(string.Format("Operation: {0}", i));
                queue.AddMessage(msg);
                Console.WriteLine("Added new message: {0}",i);
            }

            #endregion
        }
    }
}
