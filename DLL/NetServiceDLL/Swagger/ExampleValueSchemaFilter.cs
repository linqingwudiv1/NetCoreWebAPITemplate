using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace NetApplictionServiceDLL.Swagger
{
    /// <summary>
    /// 默认值过滤器 chage 3
    /// </summary>
    public class ExampleValueSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null)
            {
                return;
            }

            foreach (var property in schema.Properties)
            {
                if (property.Value.Default != null && property.Value.Example == null)
                {
                    property.Value.Example = property.Value.Default;
                }
            }
        }
        //throw new System.NotImplementedException();
    }
}
