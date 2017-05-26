$SimCliFolder = "" # Path to the folder that contains "sim.exe". E.g.: "C:\SIM.CLI"
$ExportedInstancePath = "C:\SIM Exports\Sitecore 82u1 - WFFM.zip" # E.g.: "C:\SimToolExport.zip"
$SiteName = "Habitat.Coveo.dev.local"
$ConnectionString = "" # E.g.: "Data Source=...;User ID=...;Password=..."
$TempPath = "C:\Habitat.Coveo.temp"
$InstanceRootPath = "C:\websites"
$PathToLicenseFile = "" # E.g.: "C:\license.xml"
$Bindings = "habitat.coveo.dev.local:80"
$DatabaseNamePrefix = "Habitat.Coveo.dev.local"

# Navigate to the script root
$InitialShellLocation = (Get-Location).Path
cd $PSScriptRoot

# Call Build.ps1 with dev settings
$Command = "Build.ps1 -SimCliFolder `"$SimCliFolder`" -ExportedInstancePath `"$ExportedInstancePath`" -SiteName `"$SiteName`" -ConnectionString `"$ConnectionString`" -TempPath `"$TempPath`" -InstanceRootPath `"$InstanceRootPath`" -PathToLicenseFile `"$PathToLicenseFile`" -Bindings `"$Bindings`" -DatabaseNamePrefix `"$DatabaseNamePrefix`""
Write-Host "> $Command"
Invoke-Expression ".\$Command"

# Restore shell initial location
cd $InitialShellLocation