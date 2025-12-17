<p align="center">
  <img src="https://camo.githubusercontent.com/15411371090ab808f4644366f83bf2871aeacd208b2879fbe87f562a20150a3d/68747470733a2f2f70616e2e73616d7979632e6465762f732f56596d4d5845" />
</p>

# SimpleResetKnife-SwiftlyS2

A **lightweight**, **exploit-proof**, and **fully customizable** Knife Reset plugin for **Counter-Strike 2**, built on the **SwiftlyS2 framework**.

Designed for **Surf, Bhop, and Jailbreak** servers where players might accidentally drop their knives. It allows players to get a new knife instantly while preventing map clutter and spam.

## 🚀 Features

### 🛡️ Smart Abuse Protection
- **"Have Knife" Check:** Prevents players from getting a new knife if they already have one (including Bayonets and custom knives).
- **Anti-Spam:** Stops players from flooding the map with dropped knives, protecting server FPS.

### ⏱️ Cooldown System
- Configurable delay (in seconds) between commands.
- Prevents command flooding and ensures fair usage.

### ⚔️ Team-Based Logic
- Automatically detects player team (Terrorist / Counter-Terrorist).
- Gives the correct default knife (`weapon_knife_t` or `weapon_knife`).

### 🎨 Fully Customizable
- **Custom Prefix:** Change the chat tag directly from the config.
- **Color Support:** Supports all Swiftly chat colors (e.g., `{DarkRed}`, `{Green}`).
- **Translation Ready:** Comes with English (`en.jsonc`) and Turkish (`tr.jsonc`) support.

### 🔧 Native Item Handling
- Uses `ItemServices` to give items directly.
- **Does NOT require `sv_cheats 1`**.

## Requirements
- [SwiftlyS2](https://github.com/swiftly-solution/swiftlys2)

## Configuration
The plugin creates a JSON configuration file (`config.json`) inside the plugin directory (`addons/swiftly/plugins/SimpleResetKnife-SwiftlyS2/`).

- **Prefix** — The chat prefix shown before messages. Supports color codes.
- **CooldownSeconds** — How many seconds a player must wait before using the command again.

### Example Config:
```json
{
  "Main": {
    "Prefix": "{DarkRed}[ResetKnife] {White}",
    "CooldownSeconds": 10
  }
}
```
## Commands

- `!resetknife` or `!rk`: Checks your team and inventory, then gives you a default knife if you don't have one.

## Translations
The plugin supports multi-language messages. You can edit them in `resources/translations/`.

**Supported Languages:**
- 🇺🇸 English (`en.jsonc`)
- 🇹🇷 Turkish (`tr.jsonc`)

### Adding a Language
Simply create a new `.jsonc` file with your language code and translate the keys:
- `error_cooldown`
- `error_team`
- `knife_given`
- `error_has_knife`

## Installation
1. Download the latest release.
2. Extract the folder into `addons/swiftly/plugins/`.
3. Restart your server or use `sw plugin reload SimpleResetKnife-SwiftlyS2`.

## Author
- PoncikMarket (Discord: `poncikmarket`)
