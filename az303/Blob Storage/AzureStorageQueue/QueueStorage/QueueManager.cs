using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueStorage
{
    class QueueManager
    {
        public static void CreateQueueAsync(CloudStorageAccount storageAccount, string queueName)
        {
            try
            {
                Task t = QueueStorage.QueueMethods.CreateQueueAsync(storageAccount, queueName);
                
                t.Wait();

                if (t.IsCompleted)
                    Console.WriteLine("Queue {0} created successfully", queueName);
                else
                    Console.WriteLine("Queue {0} NOT created successfully", queueName);
            }

            catch (StorageException ex)
            {
                QueueStorage.Common.WriteStorageException(ex);
            }

            catch (Exception ex)
            {
                QueueStorage.Common.WriteException(ex);
            }

        }

        public static void SendMessageAsync(CloudStorageAccount storageAccount, string queueName, string message)
        {
            try
            {
                Task t = QueueStorage.QueueMethods.AddMessageAsync(storageAccount, queueName, message);
                
                t.Wait();

                if (t.IsCompleted)
                    Console.WriteLine("Message - {0} - added to queue {1} successfully", message, queueName);
                else
                    Console.WriteLine("Message - {0} - NOT added to queue {1} successfully", message, queueName);
            }

            catch (StorageException ex)
            {
                QueueStorage.Common.WriteStorageException(ex);
            }

            catch (Exception ex)
            {
                QueueStorage.Common.WriteException(ex);
            }

        }

        public static async void PeekMessageAsync(CloudStorageAccount storageAccount, string queueName)
        {

            try
            {

                string msgToPeek = await QueueStorage.QueueMethods.PeekMessageAsync(storageAccount, queueName);

                Console.WriteLine("Message is : {0}", msgToPeek);
            }

            catch (StorageException ex)
            {
                QueueStorage.Common.WriteStorageException(ex);
            }


            catch (Exception ex)
            {
                QueueStorage.Common.WriteException(ex);
            }
        }

        public static void GetMessageAsync(CloudStorageAccount storageAccount, string queueName)
        {
            try
            {
                Task t = QueueStorage.QueueMethods.GetMessageAsync(storageAccount, queueName);

                t.Wait();
            }
            catch (StorageException ex)
            {
                QueueStorage.Common.WriteStorageException(ex);
            }

            catch (Exception ex)
            {
                QueueStorage.Common.WriteException(ex);
            }
        }

        public static void GetMessageCount(CloudStorageAccount storageAccount, string queueName)
        {
            
            try
            {
                int? count = QueueStorage.QueueMethods.GetMessageCount(storageAccount, queueName);

                // Display number of messages.
                Console.WriteLine("Number of messages in queue : " + ((count==null) ? 0 : count) );
            }

            catch (StorageException ex)
            {
                QueueStorage.Common.WriteStorageException(ex);
            }

            catch (Exception ex)
            {
                QueueStorage.Common.WriteException(ex);
            }
        }
    }
}
