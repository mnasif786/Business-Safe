param([int] $buildNumber =0, [string] $webconfigFilePath = "")
$ErrorActionPreference = "Stop";


if ($buildNumber -eq 0)
{
    throw "ERROR: buildnumber parameter not set"
}

if ($webconfigFilePath -eq "")
{
    throw "ERROR: $webconfigFilePath parameter not set"
}


#set version number in web.config
[int] $buildNumberSeed = 0
$time = (get-date).tostring("dd.MM.yyyy")
$version = "$time." + ($buildNumber+$buildNumberSeed).ToString()

Write-Host "writing version number to: $version into $webconfigFilePath"

(Get-Content $webconfigFilePath) | Foreach { $_ -replace "<add key=""Version"" value=""(.*?)""(.*?)", "<add key=""Version"" value=""$version""" }| Set-Content $webconfigFilePath;





