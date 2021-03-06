function Ensure-Directory-Exists ([String] $directory)
{
	if(!(Test-Path $directory))
	{
		Write-Host "Creating directory '$directory'"
		New-Item -ItemType directory -Path $directory
	}
	else
	{
		Write-Host "Directory exists '$directory'"
	}
}

function Publish-Project([String] $projectName, [bool] $webSite)
{
	
	Ensure-Directory-Exists "$artifactDirectory\$projectName"
	Ensure-Directory-Exists "$artifactDirectory\$projectName\Published"
	Ensure-Directory-Exists "$artifactDirectory\$projectName\Configs"
	
	if($webSite)
	{
		$publishCommand = "$msBuildPath $buildDirectory\$projectName\$projectName.csproj /target:'Clean;Build' /p:configuration='Debug' /p:webProjectOutputDir='$artifactDirectory\$projectName\Published' /p:outDir='$artifactDirectory\$projectName\Published\bin\' "
	}
	else
	{
		$publishCommand = "$msBuildPath $buildDirectory\$projectName\$projectName.csproj /target:'Clean;Build' /p:configuration='Debug' /p:outDir='$artifactDirectory\$projectName\Published\' "
	}
	
	Write-Host "Running project build command: $publishCommand"
	Invoke-Expression $publishCommand
	
	if($LastExitCode -ne 0 ){
		Write-Host("MSBuild Failed")
		throw "ERROR: MSBuild Failed"
	}
	
	if($webSite)
	{
		$configPrefix = "web"
	}
	else
	{
		$configPrefix = "app"
	}

	Write-Host "Removing config files from artifact folder"
	Remove-Item "$artifactDirectory\$projectName\Published\$configPrefix.*config"
	Write-Host "Copying Config and Transform files to Config folder"
	Copy-Item "$buildDirectory\$projectName\$configPrefix.config" "$artifactDirectory\$projectName\Configs"
	Copy-Item "$buildDirectory\$projectName\$configPrefix.CI.config" "$artifactDirectory\$projectName\Configs"
	Copy-Item "$buildDirectory\$projectName\$configPrefix.UAT.config" "$artifactDirectory\$projectName\Configs"
	Copy-Item "$buildDirectory\$projectName\$configPrefix.LIVE.config" "$artifactDirectory\$projectName\Configs"
	Copy-Item "$buildDirectory\$projectName\Transformer.proj" "$artifactDirectory\$projectName\Configs"
	$buildConfigCommand = "$msBuildPath $artifactDirectory\$projectName\Configs\Transformer.proj /target:All "
	Write-Host "Running transform build command: $buildConfigCommand"
	
	Invoke-Expression $buildConfigCommand

	
	if($LastExitCode -ne 0 ){
		Write-Host("MSBuild Failed")
		throw "ERROR: MSBuild Failed"
	}

	Write-Host "Removing unneeded files"
	Remove-Item "$artifactDirectory\$projectName\Configs\$configPrefix.config"
	Remove-Item "$artifactDirectory\$projectName\Configs\$configPrefix.CI.config"
	Remove-Item "$artifactDirectory\$projectName\Configs\$configPrefix.UAT.config"
	Remove-Item "$artifactDirectory\$projectName\Configs\$configPrefix.LIVE.config"
	Remove-Item "$artifactDirectory\$projectName\Configs\Transformer.proj"
}