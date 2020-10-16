$apiKey = "fe68f856-1971-3bf7-841d-adfe9f0ffd5d"
$source = "http://119.23.53.36:8081/"
$packageDir="output"

Write-Host "------------Start--------------"

.\nuget.exe push -Source $source -ApiKey $apiKey $packageDir\LinkToken.External.WebApi.1.0.0.nupkg

Write-Host "-----------Finished------------"
"Any key to exit"  ;
 Read-Host | Out-Null ;
Exit