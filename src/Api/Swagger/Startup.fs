namespace AzureFunctionsOpenApiExtensions


    module Startup =
        open System.Reflection
        open System
        open Microsoft.OpenApi
        open Microsoft.OpenApi.Models
        open Microsoft.Azure.WebJobs.Hosting
        open Swashbuckle.AspNetCore.SwaggerGen
        open AzureFunctions.Extensions.Swashbuckle
        open Microsoft.Extensions.DependencyInjection
        open AzureFunctions.Extensions.Swashbuckle.Settings
        open Microsoft.Azure.Functions.Extensions.DependencyInjection
        open Microsoft.IdentityModel.Tokens
        open Microsoft.IdentityModel.Protocols.OpenIdConnect
        open Microsoft.IdentityModel.Logging
        open Microsoft.IdentityModel.Protocols
        open System.Linq
        type AdditionalParametersDocumentFilter ()  =
            interface IDocumentFilter with
                override this.Apply (openApiDoc: OpenApiDocument, context: DocumentFilterContext) =
                    let filterProperties (x: Collections.Generic.KeyValuePair<string, OpenApiSchema>) =
                        if x.Value.AdditionalProperties = null 
                        then 
                            x.Value.AdditionalPropertiesAllowed <- true
                        else 
                            ()

                    context.SchemaRepository.Schemas 
                    |> Seq.iter(filterProperties)


        type SwashBuckleStartup () = 
            inherit FunctionsStartup() with
                let t = typeof<SecurityKey>; // Microsoft.IdentityModel.Tokens
                let t1 = typeof<OpenIdConnectConfiguration>; // Microsoft.IdentityModel.Protocols.OpenIdConnect
                let t2 = typeof<LogHelper>; // Microsoft.IdentityModel.Logging
                let t3 = typeof<AuthenticationProtocolMessage>; // Microsoft.IdentityModel.Protocols

                let configureSwaggerGen  (x: SwaggerGenOptions) =
                    x.EnableAnnotations()
                    x.DocumentFilter<AdditionalParametersDocumentFilter>()

                    x.CustomSchemaIds(fun y -> y.FullName);
                    ()
                let configureOptions (opts: SwaggerDocOptions) =
                    opts.SpecVersion <- OpenApiSpecVersion.OpenApi3_0
                    opts.PrependOperationWithRoutePrefix <- true
                    opts.AddCodeParameter <- false
                    opts.ConfigureSwaggerGen <- (fun x -> configureSwaggerGen x)
                    ()

                override this.Configure (builder: IFunctionsHostBuilder ): unit =
                    let a = Assembly.GetExecutingAssembly()
                    builder.AddSwashBuckle(a, fun opts -> configureOptions opts ) 
                    |> ignore
                   

        [<assembly: WebJobsStartup(typeof<SwashBuckleStartup>)>]
        do ()