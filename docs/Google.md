# LiteX Google Cloud Storage
> LiteX.Storage.Google is a storage library which is based on LiteX.Storage.Core and Google Cloud API.

This client library enables working with the Google Cloud Storage Blob service for storing binary/blob data. 

A very simple Wrapper for the Google Cloud Storage to handle container instantiations. 

A library to abstract storing files to Google Cloud. Small library for manage storage with Google Cloud. A quick setup for Google Cloud Storage.

Wrapper library is just written for the purpose to bring a new level of ease to the developers who deal with Google Cloud integration with your system.


## Basic Usage


### Install the package

> Install via [Nuget](https://www.nuget.org/packages/LiteX.Storage.Google/).

```Powershell
PM> Install-Package LiteX.Storage.Google
```

##### AppSettings
```js
{  
  //LiteX Azure Storage settings
  "AzureBlobConfig": {
    "AzureBlobStorageConnectionString": "--- REPLACE WITH YOUR AZURE CONNECTION STRING ---",
    "AzureBlobStorageContainerName": "--- REPLACE WITH YOUR AZURE CONTAINER NAME ---",
    "AzureBlobStorageEndPoint": "--- REPLACE WITH YOUR AZURE END POINT ---",
    "EnableLogging": true
  }
}
```

##### Configure Startup Class
```cs
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // 1. Use default configuration from appsettings.json's 'AzureBlobConfig'
        services.AddLiteXAzureBlobService();

        //OR
        // 2. Load configuration settings using options.
        services.AddLiteXAzureBlobService(option =>
        {
            option.AzureBlobStorageConnectionString = "";
            option.AzureBlobStorageContainerName = "";
            option.AzureBlobStorageEndPoint = "";
            option.EnableLogging = true;
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var azureBlobConfig = new AzureBlobConfig()
        {
            AzureBlobStorageConnectionString = "",
            AzureBlobStorageContainerName = "",
            AzureBlobStorageEndPoint = "",
            EnableLogging = true
        };
        services.AddLiteXAzureBlobService(azureBlobConfig);
        
        
        // add logging (optional)
        services.AddLiteXLogging();
    }
}
```

### Sample Usage Example
> Same for all providers. 

For more helpful information about LiteX Storage, Please click [here.](https://github.com/a-patel/LiteXStorage/blob/master/README.md#step-3--use-in-controller-or-business-layer-memo)


### Coming soon...
* Multiple container or bucker support

