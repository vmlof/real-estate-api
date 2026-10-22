param(
    [string]$BaseUrl = 'http://localhost:5149',
    [int]$LogoutDelayMilliseconds = 0,
    [int]$RefreshDelayMilliseconds = 0
)

$ErrorActionPreference = 'Stop'
$loginBody = @{ email = 'john.smith@example.com'; password = 'change123' } | ConvertTo-Json
$login = Invoke-WebRequest "$BaseUrl/api/auth/login" -Method Post -ContentType 'application/json' -Body $loginBody -UseBasicParsing
$cookie = ($login.Headers['Set-Cookie'] -split ';')[0]
if ($login.StatusCode -ne 200 -or $cookie -notmatch '^refreshToken=') { throw 'Login failed.' }

$startAt = [DateTime]::UtcNow.AddSeconds(2)
$jobs = foreach ($operation in @('refresh-token', 'logout')) {
    Start-Job -ArgumentList $BaseUrl, $cookie, $startAt, $operation, $LogoutDelayMilliseconds, $RefreshDelayMilliseconds -ScriptBlock {
        param($url, $requestCookie, $start, $action, $logoutDelay, $refreshDelay)
        while ([DateTime]::UtcNow -lt $start) { Start-Sleep -Milliseconds 10 }
        if ($action -eq 'logout' -and $logoutDelay -gt 0) { Start-Sleep -Milliseconds $logoutDelay }
        if ($action -eq 'refresh-token' -and $refreshDelay -gt 0) { Start-Sleep -Milliseconds $refreshDelay }
        $response = curl.exe -s -i -X POST -H "Cookie: $requestCookie" "$url/api/auth/$action"
        $status = [int]([regex]::Match(($response -join "`n"), 'HTTP/\d(?:\.\d)? (\d{3})').Groups[1].Value)
        $setCookie = ($response | Where-Object { $_ -match '^Set-Cookie: refreshToken=' } | Select-Object -First 1)
        $replacementCookie = if ($setCookie) { (($setCookie -replace '^Set-Cookie: ', '') -split ';')[0] } else { $null }
        [pscustomobject]@{ Operation = $action; Status = $status; Cookie = $replacementCookie }
    }
}

$results = $jobs | Wait-Job | Receive-Job
$jobs | Remove-Job
$refresh = $results | Where-Object Operation -eq 'refresh-token'
$logout = $results | Where-Object Operation -eq 'logout'
if ($logout.Status -ne 204 -or $refresh.Status -notin @(200, 401)) {
    throw "Unexpected outcome: refresh=$($refresh.Status), logout=$($logout.Status)"
}

if ($refresh.Status -eq 200) {
    if ($refresh.Cookie -notmatch '^refreshToken=') { throw 'Refresh returned no replacement cookie.' }
    $check = curl.exe -s -w 'STATUS:%{http_code}' -X POST -H "Cookie: $($refresh.Cookie)" "$BaseUrl/api/auth/refresh-token"
    if (($check -join '') -notmatch 'STATUS:401$') { throw 'Replacement token remained active after logout.' }
}

Write-Output "PASS: refresh=$($refresh.Status), logout=$($logout.Status); no active replacement after logout."
