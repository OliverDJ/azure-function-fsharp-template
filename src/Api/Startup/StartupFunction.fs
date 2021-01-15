namespace Api.Startup

    

    module HealthCheckSetupFunction =
        open ServiceSetup
        open Microsoft.Azure.WebJobs.Hosting
        open AzureFunctionsDependencyInjectionExtensions.Config
        open Microsoft.Azure.Functions.Extensions.DependencyInjection
        open Microsoft.Extensions.DependencyInjection
        open Microsoft.Extensions.Configuration
        open Microsoft.IdentityModel.Tokens
        open Microsoft.IdentityModel.Protocols.OpenIdConnect
        open Microsoft.IdentityModel.Logging
        open Microsoft.IdentityModel.Protocols

        open Microsoft.Extensions.DependencyInjection
        open Microsoft.Extensions.Configuration
        open Microsoft.IdentityModel.Tokens
        open Microsoft.IdentityModel.Protocols.OpenIdConnect
        open Microsoft.IdentityModel.Logging
        open Microsoft.IdentityModel.Protocols
        open ServiceSetup
        open Microsoft.Azure.WebJobs.Hosting
        open AzureFunctionsDependencyInjectionExtensions.Config
        open Microsoft.Azure.Functions.Extensions.DependencyInjection
        open Microsoft.Extensions.DependencyInjection


        type HealthCheckSetupFunctionStartup () = 
            inherit FunctionsStartup() with  
                override this.Configure (builder: IFunctionsHostBuilder ): unit =  
                    
                    let t = typeof<SecurityKey>;                    // Microsoft.IdentityModel.Tokens
                    let t1 = typeof<OpenIdConnectConfiguration>;    // Microsoft.IdentityModel.Protocols.OpenIdConnect
                    let t2 = typeof<LogHelper>;                     // Microsoft.IdentityModel.Logging
                    let t3 = typeof<AuthenticationProtocolMessage>; // Microsoft.IdentityModel.Protocols

                    let services = builder.Services
                    let sp = services.BuildServiceProvider()
                    let conf = sp.GetService<IConfiguration>()

                    builder.AddDependencyInjection(fun s -> s |> setupServices conf ) |> ignore

        [<assembly: WebJobsStartup(typeof<HealthCheckSetupFunctionStartup>)>]
        do ()
      