# LiteX FileSystem Storage
> LiteX.Storage.Local is a storage library which is based on LiteX.Storage.Core and Local FileSystem.


This client library enables working with the Local FileSystem Storage service for storing binary/blob data. 

Small library to abstract storing files to Local FileSystem. Quick setup for Local FileSystem and very simple wrapper for the Local FileSystem Storage to handle container instantiations. 

Very simple configuration in advanced ways. Purpose of this package is to bring a new level of ease to the developers who deal with Local FileSystem Storage integration with their system.


## Basic Usage

### Install the package

> Install via [Nuget](https://www.nuget.org/packages/LiteX.Storage.Local/).

```Powershell
PM> Install-Package LiteX.Storage.Local
```

##### AppSettings
```js
{  
  //LiteX Local File System Storage settings
  "FileSystemBlobConfig": {
    "Directory": "--- REPLACE WITH YOUR LOCAL FILE SYSTEM DIRECTORY ---",
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
        // 1. Use default configuration from appsettings.json's 'FileSystemBlobConfig'
        services.AddLiteXFileSystemBlobService();

        //OR
        // 2. Load configuration settings using options.
        services.AddLiteXFileSystemBlobService(option =>
        {
            option.Directory = "UploadFolder";
            option.EnableLogging = true;
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var fileSystemBlobConfig = new FileSystemBlobConfig()
        {
            Directory = "",
            EnableLogging = true
        };
        services.AddLiteXFileSystemBlobService(fileSystemBlobConfig);
        
        
        // add logging (optional)
        services.AddLiteXLogging();
    }
}
```

### Sample Usage Example
> Same for all providers. 

For more helpful information about LiteX Storage, Please click [here.](https://github.com/a-patel/LiteXStorage/blob/master/README.md#step-3--use-in-controller-or-business-layer-memo)

