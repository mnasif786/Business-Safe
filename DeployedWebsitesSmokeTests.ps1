param([string]$businessSafeUrl="http://businesssafe.dev-peninsula-online.com/HealthStatus/Index", [string] $businessSafeChecklistUrl ="http://businesssafe.dev-peninsula-online.com/Checklists/HealthStatus/Index")



function CheckWebsiteRunning($url, $websiteName)
{
    
    write-host "$url checking" 
    
    [net.httpWebRequest] $req = [net.webRequest]::create($url)
    $req.Method = "HEAD"
    $req.Timeout = 600000
    
        [net.httpWebResponse] $res = $req.getResponse()
        
        if ($res.StatusCode -ge "200") {
            write-host "`n$websiteName is up and running`n" `
                -foregroundcolor green
        }
        else 
        {
            write-host "`n$websiteName is not running`n" `
                -foregroundcolor red
            throw "$websiteName is not running"
        }
    
    
}

CheckWebsiteRunning $businessSafeUrl 'BusinessSafe'
CheckWebsiteRunning $businessSafeChecklistUrl 'BusinessSafe Checklist'

