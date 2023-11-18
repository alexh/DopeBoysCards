param(
    [Parameter(Mandatory)]
    [ValidateSet('Debug','Release','MultiplayerDebug')]
    [System.String]$Target,
    [System.String]$VersionOverride
)

# Make sure Get-Location is the script path
Push-Location -Path (Split-Path -Parent $MyInvocation.MyCommand.Path)

# Globals
$ModName = "DopeBoys"
$PluginFolder = "$env:USERPROFILE\AppData\Roaming\Thunderstore Mod Manager\DataFolder\ROUNDS\profiles\DEV\BepInEx\plugins/Phalex-$ModName"
$ProjectPath = "$env:USERPROFILE\source\repos\DopeBoys"
$ReadmeFile = "$ProjectPath\README.md"
$ManifestFile = "$ProjectPath\manifest.json"
$ThunderStoreFolder = "$ProjectPath\ThunderStore"
$LicenseFile = "$ProjectPath\LICENSE"
$TargetPath = "$ProjectPath\bin\$Target\netstandard2.1"
$TargetAssembly = "$ModName.dll"

# Get Version Tag from Manifest
    $manifest = Get-Content $ManifestFile | ConvertFrom-Json
    $Version = $manifest.version_number
    $ManifestVersion = $Version
    if ($VersionOverride) {$Version = $VersionOverride}

Write-Host "v$Version"

# Go
Write-Host "Making for $Target from $TargetPath"


# Debug copies the dll to ROUNDS
if ($Target.Equals("Debug")) {
    Write-Host "Updating local installation in Thunderstore DEV profile"
    
    # $plug = New-Item -Type Directory -Path "$RoundsPath\BepInEx\plugins\$name" -Force
    Write-Host "Copy $TargetAssembly to $PluginFolder"
    Copy-Item -Path "$TargetPath\$ModName.dll" -Destination $PluginFolder -Force
}

# Release package for ThunderStore
if($Target.Equals("Release") ) {
    $package = "$ProjectPath\release"
    $zipPath = "$package\Thunderstore\$ModName.$Version.zip"

    Write-Host "Packaging for ThunderStore"
    New-Item -Type Directory -Path "$package\Thunderstore" -Force
    $thunder = New-Item -Type Directory -Path "$package\Thunderstore\package"
    $thunder.CreateSubdirectory('plugins')
    Copy-Item -Path "$TargetPath\$ModName.dll" -Destination "$thunder\plugins\"
    Copy-Item -Path $ReadmeFile -Destination "$thunder\README.md"
    Copy-Item -Path "$ProjectPath\manifest.json" -Destination "$thunder\manifest.json"
    Copy-Item -Path "$ProjectPath\icon.png" -Destination "$thunder\icon.png"

    ((Get-Content -path "$thunder\manifest.json" -Raw) -replace "`"version_number`": `"$ManifestVersion`"", "`"version_number`": `"$Version`"") | Set-Content -Path "$thunder\manifest.json"

    if ([System.IO.File]::Exists($zipPath)) {
        Remove-Item -Path "$package\Thunderstore\$ModName.$Version.zip" -Force
    }

    Compress-Archive -Path "$thunder\*" -DestinationPath "$package\Thunderstore\$ModName.$Version.zip"
    $thunder.Delete($true)

}

if($Target.Equals("Release")) {
    $package = "$ProjectPath\release"
    Copy-Item -Path "$TargetPath\$ModName.dll" -Destination "$package\$ModName.$Version.dll"
}

Pop-Location