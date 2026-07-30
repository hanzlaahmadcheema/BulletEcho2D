# EnemyHealth

## Purpose

This script stores enemy health and handles enemy death. It updates the game enemy count, spawns death particles, and removes the enemy object.

**Status:** Active.

## Attached Scene Object

- `Enemy_01`
- `Enemy_02`
- `Enemy_03`

## Main Responsibility

- Enemy health
- Enemy death
- Enemy counter update
- Death effect trigger

## Important Variables / Inspector Fields

| Field | Meaning | Example value |
| --- | --- | --- |
| `maxHealth` | Maximum health for the enemy | `1` |
| `currentHealth` | Current enemy health | Starts at `1` |
| `destroyOnDeath` | Chooses destroy or deactivate behavior | `true` |
| `IsDead` | Reports whether death has happened | Runtime value |
| `deathHandled` | Prevents duplicate death logic | `false` initially |

## Main Functions

| Function | What it does | When it runs |
| --- | --- | --- |
| `Awake()` | Sets current health to maximum health | Enemy initialization |
| `TakeDamage(int)` | Reduces enemy health and checks death | Damage is received |
| `KillInstantly()` | Sets health to zero and starts death | Player contact |
| `Die()` | Spawns particles, updates count, and removes enemy | Health reaches zero |

## Function Signatures and Parameters

| Signature | Parameters | Return value |
| --- | --- | --- |
| `private void Awake()` | None | None |
| `public void TakeDamage(int damage)` | `damage`: health points to remove | None |
| `public void KillInstantly()` | None | None |
| `private void Die()` | None | None |
| `public bool IsDead` | Property with no parameter | Returns enemy death state |

`TakeDamage()` supports normal damage. `KillInstantly()` is the current contact-kill entry point.

## Important Code Lines

| Source lines | Code | Explanation |
| --- | --- | --- |
| `5–7` | Health and removal fields | Configure enemy health and death behavior. |
| `11` | `deathHandled || currentHealth <= 0` | Gives other scripts one reliable death property. |
| `15` | `currentHealth = maxHealth;` | Resets enemy health at scene start. |
| `20–23` | Dead guard | Prevents damage after death. |
| `25–26` | Damage and clamp | Removes health and prevents negative values. |
| `28–31` | Health death check | Starts `Die()` at zero health. |
| `36–39` | Instant-kill guard | Prevents duplicate contact kills. |
| `41–42` | Instant death | Sets health to zero and calls `Die()`. |
| `47–52` | `deathHandled` guard | Ensures count and particles happen once. |
| `54–59` | Particle link | Finds and calls `DeathEffectSpawner`. |
| `61–64` | Manager link | Reports one eliminated enemy. |
| `66–73` | Remove/deactivate choice | Destroys the enemy by default, with deactivation fallback. |

## Execution Sequence

1. `Awake()` sets enemy health to 1.
2. Player contact calls `KillInstantly()`.
3. `KillInstantly()` checks `IsDead`.
4. Health becomes zero.
5. `Die()` locks death handling.
6. Enemy particles spawn.
7. `GameManager` receives the kill.
8. Enemy object is destroyed.

## How It Works In Game

Each enemy starts with one health. The current contact-kill system calls `KillInstantly()`. `Die()` runs once, tells the particle script to show the effect, informs `GameManager`, and destroys the enemy object.

## Linked Scripts

- `PlayerContactKill` calls `KillInstantly()`.
- Legacy `PlayerAttack` can call `TakeDamage()`.
- `DeathEffectSpawner` creates enemy death particles.
- `GameManager.RegisterEnemyKilled()` updates progress.
- `EnemyPatrol` and `EnemyShooter` read `IsDead`.

## Linked Scene Objects

- `Enemy_01`
- `Enemy_02`
- `Enemy_03`
- `GameManager`
- `Canvas/EnemyText`
- `EnemyDeathParticles` prefab

## Why This Approach Is Used

Central enemy death

## Notes For Presentation

“This script handles enemy health and death. When the player touches an enemy, it triggers instant death, updates the target counter, shows particles, and removes the enemy.”
