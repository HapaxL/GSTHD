# Gossip Stone Tracker HD v0.1 for the [Ocarina of Time Randomizer](https://ootrandomizer.com)
Fork of [Drekorig](https://github.com/Draeko/ootr_gst/tree/ladder_version)'s (`Drekorig#2506`) Gossip Stones Tracker for Zelda Ocarina of Time Randomizer

Made by Hapax (Discord: `Hapax#1729`).

**Work in progress.**
___

### What's new
#### Control scheme changes
- Left click "progresses" a UI element (e.g. greyed out Hookshot -> Hookshot -> Longshot). Applies to every UI element.
- Right click "regresses" a UI element (e.g. Longshot -> Hookshot -> greyed out Hookshot). Applies to every UI element.
- No cycling (e.g. left-clicking the Longshot does not reset it to a greyed-out Hookshot anymore).
- Middle click resets a UI element to its default value (including WotH/Barren panels, dungeon names, collected items, etc).
- Drag & drop is now done with middle click.

**Note:** a big issue I had with Draeko's tracker was a feeling of unresponsivity from clicking on things on the tracker. This was due to left click being used to drag & drop, which meant the action of left-clicking normally on an icon to progress it could not go through until the mouse button was *released*, and also meant that moving the mouse even one pixel due to a click meant the click would not go through. I was dissatisfied with this behaviour, and opted to completely change the way the tracker controls. If this is not to your liking, please feel free to contact me on Discord (`Hapax#1729`), or [open a Github issue](https://github.com/HapaxL/ootr_gst/issues), I can probably code a way to set one's preferred control schemes in the Settings file.

#### New features
- Mouse wheel can now be used to progress and regress icons (applicable to progressive items, collected items, and dungeon names).
- New "Gossip Stone grid" UI element that can be used to add Gossip Stones in bulk to a layout.
- Drag & dropped icons can now be moved around again from Gossip Stone to Gossip Stone (or to a Song's small icon).
- Medallions can now be drag & dropped.
- Collected items have been overhauled:
  - Layouts now have the option to place the amount number to a different place within a collected item's icon.
  - Layouts now have the option to set a maximum amount for a given collected item.
  - Collected items can now be drag & dropped.
  - Collected item number can now be reset to 0 with middle click.

#### Other changes and fixes
- Fixed small issues with collected items' number display.
- Fixed Song icons being flattened when drag & dropped.
- Changed default WoTH colors, added "last woth" color (right click on a white WotH).
- Fixed other small UI/UX details.
___

### To be added
#### Planned features and changes
- Group-selecting, and group-drag & dropping, a set of icons in a Gossip Stone grid.
- Writing text inside a Gossip Stone which can be drag & dropped.
- Allowing layouts to set and edit text labels for the UI.
- Allowing layouts to set a "step" value for collected items (e.g. counting Skulltulas 10 by 10).
- Allowing layouts to set the preferred color scheme for WotH panels.
- Allowing layouts to set the order of dungeon names under medallions.
- Allowing more customization options in layouts and in the Settings file.
- Using mouse wheel to progress/regress Gossip Stones and icons on Gossip Stones.

#### Known issues
- Drag & dropping a Song's small icon currently does not work correctly (acts like a drag & drop on the big icon instead, while deleting the contents of the small icon).
___

### Special thanks
- Draeko, for his genius drag & drop idea, and his nice tracker base.
- ArthurOudini, for inspiring me to add some of the features in this project.
- Everyone who submitted, and will submit, suggestions and bug fixes for the project.