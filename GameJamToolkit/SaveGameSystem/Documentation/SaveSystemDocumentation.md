# Save Game System

## Profile

The Profile class represents a player's game progress and settings.

### Properties

Name: The name of the player.<br>
LastSave: The date and time of the last save for this profile.

## ProfileSelector

The ProfileSelector class manages the selection, creation, and deletion of player profiles.

### Properties

ActiveProfile: The currently selected player profile.

### Methods
SetActiveProfile(profileName: string): Sets the active player profile based on the provided profile name.<br>
UpdateProfile(profile: Profile): Updates the provided profile's last save timestamp.<br>
DeleteProfile(profile: Profile): Deletes the specified profile and its associated data.<br>

## SaveSlotId

An enumeration of possible save slot identifiers.

## FileSaveSystem (implements ISaveSystem)

The FileSaveSystem class handles saving, loading, and deleting game data using files.

### Methods

SaveData(key: string, json: string): Saves data in JSON format to a file.<br>
LoadData(key: string): Loads data from a file based on the provided key.<br>
SaveExists(slotId: SaveSlotId): Checks if a save file exists for a specific save slot.<br>
DeleteSave(key: string): Deletes a save file based on the provided key.<br>

## ISaveSystem

The ISaveSystem interface defines the methods required for a save system implementation.

### Methods

SaveExists(slotId: SaveSlotId): Checks if a save exists for a specific save slot.<br>
SaveData(key: string, json: string): Saves data using the provided key and JSON data.<br>
LoadData(key: string): Loads data using the provided key.<br>
DeleteSave(key: string): Deletes a save file using the provided key.<br>

## SaveSystem

The SaveSystem class manages the overall save system functionality.

### Properties

ActiveSlotId: The currently active save slot identifier.

### Methods

Save(key: string, json: string): Saves data using the active save system.<br>
Load<T>(key: string): Loads data of type T using the active save system.<br>
SaveExists(slotId: SaveSlotId): Checks if a save exists for a specific save slot.<br>
HasSaveGames(): Checks if there are any save games across all save slots.<br>
SetActiveSlot(slotId: SaveSlotId): Sets the active save slot.<br>
GetLastModified(slotId: SaveSlotId): Gets the last modified date for a specific save slot.<br>
SlotIsPopulated(slotId: SaveSlotId): Checks if a specific save slot is populated.<br>
DeleteSaveSlot(slotId: SaveSlotId): Deletes data associated with a specific save slot.<br>

## Helper Methods

GetProfileFolder(profileName: string): Returns the path to the profile folder.<br>
GetSaveFolder(profileName: string, slotId: SaveSlotId): Returns the path to a specific save folder.<br>
GetSaveFolderForCurrentSaveSlot(profileName: string): Returns the path to the current active save slot folder.<br>
GetSaveFilePathForCurrentSaveSlot(profileName: string, key: string): Returns the path to a specific save file.<br>