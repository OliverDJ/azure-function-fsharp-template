namespace Api.Swagger

    module Swagger =
        open System.Net.Http
        open Microsoft.Azure.WebJobs
        open AzureFunctions.Extensions.Swashbuckle
        open Microsoft.Azure.WebJobs.Extensions.Http
        open FSharp.Control.Tasks.V2.ContextInsensitive
        open AzureFunctions.Extensions.Swashbuckle.Attribute

        [<SwaggerIgnore>]
        [<FunctionName("Swagger")>]
        let runSwaggerJson 
            (
                  [<HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Swagger/json")>] req: HttpRequestMessage
                , [<SwashBuckleClient>]swashBuckleClient: ISwashBuckleClient
            ) =
            task{
                return swashBuckleClient.CreateSwaggerDocumentResponse(req)
            }

        [<SwaggerIgnore>]
        [<FunctionName("SwaggerUi")>]
        let runSwaggerUI 
            (
                  [<HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Swagger/ui")>] req: HttpRequestMessage
                , [<SwashBuckleClient>]swashBuckleClient: ISwashBuckleClient
            ) =
            task{
                return swashBuckleClient.CreateSwaggerUIResponse(req, "swagger/json")
            }

    
