#region Imports
using LiteX.Storage.Core;
using LiteXStorage.Demo.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace LiteXStorage.Demo.Controllers
{
    /// <summary>
    /// Storage (Factory) controller
    /// </summary>
    [Route("api/[controller]")]
    public class StorageFactoryController : Controller
    {
        #region Fields

        // when using single provider
        // private readonly ILiteXBlobService _provider;

        // when using multiple provider
        private readonly ILiteXStorageProviderFactory _factory;

        #endregion

        #region Ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        // <param name="provider"></param>
        public StorageFactoryController(ILiteXStorageProviderFactory factory)
        {
            _factory = factory;
            //_provider = provider;
            //_provider = _factory.GetStorageProvider("azure");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the provider from factory with its name
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("demo-usage")]
        public async Task<IActionResult> DemoUsage()
        {
            // get the provider from factory with its name
            var provider = _factory.GetStorageProvider("azure");
            //var provider = _factory.GetStorageProvider("amazons3");
            //var provider = _factory.GetStorageProvider("googlecloudstorage");
            //var provider = _factory.GetStorageProvider("filesystem");
            //var provider = _factory.GetStorageProvider("Kvpbase");
            //var provider = _factory.GetStorageProvider("other");


            List<BlobDescriptor> blobs = (await provider.GetBlobsAsync()).ToList();

            // sync
            //List<BlobDescriptor> blobs = _blobService.GetBlobs().ToList();

            return Ok(blobs);
        }


        /// <summary>
        /// Get Storage Provider Type
        /// </summary>
        /// <param name="storageProviderType">Storage provider type</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-storage-provider-type")]
        public IActionResult GetStorageProviderType(StorageProviderType storageProviderType)
        {
            switch (storageProviderType)
            {
                case StorageProviderType.Azure:
                    var providerAzure = _factory.GetStorageProvider("azure");
                    return Ok(providerAzure.StorageProviderType.ToString());

                case StorageProviderType.Amazon:
                    var providerAmazons3 = _factory.GetStorageProvider("amazons3");
                    return Ok(providerAmazons3.StorageProviderType.ToString());

                case StorageProviderType.Google:
                    var providerGCP = _factory.GetStorageProvider("googlecloudstorage");
                    return Ok(providerGCP.StorageProviderType.ToString());

                case StorageProviderType.FileSystem:
                    var providerFileSystem = _factory.GetStorageProvider("filesystem");
                    return Ok(providerFileSystem.StorageProviderType.ToString());

                case StorageProviderType.Kvpbase:
                    var providerKvpbase = _factory.GetStorageProvider("Kvpbase");
                    return Ok(providerKvpbase.StorageProviderType.ToString());

                case StorageProviderType.Other:
                    var providerOther = _factory.GetStorageProvider("other");
                    return Ok(providerOther.StorageProviderType.ToString());

                default:
                    return BadRequest("Provider not supported");
            }
        }


        /// <summary>
        /// Get blob list
        /// from default Container/Bucket
        /// </summary>
        /// <param name="storageProviderType">Storage provider type</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-blobs")]
        public async Task<IActionResult> GetBlobs(StorageProviderType storageProviderType)
        {
            List<BlobDescriptor> blobs = new List<BlobDescriptor>();

            switch (storageProviderType)
            {
                case StorageProviderType.Azure:
                    var providerAzure = _factory.GetStorageProvider("azure");
                    blobs = (await providerAzure.GetBlobsAsync()).ToList();
                    break;

                case StorageProviderType.Amazon:
                    var providerAmazons3 = _factory.GetStorageProvider("amazons3");
                    blobs = (await providerAmazons3.GetBlobsAsync()).ToList();
                    break;

                case StorageProviderType.Google:
                    var providerGCP = _factory.GetStorageProvider("googlecloudstorage");
                    blobs = (await providerGCP.GetBlobsAsync()).ToList();
                    break;

                case StorageProviderType.FileSystem:
                    var providerFileSystem = _factory.GetStorageProvider("filesystem");
                    blobs = (await providerFileSystem.GetBlobsAsync()).ToList();
                    break;

                case StorageProviderType.Kvpbase:
                    var providerKvpbase = _factory.GetStorageProvider("Kvpbase");
                    blobs = (await providerKvpbase.GetBlobsAsync()).ToList();
                    break;

                case StorageProviderType.Other:
                    var providerOther = _factory.GetStorageProvider("other");
                    blobs = (await providerOther.GetBlobsAsync()).ToList();
                    break;

                default:
                    return BadRequest("Provider not supported");
            }

            return Ok(blobs);
        }

        /// <summary>
        /// Get blob list
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <param name="storageProviderType">Storage provider type</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-blobs/{containerOrBucketName}")]
        public async Task<IActionResult> GetBlobs(string containerOrBucketName, StorageProviderType storageProviderType)
        {
            List<BlobDescriptor> blobs = new List<BlobDescriptor>();

            switch (storageProviderType)
            {
                case StorageProviderType.Azure:
                    var providerAzure = _factory.GetStorageProvider("azure");
                    blobs = (await providerAzure.GetBlobsAsync(containerOrBucketName)).ToList();
                    break;

                case StorageProviderType.Amazon:
                    var providerAmazons3 = _factory.GetStorageProvider("amazons3");
                    blobs = (await providerAmazons3.GetBlobsAsync(containerOrBucketName)).ToList();
                    break;

                case StorageProviderType.Google:
                    var providerGCP = _factory.GetStorageProvider("googlecloudstorage");
                    blobs = (await providerGCP.GetBlobsAsync(containerOrBucketName)).ToList();
                    break;

                case StorageProviderType.FileSystem:
                    var providerFileSystem = _factory.GetStorageProvider("filesystem");
                    blobs = (await providerFileSystem.GetBlobsAsync(containerOrBucketName)).ToList();
                    break;

                case StorageProviderType.Kvpbase:
                    var providerKvpbase = _factory.GetStorageProvider("Kvpbase");
                    blobs = (await providerKvpbase.GetBlobsAsync(containerOrBucketName)).ToList();
                    break;

                case StorageProviderType.Other:
                    var providerOther = _factory.GetStorageProvider("other");
                    blobs = (await providerOther.GetBlobsAsync(containerOrBucketName)).ToList();
                    break;

                default:
                    return BadRequest("Provider not supported");
            }

            return Ok(blobs);
        }


        /// <summary>
        /// Create/Replace blob file
        /// from default Container/Bucket
        /// </summary>
        /// <param name="model">Blob file</param>
        /// <param name="isPublic">Is Private or Public blob</param>
        /// <param name="storageProviderType">Storage provider type</param>
        /// <returns></returns>
        [HttpPost]
        [Route("upload-blob")]
        public async Task<IActionResult> UploadBlob(FileModel model, bool isPublic = true, StorageProviderType storageProviderType = StorageProviderType.Azure)
        {
            string blobName = model.File.FileName;
            Stream stream = model.File.OpenReadStream();
            string contentType = model.File.ContentType;
            BlobProperties properties = new BlobProperties
            {
                ContentType = contentType,
                Security = isPublic ? BlobSecurity.Public : BlobSecurity.Private
            };
            var isUploaded = false;

            switch (storageProviderType)
            {
                case StorageProviderType.Azure:
                    var providerAzure = _factory.GetStorageProvider("azure");
                    isUploaded = await providerAzure.UploadBlobAsync(blobName, stream, properties);
                    break;

                case StorageProviderType.Amazon:
                    var providerAmazons3 = _factory.GetStorageProvider("amazons3");
                    isUploaded = await providerAmazons3.UploadBlobAsync(blobName, stream, properties);
                    break;

                case StorageProviderType.Google:
                    var providerGCP = _factory.GetStorageProvider("googlecloudstorage");
                    isUploaded = await providerGCP.UploadBlobAsync(blobName, stream, properties);
                    break;

                case StorageProviderType.FileSystem:
                    var providerFileSystem = _factory.GetStorageProvider("filesystem");
                    isUploaded = await providerFileSystem.UploadBlobAsync(blobName, stream, properties);
                    break;

                case StorageProviderType.Kvpbase:
                    var providerKvpbase = _factory.GetStorageProvider("Kvpbase");
                    isUploaded = await providerKvpbase.UploadBlobAsync(blobName, stream, properties);
                    break;

                case StorageProviderType.Other:
                    var providerOther = _factory.GetStorageProvider("other");
                    isUploaded = await providerOther.UploadBlobAsync(blobName, stream, properties);
                    break;

                default:
                    return BadRequest("Provider not supported");
            }

            return Ok(isUploaded);
        }

        /// <summary>
        /// Create/Replace blob file
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <param name="model">Blob file</param>
        /// <param name="isPublic">Is Private or Public blob</param>
        /// <param name="storageProviderType">Storage provider type</param>
        /// <returns></returns>
        [HttpPost]
        [Route("upload-blob/{containerOrBucketName}")]
        public async Task<IActionResult> UploadBlob(string containerOrBucketName, FileModel model, bool isPublic = true, StorageProviderType storageProviderType = StorageProviderType.Azure)
        {
            string blobName = model.File.FileName;
            Stream stream = model.File.OpenReadStream();
            string contentType = model.File.ContentType;
            BlobProperties properties = new BlobProperties
            {
                ContentType = contentType,
                Security = isPublic ? BlobSecurity.Public : BlobSecurity.Private
            };
            var isUploaded = false;

            switch (storageProviderType)
            {
                case StorageProviderType.Azure:
                    var providerAzure = _factory.GetStorageProvider("azure");
                    isUploaded = await providerAzure.UploadBlobAsync(containerOrBucketName, blobName, stream, properties);
                    break;

                case StorageProviderType.Amazon:
                    var providerAmazons3 = _factory.GetStorageProvider("amazons3");
                    isUploaded = await providerAmazons3.UploadBlobAsync(containerOrBucketName, blobName, stream, properties);
                    break;

                case StorageProviderType.Google:
                    var providerGCP = _factory.GetStorageProvider("googlecloudstorage");
                    isUploaded = await providerGCP.UploadBlobAsync(containerOrBucketName, blobName, stream, properties);
                    break;

                case StorageProviderType.FileSystem:
                    var providerFileSystem = _factory.GetStorageProvider("filesystem");
                    isUploaded = await providerFileSystem.UploadBlobAsync(containerOrBucketName, blobName, stream, properties);
                    break;

                case StorageProviderType.Kvpbase:
                    var providerKvpbase = _factory.GetStorageProvider("Kvpbase");
                    isUploaded = await providerKvpbase.UploadBlobAsync(containerOrBucketName, blobName, stream, properties);
                    break;

                case StorageProviderType.Other:
                    var providerOther = _factory.GetStorageProvider("other");
                    isUploaded = await providerOther.UploadBlobAsync(containerOrBucketName, blobName, stream, properties);
                    break;

                default:
                    return BadRequest("Provider not supported");
            }

            return Ok(isUploaded);
        }


        /// <summary>
        /// Get blob url
        /// from default Container/Bucket
        /// </summary>
        /// <param name="blobName">Name of the Blob.</param>
        /// <param name="storageProviderType">Storage provider type</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-blob-url/{blobName}")]
        public async Task<IActionResult> GetBlobUrl(string blobName, StorageProviderType storageProviderType = StorageProviderType.Azure)
        {
            string blobUrl = string.Empty;

            switch (storageProviderType)
            {
                case StorageProviderType.Azure:
                    var providerAzure = _factory.GetStorageProvider("azure");
                    blobUrl = await providerAzure.GetBlobUrlAsync(blobName);
                    break;

                case StorageProviderType.Amazon:
                    var providerAmazons3 = _factory.GetStorageProvider("amazons3");
                    blobUrl = await providerAmazons3.GetBlobUrlAsync(blobName);
                    break;

                case StorageProviderType.Google:
                    var providerGCP = _factory.GetStorageProvider("googlecloudstorage");
                    blobUrl = await providerGCP.GetBlobUrlAsync(blobName);
                    break;

                case StorageProviderType.FileSystem:
                    var providerFileSystem = _factory.GetStorageProvider("filesystem");
                    blobUrl = await providerFileSystem.GetBlobUrlAsync(blobName);
                    break;

                case StorageProviderType.Kvpbase:
                    var providerKvpbase = _factory.GetStorageProvider("Kvpbase");
                    blobUrl = await providerKvpbase.GetBlobUrlAsync(blobName);
                    break;

                case StorageProviderType.Other:
                    var providerOther = _factory.GetStorageProvider("other");
                    blobUrl = await providerOther.GetBlobUrlAsync(blobName);
                    break;

                default:
                    return BadRequest("Provider not supported");
            }

            return Ok(blobUrl);
        }

        /// <summary>
        /// Get blob url
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <param name="blobName">Name of the Blob.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-blob-url/{containerOrBucketName}/{blobName}")]
        public async Task<IActionResult> GetBlobUrl(string containerOrBucketName, string blobName, StorageProviderType storageProviderType)
        {
            string blobUrl = string.Empty;

            switch (storageProviderType)
            {
                case StorageProviderType.Azure:
                    var providerAzure = _factory.GetStorageProvider("azure");
                    blobUrl = await providerAzure.GetBlobUrlAsync(containerOrBucketName, blobName);
                    break;

                case StorageProviderType.Amazon:
                    var providerAmazons3 = _factory.GetStorageProvider("amazons3");
                    blobUrl = await providerAmazons3.GetBlobUrlAsync(containerOrBucketName, blobName);
                    break;

                case StorageProviderType.Google:
                    var providerGCP = _factory.GetStorageProvider("googlecloudstorage");
                    blobUrl = await providerGCP.GetBlobUrlAsync(containerOrBucketName, blobName);
                    break;

                case StorageProviderType.FileSystem:
                    var providerFileSystem = _factory.GetStorageProvider("filesystem");
                    blobUrl = await providerFileSystem.GetBlobUrlAsync(containerOrBucketName, blobName);
                    break;

                case StorageProviderType.Kvpbase:
                    var providerKvpbase = _factory.GetStorageProvider("Kvpbase");
                    blobUrl = await providerKvpbase.GetBlobUrlAsync(containerOrBucketName, blobName);
                    break;

                case StorageProviderType.Other:
                    var providerOther = _factory.GetStorageProvider("other");
                    blobUrl = await providerOther.GetBlobUrlAsync(containerOrBucketName, blobName);
                    break;

                default:
                    return BadRequest("Provider not supported");
            }

            return Ok(blobUrl);
        }

        #endregion
    }
}



#region Reference
/*
https://github.com/dotnetcore/EasyCaching
     
*/
#endregion
