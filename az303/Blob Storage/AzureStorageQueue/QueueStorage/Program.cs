using Microsoft.Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32 option1 = DisplayMainMenu();
            MainCall(option1);
        }

        /// <summary>
        /// Containing switch to handle which method to execute
        /// <param name="option">Option to perform Queue Operations</param>
        /// </summary>
        private static void MainCall(Int32 option)
        {

            CloudStorageAccount storageAccount = QueueStorage.StorageAccountManager.GetStorageAccount();

            Int32 option1 = 0;

            switch (option)
            {
                
                case 1:

                    QueueStorage.QueueManager.CreateQueueAsync(storageAccount, AppConstants.QueueName);

                    option1 = DisplayMainMenu();
                    MainCall(option1);

                    break;

                case 2:

                    for (int i = 0; i < 10; i++)
                        QueueStorage.QueueManager.SendMessageAsync(storageAccount, AppConstants.QueueName, "This is message " + i);

                    option1 = DisplayMainMenu();
                    MainCall(option1);

                    break;

                case 3:

                    QueueStorage.QueueManager.PeekMessageAsync(storageAccount, AppConstants.QueueName);

                    option1 = DisplayMainMenu();
                    MainCall(option1);

                    break;

                case 4:

                    QueueStorage.QueueManager.GetMessageAsync(storageAccount, AppConstants.QueueName);

                    option1 = DisplayMainMenu();
                    MainCall(option1);

                    break;
                case 5:

                    QueueStorage.QueueManager.GetMessageCount(storageAccount, AppConstants.QueueName);

                    option1 = DisplayMainMenu();
                    MainCall(option1);

                    break;

                case 9:
                    Console.WriteLine("Thanks for using Queue Storage Client.");
                    break;

                default:
                    Console.WriteLine("Invalid Input!!!!!!Re-Enter");
                    option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
            }
        }


        /// <summary>
        /// Display Main menu
        /// </summary>
        private static int DisplayMainMenu()
        {
            Console.WriteLine("***********************");
            Console.WriteLine("  Queue Storage Client Menu  ");
            Console.WriteLine("***********************");
            Console.WriteLine("Select any option:");

            Console.WriteLine("1. Create Queue (async)");
            Console.WriteLine("2. Send message to Queue (async)");
            Console.WriteLine("3. Peek message from Queue (async)");
            Console.WriteLine("4. Get message from Queue (async)");
            Console.WriteLine("5. Get Queue message count");

            Console.WriteLine("9. Exit");
            Console.WriteLine("Please enter your choice : ");


            Int32 option = 0;
            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Some Error Occured");
            }
            return option;
        }


    }
}
