# DopeBoys

A stylish, high-impact card pack for **ROUNDS** with custom effects, strong tradeoffs, and clean vanilla-friendly design.

[![Thunderstore Downloads](https://img.shields.io/thunderstore/dt/Phalex/DopeBoys?label=Thunderstore%20downloads)](https://thunderstore.io/c/rounds/p/Phalex/DopeBoys/) [![Latest Release](https://img.shields.io/github/v/release/alexh/DopeBoysCards?label=GitHub%20release)](https://github.com/alexh/DopeBoysCards/releases)

<!-- thunderstore-downloads:start -->
Thunderstore downloads: **72,419** (updated 2026-02-18 UTC)
<!-- thunderstore-downloads:end -->

## Feature Snapshot

- Custom card mechanics built on UnboundLib + ModdingUtils.
- Purposeful stat tradeoffs that create distinct playstyles.
- Dedicated visual identity with custom card art in `Assets/Cards`.
- Production-ready packaging flow for Thunderstore releases.

## Card Lineup

| Card | Rarity | Core Effect | Tradeoff / Stat Flavor |
| --- | --- | --- | --- |
| Stink Master | Rare | Block spawns toxic clouds. | +20% health, +0.25s block cooldown. |
| Wiggle Wiggle | Uncommon | Hits trigger a wiggle effect on opponents. | -25% attack speed. |
| SAM Turret | Rare | Heavy turret-style loadout with more ammo and blocks. | Very low move speed, higher gravity, lower jump. |
| Smoot Bullets | Uncommon | Hit targets are pulled toward you. | +25% attack speed. |
| Head Hunter | Common | Gain stacking damage per kill, reset each round. | Snowball power with round reset pacing. |
| Touch Tips | Uncommon | Contact launch effect when touching another player. | +20% health. |

## Install

Install via Thunderstore:

- Package: [Phalex / DopeBoys](https://thunderstore.io/c/rounds/p/Phalex/DopeBoys/)
- Manager support: Thunderstore Mod Manager

## Development Notes

- Framework: `.NET Standard 2.1`
- Mod loader target: BepInEx + ROUNDS mod ecosystem dependencies in `manifest.json`
- Main plugin entry: `DopeBoys.cs`
- Card implementations: `Cards/`
- Runtime effects/extensions: `RoundsEffects/` and `Extensions/`

## Release Flow

- Build your target config in the solution.
- Use `publish.ps1` to package the Thunderstore zip and sync local dev profile when needed.

## Links

- Thunderstore: [DopeBoys package](https://thunderstore.io/c/rounds/p/Phalex/DopeBoys/)
- Source: [github.com/alexh/DopeBoysCards](https://github.com/alexh/DopeBoysCards)
