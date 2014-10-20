
[string]$profile = "nservicebus.integration NSERVICEBUS.MSMQDISTRIBUTOR";
[array]$workers =  @("Orders.Handler.Worker1","Orders.Handler.Worker2");
 
clear

Write "Running Orders.Handler"
 
$Path =  ".\Orders.Handler\bin\debug"

Start-Process -FilePath $Path\NServiceBus.Host.exe -ArgumentList $profile;

$profile = "nservicebus.integration NSERVICEBUS.MSMQWORKER"

foreach ($worker in $workers) {
	Write "Running $worker"
 
	$Path =  ".\$worker\bin\debug"
 
    Start-Process -FilePath $Path\NServiceBus.Host.exe -ArgumentList $profile;
}

$Path =  ".\Orders.Sender\bin\debug"
$profile = "nservicebus.integration"

Start-Process -FilePath $Path\NServiceBus.Host.exe -ArgumentList $profile;

Write "Done..."