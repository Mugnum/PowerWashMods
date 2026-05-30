## List of PowerWash Simulator mods
- **Universal movement speed**  
    Disables movement speed reduction when looking down. The minimum speed can be customized in config.  
    Can be toggled with `~` shortcut, which can be customized in config.

## Installation
- Download [MelonLoader installer](https://melonloader.co/download.html).
- Install MelonLoader for PowerWash Simulator.
- Download mods from [releases](https://github.com/Mugnum/PowerWashMods/releases).
- Extract mods' archives to base game folder.
- Disable MelonLoader console:
  - Either add `--melonloader.hideconsole` to launch options.
  - Or edit `\PowerWash Simulator\UserData\Loader.cfg` and set `hide_console = true`.
- Customization:
  - Run game at least once and edit `\PowerWash Simulator\UserData\MelonPreferences.cfg` to change shortcuts and variables.
 
## Build instructions
- Install MelonLoader and run the game at least once.
- Clone repository and simply open the solution.
- It should automatically detect assembly dependencies for Steam installation.
- Build "Release" version.
- Copy `PowerWashMods\<mod>\bin\Release\net6.0\<mod>.dll` to `\PowerWash Simulator\Mods\`.
