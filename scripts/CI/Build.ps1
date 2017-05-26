# Prerequisites:
# - Visual Studio 2015
#   - Extensions:
#     - Web Compiler
#     - EditorConfig
# - NodeJS
# - NPM
# - Gulp

# get params
[CmdletBinding()]
Param(
    [Parameter(Mandatory=$True)] [string]$SimCliFolder,
    [Parameter(Mandatory=$True)] [string]$ExportedInstancePath,
    [Parameter(Mandatory=$True)] [string]$SiteName,
    [Parameter(Mandatory=$True)] [string]$ConnectionString,
    [Parameter(Mandatory=$True)] [string]$TempPath,
    [Parameter(Mandatory=$True)] [string]$InstanceRootPath,
    [Parameter(Mandatory=$True)] [string]$PathToLicenseFile,
    [Parameter(Mandatory=$True)] [string]$Bindings,
    [Parameter(Mandatory=$True)] [string]$DatabaseNamePrefix
)

$ScriptRootToDevRootPath = "..\.."

Write-Host "> Build.ps1: Start Build"

# Navigate to the script root
$InitialShellLocation = (Get-Location).Path
cd $PSScriptRoot

# 1. Import a Sitecore instance with the required modules
$Command = "Import-Instance.ps1 -SimCliFolder `"$SimCliFolder`" -ExportedInstancePath `"$ExportedInstancePath`" -SiteName `"$SiteName`" -ConnectionString `"$ConnectionString`" -TempPath `"$TempPath`" -InstanceRootPath `"$InstanceRootPath`" -PathToLicenseFile `"$PathToLicenseFile`" -Bindings `"$Bindings`" -DatabaseNamePrefix `"$DatabaseNamePrefix`""
Write-Host "> Build.ps1: $Command"
Invoke-Expression ".\$Command"

# Navigate to the dev root
cd $ScriptRootToDevRootPath

# 2. Run NPM install
Write-Host "> Build.ps1: npm install"
npm install

# 3. Run Gulp
Write-Host "> Build.ps1: gulp"
gulp

# Change the Sitecore Administrator password


# Remove the SIM Tool assets from the web root


# Remove the Unicorn, Unit Test and other useless assets from the web root


# Export the resulting Sitecore instance (This is to be deployed in staging/prod or be shared with partners)


# Restore shell initial location
cd $InitialShellLocation

Write-Host "> Build.ps1: Done Build"