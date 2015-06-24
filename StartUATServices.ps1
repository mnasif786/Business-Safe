clear
Write-Host "Starting databases on uatsql2"
invoke-command uatsql2  {
	#Get-Service
    Start-Service -name SQLBrowser
	Start-Service -name MSSQLSERVER
	Start-Service -name SQLSERVERAGENT
	Start-Service -name MSSQL`$UAT
	Start-Service -name SQLAgent`$UAT
}

Write-Host "Database services started"

Write-Host "Starting message handlers on uatbso1"
invoke-command uatbso1  {
	#Get-Service
    Start-Service -name BSOMH
	Start-Service -name BSOMHE
	Start-Service -name POMH
	Start-Service -name POMHE
}

Write-Host "Message handlers started"