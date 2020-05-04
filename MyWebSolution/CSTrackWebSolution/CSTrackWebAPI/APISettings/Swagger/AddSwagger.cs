using CSTrackWebAPI.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CytometrixWebAPI.APISettings.Swagger
{
    public static class SwaggerConfiguration
    {
        public static void AddSwagger(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Constants.APIVersion, new Info { Title = GetApplicationName(), Version = Constants.APIVersion });

                // Add Grouping to SwaggerUI.
                c.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));

                // This is a fix for the deprecated method.
                c.TagActionsBy((api) =>
                {
                    List<string> groupNames = new List<string>
                    {
                        api.GroupName
                    };

                    return groupNames;
                });

                // Add Http and Https Schemas to SwaggerUI.
                c.DocumentFilter<SchemaFilter>();

                // Include Descriptions from XML Comments
                var filePath = Path.Combine(AppContext.BaseDirectory, "CSTrackWebAPI.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public static void UseSwaggerBase(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{Constants.APIVersion}/swagger.json", $"{GetApplicationName()} {Constants.APIVersion}");
            });
        }

        private static string GetApplicationName() => Assembly.GetCallingAssembly().GetName().Name;
    }
}
