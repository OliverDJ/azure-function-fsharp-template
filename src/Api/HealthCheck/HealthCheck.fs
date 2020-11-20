namespace Api

module HealthCheck =
    open System.Net.Http
    open Microsoft.Azure.WebJobs
    open Microsoft.Azure.WebJobs.Extensions.Http
    open FSharp.Control.Tasks.V2.ContextInsensitive
    open Microsoft.Extensions.Diagnostics.HealthChecks
    open AzureFunctions.Extensions.Swashbuckle.Attribute
    open AzureFunctionsDependencyInjectionExtensions

    [<Literal>]
    let functionname = "HealthCheck"

    [<SwaggerIgnore>]
    [<FunctionName(functionname)>]
    //[<ProducesResponseType(typeof<HealthReport>, 200)>]
    let healthCheck 
        (
              [<HttpTrigger(AuthorizationLevel.Function, "get", Route = functionname)>] req: HttpRequestMessage
            , [<Inject>] _hcs2: HealthCheckService
        ) =
        task {
            return! _hcs2.CheckHealthAsync()
        }
