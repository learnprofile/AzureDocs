using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;

namespace ManagingAzureStorage
{
    class Program
    {
        private static string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=prademostorage11;AccountKey=+vGZiiccSdnluBHsKbjLrkHaMq+Htf9hgqPXb1Q1yGt4Y2wholDsftKVRnSxqZ0qEUMBrdf+1MNCbIv5qtOKow==;EndpointSuffix=https://prademostorage11.table.core.windows.net/test";

        private static string storageAccount = "teststorage";

        public static void Main()
        {
            int userInput = -1;
            while (userInput != 0)
            {
                Console.WriteLine("Working with Azure Storage, demo by ");
                Console.WriteLine("Please select any option to continue");
                Console.WriteLine("Press 1 Upload a Blob");
                Console.WriteLine("Press 2 To write message to queue");
                Console.WriteLine("Press 3 To add row in Azure Storage Table");
                Console.WriteLine("Press 0 to Exit");
                userInput = Convert.ToInt32(Console.ReadLine());
                if (userInput == 1)
                {
                    WorkingWithBlob().GetAwaiter().GetResult();
                }
                else if (userInput == 2)
                {
                    createQueue();
                }
                else if (userInput == 3)
                {
                    insertDataToTable();
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }
        }

        private static async Task WorkingWithBlob()
        {
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer cloudBlobContainer = null;
            string sourceFile = null;


            if (CloudStorageAccount.TryParse(Program.storageConnectionString, out storageAccount))
            {
                try
                {
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    cloudBlobContainer = cloudBlobClient.GetContainerReference("container_name" + Guid.NewGuid().ToString());
                    await cloudBlobContainer.CreateAsync();
                    Console.WriteLine("Created container '{0}'", cloudBlobContainer.Name);
                    Console.WriteLine();

                    // Make blob public
                    BlobContainerPermissions permissions = new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    };
                    await cloudBlobContainer.SetPermissionsAsync(permissions);
                    // uploading a file into our blob
                    string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string localFileName = "file_name.txt";
                    sourceFile = Path.Combine(localPath, localFileName);

                    // uploading to blob

                    CloudBlockBlob cb = cloudBlobContainer.GetBlockBlobReference(localFileName);
                    await cb.UploadFromFileAsync(sourceFile);

                    Console.WriteLine("Uploading file to blob '{0}'", localFileName);
                    Console.WriteLine();
                }
                catch (StorageException ex)
                {
                    Console.WriteLine("Something went wrong: {0}", ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid Connection String");
            }
        }
        private static void createQueue()
        {
            // Create the queue client.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Program.storageConnectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference("createdqueue");

            // Create the queue if it doesn't already exist.
            queue.CreateIfNotExists();

            // Create a message and add it to the queue.
            CloudQueueMessage message = new CloudQueueMessage("Prashanth Kumar");
            queue.AddMessage(message);
            Console.WriteLine("Queue created successfully");
        }
        private static void insertDataToTable()
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Program.storageConnectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "student" table.
            CloudTable table = tableClient.GetTableReference("student");

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();

            // Create a new customer entity.
            Student student = new Student("Prashanth", "Kumar");
            student.rollNumber = "145";
            student.marks = "1";

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(student);

            // Execute the insert operation.
            table.Execute(insertOperation);
            Console.WriteLine("Student data inserted successfully");
        }
    }
    public class Student : TableEntity
    {
        public Student(string fName, string lName)
        {
            this.PartitionKey = lName;
            this.RowKey = fName;
        }

        public Student() { }

        public string rollNumber { get; set; }

        public string marks { get; set; }
    }
}
