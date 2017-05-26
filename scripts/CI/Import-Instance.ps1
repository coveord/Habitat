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

# normalize paths
$SimCliFolder = [System.IO.Path]::GetFullPath($SimCliFolder)
$ExportedInstancePath = [System.IO.Path]::GetFullPath($ExportedInstancePath)
$TempPath = [System.IO.Path]::GetFullPath($TempPath)
$InstanceRootPath = [System.IO.Path]::GetFullPath($InstanceRootPath)
$PathToLicenseFile = [System.IO.Path]::GetFullPath($PathToLicenseFile)

Write-Host "> Import-Instance.ps1: Start Importing Sitecore Instance"

$Command = "$SimCliFolder\sim.exe"
$Arguments = "import -p `"$ExportedInstancePath`" -s `"$SiteName`" -c `"$ConnectionString`" -t `"$TempPath`" -r `"$InstanceRootPath`" -u True -l `"$PathToLicenseFile`" -b `"$Bindings`" -d `"$DatabaseNamePrefix`""
Write-Host "> Import-Instance.ps1: `"$Command`" $Arguments"
Start-Process -FilePath "$Command" -ArgumentList "$Arguments" -Wait -RedirectStandardOutput stdout.txt -RedirectStandardError stderr.txt

Get-Content stdout.txt | Write-Host
Get-Content stderr.txt | Write-Host

Write-Host "> Import-Instance.ps1: Done Importing Sitecore Instance"
Write-Host ""