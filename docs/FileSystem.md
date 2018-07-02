# LiteX FileSystem Storage
> LiteX.Storage.Local is a storage library which is based on LiteX.Storage.Core and Local FileSystem.

This client library enables working with the Local FileSystem for storing binary/blob data. 

A library to abstract storing files to Microsoft Azure. Small library for manage storage with Azure. A quick setup for Azure Storage.

Wrapper library is just written for the purpose to bring a new level of ease to the developers who deal with Local File System integration with your system.


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


### Coming soon...
* Multiple container or bucker support

