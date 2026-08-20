# Whatcha Got There?

[![Thunderstore Version](https://img.shields.io/thunderstore/v/WarperSan/WhatchaGotThere)](https://thunderstore.io/package/WarperSan/WhatchaGotThere/)
[![Thunderstore Downloads](https://img.shields.io/thunderstore/dt/WarperSan/WhatchaGotThere?color=purple)](https://thunderstore.io/package/WarperSan/WhatchaGotThere/versions/)
[![License](https://img.shields.io/github/license/WarperSan/WhatchaGotThere?color=orange)](https://raw.githubusercontent.com/WarperSan/WhatchaGotThere/master/LICENSE)

Shows what equipment an ally is currently holding.

![img.png](https://raw.githubusercontent.com/WarperSan/WhatchaGotThere/refs/heads/master/assets/img.png)

This shows the equipment held, the cooldown, and the description of the equipment.

### Mod API

It is possible that certain mods fit the conditions, but shouldn't display the equipment icon. Mod developers can add a soft dependency to this mod, and configure it via the class `DisplayHandler`.

The mod will check the built-in conditions. If none of them declare the icon as hidden, the mod will check the conditions added via the method `AddCondition()`.