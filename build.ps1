param (
    [switch] $noinit
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
    
    Push-Location .\src\RestServices\
    
    Write-Output "Restoring nuget packages for RestServices"
    dotnet restore;

    Write-Output "Building RestServices"
    dotnet build;

    if($LASTEXITCODE -ne 0) {
        exit;
    }

    Write-Output "Running RestServices"
    dotnet run;

}

main;