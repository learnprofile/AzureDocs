using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueStorage
{
    class QueueMethods
    {

        /// <summary>
        /// Create a queue for the application to process messages in. 
        /// </summary>
        /// <param name="storageAccount">The storage account</param>
        /// <param name="queueName">The queue name</param>
        public static async Task CreateQueueAsync(CloudStorageAccount storageAccount, string queueName)
        {

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference(queueName);

            await queue.CreateIfNotExistsAsync();

            await queue.ClearAsync();
        }

        /// <summary>
        /// Demonstrate adding a message to a queue
        /// </summary>
        /// <param name="storageAccount">The storage account</param>
        /// <param name="queueName">The queue name</param>
        /// <param name="message">The message to add to the queue</param>
        public static async Task AddMessageAsync(CloudStorageAccount storageAccount, string queueName, string message)
        {

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference(queueName);

            //await queue.CreateIfNotExistsAsync();

            //await queue.ClearAsync();

            CloudQueueMessage msgToSend = new CloudQueueMessage(message);

            await queue.AddMessageAsync(msgToSend);
        }

        /// <summary>
        /// Get number of messages stored in queue
        /// </summary>
        /// <param name="storageAccount">The storage account</param>
        /// <param name="queueName">The queue name</param>
        public static int? GetMessageCount(CloudStorageAccount storageAccount, string queueName)
        {
            
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference(queueName);

            // The FetchAttributes method asks the Queue service to retrieve the queue attributes, including an approximation of message count 
            queue.FetchAttributes();

            // Retrieve the cached approximate message count.
            int? cachedMessageCount = queue.ApproximateMessageCount;

            return cachedMessageCount;
        }

        /// <summary>
        /// Get the next available message without removing it from queue
        /// </summary>
        /// <param name="storageAccount">The storage account</param>
        /// <param name="queueName">The queue name</param>
        public static async Task<string> PeekMessageAsync(CloudStorageAccount storageAccount, string queueName)
        {
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference(queueName);

            //await queue.CreateIfNotExistsAsync();

            //await queue.ClearAsync();
            CloudQueueMessage msgToPeek = await queue.PeekMessageAsync();

            return msgToPeek.AsString;
        }

        public static async Task GetMessageAsync(CloudStorageAccount storageAccount, string queueName)
        {

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference(queueName);

            CloudQueueMessage msg = await queue.GetMessageAsync();

            Console.WriteLine("Message is : {0} Dequeue Count is : {1}", msg.AsString, msg.DequeueCount);

            //await queue.DeleteMessageAsync(msg);
        }

    }
}

