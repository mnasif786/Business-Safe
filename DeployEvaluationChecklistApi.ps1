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

function DeployApi([string] $server, [string] $baseDestinationDirectory, [string] $environment, $psSession)
{
	write-host "Deploying EvaluationChecklist.Api (SafeCheck) website STARTED" 
	StopWebSiteAndAppPool $server $evaluationChecklistIISSiteName $evaluationChecklistIISAppPoolName $psSession

    $destination = "\\" + $server  + "\" + $baseDestinationDirectory + "\EvaluationChecklist.Api"
    write-host "Copying files from $apiFolderPath to $destination for environment $environment"
    CopyFolder $apiFolderPath $destination
	ReplaceWebConfigFile $destination $environment
	
	StartWebSiteAndAppPool $server $evaluationChecklistIISSiteName $evaluationChecklistIISAppPoolName $psSession
	write-host "Deploying EvaluationChecklist.Api (SafeCheck) COMPLETED"
}


## IIS settings
$apiFolderPath = Join-Path $artifactBaseFolderPath  "EvaluationChecklist.Api"
$evaluationChecklistIISSiteName = "EvaluationChecklist.Api"
$evaluationChecklistIISAppPoolName = "EvaluationChecklist.Api"

# Folder Paths
$pathToSharedFunctions = (Get-ScriptDirectory) + '.\Deploy_SharedFunctions.ps1'

. $pathToSharedFunctions

$credential;
$session;

foreach($server in $servers)
{
	$securePassword = ConvertTo-SecureString "is74rb80pk52" -AsPlainText -force
	$credential = New-Object System.Management.Automation.PsCredential("hq\Continuous.Int",$securePassword)
	$session = New-PSSession -computername $server -credential $credential

	$command = {DeployApi $server $baseDestinationDirectory $environment $session; }
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
