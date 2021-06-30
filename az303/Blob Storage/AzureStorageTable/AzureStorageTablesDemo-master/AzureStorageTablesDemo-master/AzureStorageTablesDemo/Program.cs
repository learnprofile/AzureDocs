using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureStorageTablesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storedAccount = CloudStorageAccount.Parse
                                                (CloudConfigurationManager.GetSetting("DefaultEndpointsProtocol=https;AccountName=simplilearnstg5aa;AccountKey=U4NJL7RMnHZSN/UO6C3ExGbnZsSIQmFO0wO2dFDOtp/hlBsMlfaWfctIGBDUYAGm+2z1keO5mKuRz04Rsu2FFQ==;EndpointSuffix=core.windows.net"));
            CloudTableClient tableClient = storedAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("customers");
            table.CreateIfNotExists();
            Console.WriteLine("Table customers is ready!");

            //Insert 
            //CustomerEntity customer1 = new CustomerEntity("AP", "A01");
            //customer1.LastName = "Mark";
            //customer1.FirstName = "wines";
            //customer1.Email = "markl@gmail.com";
            //customer1.PhoneNumber = "9100945142";


            //TableOperation insertOperation =  TableOperation.Insert(customer1);
            //table.Execute(insertOperation);
            //Console.WriteLine("Row Added!");

            //TableBatchOperation batchOperation = new TableBatchOperation();
            //CustomerEntity customer1 = new CustomerEntity("MH", "A01");
            //customer1.LastName = "Mark";
            //customer1.FirstName = "wines";
            //customer1.Email = "markl@gmail.com";
            //customer1.PhoneNumber = "9100945142";

            //CustomerEntity customer2 = new CustomerEntity("MH", "A02");
            //customer2.LastName = "Mark";
            //customer2.FirstName = "wines";
            //customer2.Email = "markl@gmail.com";
            //customer2.PhoneNumber = "9100945142";


            //CustomerEntity customer3 = new CustomerEntity("MH", "A03");
            //customer3.LastName = "Mark";
            //customer3.FirstName = "wines";
            //customer3.Email = "markl@gmail.com";
            //customer3.PhoneNumber = "9100945142";

            //CustomerEntity customer4 = new CustomerEntity("MH", "A04");
            //customer4.LastName = "Mark";
            //customer4.FirstName = "wines";
            //customer4.Email = "markl@gmail.com";
            //customer4.PhoneNumber = "9100945142";


            //batchOperation.Insert(customer1);
            //batchOperation.Insert(customer2);
            //batchOperation.Insert(customer3);
            //batchOperation.Insert(customer4);

            //table.ExecuteBatch(batchOperation);

            TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "MH"));
            foreach (var item in table.ExecuteQuery(query))
            {
                Console.WriteLine($"{ item.FirstName }{item.LastName}{item.PhoneNumber}");
            }
            Console.WriteLine("Completed");
            Console.ReadLine();

        }

        public class CustomerEntity:TableEntity
        {
            public CustomerEntity(string state,string customerId)
            {
                this.PartitionKey = state;
                this.RowKey = customerId;
            }
            public CustomerEntity()
            {

            }

            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}
