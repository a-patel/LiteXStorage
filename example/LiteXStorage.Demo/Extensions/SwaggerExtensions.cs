#region Imports
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
#endregion

namespace LiteXStorage.Demo
{
    /// <summary>
    /// Swagger extensions.
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Add LiteX Storage Swagger services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLiteXStorageSwagger(this IServiceCollection services)
        {
            services.AddSwaggerExamples();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "LiteX Storage",
                    Description = $"LiteX Storage [Azure Storage (Blob), Amazon S3, Google Cloud Storage, File System (Local), Kvpbase (deprecated)]",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Aashish Patel", Email = "toaashishpatel@gmail.com", Url = "http://aashishpatel.co.nf/" },
                    License = new License() { Name = "LiteX LICENSE", Url = "https://github.com/a-patel/LiteXStorage/blob/master/LICENSE" }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.DescribeAllEnumsAsStrings();
                options.IgnoreObsoleteProperties();
                options.IgnoreObsoleteActions();
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                options.ExampleFilters();

                options.OperationFilter<AddResponseHeadersFilter>(); // [SwaggerResponseHeader]
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>(); // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization

                // add Security information to each operation for OAuth2
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                // if you're using the SecurityRequirementsOperationFilter, you also need to tell Swashbuckle you're using OAuth2
                options.AddSecurityDefinition("oauth2", new ApiKeyScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });
            });

            return services;
        }

        /// <summary>
        /// Use LiteX Storage Swagger services
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLiteXStorageSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "LiteX Storage (V7)");
                options.DocumentTitle = "LiteX Storage";
                options.DocExpansion(DocExpansion.None);
                options.DisplayRequestDuration();
            });

            return app;
        }
    }
}
