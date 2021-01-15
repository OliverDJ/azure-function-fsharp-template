namespace Api.Startup

    module ServiceSetup = 
        
        open System
        open Microsoft.Extensions.DependencyInjection
        open Microsoft.Extensions.Configuration
        //open Microsoft.EntityFrameworkCore
        //open Azure.Identity

        open Api.Configuration.Models
        let findServiceDescriptorByName name (s:ServiceDescriptor) = 
            match s.ImplementationType with 
            | null -> false 
            | a -> a.Name = name 

        let createKeyVaultUrl kvName = sprintf @"https://%s.vault.azure.net/" kvName

        let registerDefaults (conf:IConfiguration) (services: IServiceCollection) =
            let config = conf.Get<ApiSettings>()

            config, services
                .AddLogging()
                .AddOptions()
                .AddHttpClient()
                .AddSingleton(config)

        //let registerHealthCheck config (services:IServiceCollection) = 
        //    let keyvaulturl = config.KeyVaultName |> createKeyVaultUrl
        //    let kvToken = ManagedIdentityCredential()
        //    services
        //            .AddHealthChecks()
        //            .AddSqlServer(config.ConnectionStrings.InvoicingDatabase)
        //            .AddTcpHealthCheck((fun s -> ()), "TCP check" )
        //            .AddPrivateMemoryHealthCheck(1000000000L)
        //            .AddDiskStorageHealthCheck((fun s -> ()), "Disk check")
        //            .AddAzureBlobStorage(config.AzureWebJobsStorage, config.ContainerName, "storage check")
        //            .AddAzureKeyVault(keyvaulturl |> Uri, kvToken, (fun s -> s.UseKeyVaultUrl(keyvaulturl) |> ignore), "keyvault")
        //    |> ignore

        //    services
        //        .Remove(services.FirstOrDefault(fun s -> findServiceDescriptorByName "HealthCheckPublisherHostedService" s)) |> ignore

        //    config, services

        //let inline registerDbContext<'DbContext when 'DbContext :> DbContext> f (config) (services:IServiceCollection)  =
        //    let connStr:string = config |> f
        //    config, services
        //        .AddEntityFrameworkSqlServer()
        //        .AddDbContextPool<'DbContext>(fun o -> o.UseSqlServer(connStr, fun x -> x.UseNetTopologySuite() |> ignore) |> ignore)

            
        let setupServices 
            (conf:IConfiguration) 
            (services:IServiceCollection) =

            (conf, services)
            ||> registerDefaults
            //||> registerDbContext<DbRepository.InvoicingDatabaseContext>  (fun x -> x.ConnectionStrings.InvoicingDatabase)
            |> ignore
            //||> registerHealthCheck    

            services.BuildServiceProvider() :> IServiceProvider |> ignore