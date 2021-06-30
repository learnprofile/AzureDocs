using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueStorage
{

    public static class AppConstants
    {
        private static string _storageConnectionString;
        private static string _queueName;


        public static String StorageConnectionString
        {
            get
            {
                if (_storageConnectionString == null)
                    //_storageConnectionString = System.Configuration.ConfigurationManager.AppSettings.Get("StorageConnectionString");
                _storageConnectionString = Microsoft.Azure.CloudConfigurationManager.GetSetting("StorageConnectionString");

                return _storageConnectionString;

            }
        }

        
        public static String QueueName
        {
            get
            {
                if (_queueName == null)
                    //_queueName = System.Configuration.ConfigurationManager.AppSettings.Get("queueName");
                    _queueName = Microsoft.Azure.CloudConfigurationManager.GetSetting("queueName");

                return _queueName;

            }
        }
    }
}