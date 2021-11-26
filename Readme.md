# Chris OS Configurator
#### Merge OpenSimulator configurations.

This small tool solves a big problem in OpenSimulator. Everybody knows it. You have 100 running simulations and want to change a setting in all of them. Now you have to go into 100 different ini files and adjust them.

This is where this little tool comes in. It allows you to create a config-template for each region and store unique settings for each region. These are then merged with a main config file. This way there is a single "main" configuration but each region can still have its own settings.

------------

## Examples

For demonstration purposes, let's take a problem that everyone knows. Each region needs its own network port.
Normally you would make a configuration and copy it for each region to adjust the port.

With this tool you can use the configuration unchanged.
All you have to do is to start Chris.OS.Configurator.exe before every start of OpenSimulator. The tool looks in the "config-template" folder for matching templates and overwrites the corresponding entries in the OpenSim.ini. This way it is possible to copy a main configuration with changed settings at each start of a region but at the same time start each region with different settings.

#### Examples for Port changes (File: config-template/OpenSim.ini)

    [ChrisOS.Configurator]
        DestinationFile = OpenSim.ini
    
    [Network]
        http_listener_port = 7000
