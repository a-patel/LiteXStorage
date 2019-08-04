#region Imports
using LiteX.Storage.Core;
using LiteXStorage.Demo.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace LiteXStorage.Demo.Controllers
{
    /// <summary>
    /// Storage controller
    /// </summary>
    [Route("api/[controller]")]
    public class StorageController : Controller
    {
        #region Fields

        private readonly ILiteXBlobService _blobService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="blobService"></param>
        public StorageController(ILiteXBlobService blobService)
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
        /// Get blob list
        /// from default Container/Bucket
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-blobs")]
        public async Task<IActionResult> GetBlobs()
        {
            List<BlobDescriptor> blobs = (await _blobService.GetBlobsAsync()).ToList();

            // sync
            //List<BlobDescriptor> blobs = _blobService.GetBlobs().ToList();

            return Ok(blobs);
        }

        /// <summary>
        /// Get blob list
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-blobs/{containerOrBucketName}")]
        public async Task<IActionResult> GetBlobs(string containerOrBucketName)
        {
            List<BlobDescriptor> blobs = (await _blobService.GetBlobsAsync(containerOrBucketName)).ToList();

            // sync
            //List<BlobDescriptor> blobs = _blobService.GetBlobs(containerOrBucketName).ToList();

            return Ok(blobs);
        }


        /// <summary>
        /// Create/Replace blob file
        /// from default Container/Bucket
        /// </summary>
        /// <param name="model">Blob file</param>
        /// <param name="isPublic">Is Private or Public blob</param>
        /// <returns></returns>
        [HttpPost]
        [Route("upload-blob")]
        public async Task<IActionResult> UploadBlob(FileModel model, bool isPublic = true)
        {
            string blobName = model.File.FileName;
            Stream stream = model.File.OpenReadStream();
            string contentType = model.File.ContentType;
            BlobProperties properties = new BlobProperties
            {
                ContentType = contentType,
                Security = isPublic ? BlobSecurity.Public : BlobSecurity.Private
            };

            var isUploaded = await _blobService.UploadBlobAsync(blobName, stream, properties);

            // sync
            //var isUploaded = _blobService.UploadBlob(blobName, stream, properties);

            return Ok(isUploaded);
        }

        /// <summary>
        /// Create/Replace blob file
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <param name="model">Blob file</param>
        /// <param name="isPublic">Is Private or Public blob</param>
        /// <returns></returns>
        [HttpPost]
        [Route("upload-blob/{containerOrBucketName}")]
        public async Task<IActionResult> UploadBlob(string containerOrBucketName, FileModel model, bool isPublic = true)
        {
            string blobName = model.File.FileName;
            Stream stream = model.File.OpenReadStream();
            string contentType = model.File.ContentType;
            BlobProperties properties = new BlobProperties
            {
                ContentType = contentType,
                Security = isPublic ? BlobSecurity.Public : BlobSecurity.Private
            };

            var isUploaded = await _blobService.UploadBlobAsync(containerOrBucketName, blobName, stream, properties);

            // sync
            //var isUploaded = _blobService.UploadBlob(containerOrBucketName, blobName, stream, properties);

            return Ok(isUploaded);
        }


        /// <summary>
        /// Create/Replace blob file in directory/folder
        /// from default Container/Bucket
        /// </summary>
        /// <param name="directoryName">Name of directory/folder.</param>
        /// <param name="model">Blob file</param>
        /// <param name="isPublic">Is Private or Public blob</param>
        /// <returns></returns>
        [HttpPost]
        [Route("upload-blob-in-directory/{directoryName}")]
        public async Task<IActionResult> UploadBlobInDirectory(string directoryName, FileModel model, bool isPublic = true)
        {
            string blobName = $"{directoryName}/{model.File.FileName}";
            Stream stream = model.File.OpenReadStream();
            string contentType = model.File.ContentType;
            BlobProperties properties = new BlobProperties
            {
                ContentType = contentType,
                Security = isPublic ? BlobSecurity.Public : BlobSecurity.Private
            };

            var isUploaded = await _blobService.UploadBlobAsync(blobName, stream, properties);

            // sync
            //var isUploaded = _blobService.UploadBlob(blobName, stream, properties);

            return Ok(isUploaded);
        }

        /// <summary>
        /// Create/Replace blob file in directory/folder
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <param name="directoryName">Name of directory/folder.</param>
        /// <param name="model">Blob file</param>
        /// <param name="isPublic">Is Private or Public blob</param>
        /// <returns></returns>
        [HttpPost]
        [Route("upload-blob-in-directory/{containerOrBucketName}/{directoryName}")]
        public async Task<IActionResult> UploadBlobInDirectory(string containerOrBucketName, string directoryName, FileModel model, bool isPublic = true)
        {
            string blobName = $"{directoryName}/{model.File.FileName}";
            //string blobName = $"{directoryName}{Path.DirectorySeparatorChar}{file.FileName}";
            Stream stream = model.File.OpenReadStream();
            string contentType = model.File.ContentType;
            BlobProperties properties = new BlobProperties
            {
                ContentType = contentType,
                Security = isPublic ? BlobSecurity.Public : BlobSecurity.Private
            };

            var isUploaded = await _blobService.UploadBlobAsync(containerOrBucketName, blobName, stream, properties);

            // sync
            //var isUploaded = _blobService.UploadBlob(containerOrBucketName, blobName, stream, properties);

            return Ok(isUploaded);
        }


        /// <summary>
        /// Get blob file data (bytes or stream)
        /// from default Container/Bucket
        /// </summary>
        /// <param name="blobName">Name of the Blob.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("download-blob/{blobName}")]
        public async Task<IActionResult> DownloadBlob(string blobName)
        {
            // get blob
            Stream stream = await _blobService.GetBlobAsync(blobName);

            // sync
            //Stream stream = _blobService.GetBlob(blobName);

            var response = File(stream, "application/octet-stream", blobName); // FileStreamResult
            return response;
        }

        /// <summary>
        /// Get blob file data (bytes or stream)
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <param name="blobName">Name of the Blob.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("download-blob/{containerOrBucketName}/{blobName}")]
        public async Task<IActionResult> DownloadBlob(string containerOrBucketName, string blobName)
        {
            // get blob
            Stream stream = await _blobService.GetBlobAsync(containerOrBucketName, blobName);

            // sync
            //Stream stream = _blobService.GetBlob(containerOrBucketName, blobName);

            var response = File(stream, "application/octet-stream", blobName); // FileStreamResult
            return response;
        }


        /// <summary>
        /// Get blob url
        /// from default Container/Bucket
        /// </summary>
        /// <param name="blobName">Name of the Blob.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-blob-url/{blobName}")]
        public async Task<IActionResult> GetBlobUrl(string blobName)
        {
            string blobUrl = await _blobService.GetBlobUrlAsync(blobName);

            // sync
            //string blobUrl = _blobService.GetBlobUrl(blobName);

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
        public async Task<IActionResult> GetBlobUrl(string containerOrBucketName, string blobName)
        {
            string blobUrl = await _blobService.GetBlobUrlAsync(containerOrBucketName, blobName);

            // sync
            //string blobUrl = _blobService.GetBlobUrl(containerOrBucketName, blobName);

            return Ok(blobUrl);
        }


        /// <summary>
        /// Get blob SAS url
        /// from default Container/Bucket
        /// </summary>
        /// <param name="blobName">Name of the Blob.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-blob-sasurl/{blobName}")]
        public async Task<IActionResult> GetBlobSasUrl(string blobName)
        {
            string blobUrl = await _blobService.GetBlobSasUrlAsync(blobName, DateTimeOffset.UtcNow.AddHours(2), BlobUrlAccess.Read);

            // sync
            //string blobUrl = _blobService.GetBlobSasUrl(blobName, DateTimeOffset.UtcNow.AddHours(2), BlobUrlAccess.Read);

            return Ok(blobUrl);
        }

        /// <summary>
        /// Get blob SAS url
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <param name="blobName">Name of the Blob.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-blob-sasurl/{containerOrBucketName}/{blobName}")]
        public async Task<IActionResult> GetBlobSasUrl(string containerOrBucketName, string blobName)
        {
            string blobUrl = await _blobService.GetBlobSasUrlAsync(containerOrBucketName, blobName, DateTimeOffset.UtcNow.AddHours(2), BlobUrlAccess.Read);

            // sync
            //string blobUrl = _blobService.GetBlobSasUrl(containerOrBucketName, blobName, DateTimeOffset.UtcNow.AddHours(2), BlobUrlAccess.Read);

            return Ok(blobUrl);
        }


        /// <summary>
        /// Delete blob file
        /// from default Container/Bucket
        /// </summary>
        /// <param name="blobName">Name of the Blob.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-blob/{blobName}")]
        public async Task<IActionResult> DeleteBlobFile(string blobName)
        {
            var isDeleted = await _blobService.DeleteBlobAsync(blobName);

            // sync
            //var isDeleted = _blobService.DeleteBlob(blobName);

            return Ok(isDeleted);
        }

        /// <summary>
        /// Delete blob file
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <param name="blobName">Name of the Blob.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-blob/{containerOrBucketName}/{blobName}")]
        public async Task<IActionResult> DeleteBlobFile(string containerOrBucketName, string blobName)
        {
            var isDeleted = await _blobService.DeleteBlobAsync(containerOrBucketName, blobName);

            // sync
            //var isDeleted = _blobService.DeleteBlob(containerOrBucketName, blobName);

            return Ok(isDeleted);
        }


        /// <summary>
        /// Get blob metadata
        /// from default Container/Bucket
        /// </summary>
        /// <param name="blobName">Name of the Blob.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-blob-metadata/{blobName}")]
        public async Task<IActionResult> GetBlobMetadata(string blobName)
        {
            BlobDescriptor blobDescriptor = await _blobService.GetBlobDescriptorAsync(blobName);

            // sync
            //BlobDescriptor blobDescriptor = _blobService.GetBlobDescriptor(blobName);

            var metadata = blobDescriptor.Metadata;

            return Ok(blobDescriptor);
        }

        /// <summary>
        /// Get blob metadata
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <param name="blobName">Name of the Blob.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-blob-metadata/{containerOrBucketName}/{blobName}")]
        public async Task<IActionResult> GetBlobMetadata(string containerOrBucketName, string blobName)
        {
            BlobDescriptor blobDescriptor = await _blobService.GetBlobDescriptorAsync(containerOrBucketName, blobName);

            // sync
            //BlobDescriptor blobDescriptor = _blobService.GetBlobDescriptor(containerOrBucketName, blobName);

            var metadata = blobDescriptor.Metadata;

            return Ok(blobDescriptor);
        }


        /// <summary>
        /// Set blob metadata
        /// from default Container/Bucket
        /// </summary>
        /// <param name="blobName">Name of the Blob.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("set-blob-metadata/{blobName}")]
        public async Task<IActionResult> SetBlobMetadata(string blobName)
        {
            IDictionary<string, string> metadata = new Dictionary<string, string>();

            BlobProperties properties = new BlobProperties()
            {
                ContentType = "",
                Metadata = metadata,
                ContentDisposition = "",
                Security = BlobSecurity.Public
            };

            var isSet = await _blobService.SetBlobPropertiesAsync(blobName, properties);

            // sync
            //var isSet = _blobService.SetBlobProperties(blobName, properties);

            return Ok(isSet);
        }

        /// <summary>
        /// Set blob metadata
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <param name="blobName">Name of the Blob.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("set-blob-metadata/{containerOrBucketName}/{blobName}")]
        public async Task<IActionResult> SetBlobMetadata(string containerOrBucketName, string blobName)
        {
            IDictionary<string, string> metadata = new Dictionary<string, string>();

            BlobProperties properties = new BlobProperties()
            {
                ContentType = "",
                Metadata = metadata,
                ContentDisposition = "",
                Security = BlobSecurity.Public
            };

            var isSet = await _blobService.SetBlobPropertiesAsync(containerOrBucketName, blobName, properties);

            // sync
            //var isSet = _blobService.SetBlobProperties(containerOrBucketName, blobName, properties);

            return Ok(isSet);
        }


        /// <summary>
        /// Delete directory/folder from default Container/Bucket
        /// </summary>
        /// <param name="directoryName">Name of directory/folder.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-directory/{directoryName}")]
        public async Task<IActionResult> DeleteDirectory(string directoryName)
        {
            var isDeleted = await _blobService.DeleteDirectoryAsync(directoryName);

            // sync
            //var isDeleted = _blobService.DeleteDirectory(directoryName);

            return Ok(isDeleted);
        }

        /// <summary>
        /// Delete directory/folder from Container/Bucket
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <param name="directoryName">Name of directory/folder.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-directory/{containerOrBucketName}/{directoryName}")]
        public async Task<IActionResult> DeleteDirectory(string containerOrBucketName, string directoryName)
        {
            var isDeleted = await _blobService.DeleteDirectoryAsync(containerOrBucketName, directoryName);

            // sync
            //var isDeleted = _blobService.DeleteDirectory(containerOrBucketName, directoryName);

            return Ok(isDeleted);
        }


        /// <summary>
        /// Get number to total items/files in Container/Bucket
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-container-or-bucket-item-count")]
        public async Task<IActionResult> GetContainerOrBucketItemCount()
        {
            var itemsCount = await _blobService.GetContainerItemCountAsync();

            // sync
            //var itemsCount = _blobService.GetContainerItemCount();

            return Ok(itemsCount);
        }

        /// <summary>
        /// Get number to total items/files in Container/Bucket
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-container-or-bucket-item-count/{containerOrBucketName}")]
        public async Task<IActionResult> GetContainerOrBucketItemCount(string containerOrBucketName)
        {
            var itemsCount = await _blobService.GetContainerItemCountAsync(containerOrBucketName);

            // sync
            //var itemsCount = _blobService.GetContainerItemCount(containerOrBucketName);

            return Ok(itemsCount);
        }


        /// <summary>
        /// Get Container/Bucket size in bytes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-container-or-bucket-size")]
        public async Task<IActionResult> GetContainerOrBucketSize()
        {
            var size = await _blobService.GetContainerSizeAsync();

            // sync
            //var size = _blobService.GetContainerSize();

            return Ok(size);
        }

        /// <summary>
        /// Get Container/Bucket size in bytes
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-container-or-bucket-size/{containerOrBucketName}")]
        public async Task<IActionResult> GetContainerOrBucketSize(string containerOrBucketName)
        {
            var size = await _blobService.GetContainerSizeAsync(containerOrBucketName);

            // sync
            //var size = _blobService.GetContainerSize(containerOrBucketName);

            return Ok(size);
        }


        /// <summary>
        /// Create Container/Bucket
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("create-container-or-bucket/{containerOrBucketName}")]
        public async Task<IActionResult> CreateContainerOrBucket(string containerOrBucketName)
        {
            var isCreated = await _blobService.CreateContainerAsync(containerOrBucketName);

            // sync
            //var isCreated = _blobService.CreateContainer(containerOrBucketName);

            return Ok(isCreated);
        }

        /// <summary>
        /// Delete Container/Bucket
        /// </summary>
        /// <param name="containerOrBucketName">Name of the Bucket/Container.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-container-or-bucket/{containerOrBucketName}")]
        public async Task<IActionResult> DeleteContainerOrBucket(string containerOrBucketName)
        {
            var isDeleted = await _blobService.DeleteContainerAsync(containerOrBucketName);

            // sync
            //var isDeleted = _blobService.DeleteContainer(containerOrBucketName);

            return Ok(isDeleted);
        }



        /// <summary>
        /// Get all Containers/Buckets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-all-containers-or-buckets")]
        public async Task<IActionResult> GetAllContainersOrBuckets()
        {
            var containers = await _blobService.GetAllContainersAsync();

            // sync
            //var containers = _blobService.GetAllContainers();

            return Ok(containers);
        }

        /// <summary>
        /// Delete all Containers/Buckets
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-all-containers-or-buckets")]
        public async Task<IActionResult> DeleteAllContainersOrBuckets()
        {
            var isDeleted = await _blobService.DeleteAllContainersAsync();

            // sync
            //var isDeleted = _blobService.DeleteAllContainers();

            return Ok(isDeleted);
        }

        #endregion
    }
}
