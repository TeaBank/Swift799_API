using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swift799_API.SwaggerFilters
{
    /// <summary>
    /// Used to mark the request body as a requirment of the AddMessage endpoint,
    /// thus avoiding the need for a [FromBody] tag that would try to deserialize the given text.
    /// 
    /// Reason for avoiding the [FromBody] tag: It would try to convert the plain text with the special characters
    /// in it to string thus throwing an exception.
    /// </summary>
    public class AddMessageFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor.DisplayName.Contains("AddMessage"))
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Required = true,
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["text/plain"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "string"
                            }
                        }
                    }
                };
            }
        }
    }
}
