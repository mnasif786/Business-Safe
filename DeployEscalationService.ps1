param([string[]] $servers, [string]$environment="", [string] $artifactBaseFolderPath ="C:\BusinessSafe\Artifacts\BusinessSafe.EscalationService", [string] $baseDestinationDirectory = "C$\EscalationService\BusinessSafeEscalation")
$ErrorActionPreference = "Stop";

<# To install
 
C:\BusinessSafeEscalation\BusinessSafe.Escalation.exe install"

#>

<# to uninstall

C:\BusinessSafeEscalation\BusinessSafe.Escalation.exe uninstall"

#>

if ($environment -eq "")
{
    throw "ERROR: environment parameter not set"
}

if ($server -eq "")
{
    throw "ERROR: server parameter not set"
}

# Folder Paths
$businessSafeEscalationServiceFolderPath = $artifactBaseFolderPath 

function DeployEscalationService([string] $server, [string] $baseDestinationDirectory, [string] $environment)
{
	# stop the service
	invoke-command $server {
	    Stop-Service -serviceName:BusinessSafeEscalationService
	}
	
	$destination = "\\" + $server  + "\"  + $baseDestinationDirectory 
	
	# copy the artifact folder to the destination folder
	write-host "Deploying BusinessSafeEscalation Service to $destination for environment $environment"
	CopyFolder $businessSafeEscalationServiceFolderPath $destination 
	
    # Don't need just yet!
    ReplaceAppConfigFile $destination "BusinessSafe.EscalationService.exe.config" $environment
    
        
	# start the service
	invoke-command $server {
    	Start-Service -serviceName:BusinessSafeEscalationService
	}
}

#if destination is on another server then use c$\folder
function CopyFolder($source, $destination)
{
	if(test-path -path $destination)
	{
		Remove-Item $destination -Recurse -force
	}	
	Copy-Item $source $destination -recurse -force
}

function ReplaceAppConfigFile([string] $messageHandlerArtifactFolder, [string] $existingAppConfigFileName, [string] $environment)
{
	
	$sourceConfig = $messageHandlerArtifactFolder + "\Configs\App." + $environment + ".Transformed.config"
                $destinationConfigFolder = $messageHandlerArtifactFolder + "\Published"
                copy-item $sourceConfig $destinationConfigFolder -force

                $appConfigFileToRename = $destinationConfigFolder + "\App." + $environment + ".Transformed.config"
                $existingAppConfigFullPath = $destinationConfigFolder + "\" + $existingAppConfigFileName

				#Write-Host "existing $existingAppConfigFullPath. file to rename $appConfigFileToRename"
				if (Test-Path -Path $existingAppConfigFullPath )
				{
					Remove-Item $existingAppConfigFullPath 
					Start-Sleep -s 5
				}
				
                Rename-Item $appConfigFileToRename $existingAppConfigFileName -force
}

function Execute-Command ($command, $numberOfRetries)
{
	$currentRetry = 0;
    $success = $false;
	do {
        try
        {
			& $Command;
			$success = $true;
        }
        catch [System.Exception]
        {
            if ($currentRetry -gt $numberOfRetries) {
                throw $_.Exception;
            } else {
				Write-Host  $_.Exception.ToString();
                Start-Sleep -s 2;
            }
            $currentRetry = $currentRetry + 1;
        }
    } while (!$success);
}


foreach($server in $servers)
{
	$command = {DeployEscalationService $server $baseDestinationDirectory $environment; }
	Execute-Command $command 1
}



Write-Host "Deployment complete"