param([string[]] $servers, [string]$environment="", [string] $artifactBaseFolderPath ="C:\BusinessSafe\Artifacts\", [string] $baseDestinationDirectory = "C$\inetpub\BusinessSafeOnline", [string] $buildDirectory = "C:\Jenkins\BusinessSafe\BuildingDirectory")
$ErrorActionPreference = "Stop";

if ($environment -eq "")
    {
        throw "ERROR: environment parameter not set"
    }

    if ($server -eq "")
    {
        throw "ERROR: server parameter not set"
    }

function Get-ScriptDirectory
{
    Split-Path $script:MyInvocation.MyCommand.Path
}

function DeployWebsite([string] $server, [string] $baseDestinationDirectory, [string] $environment, $psSession)
{
	write-host "Deploying BSO website STARTED" 
	StopWebSiteAndAppPool $server $businessSafeIISSiteName $businessSafeIISAppPoolName $psSession

    $destination = "\\" + $server  + "\" + $baseDestinationDirectory + "\BusinessSafe.Website"
    write-host "Copying BSO website files from $businessSafeWebsiteFolderPath to $destination for environment $environment"
    CopyFolder $businessSafeWebsiteFolderPath $destination
	ReplaceWebConfigFile $destination $environment
	
	StartWebSiteAndAppPool $server $businessSafeIISSiteName $businessSafeIISAppPoolName $psSession
	write-host "Deploying BSO website COMPLETED"
}

function DeployChecklistWebsite([string] $server, [string] $baseDestinationDirectory, [string] $environment, $psSession)
{
	write-host "Deploying checklist website STARTED"
	StopWebSiteAndAppPool $server $businessSafeIISSiteName $businessSafeIISAppPoolName $psSession

    $destination = "\\" + $server  + "\"  + $baseDestinationDirectory + "\BusinessSafe.Checklists"
    write-host "Deploying checklist website files from $checklistsWebsiteFolderPathName to $destination for environment $environment"
	
    CopyFolder $checklistsWebsiteFolderPathName $destination
	ReplaceWebConfigFile $destination $environment

	StartWebSiteAndAppPool $server $businessSafeIISSiteName $businessSafeIISAppPoolName $psSession
	StartWebSiteAndAppPool $server $businessSafeIISSiteName $businessSafeChecklistsIISAppPoolName $psSession
	write-host "Deploying checklist website COMPLETED"    
    
}

## IIS settings
$businessSafeWebsiteFolderPath = Join-Path $artifactBaseFolderPath  "BusinessSafe.WebSite"
$businessSafeIISSiteName = "BusinessSafe"

$businessSafeIISAppPoolName = "BusinessSafe"
$businessSafeChecklistsIISAppPoolName = "checklists"
$businessSafeAPIIISAppPoolName = "businesssafe_api"

# Folder Paths
#Write-output  "BusinessSafe folder is : "  $businessSafeWebsiteFolderPath
$checklistsWebsiteFolderPathName = Join-Path  $artifactBaseFolderPath  "BusinessSafe.Checklists"
#write-output "Checklists folder is : " $checklistsWebsiteFolderPathName

$sqlScriptFolderPathName = Join-Path  $artifactBaseFolderPath  "SqlScripts"

$pathToSharedFunctions = (Get-ScriptDirectory) + '.\Deploy_SharedFunctions.ps1'

. $pathToSharedFunctions

$credential;
$session;

foreach($server in $servers)
{
	if(($environment -eq "LIVE") -or ($server -eq "UATBSO2"))
	{
		#When invoke commands on server in another domain we need to use the fully qualifed name. This must be setup in DNS.
		$fullyQualifiedServerName = "$server.hq.peninsula-uk.local"
		$securePassword = ConvertTo-SecureString "Password1!" -AsPlainText -force
		$credential = New-Object System.Management.Automation.PsCredential -argumentlist "peninsulaonline\developer",$securePassword
		$session = New-PSSession -computername $fullyQualifiedServerName -credential $credential
		#We need to use the ip address when copying files
		$server = [System.Net.Dns]::GetHostAddresses($server) | select-object IPAddressToString -expandproperty  IPAddressToString
		Write-Host "ip address $server"
	}
	else
	{
		$securePassword = ConvertTo-SecureString "is74rb80pk52" -AsPlainText -force
		$credential = New-Object System.Management.Automation.PsCredential("hq\Continuous.Int",$securePassword)
		$session = New-PSSession -computername $server -credential $credential
	}

	$command = {CopySqlScripts $server $baseDestinationDirectory; }
	Execute-Command $command 1

	$command = {DeployWebsite $server $baseDestinationDirectory $environment $session; }
	Execute-Command $command 1
	$command = {DeployChecklistWebsite $server $baseDestinationDirectory $environment $session;}
	Execute-Command $command 1
	
	Remove-PSSession -session $session
	
}

#revemove all powershell sessions ? Might be a problem if another powershell script is running at the same time
get-pssession | remove-pssession


<# If you get a logon authentication exception when copying files you need to logon onto the computer that is running this script with the user credentials that the script is executing as
For example: when using the CI box (uatbsoci1) to copy to stage (pbsbsostage1)
Logon onto  uatbsoci1 using hq\Continuous.int
Get the ip address of pbsbsostage1. 172.16.1.129
Open window explorer and navigate to \\172.16.1.129\c$
Enter the account details for peninsulaonline\developer and check the box to save credentials
Copying files should now work
#>
