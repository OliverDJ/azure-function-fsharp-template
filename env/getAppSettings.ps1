

Param(
    [string]$Application,
    [string]$VaultName,
    [string]$ResourceGroup,
    [string]$Subscription
)

function isKeyVaultRef([string]$candidate){
    $marker = "@Microsoft.KeyVault(SecretUri="
    $size = $marker.Length
    if ($candidate.Length -gt $size){
        $subC = $candidate.substring(0, $size)
        if($subC -eq $marker){ return $true }
        else{ return $false }
    }
    else { return $false }
}
function getSecretFromRef([string]$uri){
    $parts = $uri.Split('/')
    $secret = $parts[$parts.Length-2]
    return $secret
}

function createKeyValuePair([string]$key, [string]$value){
    $obj = New-Object -TypeName psobject
    $obj | Add-Member -MemberType NoteProperty -Name Key -Value $key
    $obj | Add-Member -MemberType NoteProperty -Name Value -Value $value
    return $obj
}

function getSecretValueFromKeyvault([string]$secretName, [string]$keyvault, [string]$subscription){
    $r = az keyvault secret show `
        --name $secretName `
        --vault-name $keyvault `
        --subscription $subscription | ConvertFrom-Json
    return $r.value
}

function printArt(){
    $t = " 
  _                 _   ___      _             
 | |   ___  __ __ _| | / __| ___| |_ _  _ _ __ 
 | |__/ _ \/ _/ _\` | | \__ \/ -_)  _| || | '_ \
 |____\___/\__\__,_|_| |___/\___|\__|\_,_| .__/
                                         |_|   "

    $g = "
          .-.,     ,.-.
    '-.  /:::\\   //:::\  .-'
    '-.\|':':' `"` ':':'|/.-'
    `-./`. .-=-. .-=-. .`\.-`
      /=- /     |     \ -=\
     ;   |      |      |   ;
     |=-.|______|______|.-=|
     |==  \  0 /_\ 0  /  ==|
     |=   /'---( )---'\   =|
      \   \:   .'.   :/   /
       `\=   '--`   `--' =/'
         `-=._     _.=-'"

    write-host $t
    write-host "$g`n`n`n"
}


# ---------------------------------------------#
#                  Invocation                  #
#----------------------------------------------#
printArt
$appsettings = 
    az functionapp config appsettings list `
        --name $Application `
        --resource-group $ResourceGroup `
        --subscription $Subscription | ConvertFrom-Json

$pureAppSettings = @()
$kvRefs = @()

write-host "Filtering key vault references..."
foreach ($setting in $appsettings){
    $name = $setting.name
    $value = $setting.value
    if (isKeyVaultRef $value -eq $true ){
        $secret = getSecretFromRef $value
        $appkvPair = createKeyValuePair $name $secret
        $kvRefs += $appkvPair
    }
    else{
        $pureAppSettings += createKeyValuePair $name $value
    }
}

write-host "Resolving key vault references..."
foreach($elem in $kvRefs){
    write-host  "`t - " $elem.Value
    $secretValue = getSecretValueFromKeyvault $elem.Value $VaultName $Subscription
    $pureAppSettings += createKeyValuePair $elem.Key $secretValue
}


write-host "Applying appsettings...`n"
foreach($s in $pureAppSettings){
    $key = $s.Key
    $value = $s.Value
    write-host "`t - setting: $key => $value"
    [Environment]::SetEnvironmentVariable($key, $value)
}

