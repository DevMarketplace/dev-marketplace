param (
    [switch] $noinit,

    [switch] $noDbConfig
)

#verifies all prerequisites
function checkForNetCore () {

    #checking for .NetCore runtime
    if((Test-Path "C:\Program Files\dotnet\shared\Microsoft.NETCore.App\1.1.1") -eq $false) {
        Write-Output "The .Net Core runtime 1.1.1 is not installed on your machine.`r`nPlease get it from here: 'https://go.microsoft.com/fwlink/?linkid=843433' ";
        exit;
    }

    #.NetCore SDK
    if((Test-Path "C:\Program Files\dotnet\sdk\1.0.3") -eq $false) {
        Write-Output "The .Net Core SDK 1.0.3 is not installed on your machine.`r`nPlease get it from here: 'https://go.microsoft.com/fwlink/?linkid=843448' "
        exit;
    }
}

function configureDatabase([string] $projectFolder, [string] $connectionString) {
    Push-Location $projectFolder

    $appSettings = (Get-Content "appsettings.json" | Out-String | ConvertFrom-Json)
    $appSettings.ConnectionStrings.Default = $connectionString;
    $appSettings | ConvertTo-Json -depth 100 | Out-File "appsettings.json" -Force    

    Pop-Location
}

function buildProject([string] $folder, [string] $projectName) {
    Push-Location $folder
    
    Write-Output "Restoring nuget packages for $projectName"
    dotnet restore;

    Write-Output "Building $projectName"
    dotnet build;

    if($LASTEXITCODE -ne 0) {
        exit;
    }

    Write-Output "Running $projectName"
    Start-Process powershell {dotnet run};
    Pop-Location;
}

#main routine
function main() {
    if ([System.IntPtr]::Size -eq 4) 
    { 
        Write-Output "You have a 32 bit OS. This script only supports 64 bit OS. Please build the project manually or upgrade your Windows to 64 bit."
        return;
    }

    if($noinit -eq $false) {
        checkForNetCore;
    }

    if($noDbConfig -eq $false) {
        $connectionString = Read-Host -Prompt "Please set a connection string to an empty or existing database"
        configureDatabase -projectFolder ".\src\RestServices\" -connectionString $connectionString;
        configureDatabase -projectFolder ".\src\UI\" -connectionString $connectionString;
    }
    
    buildProject -folder ".\src\RestServices\" -projectName "RestServices"

    buildProject -folder ".\src\UI\" -projectName "DevMarketplace"
}

main;