Set-StrictMode -Version 2

function AddCounters {

    $counterCollection = new-object System.Diagnostics.CounterCreationDataCollection
    $counterCollection.AddRange($counters)

    [void] [System.Diagnostics.PerformanceCounterCategory]::Create($categoryName, 
																   "NServiceBus performance counters oriented to endpoints.", 
																   [System.Diagnostics.PerformanceCounterCategoryType]::MultiInstance, 
																   $counterCollection)
    [System.Diagnostics.PerformanceCounter]::CloseSharedResources()    # http://blog.dezfowler.com/2007/08/net-performance-counter-problems.html
    Write-Host -ForegroundColor Green "Successfully Added Counters" | Out-Default
}

function DeleteCategory {
    [void] [System.Diagnostics.PerformanceCounterCategory]::Delete($categoryName)
}

function DoesCategoryExist {
    [System.Diagnostics.PerformanceCounterCategory]::Exists($categoryName)
}

$categoryName = "CGS:NSB_EndPoints"

$counters = @(
    (new-object System.Diagnostics.CounterCreationData( "End Point is Active", 
                                                        "When critical fatal exception is received from NSB relating to problems with transport this counter should set to zero, else non zero. Resuming to non zero on first message that arrives in queue successfully.", 
                                                        [System.Diagnostics.PerformanceCounterType]::NumberOfItems32)),

    (new-object System.Diagnostics.CounterCreationData( "Total Messages Started Processing", 
                                                        "Count all messages arrived and started processing by this endpoint. Metric will count also all retry attempts. This metric will give indication on the volume of messages arriving to this endpoint.", 
                                                        [System.Diagnostics.PerformanceCounterType]::NumberOfItems64)),

    (new-object System.Diagnostics.CounterCreationData( "Messages in Process", 
                                                        "Count total number of messages being currently executed concurrently. This metric will give indication on the concurrent load on an endpoint.", 
                                                        [System.Diagnostics.PerformanceCounterType]::NumberOfItems32)),

    (new-object System.Diagnostics.CounterCreationData( "Total Messages Completed Processing Successfully", 
                                                        "Count all messages that completed processing successfully by this endpoint. This metric will give indication on the volume of messages processed by this endpoint.", 
                                                        [System.Diagnostics.PerformanceCounterType]::NumberOfItems64)),

    (new-object System.Diagnostics.CounterCreationData( "Total NSB Retry Errors", 
                                                        "Count total retry errors detected by end point. The application should increase the counter per error received. Normal operation of an end point should be with no retry errors even if the end point was able to recover.", 
                                                        [System.Diagnostics.PerformanceCounterType]::NumberOfItems64))

)

$currentIdentity = [System.Security.Principal.WindowsIdentity]::GetCurrent()
$currentPrincipal = new-object System.Security.Principal.WindowsPrincipal($currentIdentity)
$adminRole = [System.Security.Principal.WindowsBuiltInRole]::Administrator

if (-not $currentPrincipal.IsInRole($adminRole)) {
	Write-Host "Elevated permissions are required to run this script"
	Write-Host "Use an elevated command prompt to complete these tasks"
	exit 1
}

$transcribe = ($args.Count -gt 0)

if ($transcribe) {
    Start-Transcript -Path $args[0] -Force
}
try
{
    # Command: 1 - Install, 2 UnInstall #
	$command = 1

	if ($command -eq 1) {
		if (DoesCategoryExist) {
			DeleteCategory
			Write-Host -ForegroundColor Yellow "Successfully Removed Counters"
		}
        AddCounters
	}
	else {
		if ($command -eq 2) {
			if (DoesCategoryExist) {
				DeleteCategory
				Write-Host -ForegroundColor Yellow "Successfully Removed Counters"
			}
		}
	}
}
finally {
    if ($transcribe) {
        Stop-Transcript
    }
}