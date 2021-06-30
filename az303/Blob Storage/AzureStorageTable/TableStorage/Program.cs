using System;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace TableStorage
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var table = GetTableReference();

            CreateTable(table);

            InsertSingleRecord(table);

            InsertMultipleRecords(table);

            GetAllRecords(table);

            UpdateTable(table);

            DeleteRecord(table);

            Console.WriteLine();
        }

        private static CloudTable GetTableReference()
        {
            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            var tableClient = storageAccount.CreateCloudTableClient();

            return tableClient.GetTableReference("orders");
        }

        private static void CreateTable(CloudTable table)
        {
            table.CreateIfNotExists();
        }

        private static void InsertSingleRecord(CloudTable table)
        {
            var order = new OrderEntity("Prashanth", "20180404")
            {
                OrderNumber = "123",
                ShippedDate = Convert.ToDateTime("1/25/2020"),
                Status = "shipped"
            };

            var insertOperation = TableOperation.Insert(order);
            table.Execute(insertOperation);
        }

        private static void InsertMultipleRecords(CloudTable table)
        {
            var batchOperation = new TableBatchOperation();

            var newOrder1 = new OrderEntity("Prashanth", "20180405")
            {
                OrderNumber = "111",
                ShippedDate = Convert.ToDateTime("2018/04/07"),
                Status = "pending"
            };
            var newOrder2 = new OrderEntity("Prashanth", "20180406")
            {
                OrderNumber = "222",
                ShippedDate = Convert.ToDateTime("2018/04/08"),
                Status = "open"
            };
            var newOrder3 = new OrderEntity("Prashanth", "20180407")
            {
                OrderNumber = "333",
                ShippedDate = Convert.ToDateTime("2018/04/09"),
                Status = "shipped"
            };

            batchOperation.Insert(newOrder1);
            batchOperation.Insert(newOrder2);
            batchOperation.Insert(newOrder3);

            table.ExecuteBatch(batchOperation);
        }

        private static void GetAllRecords(CloudTable table)
        {
            var query = new TableQuery<OrderEntity>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Prashanth"));

            foreach (var entity in table.ExecuteQuery(query))
            {
                Console.WriteLine(
                    $"PartitionKey: {entity.PartitionKey}, RowKey: {entity.RowKey}, Status: {entity.Status}");
            }
        }

        private static void UpdateTable(CloudTable table)
        {
            var updateEntity = RetrieveEntity("Prashanth", "20180405", table);

            if (updateEntity != null)
            {
                updateEntity.Status = "shipped";
                updateEntity.ShippedDate = Convert.ToDateTime("2018/04/15");
                var insertOrReplaceOperation = TableOperation.InsertOrReplace(updateEntity);
                table.Execute(insertOrReplaceOperation);
            }
        }

        private static void DeleteRecord(CloudTable table)
        {
            var deleteEntity = RetrieveEntity("Prashanth", "20180405", table);

            var deleteOperation = TableOperation.Delete(deleteEntity);
            table.Execute(deleteOperation);

            Console.WriteLine("Entity deleted.");
        }

        private static OrderEntity RetrieveEntity(string customerName, string orderDate, CloudTable table)
        {
            var retrieveOperation = TableOperation.Retrieve<OrderEntity>(customerName, orderDate);
            var retrievedResult = table.Execute(retrieveOperation);

            return (OrderEntity) retrievedResult.Result;
        }
    }
}