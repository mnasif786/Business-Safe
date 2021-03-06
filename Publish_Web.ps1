param([string] $buildDirectory = "C:\Jenkins\BusinessSafe\BuildingDirectory", [string] $artifactDirectory = "C:\BusinessSafe\Artifacts")

$ErrorActionPreference = "Stop"

$msBuildPath = "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"

. "$buildDirectory\Publish_SharedFunctions.ps1"

function Remove-Folder ($path)
{
	if(Test-Path $path)
	{
		Remove-Item -Recurse -Force $path
	}
}

if(Test-Path $artifactDirectory)
{
	Write-Host "Clearing down Artifact directory '$artifactDirectory'" -ForegroundColor DarkGreen
	Remove-Folder $artifactDirectory"\BusinessSafe.Checklists"
	Remove-Folder $artifactDirectory"\BusinessSafe.MessageHandlers"
	Remove-Folder $artifactDirectory"\BusinessSafe.MessageHandlers.Emails"
	Remove-Folder $artifactDirectory"\BusinessSafe.Website"
	Remove-Folder $artifactDirectory"\EscalationService"
	Remove-Folder $artifactDirectory"\SqlScripts"	
	
	Start-Sleep -s 5;
}

Ensure-Directory-Exists $artifactDirectory
Ensure-Directory-Exists "$artifactDirectory\SqlScripts"
Publish-Project "BusinessSafe.Website" $True 
Publish-Project "BusinessSafe.Checklists" $True
Publish-Project "BusinessSafe.MessageHandlers" $False 
Publish-Project "BusinessSafe.MessageHandlers.Emails" $False
Publish-Project "BusinessSafe.EscalationService" $False
Publish-Project "BusinessSafe.WCF" $True

Write-Host "Copying SQL Scripts"
$sqlScriptFilter = [regex] "^[0-9]+_.+\.sql"
$sqlScripts = Get-ChildItem -Path "$buildDirectory\SQLScripts\BusinessSafe" | Where-Object {$_.name -match $sqlScriptFilter}

foreach ($sqlScript in $sqlScripts) 
{ 
	Write-Host "Copying SQL Script '$sqlScript'"
	Copy-Item $sqlScript.FullName "$artifactDirectory\SqlScripts"
}
    





