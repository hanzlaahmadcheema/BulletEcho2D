# BullectEcho2D Project Overview

## Project Information

| Item | Description |
| --- | --- |
| Project name | BullectEcho2D |
| Game type | Top-down 2D maze action game |
| Main scene | `Assets/Scenes/MainScene.unity` |
| Player character | Dagger-equipped tactical player |
| Main objective | Eliminate four enemies and collect five objectives |

## Main Idea

This is a top-down 2D maze action game where the player avoids enemy gun sight, uses walls as cover, touches enemies to eliminate them, collects all objectives, and wins after completing all objectives.

The player moves through an industrial maze. Enemies follow fixed patrol roads and attack only when the player is close enough and visible. Walls provide physical cover and block enemy shooting checks.

## Player Goal

- Move through the Level 1 maze.
- Use walls to avoid enemy line-of-sight.
- Touch enemies to eliminate them.
- Collect all five objective tokens.
- Survive with the available health.

## Win Condition

The player wins when:

- All four enemies are eliminated.
- All five objectives are collected.

`GameManager.CheckWinCondition()` checks both requirements. It then stops player control and asks `UIManager` to display `WinPanel`.

## Lose Condition

The player loses when health reaches zero.

Enemy shots call `PlayerHealth.TakeDamage(1)`. When health becomes zero, `PlayerHealth.Die()` calls `GameManager.LoseGame()`. The game stops player control and displays `LosePanel`.

## Main Features

- Keyboard player movement.
- Smooth camera following.
- Player health starting at 5.
- Contact-based enemy elimination.
- Distance-based enemy shooting.
- Wall-blocked line-of-sight.
- Fixed waypoint patrol routes.
- Five collectible objectives.
- Enemy and objective progress HUD.
- Win and lose result panels.
- Lightweight death particles.
- Industrial maze environment.

## Current Version Scope

The inspected project contains the Version 5.1 gameplay setup:

- Four enemies.
- Sixteen patrol waypoints.
- Five collectible objectives.
- Balanced enemy ranges and cooldowns.
- One point of damage per enemy shot.
- Player contact kill.
- Enemy body contact disabled.
- `MainScene` as the only build scene.

## Important Design Decisions

1. `PlayerAttack` exists but is disabled because current combat uses contact kill.
2. `EnemyTouchDamage` exists but is disabled because enemy body contact is harmless.
3. Enemy shooting uses distance and `Physics2D.Linecast()`, not bullets.
4. Walls use the `Wall` layer so the first linecast hit can block shooting.
5. The player has 5 health and each successful enemy shot removes 1 health.
6. `PlayerContactKill` eliminates enemies when their colliders touch.
7. Enemies use fixed waypoint roads instead of pathfinding.
8. Death particles provide lightweight visual feedback.
9. `GameManager` controls counters, win/lose state, and gameplay stopping.
10. `MainScene` is the only scene included in build settings.

## Intentionally Not Included

- No Level 2.
- No pathfinding.
- No enemy chasing.
- No bullets or projectile physics.
- No boss enemy.
- No advanced enemy AI.
- No inventory or shop.
- No level selection.
- No runtime map generation.
- No audio system.

## Main System Connections

| System | Main script | Connects to |
| --- | --- | --- |
| Player movement | `PlayerController` | `Rigidbody2D`, `GameManager` |
| Player health | `PlayerHealth` | `UIManager`, `GameManager`, `DeathEffectSpawner` |
| Player combat | `PlayerContactKill` | `EnemyHealth` |
| Enemy movement | `EnemyPatrol` | Waypoint objects, `Rigidbody2D`, `GameManager` |
| Enemy shooting | `EnemyShooter` | Player, wall/player layers, `PlayerHealth` |
| Objectives | `CashCollectible` | Player collider, `GameManager` |
| Game state | `GameManager` | Enemies, objectives, player scripts, `UIManager` |
| UI | `UIManager` | HUD texts, `WinPanel`, `LosePanel` |
| Camera | `CameraFollow` | Player transform |
| Effects | `DeathEffectSpawner` | Particle prefabs |
