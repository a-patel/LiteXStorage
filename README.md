# LiteXStorage
LiteXStorage is simple yet powerful and very high-performance storage mechanism and incorporating both synchronous and asynchronous usage with some advanced usages of cloud storage which can help us to handle storage more easier! 

Provide Storage service for any type of application (.NET Core, .NET Standard). 

Very simple yet advanced configuration. Minimal (one line) code configuration is required. 

It provides possibility to upload files, upload the content of a folder inclusively subfolders, enumerate buckets/container, enumerate the content of a specific 'subfolder', delete buckets/container and delete files from specific subfolders. 

It supports various storage providers and implements many advanced features. You can also write your own and extend it also extend existing providers. Easily migrate or switch between one to another provider with no code breaking changes. 

LiteX Storage uses the least common denominator of functionality between the supported providers to build a cross-cloud storage solution. Abstract interface to implement any kind of basic blob storage services. 

Having a default/generic implementation to wrap the Azure, Amazon AWS S3, Google Cloud, FileSystem (Local), Kvpbase storage. A cross-cloud storage abstraction. 

The Core library contains all base interfaces and tools. One should install at least one other LiteXStorage package to get storage handle implementation. 

This is the ASP.NET Core configuration integration package (Built-in). 

## Add a dependency

### Nuget

Run the nuget command for installing the client as,
* `Install-Package LiteX.Storage.Core`
* `Install-Package LiteX.Storage.Azure`
* `Install-Package LiteX.Storage.Amazon`
* `Install-Package LiteX.Storage.Google`
* `Install-Package LiteX.Storage.Kvpbase` // DEPRECATED
* `Install-Package LiteX.Storage.Local`


## Configuration

**AppSettings**
```js
{
  //LiteX Azure Storage settings
  "AzureBlobConfig": {
    "AzureBlobStorageConnectionString": "--- REPLACE WITH YOUR AZURE CONNECTION STRING ---",
    "AzureBlobStorageContainerName": "--- REPLACE WITH YOUR AZURE CONTAINER NAME ---",
    "AzureBlobStorageEndPoint": "--- REPLACE WITH YOUR AZURE END POINT ---"
  },

  //LiteX Amazon Storage settings
  "AmazonBlobConfig": {
    "AmazonAwsAccessKeyId": "--- REPLACE WITH YOUR AMAZON ACCESS KEY ID ---",
    "AmazonAwsSecretAccessKey": "--- REPLACE WITH YOUR AMAZON SECRET ACCESS KEY ---",    
    "AmazonRegion": "--- REPLACE WITH YOUR AMAZON REGION ---",
    "AmazonBucketName": "--- REPLACE WITH YOUR AZURE AMAZON BUCKET NAME ---"
  },

  //LiteX Google Storage settings
  "GoogleCloudBlobConfig": {
    "GoogleProjectId": "--- REPLACE WITH YOUR GOOGLE PROJECT ID ---",
    "GoogleJsonAuthPath": "--- REPLACE WITH YOUR GOOGLE JSON AUTH PATH ---",
    "GoogleBucketName": "--- REPLACE WITH YOUR GOOGLE BUCKET NAME ---"
  },

  //LiteX Kvpbase Storage settings
  "KvpbaseBlobConfig": {
    "KvpbaseApiKey": "--- REPLACE WITH YOUR KVPBASE API KEY ---",
    "KvpbaseContainer": "--- REPLACE WITH YOUR KVPBASE CONTAINER ---",
    "KvpbaseEndpoint": "--- REPLACE WITH YOUR KVPBASE END POINT ---",
    "KvpbaseUserGuid": "--- REPLACE WITH YOUR KVPBASE USERGUID ---"
  },

  //LiteX Local File System Storage settings
  "FileSystemBlobConfig": {
    "Directory": "--- REPLACE WITH YOUR LOCAL FILE SYSTEM DIRECTORY ---"
  }
}
```

