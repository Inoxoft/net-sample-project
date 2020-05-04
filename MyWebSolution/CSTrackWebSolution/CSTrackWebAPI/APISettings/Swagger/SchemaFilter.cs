using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CytometrixWebAPI.APISettings.Swagger
{
    public class SchemaFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Schemes = new string[] { "https" };
        }
    }
}
