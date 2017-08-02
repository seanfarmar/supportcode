======================================================================================================================================================
LICENSE INSTALLATIONS:
======================================================================================================================================================

http://docs.particular.net/nservicebus/licensing/
http://docs.particular.net/nservicebus/licensing/license-management

Developer scenario:
- If trial version is expired:
- Go to "HKEY_CURRENT_USER\Software\ParticularSoftware"
- Two options:
1. Change "TrialStart"
- Find/create "TrialStart" key as string
- Change/set value "TrialStart" to current date (format yyyy-MM-dd)
2. Install key "License" (Multi-string value) and put license file content

Production scenario:
- License file has located by path: "[ApplicationsPath]\CentralSystem.FlowManager.Service\App_Data\NSBLicense\License.xml"
- Every application in the CSEnviroment.config file has a setting:
	<add key="NServiceBus/LicensePath" value="[ApplicationsPath]\CentralSystem.FlowManager.Service\App_Data\NSBLicense\License.xml\License.xml" />


======================================================================================================================================================
BUILT-IN NSERVICEBUS PERFORMANCE COUNTERS INSTALLATIONS:
======================================================================================================================================================

http://docs.particular.net/samples/performance-counters

Actual for servers with NServiceBus based applications (CS Operations, CS Reporting and CS FlowManager). Installation process:
•	Execute NServiceBus installer (find in TFS by “\CommonInfrastructure\NServiceBus\Installations\Particular.NServiceBusPowershell-5.0.2.exe”)
•	Run PowerShell command line as administrator
•	Execute script “Import-Module NServiceBus.PowerShell”
•	Execute script “Install-NServiceBusPerformanceCounters”


======================================================================================================================================================
CUSTOM CGS NSERVICEBUS PERFORMANCE COUNTERS INSTALLATIONS:
======================================================================================================================================================

Actual for servers with NServiceBus based applications (CS Operations, CS Reporting and CS FlowManager). Installation process:
•	Run PowerShell command line as administrator
•	If need - to run command "Set-ExecutionPolicy Unrestricted"
•	Execute script “\CentralSystem\SourcesCS\DotNet\Framework\CentralSystem.Framework.NServiceBus\Scripts\CGS-NSB_EndPoints.performance-counters.installer.ps1”
•	For installation - enter command "1".