**Startup Configuration**
```cs
public class Startup
{
    public Startup(IConfiguration configuration)
    {
    }

    public void ConfigureServices(IServiceCollection services)
    {
         #region LiteX Storage

        // blob storage (use one of below)

        #region LiteX Storage (Azure)

        // 1. Use default configuration from appsettings.json's 'AzureBlobConfig'
        services.AddLiteXAzureBlobService();

        //OR
        // 2. Load configuration settings using options.
        services.AddLiteXAzureBlobService(option =>
        {
            //option. = "";
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var azureBlobConfig = new AzureBlobConfig();
        services.AddLiteXAzureBlobService(azureBlobConfig);

        #endregion

        #region LiteX Storage (Amazon)

        // 1. Use default configuration from appsettings.json's 'AmazonBlobConfig'
        services.AddLiteXAmazonBlobService();

        //OR
        // 2. Load configuration settings using options.
        services.AddLiteXAmazonBlobService(option =>
        {
            //option. = "";
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var amazonBlobConfig = new AmazonBlobConfig();
        services.AddLiteXAmazonBlobService(amazonBlobConfig);

        #endregion

        #region LiteX Storage (Google)

        // 1. Use default configuration from appsettings.json's 'GoogleCloudBlobConfig'
        services.AddLiteXGoogleCloudBlobService();

        //OR
        // 2. Load configuration settings using options.
        services.AddLiteXGoogleCloudBlobService(option =>
        {
            //option. = "";
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var googleCloudBlobConfig = new GoogleCloudBlobConfig();
        services.AddLiteXGoogleCloudBlobService(googleCloudBlobConfig);

        #endregion

        #region LiteX Storage (Kvpbase)

        // 1. Use default configuration from appsettings.json's 'KvpbaseBlobConfig'
        services.AddLiteXKvpbaseBlobService();

        //OR
        // 2. Load configuration settings using options.
        services.AddLiteXKvpbaseBlobService(option =>
        {
            //option. = "";
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var kvpbaseBlobConfig = new KvpbaseBlobConfig();
        services.AddLiteXKvpbaseBlobService(kvpbaseBlobConfig);

        #endregion

        #region LiteX Storage (FileSystem-Local)

        // 1. Use default configuration from appsettings.json's 'FileSystemBlobConfig'
        services.AddLiteXFileSystemBlobService();

        //OR
        // 2. Load configuration settings using options.
        services.AddLiteXFileSystemBlobService(option =>
        {
            //option. = "";
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var fileSystemBlobConfig = new FileSystemBlobConfig();
        services.AddLiteXFileSystemBlobService(fileSystemBlobConfig);

        #endregion

        #endregion 
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

    }
}
```


## Usage

**Controller or Business layer**
```cs
/// <summary>
/// Customer controller
/// </summary>
[Route("api/[controller]")]
public class CustomerController : Controller
{
    #region Fields

    private readonly IBlobService _blobService;

    #endregion

    #region Ctor

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="blobService"></param>
    public CustomerController(IBlobService blobService)
    {
        _blobService = blobService;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Create/Replace blob file
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public IActionResult UploadFile(IFormFile file)
    {
        try
        {
            string blobName = file.FileName;
            Stream stream = file.OpenReadStream();
            string contentType = file.ContentType;
            BlobProperties properties = new BlobProperties { ContentType = contentType };

            bool isUploaded = _blobService.UploadBlob(blobName, stream, properties);
            //bool isUploaded = await _blobService.UploadBlobAsync(blobName, stream, properties);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
        return Ok();
    }

    /// <summary>
    /// Get blob file data (bytes or stream)
    /// </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    public IActionResult GetBlobFile(string blobName)
    {
        try
        {
            // get blob
            Stream stream = _blobService.GetBlob(blobName);
            //Stream stream = await _blobService.GetBlobAsync(blobName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
        return Ok();
    }

    /// <summary>
    /// Get blob url
    /// </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    public IActionResult GetBlobUrl(string blobName)
    {
        try
        {
            string blobUrl = _blobService.GetBlobUrl(blobName);
            //string blobUrl = await _blobService.GetBlobUrlAsync(blobName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
        return Ok();
    }

    /// <summary>
    /// Get blob sas url
    /// </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    public IActionResult GetBlobSasUrl(string blobName)
    {
        try
        {
            string blobUrl = _blobService.GetBlobSasUrl(blobName, DateTimeOffset.UtcNow.AddHours(2), BlobUrlAccess.Read);
            //string blobUrl = await _blobService.GetBlobSasUrlAsync(blobName, DateTimeOffset.UtcNow.AddHours(2), BlobUrlAccess.Read);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
        return Ok();
    }

    /// <summary>
    /// Delete blob file
    /// </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    public IActionResult DeleteBlobFile(string blobName)
    {
        try
        {
            bool isDeleted = _blobService.DeleteBlob(blobName);
            //bool isDeleted = await _blobService.DeleteBlobAsync(blobName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
        return Ok();
    }

    /// <summary>
    /// Get blob metadata
    /// </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    public IActionResult GetBlobMetadata(string blobName)
    {
        try
        {
            BlobDescriptor blobDescriptor = _blobService.GetBlobDescriptor(blobName);
            //BlobDescriptor blobDescriptor =  await _blobService.GetBlobDescriptorAsync(blobName);
            var metadata = blobDescriptor.Metadata;
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
        return Ok();
    }

    /// <summary>
    /// Set blob metadata
    /// </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
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
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
        return Ok();
    }

    /// <summary>
    /// Get blob list
    /// </summary>
    /// <returns></returns>
    public IActionResult GetBlobs()
    {
        try
        {
            List<BlobDescriptor> blobs = _blobService.GetBlobs().ToList();
            //List<BlobDescriptor> blobs = await _blobService.GetBlobsAsync().ToList();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
        return Ok();
    }

    #endregion

    #region Utilities

    private IList<Customer> GetCustomers()
    {
        IList<Customer> customers = new List<Customer>();

        customers.Add(new Customer() { Id = 1, Username = "ashish", Email = "toaashishpatel@outlook.com" });

        return customers;
    }

    private Customer GetCustomerById(int id)
    {
        Customer customer = null;

        customer = GetCustomers().ToList().FirstOrDefault(x => x.Id == id);

        return customer;
    }

    #endregion
}
```
