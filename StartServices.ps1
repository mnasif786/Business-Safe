param([string]$server="")

invoke-command $server {
    Start-Service -serviceName:BSOMH
}

invoke-command $server {
    Start-Service -serviceName:BSOMHE
}

