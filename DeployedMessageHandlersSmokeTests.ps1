param([string]$server="uatbso1")


function CheckServiceRunning($server, $serviceName, $friendlyServiceName)
{
    invoke-command $server { param($serviceName, $friendlyServiceName)
        $m = Get-Service | Where-Object { $_.Name -eq $serviceName -and  $_.Status -eq "Running"} | measure

        if($m.Count -eq 0){
           throw "$friendlyServiceName is not running"
        }
        
         write-host "`n$friendlyServiceName is up and running`n" `
                    -foregroundcolor green
   } -args $serviceName, $friendlyServiceName
    
}



CheckServiceRunning $server "BSOMHE" "Business Safe Message Handlers Emails"
CheckServiceRunning $server "BSOMH" "Business Safe Message Handlers"