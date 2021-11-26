# Chris OS Additions
#### Additional functions for OpenSimulator

This module provides some additional functions for OpenSimulator.
It combines several separate modules that I have created in the past. 

### Documentations
Individual documentations can be found here:

### New script functions

#### *DataValue*
> DataValue is a KeyValue database that allows to store data permanently. This data can be used in real time in other scripts. Even if they are located on other regions. The access to the data is limited to groups. Only scripts of the same group can access the same data. This ensures that no unauthorized access can occur.

###### Script commands
`String osGetDataValue(String key)`
> Retrieves a value from the database and outputs it as a string. If there is no entry an empty string is returned.

`void osSetDataValue(String key, String value)`
> Stores a value for this key in the database.

`void osDeleteDataValue(String key)`
> Delete the key from the database

`bool osCheckDataValue(String key)`
> Checks if there is an entry for this key in the database.

#### *EasyDialog*
> EasyDialog allows you to create multi-page dialogs without having to implement the logic in the script. You only need to pass a list with an unlimited number of buttons and the rest gets handled automatically.

###### Script commands
`Integer osEasyDialog(Key avatar, String message, List buttons)`
> Returns an integer that can later be compared to the channel in the listener event. With the help of the channel several dialogs can be handled at the same time.

#### *GetInventoryList*
> Returns a list with all item names from the object inventory without the need to work with llGetInventoryNumber and llGetInventoryName.

###### Script commands
`List osGetInventoryList()`
> Returns a list with all item names from the object inventory.

####*InternalRegionToScriptEvents*
> Forwards some additional events to the script. Teleports, parcel transitions, avatars entering or leaving the region, DataValue events. These events arrive as link_message in the script.

###### Script commands
`void osStartScriptEvents()`
> Enables the event system. Without this call, the events are not forwarded to the script. Must be executed again after each script reset.

`void osStopScriptEvents()`
> Deactivates the event system. After calling, no new events are forwarded to the script.

`void osTriggerCustomEvent(String data)`
> Sends own data via the event system.

#### *Discord Chat Relay*
> This function allows to connect the region chat to a Discord channel. Similar to the IRCBridgeModule but for Discord. You can choose if only the text from humans should be transmitted or everything else that comes from scripts.
For this function you need 2 programs. 
1) This module which transfers the chat to Discord via Discord WebHook.
2) A Discord Bot which transfers the text from Discord to OpenSim.

###### Configuration

***OpenSim Configuration: OpenSim.ini* **

	[Discord]
		WebHookURL = https://discord.com/api/webhooks/...
		ScriptChat = false

***Discord Bot Configuration: config.json* **

For the Discord Bot you need:
 - Discord Bot Token from https://discord.com/developers/applications
 - The Discord channel ID from which the chat should be transferred.
 - One or more OpenSimulator server addresses to send the chat to.
