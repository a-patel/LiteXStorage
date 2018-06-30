# LiteXStorage
> LiteXStorage is simple yet powerful and very high-performance storage mechanism and incorporating both synchronous and asynchronous usage with some advanced usages of cloud storage which can help us to handle storage more easier! 

> Provide Storage service for any type of application (.NET Core, .NET Standard). 

Very simple yet advanced configuration. Minimal (one line) code configuration is required. 

It provides possibility to upload files, upload the content of a folder inclusively subfolders, enumerate buckets/container, enumerate the content of a specific 'subfolder', delete buckets/container and delete files from specific subfolders. 

It supports various storage providers and implements many advanced features. You can also write your own and extend it also extend existing providers. Easily migrate or switch between one to another provider with no code breaking changes. 

LiteX Storage uses the least common denominator of functionality between the supported providers to build a cross-cloud storage solution. Abstract interface to implement any kind of basic blob storage services. 

Having a default/generic implementation to wrap the Azure, Amazon AWS S3, Google Cloud, FileSystem (Local), Kvpbase storage. A cross-cloud storage abstraction. 

The Core library contains all base interfaces and tools. One should install at least one other LiteXStorage package to get storage handle implementation. 

This is the ASP.NET Core configuration integration package (Built-in). 



## Storage Providers :books:
- [Azure](docs/Azure.md)
- [AmazonS3](docs/Amazon.md)
- [Google Cloud](docs/Google.md)
- [FileSystem](docs/FileSystem.md)
- [Kvpbase](docs/Kvpbase.md) - deprecated



## Features :pager:
- Create container or bucket
- Get Shared Access Signature (SAS) url
- Upload Blobb file
- Get Blob metadata
- Set Blob metadata
- Async compatible
- Thread safe, concurrency ready
- Interface based API to support the test driven development and dependency injection
- Leverages a provider model on top of ILiteXBlobService under the hood and can be extended with your own implementation



## Basic Usage :page_facing_up:


### Step 1 : Install the package :package:

