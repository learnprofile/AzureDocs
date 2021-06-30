using Microsoft.Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueStorage
{
    class Common
    {
        public static void WriteException(Exception ex)
        {
            //Console.WriteLine("Exception thrown. {0}, msg = {1}", ex.Source, ex.Message);
            Console.WriteLine("    Exception thrown. Message = {0}{1}    Strack Trace = {2}", ex.Message, Environment.NewLine, ex.StackTrace);
        }

        public static void WriteStorageException(StorageException ex)
        {
            var requestInformation = ex.RequestInformation;
            Console.WriteLine(requestInformation.HttpStatusMessage);

            // get more details about the exception 
            var information = requestInformation.ExtendedErrorInformation;

            var errorCode = information.ErrorCode;

            var message = string.Format("({0}) {1}",
                errorCode,
                information.ErrorMessage);

            var details = information
                            .AdditionalDetails
                            .Aggregate("", (s, pair) =>
                            {
                                return s + string.Format("{0}={1},", pair.Key, pair.Value);
                            });

            Console.WriteLine(message + " details " + details);

            switch (information.ErrorCode)
            {
                // (409) Conflict  
                // "There is already a lease present."
                case "LeaseAlreadyPresent":
                    // throw a user defined exception
                    //throw;
                    break;

                // (404) Not Found  
                // The specified container does not exist.
                case "ContainerNotFound":
                    // throw a user defined exception
                    //throw;
                    break;
                default:
                    // throw a user defined exception for unknown code
                    //throw;
                    break;
            }
        
        }
    }
}
