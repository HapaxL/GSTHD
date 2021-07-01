# Gossip Stones Tracker HD v0.2
Fork of Drekorig (`Drekorig#2506`)'s [Gossip Stones Tracker](https://github.com/Draeko/ootr_gst/tree/ladder_version) for the [Ocarina of Time Randomizer](https://ootrandomizer.com)

Made by Hapax (`Hapax#1729`). **Work in progress.**

## [>>> Get the latest release <<<](https://github.com/HapaxL/GSTHD/releases/latest)

I'm really sorry I haven't wrote any real tutorial yet to explain all the new (and old) stuff, I will write one soon !! In the meantime feel free to ask me about anything in here or (preferably) on Discord (`Hapax#1729`).
___

### What's new
#### Control scheme changes
- Left click "progresses" a UI element (e.g. greyed out Hookshot -> Hookshot -> Longshot). Applies to every UI element.
- Right click "regresses" a UI element (e.g. Longshot -> Hookshot -> greyed out Hookshot). Applies to every UI element.
- No cycling (e.g. left-clicking the Longshot does not reset it to a greyed-out Hookshot anymore).
- Middle click resets a UI element to its default value (including WotH/Barren panels, dungeon names, collected items, etc).
- Drag & drop is now done with middle click.

**Note:** a big issue I had with Drekorig's tracker was a feeling of unresponsivity from clicking on things on the tracker. This was due to left click being used to drag & drop, which meant the action of left-clicking normally on an icon to progress it could not go through until the mouse button was *released*, and also meant that moving the mouse even one pixel due to a click meant the click would not go through. I was dissatisfied with this behaviour, and opted to completely change the way the tracker controls. If this is not to your liking, please feel free to contact me on Discord (`Hapax#1729`), or [open a Github issue](https://github.com/HapaxL/ootr_gst/issues), I can probably code a way to set one's preferred control schemes in the Settings file.

#### New features
- Mouse wheel can now be used to progress and regress icons (applicable to progressive items, collected items, and dungeon names). Direction can be inverted or not.
- New "Gossip Stone grid" UI element that can be used to add Gossip Stones in bulk to a layout.
- Drag & dropped icons can now be moved around again from Gossip Stone to Gossip Stone (or to a Song's small icon).
- Medallions can now be drag & dropped.
- Collected items have been overhauled:
  - Layouts now have the option to place the amount number to a different place within a collected item's icon.
  - Layouts now have the option to set a maximum amount for a given collected item.
  - Collected items can now be drag & dropped.
  - Collected item number can now be reset to 0 with middle click.
  - Collected item number can now be set to increase/decrease by a set amount (e.g. 10 by 10) instead of 1 by 1.
- Layouts can now specify default image sets for song markers and Gossip Stones, as well as default dungeon names for medallions.
- It is now possible to add labels to a layout (for aesthetic purposes).
- It is now possible to add generic (no autofill) text boxes to a layout.
- Layouts can now set the order of dungeon names under medallions, as well as the starting value.
- Different behaviours for song markers are now available (None, CheckOnly, DropOnly, DropAndCheck, DragAndDrop, Full).

#### Other changes and fixes
- Fixed small issues with collected items' number display.
- Fixed Song icons being flattened when drag & dropped.
- Fixed drag & drop on small Song icons to correspond to Gossip Stone behaviour.
- Changed default WoTH colors, added "last woth" color (right click on a white WotH).
- Fixed other small UI/UX details.
___

### To be added
#### Planned features and changes
- Group-selecting, and group-drag & dropping, a set of icons in a Gossip Stone grid.
- Writing text inside a Gossip Stone which can be drag & dropped.
- Allowing layouts to set the preferred color scheme for WotH panels.
- Allowing more customization options in layouts and in the Settings file.
- Using mouse wheel to progress/regress Gossip Stones and icons on Gossip Stones.
- Toolbar controls for changing settings and loading layouts.

#### Known issues
- No known big issues for now, feel free to contact me on Discord or to open a Github issue if you find anything!
___

### Special thanks
- Drekorig, for his genius drag & drop idea, and his nice tracker base.
- ArthurOudini, for inspiring me to add some of the features in this project.
- Everyone who submitted, and will submit, suggestions and bug fixes for the project.