> Choose one kinds of sms provider type that you needs and install it via [Nuget](https://www.nuget.org/profiles/iamaashishpatel).
> To install LiteXSms, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

```Powershell
PM> Install-Package LiteX.Storage.Azure
PM> Install-Package LiteX.Storage.Amazon
PM> Install-Package LiteX.Storage.Google
PM> Install-Package LiteX.Storage.Local
PM> Install-Package LiteX.Storage.Kvpbase
```


### Step 2 : Configuration 🔨 
> Different types of storage provider have their own way to config.
> Here are samples that show you how to config.

##### 2.1 : AppSettings 
```js
{
  //LiteX Azure Storage settings
  "AzureBlobConfig": {
    "AzureBlobStorageConnectionString": "--- REPLACE WITH YOUR AZURE CONNECTION STRING ---",
    "AzureBlobStorageContainerName": "--- REPLACE WITH YOUR AZURE CONTAINER NAME ---",
    "AzureBlobStorageEndPoint": "--- REPLACE WITH YOUR AZURE END POINT ---",
    "EnableLogging": true
  },

  //LiteX Amazon Storage settings
  "AmazonBlobConfig": {
    "AmazonAwsAccessKeyId": "--- REPLACE WITH YOUR AMAZON ACCESS KEY ID ---",
    "AmazonAwsSecretAccessKey": "--- REPLACE WITH YOUR AMAZON SECRET ACCESS KEY ---",
    "AmazonRegion": "--- REPLACE WITH YOUR AMAZON REGION ---",
    "AmazonBucketName": "--- REPLACE WITH YOUR AZURE AMAZON BUCKET NAME ---",
    "EnableLogging": true
  },

  //LiteX Google Storage settings
  "GoogleCloudBlobConfig": {
    "GoogleProjectId": "--- REPLACE WITH YOUR GOOGLE PROJECT ID ---",
    "GoogleJsonAuthPath": "--- REPLACE WITH YOUR GOOGLE JSON AUTH PATH ---",
    "GoogleBucketName": "--- REPLACE WITH YOUR GOOGLE BUCKET NAME ---",
    "EnableLogging": true
  },

  //LiteX Kvpbase Storage settings
  "KvpbaseBlobConfig": {
    "KvpbaseApiKey": "--- REPLACE WITH YOUR KVPBASE API KEY ---",
    "KvpbaseContainer": "--- REPLACE WITH YOUR KVPBASE CONTAINER ---",
    "KvpbaseEndpoint": "--- REPLACE WITH YOUR KVPBASE END POINT ---",
    "KvpbaseUserGuid": "--- REPLACE WITH YOUR KVPBASE USERGUID ---",
    "EnableLogging": true
  },

  //LiteX Local File System Storage settings
  "FileSystemBlobConfig": {
    "Directory": "--- REPLACE WITH YOUR LOCAL FILE SYSTEM DIRECTORY ---",
    "EnableLogging": true
  }
}
```

##### 2.2 : Configure Startup Class
```cs
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        #region LiteX Storage (Azure)

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

        #endregion

        #region LiteX Storage (Amazon)

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

        #endregion

        #region LiteX Storage (Google)

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

        #endregion

        #region LiteX Storage (FileSystem-Local)

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

        #endregion

        #region LiteX Storage (Kvpbase)

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

        #endregion


        // add logging (optional)
        services.AddLiteXLogging();
    }
}
```

### Step 3 : Use in Controller or Business layer :memo:

```cs
/// <summary>
/// Customer controller
/// </summary>
[Route("api/[controller]")]
public class CustomerController : Controller
{
    #region Fields

    private readonly ILiteXBlobService _blobService;

    #endregion

    #region Ctor

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="blobService"></param>
    public CustomerController(ILiteXBlobService blobService)
    {
        _blobService = blobService;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Get Storage Provider Type
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("get-storage-provider-type")]
    public IActionResult GetStorageProviderType()
    {
        return Ok(_blobService.StorageProviderType.ToString());
    }

    /// <summary>
    /// Create/Replace blob file
    /// </summary>
    /// <param name="file"></param>
    /// <param name="isPublic"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("upload-blob")]
    [AddSwaggerFileUploadButton]
    public IActionResult UploadBlobFile(IFormFile file, bool isPublic = true)
    {
        try
        {
            string blobName = file.FileName;
            Stream stream = file.OpenReadStream();
            string contentType = file.ContentType;
            BlobProperties properties = new BlobProperties
            {
                ContentType = contentType,
                Security = isPublic ? BlobSecurity.Public : BlobSecurity.Private
            };

            bool isUploaded = _blobService.UploadBlob(blobName, stream, properties);
            //bool isUploaded = await _blobService.UploadBlobAsync(blobName, stream, properties);

            return Ok(isUploaded);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    /// <summary>
    /// Get blob file data (bytes or stream)
    /// </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("download-blob/{blobName}")]
    public IActionResult DownloadBlobFile(string blobName)
    {
        try
        {
            // get blob
            Stream stream = _blobService.GetBlob(blobName);
            //Stream stream = await _blobService.GetBlobAsync(blobName);

            var response = File(stream, "application/octet-stream", blobName); // FileStreamResult
            return response;
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    /// <summary>
    /// Get blob url
    /// </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("get-blob-url/{blobName}")]
    public IActionResult GetBlobUrl(string blobName)
    {
        try
        {
            string blobUrl = _blobService.GetBlobUrl(blobName);
            //string blobUrl = await _blobService.GetBlobUrlAsync(blobName);

            return Ok(blobUrl);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    /// <summary>
    /// Get blob sas url
    /// </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("get-blob-sasurl/{blobName}")]
    public IActionResult GetBlobSasUrl(string blobName)
    {
        try
        {
            string blobUrl = _blobService.GetBlobSasUrl(blobName, DateTimeOffset.UtcNow.AddHours(2), BlobUrlAccess.Read);
            //string blobUrl = await _blobService.GetBlobSasUrlAsync(blobName, DateTimeOffset.UtcNow.AddHours(2), BlobUrlAccess.Read);

            return Ok(blobUrl);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    /// <summary>
    /// Delete blob file
    /// </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("delete-blob/{blobName}")]
    public IActionResult DeleteBlobFile(string blobName)
    {
        try
        {
            bool isDeleted = _blobService.DeleteBlob(blobName);
            //bool isDeleted = await _blobService.DeleteBlobAsync(blobName);

            return Ok(isDeleted);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    /// <summary>
    /// Get blob metadata
    /// </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("get-blob-metadata/{blobName}")]
    public IActionResult GetBlobMetadata(string blobName)
    {
        try
        {
            BlobDescriptor blobDescriptor = _blobService.GetBlobDescriptor(blobName);
            //BlobDescriptor blobDescriptor =  await _blobService.GetBlobDescriptorAsync(blobName);
            var metadata = blobDescriptor.Metadata;

            return Ok(blobDescriptor);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    /// <summary>
    /// Set blob metadata
    /// </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("set-blob-metadata/{blobName}")]
    public IActionResult SetBlobMetadata(string blobName)
    {
        try
        {
            IDictionary<string, string> metadata = new Dictionary<string, string>();

            BlobProperties properties = new BlobProperties()
            {
                ContentType = "",
                Metadata = metadata,
                ContentDisposition = "",
                Security = BlobSecurity.Public
            };

            bool isSet = _blobService.SetBlobProperties(blobName, properties);
            //bool isSet = await _blobService.SetBlobPropertiesAsync(blobName, properties);

            return Ok(isSet);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    /// <summary>
    /// Get blob list
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("get-blobs")]
    public IActionResult GetBlobs()
    {
        try
        {
            List<BlobDescriptor> blobs = _blobService.GetBlobs().ToList();
            //List<BlobDescriptor> blobs = await _blobService.GetBlobsAsync().ToList();

            return Ok(blobs);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }


    /// <summary>
    /// Get blob file data (bytes or stream)
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("create-container-or-bucket/{containerName}")]
    public IActionResult CreateContainerOrBucket(string containerName)
    {
        try
        {
            // Create Container
            bool isCreated = _blobService.CreateContainer(containerName);
            //bool isCreated = await _blobService.CreateContainerAsync(containerName);

            return Ok(isCreated);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    /// <summary>
    /// Get blob file data (bytes or stream)
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("delete-container-or-bucket/{containerName}")]
    public IActionResult DeleteContainerOrBucket(string containerName)
    {
        try
        {
            // Create Container
            bool isDeleted = _blobService.DeleteContainer(containerName);
            //bool isDeleted = await _blobService.DeleteContainerAsync(containerName);

            return Ok(isDeleted);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    #endregion
}
```


## Todo List :clipboard:

#### Storage Providers

- [x] Azure
- [x] AmazonS3
- [x] Google Cloud
- [x] File System
- [x] Kvpbase

#### Basic Storage API

- [x] Get Blob SAS



#### Coming soon
* Multiple container or bucker support


---



## Support :telephone:
> Reach out to me at one of the following places!

- Email :envelope: at <a href="mailto:toaashishpatel@gmail.com" target="_blank">`toaashishpatel@gmail.com`</a>
- NuGet :package: at <a href="https://www.nuget.org/profiles/iamaashishpatel" target="_blank">`@iamaashishpatel`</a>



## Authors :boy:

* **Ashish Patel** - [A-Patel](https://github.com/a-patel)


##### Connect with me

| Linkedin | GitHub | Facebook | Twitter | Instagram | Tumblr | Website |
|----------|----------|----------|----------|----------|----------|----------|
| [![linkedin](https://cdnjs.cloudflare.com/ajax/libs/foundicons/3.0.0/svgs/fi-social-linkedin.svg)](https://www.linkedin.com/in/iamaashishpatel) | [![github](https://cdnjs.cloudflare.com/ajax/libs/foundicons/3.0.0/svgs/fi-social-github.svg)](https://github.com/a-patel) | [![facebook](https://cdnjs.cloudflare.com/ajax/libs/foundicons/3.0.0/svgs/fi-social-facebook.svg)](https://www.facebook.com/aashish.mrcool) | [![twitter](https://cdnjs.cloudflare.com/ajax/libs/foundicons/3.0.0/svgs/fi-social-twitter.svg)](https://twitter.com/aashish_mrcool) | [![instagram](https://cdnjs.cloudflare.com/ajax/libs/foundicons/3.0.0/svgs/fi-social-instagram.svg)](https://www.instagram.com/iamaashishpatel/) | [![tumblr](https://cdnjs.cloudflare.com/ajax/libs/foundicons/3.0.0/svgs/fi-social-tumblr.svg)](https://iamaashishpatel.tumblr.com/) | [![website](https://cdnjs.cloudflare.com/ajax/libs/foundicons/3.0.0/svgs/fi-social-blogger.svg)](http://aashishpatel.co.nf/) |
| | | | | | |



## Donations :dollar:

[![Buy Me A Coffee](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/iamaashishpatel)



## License :lock:

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

