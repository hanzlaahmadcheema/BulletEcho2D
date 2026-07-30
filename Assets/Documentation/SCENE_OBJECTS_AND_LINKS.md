# MainScene Objects and Script Links

## Scene Object Table

| Scene Object | Purpose | Attached Scripts | Important Child Objects |
| --- | --- | --- | --- |
| `Main Camera` | Displays and follows gameplay | `CameraFollow` | None |
| `GameManager` | Tracks game progress and result state | `GameManager` | None |
| `Player` | User-controlled character | `PlayerController`, `PlayerHealth`, `PlayerContactKill`, `DeathEffectSpawner`, disabled `PlayerAttack` | `Dagger` |
| `Player/Dagger` | Shows the player's melee weapon | None; uses `SpriteRenderer` | None |
| `Walls` | Contains maze walls and boundaries | No custom script | Boundary and maze wall objects |
| `Enemies` | Contains enemies and patrol points | No custom script | `Enemy_01` to `Enemy_04`, `PatrolPoints` |
| `Enemy_01` | Lower-area enemy | `EnemyHealth`, `EnemyPatrol`, `EnemyShooter`, `DeathEffectSpawner`, disabled `EnemyTouchDamage` | `Gun` |
| `Enemy_02` | Right-room enemy | `EnemyHealth`, `EnemyPatrol`, `EnemyShooter`, `DeathEffectSpawner`, disabled `EnemyTouchDamage` | `Gun` |
| `Enemy_03` | Final-zone enemy | `EnemyHealth`, `EnemyPatrol`, `EnemyShooter`, `DeathEffectSpawner`, disabled `EnemyTouchDamage` | `Gun` |
| `Enemy_04` | Additional upper-area enemy | `EnemyHealth`, `EnemyPatrol`, `EnemyShooter`, `DeathEffectSpawner`, disabled `EnemyTouchDamage` | `Gun` |
| Enemy `Gun` children | Show enemy weapons | None; uses `SpriteRenderer` | None |
| `PatrolPoints` | Stores fixed enemy patrol positions | No custom script | Sixteen waypoint transforms |
| `Cash` | Contains collectible objectives | No custom script | `Cash_01` to `Cash_05` |
| `Cash_01` | Collectible objective | `CashCollectible` | None |
| `Cash_02` | Collectible objective | `CashCollectible` | None |
| `Cash_03` | Collectible objective | `CashCollectible` | None |
| `Cash_04` | Collectible objective | `CashCollectible` | None |
| `Cash_05` | Collectible objective | `CashCollectible` | None |
| `Canvas` | Contains a simple text HUD and game controls | `UIManager`; `CanvasScaler` uses `1024×768` reference resolution | HUD texts, result text, runtime buttons |
| `HealthText` | Displays player health | No custom script; Unity `Text` | None |
| `CashText` | Displays collected objectives | No custom script; Unity `Text` | None |
| `EnemyText` | Displays eliminated enemies | No custom script; Unity `Text` | None |
| `WinPanel` | Displays the text-only win result | No custom script; controlled by `UIManager` | `WinTitle`, `WinSummary` |
| `LosePanel` | Displays the text-only lose result | No custom script; controlled by `UIManager` | `LoseTitle`, `LoseSummary` |
| `GameButtons` | Runtime group for Restart and End Game | Created and wired by `UIManager` | `RestartButton`, `EndGameButton` |
| `Floor` | Displays the industrial floor | No custom script | None |
| `Decorations` | Stores visual environment objects | No custom script | Crates, vents, warning markers |

## Player Object Links

| Player component | Role |
| --- | --- |
| `SpriteRenderer` | Displays the player sprite |
| `CircleCollider2D` | Detects physical contact |
| `Rigidbody2D` | Supports collision-aware movement |
| `PlayerController` | Reads input and moves the player |
| `PlayerHealth` | Stores health and handles death |
| `PlayerContactKill` | Eliminates touched enemies |
| `DeathEffectSpawner` | Spawns player death particles |
| `PlayerAttack` | Old Space attack; disabled |

## Enemy Object Links

| Enemy component | Role |
| --- | --- |
| `SpriteRenderer` | Displays the enemy sprite |
| `CircleCollider2D` | Detects physical contact |
| `Rigidbody2D` | Supports patrol movement |
| `EnemyHealth` | Handles enemy health and death |
| `EnemyPatrol` | Follows four assigned waypoints |
| `EnemyShooter` | Checks range, visibility, and cooldown |
| `DeathEffectSpawner` | Spawns enemy death particles |
| `EnemyTouchDamage` | Old body-contact damage; disabled |

