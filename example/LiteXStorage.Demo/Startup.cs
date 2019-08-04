#region Imports
using LiteX.Extensions.Logging;
using LiteX.Storage.Azure;
using LiteX.Storage.AmazonS3;
using LiteX.Storage.FileSystem;
using LiteX.Storage.GoogleCloud;
using LiteX.Storage.Kvpbase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
#endregion

namespace LiteXStorage.Demo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            #region LiteX Storage

            #region LiteX Storage (Register more than one providers)

            // Register provider using factory (AmazonS3)
            // 1. Use default configuration from appsettings.json's 'AmazonS3Config'
            services.AddLiteXAmazonS3ServiceFactory();

            //OR
            // 2. Load configuration settings using options.
            services.AddLiteXAmazonS3ServiceFactory(option =>
            {
                option.AmazonAwsSecretAccessKey = "";
                option.AmazonAwsAccessKeyId = "";
                option.AmazonBucketName = "";
                option.AmazonRegion = "";
                option.EnableLogging = true;
            }, providerName: "amazons3");

            //OR
            // 3. Load configuration settings on your own.
            // (e.g. appsettings, database, hardcoded)
            var amazonS3Config = new AmazonS3Config()
            {
                AmazonAwsSecretAccessKey = "",
                AmazonAwsAccessKeyId = "",
                AmazonBucketName = "",
                AmazonRegion = "",
                EnableLogging = true
            };
            services.AddLiteXAmazonS3ServiceFactory(providerName: "amazons3", config: amazonS3Config);

            // TODO: register more providers using factory


            // register default provider (last registered provider is the default one)
            services.AddLiteXAzureBlobStorageService();

            #endregion

            #region LiteX Storage (Azure)

            // 1. Use default configuration from appsettings.json's 'AzureBlobStorageConfig'
            services.AddLiteXAzureBlobStorageService();

            //OR
            // 2. Load configuration settings using options.
            services.AddLiteXAzureBlobStorageService(option =>
            {
                option.AzureBlobStorageConnectionString = "";
                option.AzureBlobStorageContainerName = "";
                option.AzureBlobStorageEndPoint = "";
                option.EnableLogging = true;
            });

            //OR
            // 3. Load configuration settings on your own.
            // (e.g. appsettings, database, hardcoded)
            var azureBlobStorageConfig = new AzureBlobStorageConfig()
            {
                AzureBlobStorageConnectionString = "",
                AzureBlobStorageContainerName = "",
                AzureBlobStorageEndPoint = "",
                EnableLogging = true
            };
            services.AddLiteXAzureBlobStorageService(azureBlobStorageConfig);

            #endregion

            #region LiteX Storage (Amazon)

            // 1. Use default configuration from appsettings.json's 'AmazonS3Config'
            services.AddLiteXAmazonS3Service();

            //OR
            // 2. Load configuration settings using options.
            services.AddLiteXAmazonS3Service(option =>
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
            var amazonS3Config1 = new AmazonS3Config()
            {
                AmazonAwsSecretAccessKey = "",
                AmazonAwsAccessKeyId = "",
                AmazonBucketName = "",
                AmazonRegion = "",
                EnableLogging = true
            };
            services.AddLiteXAmazonS3Service(amazonS3Config1);

            #endregion

            #region LiteX Storage (Google)

            // 1. Use default configuration from appsettings.json's 'GoogleCloudStorageConfig'
            services.AddLiteXGoogleCloudStorageService();

            //OR
            // 2. Load configuration settings using options.
            services.AddLiteXGoogleCloudStorageService(option =>
            {
                option.GoogleProjectId = "";
                option.GoogleJsonAuthPath = "";
                option.GoogleBucketName = "";
                option.EnableLogging = true;
            });

            //OR
            // 3. Load configuration settings on your own.
            // (e.g. appsettings, database, hardcoded)
            var googleCloudStorageConfig = new GoogleCloudStorageConfig()
            {
                GoogleProjectId = "",
                GoogleJsonAuthPath = "",
                GoogleBucketName = "",
                EnableLogging = true
            };
            services.AddLiteXGoogleCloudStorageService(googleCloudStorageConfig);

            #endregion

            #region LiteX Storage (FileSystem-Local)

            // 1. Use default configuration from appsettings.json's 'FileSystemStorageConfig'
            services.AddLiteXFileSystemStorageService();

            //OR
            // 2. Load configuration settings using options.
            services.AddLiteXFileSystemStorageService(option =>
            {
                option.Directory = "UploadFolder";
                option.EnableLogging = true;
            });

            //OR
            // 3. Load configuration settings on your own.
            // (e.g. appsettings, database, hardcoded)
            var fileSystemStorageConfig = new FileSystemStorageConfig()
            {
                Directory = "",
                EnableLogging = true
            };
            services.AddLiteXFileSystemStorageService(fileSystemStorageConfig);

            #endregion

            #region LiteX Storage (Kvpbase)

            // 1. Use default configuration from appsettings.json's 'KvpbaseStorageConfig'
            services.AddLiteXKvpbaseStorageService();

            //OR
            // 2. Load configuration settings using options.
            services.AddLiteXKvpbaseStorageService(option =>
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
            var kvpbaseBlobConfig = new KvpbaseStorageConfig()
            {
                KvpbaseApiKey = "",
                KvpbaseEndpoint = "",
                KvpbaseContainer = "",
                KvpbaseUserGuid = "",
                EnableLogging = true
            };
            services.AddLiteXKvpbaseStorageService(kvpbaseBlobConfig);

            #endregion

            #endregion

            // add logging (optional)
            services.AddLiteXLogging();

            services.AddLiteXStorageSwagger();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseLiteXStorageSwagger();

            app.UseMvcWithDefaultRoute();
        }
    }
}
