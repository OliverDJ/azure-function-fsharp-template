namespace Api.Configuration

module Models =

    open System
    [<CLIMutable>]
    type ConnectionStrings =
        {
            Database: string
        }

    [<CLIMutable>]
    type ApiSettings = 
        {
            KeyVaultName: string
            AzureWebJobsStorage: string
            ContainerName: string
            ConnectionStrings: ConnectionStrings
        }

