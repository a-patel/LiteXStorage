
# LiteXStorage
Abstract interface to implement any kind of basic blob storage services (e.g. Azure, Amazon, Google, Local FileSystem) for any type of application (ASP.NET Core, .Net Standard 2.x).


## Add a dependency

### nuget

Run the nuget command for installing the client as,
`Install-Package LiteX.Storage.Core`
`Install-Package LiteX.Storage.Azure`
`Install-Package LiteX.Storage.Amazon`
`Install-Package LiteX.Storage.Google`
`Install-Package LiteX.Storage.Kvpbase`
`Install-Package LiteX.Storage.Local`


## Usage

### Configuration

**AppSettings**
```json
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

        // add azure blob storage settings
        services.AddSingleton(configuration.GetSection("AzureBlobConfig").Get<AzureBlobConfig>());

        // register azure blob storage service
        services.AddTransient<IBlobService, AzureBlobService>();

        #endregion

        #region LiteX Storage (Amazon)

        // add amazon blob storage settings
        services.AddSingleton(configuration.GetSection("AmazonBlobConfig").Get<AmazonBlobConfig>());

        // register amazon blob storage service
        services.AddTransient<IBlobService, AmazonBlobService>();

        #endregion

        #region LiteX Storage (Google)

        // add google blob storage settings
        services.AddSingleton(configuration.GetSection("GoogleCloudBlobConfig").Get<GoogleCloudBlobConfig>());

        // register google blob storage service
        services.AddTransient<IBlobService, GoogleCloudBlobService>();

        #endregion

        #region LiteX Storage (Kvpbase)

        // add azure kvpbase storage settings
        services.AddSingleton(configuration.GetSection("KvpbaseBlobConfig").Get<KvpbaseBlobConfig>());

        // register kvpbase blob storage service
        services.AddTransient<IBlobService, KvpbaseBlobService>();

        #endregion

        #region LiteX Storage (FileSystem-Local)

        // add file system (local) blob storage settings
        services.AddSingleton(configuration.GetSection("FileSystemBlobConfig").Get<FileSystemBlobConfig>());

        // register file system (local) blob storage service
        services.AddTransient<IBlobService, FileSystemBlobService>();

        #endregion

        #endregion
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

    }
}
```
