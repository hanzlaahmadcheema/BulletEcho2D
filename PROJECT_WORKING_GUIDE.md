# BullectEcho2D Project Working Guide

This document maps each current game feature to the scene object, script, and main function that implements it.

Detailed Inspector, object-type, asset, sprite, and connection explanations are in `Assets/Documentation/INSPECTOR_COMPONENTS_ASSETS_AND_CONNECTIONS.md`.

## Core Feature Map

| Feature | Scene object | Script / component | Main function or feature | Why used |
|---|---|---|---|---|
| Player input | `Player` | `PlayerController` | `Update()` + `Keyboard.current` | Modern input |
| Player movement | `Player` | `PlayerController` | `FixedUpdate()` + `Rigidbody2D.MovePosition()` | Physics-safe movement |
| Player collisions | `Player` | `CircleCollider2D`, `Rigidbody2D` | Unity 2D physics | Reliable contact |
| Player health | `Player` | `PlayerHealth` | `Awake()` | Reset health |
| Taking damage | `Player` | `PlayerHealth` | `TakeDamage(int)` | Controlled damage |
| Player death | `Player` | `PlayerHealth` | `Die()` | Central death handling |
| Health HUD update | `Player` | `PlayerHealth` | `UpdateHealthUI()` | Immediate feedback |
| Contact enemy kill | `Player` | `PlayerContactKill` | `OnCollisionEnter2D()` / `OnTriggerEnter2D()` | Simple melee |
| Enemy instant death | Enemy objects | `EnemyHealth` | `KillInstantly()` | Contact assassination |
| Dagger display | `Player/Dagger` | `SpriteRenderer` | Dagger sprite | Visual clarity |
| Old Space attack | `Player` | `PlayerAttack` | Component disabled | Contact combat |
| Camera following | `Main Camera` | `CameraFollow` | `LateUpdate()` | Smooth tracking |
| Camera smoothing | `Main Camera` | `CameraFollow` | `Vector3.Lerp()` | Smooth movement |
| Camera limits | `Main Camera` | `CameraFollow` | `Mathf.Clamp()` | Map boundaries |
| Enemy patrol | `Enemy_01`–`Enemy_03` | `EnemyPatrol` | `FixedUpdate()` | Fixed routes |
| Patrol movement | Enemy objects | `EnemyPatrol` | `Rigidbody2D.MovePosition()` | Physics-safe patrol |
| Waypoint switching | Enemy objects | `EnemyPatrol` | `AdvanceWaypoint()` | Route looping |
| Enemy range check | Enemy objects | `EnemyShooter` | `Vector2.Distance()` | Short detection |
| Shooting cooldown | Enemy objects | `EnemyShooter` | `nextShootTime` | Fair fire rate |
| Shooting visibility | Enemy objects | `EnemyShooter` | `Physics2D.Linecast()` | Wall blocking |
| Gun damage | Enemy objects | `EnemyShooter` | `PlayerHealth.TakeDamage()` | Gradual damage |
| Gun display | Each enemy `Gun` child | `SpriteRenderer` | Gun sprite | Weapon visibility |
| Enemy health | Enemy objects | `EnemyHealth` | `Awake()` | Reset health |
| Enemy death | Enemy objects | `EnemyHealth` | `Die()` | Central death handling |
| Enemy body contact | Enemy objects | `EnemyTouchDamage` | Component disabled | Harmless contact |
| Objective collection | `Cash_01`–`Cash_05` | `CashCollectible` | `OnTriggerEnter2D()` | Automatic pickup |
| Objective counting | `GameManager` | `GameManager` | `CollectCash(int)` | Central tracking |
| Enemy counting | `GameManager` | `GameManager` | `RegisterEnemyKilled()` | Central tracking |
| Win checking | `GameManager` | `GameManager` | `CheckWinCondition()` | Combined objectives |
| Lose handling | `GameManager` | `GameManager` | `LoseGame()` | Central game state |
| Stop after ending | `GameManager` | `GameManager` | `DisablePlayerGameplay()` | Prevent extra input |
| HUD management | `Canvas` | `UIManager` | `UpdateHealth()`, `UpdateCash()`, `UpdateEnemies()` | Central UI |
| Win display | `Canvas/WinPanel` | `UIManager` | `ShowWin()` | Clear result |
| Lose display | `Canvas/LosePanel` | `UIManager` | `ShowLose()` | Clear result |
| Death particles | Player and enemies | `DeathEffectSpawner` | `SpawnDeathEffectOnce()` | Lightweight feedback |
| Wall collision | Objects under `Walls` | `BoxCollider2D` | Unity 2D physics | Block movement |
| Shooting cover | Objects under `Walls` | `Wall` layer | Linecast mask | Block gunfire |
| Level visuals | `Floor`, `Walls`, `Decorations` | `SpriteRenderer` | Industrial sprites | Visual identity |
| UI scaling | `Canvas` | `CanvasScaler` | `1024×768`, width/height match | Resolution support |

## Main Scene Objects

