# LiteX Google Cloud Storage
> LiteX.Storage.Google is a storage library which is based on LiteX.Storage.Core and Google Cloud API.

This client library enables working with the Google Cloud Storage (Blob) service for storing binary/blob data. 

Small library to abstract storing files to Google Cloud. Quick setup for Google Cloud and very simple wrapper for the Google Cloud to handle bucket instantiations. 

Very simple configuration in advanced ways. Purpose of this package is to bring a new level of ease to the developers who deal with Google Cloud Storage integration with their system.



## Basic Usage


### Install the package

> Install via [Nuget](https://www.nuget.org/packages/LiteX.Storage.Google/).

```Powershell
PM> Install-Package LiteX.Storage.Google
```

##### AppSettings
```js
{  
  //LiteX Google Storage settings
  "GoogleCloudBlobConfig": {
    "GoogleProjectId": "--- REPLACE WITH YOUR GOOGLE PROJECT ID ---",
    "GoogleJsonAuthPath": "--- REPLACE WITH YOUR GOOGLE JSON AUTH PATH ---",
    "GoogleBucketName": "--- REPLACE WITH YOUR GOOGLE BUCKET NAME ---",
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
        // 1. Use default configuration from appsettings.json's 'GoogleCloudBlobConfig'
        services.AddLiteXGoogleCloudBlobService();

        //OR
        // 2. Load configuration settings using options.
        services.AddLiteXGoogleCloudBlobService(option =>
        {
            option.GoogleProjectId = "";
            option.GoogleJsonAuthPath = "";
            option.GoogleBucketName = "";
            option.EnableLogging = true;
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var googleCloudBlobConfig = new GoogleCloudBlobConfig()
        {
            GoogleProjectId = "",
            GoogleJsonAuthPath = "",
            GoogleBucketName = "",
            EnableLogging = true
        };
        services.AddLiteXGoogleCloudBlobService(googleCloudBlobConfig);
        
        
        // add logging (optional)
        services.AddLiteXLogging();
    }
}
```

### Sample Usage Example
> Same for all providers. 

For more helpful information about LiteX Storage, Please click [here.](https://github.com/a-patel/LiteXStorage/blob/master/README.md#step-3--use-in-controller-or-business-layer-memo)

