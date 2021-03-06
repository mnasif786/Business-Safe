
#if destination is on another server then use c$\folder
function CopyFolder($source, $destination)
{
    if(test-path -path $destination)
    {
        Remove-Item $destination -Recurse -force
    }  
	
    Copy-Item $source $destination -recurse -force
}

function StopWebSiteAndAppPool([String] $server, [String] $sitename, [string] $appPoolName, $psSession)
{
if ($psSession -eq $null)
    {
        throw "ERROR: psSession is null"
    }

	Write-Host "Stopping website $sitename on $server"
	
	 $command = { param($sitename, $appPoolName)
		Import-Module WebAdministration;   
									
		$site = "IIS:\sites\" + $sitename
		$state = Get-WebItemState $site
		if ($state.value -eq "Started")
		{
			Stop-WebSite $sitename
		}
		
		$pool = "IIS:\apppools\" + $appPoolName
		$state = Get-WebItemState $pool
		if ($state.value -eq "Started")
		{
			Stop-WebAppPool $appPoolName
		}
		
		#this is required to prevent an access denied message when renaming the existing folder
		Get-WebItemState $site
		Get-WebItemState $pool
    }

	
	Invoke-Command -session $psSession -scriptblock $command -argumentlist $sitename, $appPoolName
	
	
}

function StartWebSiteAndAppPool([String] $server, [String] $sitename, [string] $appPoolName, $psSession)
{
if ($psSession -eq $null)
    {
        throw "ERROR: psSession is null"
    }
	Write-Host "Starting website $sitename on $server"
	 $command = {param($sitename, $appPoolName)
		Import-Module WebAdministration;   

		$pool = "IIS:\apppools\" + $appPoolName
		$state = Get-WebItemState $pool
		if ($state.value -eq "Stopped")
		{
			Start-WebAppPool $appPoolName
		}
									
		$site = "IIS:\sites\" + $sitename
		$state = Get-WebItemState $site
		if ($state.value -eq "Stopped")
		{
			Start-WebSite $sitename
		}
		
		#this is required to prevent an access denied message when renaming the existing folder
		Get-WebItemState $site
		Get-WebItemState $pool
    }
	
    Invoke-Command -session $psSession -scriptblock $command -argumentlist $sitename, $appPoolName
}

function ReplaceWebConfigFile([string] $websiteArtifactFolder, [string] $environment)
{
	
	$sourceConfig = $websiteArtifactFolder + "\Configs\Web." + $environment + ".Transformed.config"
                $destinationConfigFolder = $websiteArtifactFolder + "\Published"
                copy-item $sourceConfig $destinationConfigFolder -force

                $webConfigFileToRename = $destinationConfigFolder + "\Web." + $environment + ".Transformed.config"
                $existingWebConfig =$destinationConfigFolder + "\Web.config"

				if (Test-Path -Path $existingWebConfig )
				{
					Remove-Item $existingWebConfig 
					Start-Sleep -s 5
				}
				
                Rename-Item $webConfigFileToRename web.config -force
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

function CopySqlScripts([string] $server, [string] $baseDestinationDirectory)
{
	$destination = "\\" + $server  + "\"  + $baseDestinationDirectory  + "\SqlScripts"
	write-host "Copy from $sqlScriptFolderPathName to $destination"
	CopyFolder $sqlScriptFolderPathName $destination
	
	<# delete any rogue deployment sql scripts #>
	$deployFile = $destination + "\DBDeploy_BusinessSafe_*";
	Remove-Item $deployFile;
}