## Patrol Links

| Enemy | Assigned waypoint objects | Speed |
| --- | --- | ---: |
| `Enemy_01` | `Enemy01_WP_01` to `Enemy01_WP_04` | `1.5` |
| `Enemy_02` | `Enemy02_WP_01` to `Enemy02_WP_04` | `1.3` |
| `Enemy_03` | `Enemy03_WP_01` to `Enemy03_WP_04` | `1.6` |
| `Enemy_04` | `Enemy04_WP_01` to `Enemy04_WP_04` | `1.6` |

## Shooting Links

| Enemy | Player reference | Range | Damage | Cooldown |
| --- | --- | ---: | ---: | ---: |
| `Enemy_01` | `Player` | `2.5` | `1` | `1.6` |
| `Enemy_02` | `Player` | `2.8` | `1` | `1.5` |
| `Enemy_03` | `Player` | `3.0` | `1` | `1.4` |
| `Enemy_04` | `Player` | `3.0` | `1` | `1.4` |

All four shooters use:

- `Wall` layer for blocking.
- `Player` layer for target detection.
- `Physics2D.Linecast()` for visibility.
- No bullet or projectile object.

## Object-to-Feature Mapping

| Feature | Object | Script | Function |
| --- | --- | --- | --- |
| Keyboard input | `Player` | `PlayerController` | `Update()` |
| Player movement | `Player` | `PlayerController` | `FixedUpdate()` |
| Player health setup | `Player` | `PlayerHealth` | `Awake()` |
| Player damage | `Player` | `PlayerHealth` | `TakeDamage(int)` |
| Player death | `Player` | `PlayerHealth` | `Die()` |
| Contact kill | `Player` | `PlayerContactKill` | `TryKillEnemy()` |
| Camera tracking | `Main Camera` | `CameraFollow` | `LateUpdate()` |
| Enemy patrol | Enemy objects | `EnemyPatrol` | `FixedUpdate()` |
| Waypoint loop | Enemy objects | `EnemyPatrol` | `AdvanceWaypoint()` |
| Enemy visibility | Enemy objects | `EnemyShooter` | `HasClearLineOfSight()` |
| Enemy shot | Enemy objects | `EnemyShooter` | `Update()` |
| Enemy death | Enemy objects | `EnemyHealth` | `Die()` |
| Objective pickup | Cash objects | `CashCollectible` | `OnTriggerEnter2D()` |
| Enemy counter | `GameManager` | `GameManager` | `RegisterEnemyKilled()` |
| Objective counter | `GameManager` | `GameManager` | `CollectCash(int)` |
| Win check | `GameManager` | `GameManager` | `CheckWinCondition()` |
| Lose state | `GameManager` | `GameManager` | `LoseGame()` |
| HUD updates | `Canvas` | `UIManager` | Update methods |
| Win display | `Canvas/WinPanel` | `UIManager` | `ShowWin()` |
| Lose display | `Canvas/LosePanel` | `UIManager` | `ShowLose()` |
| Death effects | Player and enemies | `DeathEffectSpawner` | `SpawnDeathEffectOnce()` |

## Layers and Collision Links

| Layer | Objects | Purpose |
| --- | --- | --- |
| `Player` | Player and dagger | Shooter target detection |
| `Enemy` | Enemies and guns | Enemy identification |
| `Wall` | Boundaries and maze walls | Movement blocking and shooting cover |
| `UI` | Canvas objects | Interface rendering |

## Current Weapon Sprite Connection

`Dagger.png` and `Gun.png` exist under `Assets/Sprites/Weapons`, but the serialized Sprite field is currently `None` on the Dagger renderer and all four Gun renderers. These children are visual only, so gameplay logic is unaffected. See [INSPECTOR_COMPONENTS_ASSETS_AND_CONNECTIONS.md](INSPECTOR_COMPONENTS_ASSETS_AND_CONNECTIONS.md) for the asset/import explanation.

## Build Scene Link

`ProjectSettings/EditorBuildSettings.asset` contains only:

- `Assets/Scenes/MainScene.unity`

`SampleScene` is not included.
