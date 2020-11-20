Param(
    [bool]$resetDocker=$true,
    [bool]$build=$true
)
$keyvault = "insert-keyvault-name"
$applicationName = "insert-application-name"
$resourceGroup = "insert-resourcegroupname"
$subscription = "insert-azure-subscription-id"

clear

if($SetEnvs -eq $true){ .\env\getAppSettings.ps1 -Application $applicationName -VaultName $keyvault -ResourceGroup $resourceGroup -Subscription $subscription}
.\env\localDb.ps1
if($resetDocker -eq $true){ .\Docker\nuke.cmd }
docker-compose -f .\Docker\docker-compose.yml up -d
if($build -eq $true){ dotnet build .\insertprojectname.sln }

dotnet ef database update --project .\src\DbRepository\DbRepository.csproj



