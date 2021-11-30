# Chris OS Configurator
#### Merge OpenSimulator configurations.

This small tool will solve a big problem in OpenSimulator. Everybody knows this: You have 100 simlators running and want to change one setting in all of them. Now you have to go into 100 different ini files and adjust them one at a time.

This is where this little tool comes in. It allows you to create a config-template for each simulator and store unique settings for each simulator. These are then merged with a main config file. This way there is a single "main" overall configuration to make changes easy, but each simulator can still have its own settings.


For demonstration purposes, let's take a problem that everyone knows: A setting for "Opensim.ini" was changed in an update - let's say the URI for voice. Yet each simulator also needs its own network port. Normally you would copy the configuration file to each simulator and then adjust the port for each one.

------------

## Examples

- URI for voice changed from "vivox_sip_uri = foobar.vivox.com" to "vivox_sip_uri = new.vivox.com" in "Opensim.ini"
- you create a line "http_listener_port = 7000" in file "OpenSim.ini" in subfolder "config-template" for one simulator, "http_listener_port = 7001" for the second simulator and so on.

With this tool you can now make the change from "vivox_sip_uri = foobar.vivox.com" to "vivox_sip_uri = new.vivox.com" once only and then use this configuraation for all simulators. All you have to do is start Chris.OS.Configurator.exe before you start OpenSimulator. 
You can do this once after a change, e.g. after an update, or run it with a script every time you start OpenSimulator.
The tool will look in the "config-template" folder for matching templates and will overwrite the corresponding entries in the OpenSim.ini. This way it is possible to copy a main configuration with changed settings to all simulators, but still start each simulator with different settings.

#### Examples for Port changes (File: config-template/OpenSim.ini)

    [ChrisOS.Configurator]
        DestinationFile = OpenSim.ini
    
    [Network]
        http_listener_port = 7000
