using System;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TableExplorer.Entities;

namespace TableExplorer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("ConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();//storage table section
            CloudTable table = tableClient.GetTableReference("class");
            
            #region

            //table.Delete();
           //table.DeleteIfExists();

            Console.WriteLine("Table has been deleted or removed.All info has been deleted.");

            #endregion 

            #region Info of table

            table.CreateIfNotExists();

            var tableNames = tableClient.ListTables();// all tables name

            foreach (CloudTable item in tableNames)
            {
                Console.WriteLine(item.Name);
            }

            #endregion

            #region Insert data

            Teacher teacher = new Teacher("001", "Teachers")
            {
                Name = "Ricardo Celis",
                Assignment = "Programmer"
            };

            Teacher teacher2 = new Teacher("002", "Teachers")
            {
                Name = "Carlos Paredes",
                Assignment = "Tester"
            };

            TableOperation insertData = TableOperation.Insert(teacher);
            TableOperation insertData2 = TableOperation.Insert(teacher2);

            //TODO: Check batch operation
            table.Execute(insertData);
            table.Execute(insertData2);

            Console.WriteLine("Isnert has been process succesfully");

            #endregion

            #region Read data

            TableQuery<Teacher> query = new TableQuery<Teacher>().Where(TableQuery.GenerateFilterCondition("Partition Key", QueryComparisons.GreaterThan, "000"));

            foreach (Teacher item in table.ExecuteQuery(query))
            {
                Console.WriteLine("{0},  {1}\t{2}\t{3}", item.PartitionKey, item.RowKey, item.Name, item.Assignment);
            }

            Console.WriteLine("Excecution succesfully");

            #endregion

            #region Update data

            TableOperation updateData = TableOperation.Retrieve<Teacher>("002[Partition Key]", "Teachers");

            TableResult result = table.Execute(updateData);

            Teacher updateEntity = (Teacher)result.Result;

            if (updateEntity != null)
            {
                updateEntity.Name = "Diseño audiovisual";
                TableOperation updateOperation = TableOperation.Replace(updateEntity);
                table.Execute(updateOperation);
                Console.WriteLine("Entity updated");
            }
            else
            {
                Console.WriteLine("Entity not found");
            }

            #endregion

            #region Delete data

            TableOperation deleteData = TableOperation.Retrieve<Teacher>("002[Partition Key]", "Teachers");

            TableResult resultDelete = table.Execute(updateData);

            Teacher deleteEntity = (Teacher)result.Result;

            if (updateEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                table.Execute(deleteOperation);
                Console.WriteLine("Entity deleted");
            }
            else
            {
                Console.WriteLine("Entity not found");
            }

            #endregion 

            Console.ReadLine();
        }
    }
}
