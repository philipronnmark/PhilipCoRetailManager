using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace PRMDataManager.App_Start
{
    public class AuthorizationOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if(operation.paramters == null)
            {
                operation.parameters = new List<Parameter>();

            }

            operation.parameters.Add(new Paramter
            {
                name = "Authorization",
                @in = "header",
                description = "access token",
                required = false, 
                type = "string",
            });
        }
    }
}