using Microsoft.Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueStorage
{
    class StorageAccountManager
    {
        public static CloudStorageAccount GetStorageAccount()
        {
            CloudStorageAccount storageAccount;

            try
            {
                String storageConnectionString = Microsoft.Azure.CloudConfigurationManager.GetSetting("StorageConnectionString");
                //String storageConnectionString = System.Configuration.ConfigurationManager.AppSettings.Get("StorageConnectionString");

                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }

            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }

    }
}
