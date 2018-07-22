# LiteX Kvpbase Storage
> LiteX.Storage.Kvpbase is a storage library which is based on LiteX.Storage.Core and Kvpbase API.

This client library enables working with the Kvpbase Storage Blob service for storing binary/blob data. 

A very simple Wrapper for the Kvpbase Storage to handle container instantiations. 

A library to abstract storing files to Kvpbase. Small library for manage storage with Kvpbase. A quick setup for Kvpbase.

Wrapper library is just written for the purpose to bring a new level of ease to the developers who deal with Kvpbase integration with your system.


## Basic Usage


### Install the package

> Install via [Nuget](https://www.nuget.org/packages/LiteX.Storage.Kvpbase/).

```Powershell
PM> Install-Package LiteX.Storage.Kvpbase
```

##### AppSettings
```js
{  
  //LiteX Kvpbase Storage settings
  "KvpbaseBlobConfig": {
    "KvpbaseApiKey": "--- REPLACE WITH YOUR KVPBASE API KEY ---",
    "KvpbaseContainer": "--- REPLACE WITH YOUR KVPBASE CONTAINER ---",
    "KvpbaseEndpoint": "--- REPLACE WITH YOUR KVPBASE END POINT ---",
    "KvpbaseUserGuid": "--- REPLACE WITH YOUR KVPBASE USERGUID ---",
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
        // 1. Use default configuration from appsettings.json's 'KvpbaseBlobConfig'
        services.AddLiteXKvpbaseBlobService();

        //OR
        // 2. Load configuration settings using options.
        services.AddLiteXKvpbaseBlobService(option =>
        {
            option.KvpbaseApiKey = "";
            option.KvpbaseEndpoint = "";
            option.KvpbaseContainer = "";
            option.KvpbaseUserGuid = "";
            option.EnableLogging = true;
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var kvpbaseBlobConfig = new KvpbaseBlobConfig()
        {
            KvpbaseApiKey = "",
            KvpbaseEndpoint = "",
            KvpbaseContainer = "",
            KvpbaseUserGuid = "",
            EnableLogging = true
        };
        services.AddLiteXKvpbaseBlobService(kvpbaseBlobConfig);
        
        
        // add logging (optional)
        services.AddLiteXLogging();
    }
}
```

### Sample Usage Example
> Same for all providers. 

For more helpful information about LiteX Storage, Please click [here.](https://github.com/a-patel/LiteXStorage/blob/master/README.md#step-3--use-in-controller-or-business-layer-memo)

