# BullectEcho2D Script Index

Each linked script document includes exact function signatures, parameter meanings, return values, important source-code lines, and runtime execution order.

For a concept-first explanation of Inspector components, object types, assets, sprites, and reference connections, read [INSPECTOR_COMPONENTS_ASSETS_AND_CONNECTIONS.md](INSPECTOR_COMPONENTS_ASSETS_AND_CONNECTIONS.md).

## Master Script Table

| Script | Attached to | Feature | Main function | Active status |
| --- | --- | --- | --- | --- |
| `PlayerController` | `Player` | Player movement | `FixedUpdate()` | Active |
| `PlayerHealth` | `Player` | Health and death | `TakeDamage(int)` | Active |
| `PlayerContactKill` | `Player` | Contact combat | `TryKillEnemy()` | Active |
| `PlayerAttack` | `Player` | Old Space attack | `Attack()` | Disabled but kept |
| `CameraFollow` | `Main Camera` | Camera tracking | `LateUpdate()` | Active |
| `EnemyHealth` | `Enemy_01` to `Enemy_04` | Enemy death | `Die()` | Active |
| `EnemyPatrol` | `Enemy_01` to `Enemy_04` | Fixed patrol | `FixedUpdate()` | Active |
| `EnemyShooter` | `Enemy_01` to `Enemy_04` | Range shooting | `HasClearLineOfSight()` | Active |
| `EnemyTouchDamage` | `Enemy_01` to `Enemy_04` | Old contact damage | No active function | Disabled but kept |
| `CashCollectible` | `Cash_01` to `Cash_05` | Objective pickup | `OnTriggerEnter2D()` | Active |
| `GameManager` | `GameManager` | Game state | `CheckWinCondition()` | Active |
| `UIManager` | `Canvas` | HUD and panels | UI update methods | Active |
| `DeathEffectSpawner` | `Player`, all enemies | Death particles | `SpawnDeathEffectOnce()` | Active |

## Player Scripts

- `PlayerController`
- `PlayerHealth`
- `PlayerContactKill`
- `PlayerAttack` — disabled because contact kill is the current combat method.

## Enemy Scripts

- `EnemyHealth`
- `EnemyPatrol`
- `EnemyShooter`
- `EnemyTouchDamage` — disabled because enemy body contact is harmless.

## Manager Scripts

- `GameManager`

## UI Scripts

- `UIManager`

## Collectible Scripts

- `CashCollectible`

## Camera Scripts

- `CameraFollow`

## Effect Scripts

- `DeathEffectSpawner`

## Script Status Notes

### Active scripts

Active scripts run during normal gameplay and are enabled in `MainScene`.

### Disabled but kept

- `PlayerAttack` remains attached to `Player`, but it is disabled. The player now kills through `PlayerContactKill`.
- `EnemyTouchDamage` remains attached to each enemy, but it is disabled. Enemy body contact is intentionally harmless.

### Runtime-created UI

`UIManager` creates the `EventSystem`, Restart button, and End Game button when the scene starts. These objects are not serialized as normal children in `MainScene`.

## Documentation Links

| Script | Documentation |
| --- | --- |
| `PlayerController` | [Scripts/PlayerController.md](Scripts/PlayerController.md) |
| `PlayerHealth` | [Scripts/PlayerHealth.md](Scripts/PlayerHealth.md) |
| `PlayerContactKill` | [Scripts/PlayerContactKill.md](Scripts/PlayerContactKill.md) |
| `PlayerAttack` | [Scripts/PlayerAttack.md](Scripts/PlayerAttack.md) |
| `CameraFollow` | [Scripts/CameraFollow.md](Scripts/CameraFollow.md) |
| `EnemyHealth` | [Scripts/EnemyHealth.md](Scripts/EnemyHealth.md) |
| `EnemyPatrol` | [Scripts/EnemyPatrol.md](Scripts/EnemyPatrol.md) |
| `EnemyShooter` | [Scripts/EnemyShooter.md](Scripts/EnemyShooter.md) |
| `EnemyTouchDamage` | [Scripts/EnemyTouchDamage.md](Scripts/EnemyTouchDamage.md) |
| `CashCollectible` | [Scripts/CashCollectible.md](Scripts/CashCollectible.md) |
| `GameManager` | [Scripts/GameManager.md](Scripts/GameManager.md) |
| `UIManager` | [Scripts/UIManager.md](Scripts/UIManager.md) |
| `DeathEffectSpawner` | [Scripts/DeathEffectSpawner.md](Scripts/DeathEffectSpawner.md) |
