# Chris OS Configurator
#### Merge OpenSimulator configurations.

This small tool solves a big problem in OpenSimulator. Everybody knows it. You have 100 running simulations and want to change a setting in all of them. Now you have to go into 100 different ini files and adjust them.

This is where this little tool comes in. It allows you to create a config-template for each region and store unique settings for each region. These are then merged with a main config file. This way there is a single "main" configuration but each region can still have its own settings.