| Object | Purpose | Main attached items |
|---|---|---|
| `Main Camera` | Displays and follows gameplay | `Camera`, `CameraFollow` |
| `GameManager` | Controls game state and counters | `GameManager` |
| `Player` | User-controlled character | `PlayerController`, `PlayerHealth`, `PlayerContactKill`, `DeathEffectSpawner` |
| `Player/Dagger` | Visual melee weapon | `SpriteRenderer` |
| `Walls` | Maze and cover | Wall sprites and `BoxCollider2D` |
| `Enemies` | Enemy container | Three enemy objects |
| `Enemy_01` | Lower patrol enemy | Health, patrol, shooter, particles |
| `Enemy_02` | Right-room enemy | Health, patrol, shooter, particles |
| `Enemy_03` | Final-zone enemy | Health, patrol, shooter, particles |
| `Enemy_04` | Additional upper-area enemy | Health, patrol, shooter, particles |
| Enemy `Gun` children | Visual enemy weapons | `SpriteRenderer` |
| `PatrolPoints` | Stores patrol positions | Sixteen waypoint transforms |
| `Cash` | Objective container | Five collectible objects |
| `Canvas` | Displays HUD and result panels | `UIManager`, `CanvasScaler` |
| `WinPanel` | Displays victory result | UI image and text |
| `LosePanel` | Displays defeat result | UI image and text |
| `Floor` | Level background | Industrial floor sprite |
| `Decorations` | Environmental polish | Crates, vents, warning markers |

## Player Configuration

| Setting | Value | Why used |
|---|---:|---|
| Start position | `(0, -8.5, 0)` | Fixed start |
| Movement speed | `5` | Responsive movement |
| Maximum health | `5` | Fair survival |
| Player layer | `Player` | Linecast detection |
| Dagger purpose | Visual only | Simple combat |
| Attack method | Body contact | Direct gameplay |

## Enemy Configuration

| Enemy | Patrol speed | Waypoints | Shooting range | Damage | Cooldown |
|---|---:|---:|---:|---:|---:|
| `Enemy_01` | `1.5` | `4` | `2.5` | `1` | `1.6` |
| `Enemy_02` | `1.3` | `4` | `2.8` | `1` | `1.5` |
| `Enemy_03` | `1.6` | `4` | `3.0` | `1` | `1.4` |
| `Enemy_04` | `1.6` | `4` | `3.0` | `1` | `1.4` |

All enemies use:

- `EnemyHealth` for death.
- `EnemyPatrol` for fixed movement.
- `EnemyShooter` for range shooting.
- `Physics2D.Linecast()` for visibility.
- `DeathEffectSpawner` for death particles.
- Disabled `EnemyTouchDamage` for harmless body contact.

## Patrol Routes

| Enemy | Route objects | Route shape |
|---|---|---|
| `Enemy_01` | `Enemy01_WP_01` to `Enemy01_WP_04` | Lower rectangle |
| `Enemy_02` | `Enemy02_WP_01` to `Enemy02_WP_04` | Right diamond |
| `Enemy_03` | `Enemy03_WP_01` to `Enemy03_WP_04` | Final-zone loop |
| `Enemy_04` | `Enemy04_WP_01` to `Enemy04_WP_04` | Upper-area loop |

## UI Objects

| UI object | Updated by | Purpose |
|---|---|---|
| `HealthText` | `UIManager.UpdateHealth()` | Shows player health |
| `CashText` | `UIManager.UpdateCash()` | Shows objectives |
| `EnemyText` | `UIManager.UpdateEnemies()` | Shows enemy progress |
| `LevelText` | Scene text | Shows Level 1 |
| `WinPanel` | `UIManager.ShowWin()` | Shows victory |
| `LosePanel` | `UIManager.ShowLose()` | Shows defeat |

`WinPanel` and `LosePanel` start inactive and are enabled only when their result occurs.

## Layers

| Layer | Used by | Purpose |
|---|---|---|
| `Enemy` | All enemies and gun visuals | Enemy identification |
| `Player` | Player and dagger | Player detection |
| `Wall` | Maze and boundaries | Collision and cover |
| `UI` | Canvas objects | Interface rendering |

## Particle Prefabs

| Prefab | Triggered by | Purpose |
|---|---|---|
| `EnemyDeathParticles.prefab` | `EnemyHealth.Die()` | Enemy death feedback |
| `PlayerDeathParticles.prefab` | `PlayerHealth.Die()` | Player death feedback |

## Game Flow

1. `GameManager.Start()` counts enemies and objectives.
2. `PlayerController` moves the player.
3. `EnemyPatrol` moves enemies through waypoints.
4. `EnemyShooter` checks range, cooldown, and line of sight.
5. Enemy shots call `PlayerHealth.TakeDamage(1)`.
6. Player contact calls `EnemyHealth.KillInstantly()`.
7. Collectibles call `GameManager.CollectCash(1)`.
8. Enemy deaths call `GameManager.RegisterEnemyKilled()`.
9. `CheckWinCondition()` shows `WinPanel` after all requirements.
10. `LoseGame()` shows `LosePanel` when player health reaches zero.

## Build Configuration

- Main scene: `Assets/Scenes/MainScene.unity`
- Build order: first scene
- Included scenes: `MainScene` only
- `SampleScene`: not included

## Current Scope Note

This guide describes the currently inspected Version 5.1 implementation. Restart buttons, instruction text, player damage flash, and enemy muzzle feedback are not part of the current serialized scene.
