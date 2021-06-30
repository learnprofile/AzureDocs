---
services: storage
platforms: dotnet
author: azureashish
---

# Azure Queue Service in .NET

The Queue Service provides reliable messaging for workflow processing and for communication
between loosely coupled components of cloud services. This sample demonstrates how to perform common tasks including
inserting, peeking, getting and deleting queue messages, as well as creating and deleting queues.

Note: This sample uses the .NET 4.7.2 asynchronous programming model to demonstrate how to call the Storage Service using the
storage client libraries asynchronous API's. Calls to the storage service are prefixed by the await keyword.

## Running this sample

This sample can be run using either the Azure Storage Emulator that installs as part of this SDK - or by
updating the App.Config file with your AccountName and Key.
To run the sample using the Storage Emulator (default option):

1. Download and Install the Azure Storage Emulator [here](http://azure.microsoft.com/en-us/downloads/).
2. Start the Azure Storage Emulator (once only) by pressing the Start button or the Windows key and searching for it by typing "Azure Storage Emulator". Select it from the list of applications to start it.
3. Set breakpoints and run the project using F10.

To run the sample using the Storage Service

1. Open the app.config file and comment out the connection string for the emulator (UseDevelopmentStorage=True) and uncomment the connection string for the storage service (AccountName=[]...)
2. Create a Storage Account through the Azure Portal and provide your [AccountName] and [AccountKey] in the App.Config file.
3. Set breakpoints and run the project using F10.