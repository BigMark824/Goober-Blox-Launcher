# Goober-Blox-Launcher
Basic Launcher for my old Roblox Revival, feel free to use this I dont care. Quick tutorial on how to modify some stuff and prerequisites.


# Prerequesites

- Visual Studio 2022
- .NET 6.0

# Tutorial

**Step 1:**

![alt text](https://imgur.com/alpnc3O.png)

Edit the underlined "games" name to something like "RobloxPlayer" or something similar, it will be the protocol name in your browser for example: RobloxPlayer://username=JohnDoe

**Step 2:**

![alt text](https://imgur.com/d9kgJc4.png)

Modify this to your game path (preferrably AppData) and edit your url paramaters here, feel free to add more for IP etc

**Step 2:**

![alt text](https://imgur.com/rT2Avu1.png)

Modify your PHP code (or other language) to accept the username parameter, for me im just making a basic HTML button that uses php to return the username to the protocol

# Known Issues

For some reason when joining a game your username ends with a forward slash "/" this will be fixed and updated but for now this works and is a basic guide on how to create this for your revival!
