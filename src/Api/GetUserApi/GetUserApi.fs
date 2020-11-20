
namespace Api

    module GetUserApi =
        open System.Net.Http
        open Microsoft.Azure.WebJobs
        open Microsoft.AspNetCore.Mvc
        open Microsoft.Extensions.Logging
        open Microsoft.Azure.WebJobs.Extensions.Http
        open FSharp.Control.Tasks.V2.ContextInsensitive
        open AzureFunctionsDependencyInjectionExtensions
        open AzureFunctions.Extensions.Swashbuckle.Attribute
        open DbRepository
        open Api.Configuration.Models

        open DbService.Access.User
        [<Literal>]
        let functionname = "GetUserApi"

        
        

        [<FunctionName(functionname)>]
        [<ProducesResponseType(typeof<string>, 200)>]
        let HelloWorld 
            (
                  [<HttpTrigger(AuthorizationLevel.Function, "get", Route = "getUser/{identifier}")>] req: HttpRequestMessage
                  , identifier: int
                  , [<SwaggerIgnore>][<Inject>] dbctx: TemplateDatabaseContext
                  , [<SwaggerIgnore>][<Inject>] conf: ApiSettings
                  , [<SwaggerIgnore>] log: ILogger
            ) =
            task {

                let! oUser = (dbctx, identifier) ||> getUser


                return "HelloWorld"
            }
