## List of PowerWash Simulator mods
- Universal movement speed.

## Installation
- Download [MelonLoader installer](https://melonloader.co/download.html).
- Install MelonLoader for PowerWash Simulator.
- Download mod from releases and extract it to base game folder.
- Disable MelonLoader console:
  - Either add `--melonloader.hideconsole` to launch options.
  - Or edit `\PowerWash Simulator\UserData\Loader.cfg` and set `hide_console = true`.
- Customization:
  - Run game at least once and edit `\PowerWash Simulator\UserData\MelonPreferences.cfg` to change shortcuts and variables.
 
## Build instructions
- Install MelonLoader and run game at least once.
- Clone repository and just open the solution.
- It should automatically detect assembly dependencies from Steam installation.
- Build "Release" version.
- Copy `\<mod>\bin\Release\net6.0\<mod>.dll` to `PowerWash Simulator\Mods\`.
