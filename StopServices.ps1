param([string]$server="")

invoke-command $server {
    Stop-Service -serviceName:BSOMH
}

invoke-command $server {
    Stop-Service -serviceName:BSOMHE
}