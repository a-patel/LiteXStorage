# LiteXStorage
Abstract interface to implement any kind of basic blob storage services (e.g. Azure, Amazon, Google, Local FileSystem) for any type of application (ASP.NET Core, .Net Standard 2.x).


## Add a dependency

### Nuget

Run the nuget command for installing the client as,
* `Install-Package LiteX.Storage.Core`
* `Install-Package LiteX.Storage.Local`


## Configuration

**AppSettings**
```js
{
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
    public void ConfigureServices(IServiceCollection services)
    {
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
