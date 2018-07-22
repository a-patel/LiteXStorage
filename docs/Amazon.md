# LiteX Amazon Storage
> LiteX.Storage.Amazon is a storage library which is based on LiteX.Storage.Core and Amazon (AWS) S3 API.

This client library enables working with the Amazon S3 Storage (Blob) service for storing binary/blob data. 

Small library to abstract storing files to Amazon S3. Quick setup for Amazon S3 and very simple wrapper for the Amazon S3 to handle bucket instantiations. 

Very simple configuration in advanced ways. Purpose of this package is to bring a new level of ease to the developers who deal with Amazon S3 integration with their system.



## Basic Usage


### Install the package

> Install via [Nuget](https://www.nuget.org/packages/LiteX.Storage.Amazon/).

```Powershell
PM> Install-Package LiteX.Storage.Amazon
```

##### AppSettings
```js
{  
  //LiteX Amazon Storage settings
  "AmazonBlobConfig": {
    "AmazonAwsAccessKeyId": "--- REPLACE WITH YOUR AMAZON ACCESS KEY ID ---",
    "AmazonAwsSecretAccessKey": "--- REPLACE WITH YOUR AMAZON SECRET ACCESS KEY ---",
    "AmazonRegion": "--- REPLACE WITH YOUR AMAZON REGION ---",
    "AmazonBucketName": "--- REPLACE WITH YOUR AZURE AMAZON BUCKET NAME ---",
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
        // 1. Use default configuration from appsettings.json's 'AmazonBlobConfig'
        services.AddLiteXAmazonBlobService();

        //OR
        // 2. Load configuration settings using options.
        services.AddLiteXAmazonBlobService(option =>
        {
            option.AmazonAwsSecretAccessKey = "";
            option.AmazonAwsAccessKeyId = "";
            option.AmazonBucketName = "";
            option.AmazonRegion = "";
            option.EnableLogging = true;
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var amazonBlobConfig = new AmazonBlobConfig()
        {
            AmazonAwsSecretAccessKey = "",
            AmazonAwsAccessKeyId = "",
            AmazonBucketName = "",
            AmazonRegion = "",
            EnableLogging = true
        };
        services.AddLiteXAmazonBlobService(amazonBlobConfig);


        // add logging (optional)
        services.AddLiteXLogging();
    }
}
```

### Sample Usage Example
> Same for all providers. 

For more helpful information about LiteX Storage, Please click [here.](https://github.com/a-patel/LiteXStorage/blob/master/README.md#step-3--use-in-controller-or-business-layer-memo)

