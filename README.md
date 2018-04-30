# LiteXStorage
Abstract interface to implement any kind of basic blob storage services (e.g. Azure, Amazon, Google, Local FileSystem) for any type of application (ASP.NET Core, .Net Standard 2.x).


## Add a dependency

### Nuget

Run the nuget command for installing the client as,
* `Install-Package LiteX.Storage.Core`
* `Install-Package LiteX.Storage.Azure`
* `Install-Package LiteX.Storage.Amazon`
* `Install-Package LiteX.Storage.Google`
* `Install-Package LiteX.Storage.Kvpbase`
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
    public IConfiguration configuration { get; }

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        #region LiteX Storage

        // blob storage (use one of below)

        #region LiteX Storage (Azure)

        services.AddLiteXAzureBlobService(configuration);

        // OR
        // load configuration settings on your own.
        // from appsettings, database, hardcoded etc.
        var azureBlobConfig = new AzureBlobConfig();
        services.AddLiteXAzureBlobService(configuration, azureBlobConfig);

        #endregion

        #region LiteX Storage (Amazon)

        services.AddLiteXAmazonBlobService(configuration);

        // OR
        // load configuration settings on your own.
        // from appsettings, database, hardcoded etc.
        var amazonBlobConfig = new AmazonBlobConfig();
        services.AddLiteXAmazonBlobService(configuration, amazonBlobConfig);

        #endregion

        #region LiteX Storage (Google)

        services.AddLiteXGoogleCloudBlobService(configuration);

        // OR
        // load configuration settings on your own.
        // from appsettings, database, hardcoded etc.
        var googleCloudBlobConfig = new GoogleCloudBlobConfig();
        services.AddLiteXGoogleCloudBlobService(configuration, googleCloudBlobConfig);

        #endregion

        #region LiteX Storage (Kvpbase)

        services.AddLiteXKvpbaseBlobService(configuration);

        // OR
        // load configuration settings on your own.
        // from appsettings, database, hardcoded etc.
        var kvpbaseBlobConfig = new KvpbaseBlobConfig();
        services.AddLiteXKvpbaseBlobService(configuration, kvpbaseBlobConfig);

        #endregion

        #region LiteX Storage (FileSystem-Local)

        services.AddLiteXFileSystemBlobService(configuration);

        // OR
        // load configuration settings on your own.
        // from appsettings, database, hardcoded etc.
        var fileSystemBlobConfig = new FileSystemBlobConfig();
        services.AddLiteXFileSystemBlobService(configuration, fileSystemBlobConfig);

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
    public IActionResult UploadBlobFile(IFormFile file)
    {
        try
        {
            string blobName = file.FileName;
            byte[] data = StreamToByteArray(file.OpenReadStream());
            string contentType = file.ContentType;

            bool isUploaded = _blobService.UploadBlob(blobName, data, contentType);
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
            // get blob bytes
            byte[] data = _blobService.GetBlob(blobName);

            // get memeory stream
            MemoryStream stream = _blobService.GetBlobStream(blobName);
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
            IDictionary<string, string> metadata = _blobService.GetBlobMetadata(blobName);
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
            //list of metadata to update
            //metadata.Add("", "");

            bool isSet = _blobService.SetBlobMetadata(blobName, metadata);
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

    public static byte[] StreamToByteArray(Stream input)
    {
        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }

    #endregion
}
```
