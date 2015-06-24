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

function DeployBusinessSafeWcfService([string] $server, [string] $baseDestinationDirectory, [string] $environment, $psSession)
{
	write-host "Deploying BusinessSafe WCF STARTED" 
	StopWebSiteAndAppPool $server $businessSafeWcfIisSiteName $businessSafeWcfIisAppPoolName $psSession

    $destination = "\\" + $server  + "\" + $baseDestinationDirectory + "\BusinessSafe.WCF"
    write-host "Copying BusinessSafe WCF files from $businessSafeWcfFolderPath to $destination for environment $environment"
    CopyFolder $businessSafeWcfFolderPath $destination
	ReplaceWebConfigFile $destination $environment
	
	StartWebSiteAndAppPool $server $businessSafeWcfIisSiteName $businessSafeWcfIisAppPoolName $psSession
	write-host "Deploying BusinessSafe WCF COMPLETED"
}

## IIS settings
$businessSafeWcfFolderPath = Join-Path $artifactBaseFolderPath "BusinessSafe.WCF"
$businessSafeWcfIisSiteName = "BusinessSafe.WCF"
$businessSafeWcfIisAppPoolName = "businesssafewcf"


$pathToSharedFunctions = (Get-ScriptDirectory) + '.\Deploy_SharedFunctions.ps1'

. $pathToSharedFunctions

$credential;
$session;

foreach($server in $servers)
{
	
	$securePassword = ConvertTo-SecureString "is74rb80pk52" -AsPlainText -force
	$credential = New-Object System.Management.Automation.PsCredential("hq\Continuous.Int",$securePassword)
	$session = New-PSSession -computername $server -credential $credential

	$command = {DeployBusinessSafeWcfService $server $baseDestinationDirectory $environment $session; }
	Execute-Command $command 1

	Remove-PSSession -session $session
}

#revemove all powershell sessions ? Might be a problem if another powershell script is running at the same time
get-pssession | remove-pssession

