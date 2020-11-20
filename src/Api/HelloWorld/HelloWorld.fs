namespace Api

    module HelloWorld =
        open System.Net.Http
        open Microsoft.Azure.WebJobs
        open Microsoft.AspNetCore.Mvc
        open Microsoft.Extensions.Logging
        open Microsoft.Azure.WebJobs.Extensions.Http
        open FSharp.Control.Tasks.V2.ContextInsensitive
        open AzureFunctionsDependencyInjectionExtensions
        open AzureFunctions.Extensions.Swashbuckle.Attribute
        open Api.Configuration.Models

        [<Literal>]
        let functionname = "HelloWorld"


        [<FunctionName(functionname)>]
        [<ProducesResponseType(typeof<string>, 200)>]
        let HelloWorld 
            (
                  [<HttpTrigger(AuthorizationLevel.Function, "get", Route = "helloWorld")>] req: HttpRequestMessage
                  //, [<SwaggerIgnore>][<Inject>] dbctx: DatabaseContext
                  , [<SwaggerIgnore>][<Inject>] conf: ApiSettings
                  , [<SwaggerIgnore>] log: ILogger
            ) =
            task {
                return "HelloWorld"
            }
