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

Write-Host "> Import-Instance.ps1 -SimCliFolder `"$SimCliFolder`" -ExportedInstancePath `"$ExportedInstancePath`" -SiteName `"$SiteName`" -ConnectionString `"$ConnectionString`" -TempPath `"$TempPath`" -InstanceRootPath `"$InstanceRootPath`" -PathToLicenseFile `"$PathToLicenseFile`" -Bindings `"$Bindings`" -DatabaseNamePrefix `"$DatabaseNamePrefix`""
Invoke-Expression ".\Import-Instance.ps1 -SimCliFolder `"$SimCliFolder`" -ExportedInstancePath `"$ExportedInstancePath`" -SiteName `"$SiteName`" -ConnectionString `"$ConnectionString`" -TempPath `"$TempPath`" -InstanceRootPath `"$InstanceRootPath`" -PathToLicenseFile `"$PathToLicenseFile`" -Bindings `"$Bindings`" -DatabaseNamePrefix `"$DatabaseNamePrefix`""
