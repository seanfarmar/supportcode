This solution illustrates the issue of being able to run the solution with the Lite profile, but not the Production profile. This code is provided only for the purpose of demonstrating an issue and is not meant for distribution or production use.

Build the solution; ensure that the PlexisNsbSample project properties Start Action 'Start external program' selection points to the NServiceBus.Host in your debug bin directory. Command Line Arguments should be set to the PlexisNsbSample.Lite profile.

Run both projects in the solution. Once the service is up and running, you may press enter in the PlexisBusPass console window to fire a message to the PlexisNsbSample. Verify that the message is handled.

To illustrate the issue, stop the debugger.  Change the command Line arguments under the start options to use PlexisNsbSample.Production. 

Start the PlexisNsbSample. Observe that the service goes as far as 'Checking if the queue exists'. Service does not handle messages at this point. 
***Note that in the production profile, the console does not log. All logs will be created in the Logs directory in the project

