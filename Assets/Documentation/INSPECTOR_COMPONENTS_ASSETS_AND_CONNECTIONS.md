# Unity Inspector, Objects, Assets, Sprites, and Connections

This learning reference describes the current serialized state of `Assets/Scenes/MainScene.unity`.

## Core Concepts

| Concept | Meaning | Example in this project |
| --- | --- | --- |
| `GameObject` | A named scene container. It gains data and behaviour from attached components. | `Player`, `Enemy_01`, `Canvas`, and every wall. |
| Component | One responsibility attached to a GameObject. | `Rigidbody2D` adds physics; `PlayerController` adds movement logic. |
| `Transform` | Position, rotation, scale, parent, and children. Every GameObject has one. | Patrol points use only Transform; weapon visuals are character children. |
| Asset | A reusable file under `Assets/`, separate from a live scene object. | PNGs, scripts, prefabs, input actions, and URP settings. |
| Scene instance | An object placed in a scene that may reference assets. | Player is an instance whose SpriteRenderer references `TacticalPlayer.png`. |
| Inspector reference | A serialized field connecting an object, component, or asset to another component. | `CameraFollow.target` points to Player. |
| Layer | A category used by physics and masks. | Player, Enemy, Wall, and UI layers. |
| Trigger | A collider that detects overlap without physically blocking movement. | Cash objects use triggers for collection. |
| Prefab | A reusable saved GameObject/component setup. | Player and enemy death particle effects. |

## Built-in Inspector Components

| Component | Concept and project use |
| --- | --- |
| `Transform` | Positions world objects and establishes hierarchy relationships. |
| `RectTransform` | UI Transform with anchors, pivot, offsets, and size. |
| `SpriteRenderer` | Draws a Sprite asset in 2D world space. Used by characters, walls, floor, cash, and decorations. |
| `CircleCollider2D` | Circular physics shape used by player/enemy contact and cash triggers. |
| `BoxCollider2D` | Rectangular physics shape used by maze walls. |
| `Rigidbody2D` | Physics body moved by PlayerController or EnemyPatrol. Gravity is zero and Z rotation is frozen. |
| `Camera` | Orthographic camera that renders the top-down level. |
| `AudioListener` | Scene audio receiver on Main Camera; the project currently has no gameplay audio system. |
| `UniversalAdditionalCameraData` | Universal Render Pipeline settings added to the camera. |
| `Canvas` | Screen-space root for the interface. |
| `CanvasScaler` | Scales UI from the current `1024 x 768` reference resolution. |
| `GraphicRaycaster` | Allows pointer input to hit UI graphics and buttons. |
| `Image` | Draws a UI sprite/color for icons and panels. |
| `Text` | Legacy Unity UI text used by HUD and result labels. |
| `ParticleSystem` | Emits short-lived visual particles inside the death-effect prefabs. |

## Custom Script Components

The public fields now include `Header`, `Tooltip`, and safe numeric `Min` attributes. Hover a field in Unity to read its project-specific explanation.

| Script | Attached to | Inspector field types | Main connection |
| --- | --- | --- | --- |
| `CameraFollow` | Main Camera | `Transform`, `float`, `Vector2` | Reads Player position and moves the camera. |
| `PlayerController` | Player | `float` | Reads WASD/arrows and moves the attached Rigidbody2D. |
| `PlayerHealth` | Player | `int` | Updates UI, spawns death effects, and starts the lose state. |
| `PlayerContactKill` | Player | No fields | Contact finds `EnemyHealth` and kills that enemy. |
| `PlayerAttack` | Player, disabled | `float`, `int`, `LayerMask` | Old Space-key attack; contact kill replaced it. |
| `EnemyHealth` | Enemy_01 to Enemy_04 | `int`, `bool` | Reports enemy death to GameManager and spawns particles. |
| `EnemyPatrol` | Enemy_01 to Enemy_04 | `Transform[]`, `float` | Follows four ordered waypoint references. |
| `EnemyShooter` | Enemy_01 to Enemy_04 | `Transform`, `float`, `int`, `LayerMask` | Linecasts through Wall/Player layers and damages PlayerHealth. |
| `EnemyTouchDamage` | Four enemies, disabled | No fields | Compatibility component with intentionally harmless body contact. |
| `CashCollectible` | Cash_01 to Cash_05 | `int` | Trigger calls `GameManager.CollectCash` and destroys itself. |
| `DeathEffectSpawner` | Player and enemies | `ParticleSystem` | Instantiates the assigned particle prefab once. |
| `GameManager` | GameManager | `UIManager`, `int`, `bool` | Central progress, win, lose, and UI connection point. |
| `UIManager` | Canvas | `Text`, `GameObject` | Updates HUD, controls panels, and creates runtime buttons. |

### Inspector Field Types

| Type | Meaning |
| --- | --- |
| `int` | Whole number: health, damage, or a counter. |
| `float` | Decimal number: speed, range, time, or distance. |
| `bool` | On/off checkbox. |
| `Vector2` | X/Y number pair. |
| `Transform` | Reference to another object's Transform. |
| `Transform[]` | Ordered list of Transform references. |
| `GameObject` | Reference to an entire scene object. |
| `Text`, `UIManager`, `ParticleSystem` | Reference restricted to that component type. |
| `LayerMask` | Multi-layer physics selection stored as bits. |

## Scene Objects and Their Types

| Object/group | Type and attached items | Purpose |
| --- | --- | --- |
| `Main Camera` | Camera object with Camera, AudioListener, URP data, CameraFollow | Renders and follows gameplay. |
| `GameManager` | Manager object with GameManager | Counts four enemies and five collectibles at startup. |
| `Player` | Character with SpriteRenderer, CircleCollider2D, Rigidbody2D, player scripts | Movement, health, and contact combat. |
| `Player/Dagger` | Child visual with SpriteRenderer | Decoration only; PlayerContactKill performs combat. |
| `Walls` | Empty Transform parent | Organizes boundaries, walls, and eight maze blocks. |
| Wall children | Environment objects with SpriteRenderer and BoxCollider2D | Display walls, block movement, and block enemy sight. |
| `Enemies` | Empty Transform parent | Organizes four enemies and PatrolPoints. |
| `Enemy_01` to `Enemy_04` | Characters with SpriteRenderer, CircleCollider2D, Rigidbody2D, enemy scripts | Patrol, shoot, die, and report progress. |
| Enemy `Gun` children | Child visuals with SpriteRenderer | Decoration only; EnemyShooter performs attacks. |
| `PatrolPoints` | Empty Transform parent | Organizes sixteen waypoint markers. |
| `Enemy01_WP_01` to `Enemy04_WP_04` | Transform-only marker objects | Supply positions to EnemyPatrol arrays. |
| `Cash` | Empty Transform parent | Organizes five objectives. |
| `Cash_01` to `Cash_05` | SpriteRenderer, trigger CircleCollider2D, CashCollectible | Collectible objectives. |
| `Canvas` | RectTransform, Canvas, CanvasScaler, GraphicRaycaster, UIManager | Screen interface root. |
| HUD objects | RectTransform, CanvasRenderer, Text or Image | Health, objectives, target count, level, and icons. |
| `WinPanel` / `LosePanel` | UI panel objects with Text children | Result displays controlled by UIManager. |
| `Floor` | SpriteRenderer environment object | Industrial background. |
| `Decorations` | Transform parent with sprite-only children | Crates, vents, and warning markers. |
| `EventSystem` | Created at runtime by UIManager | Routes Input System events to UI. |
| `GameButtons` | Created at runtime by UIManager | Holds Restart and End Game buttons; not serialized in MainScene. |

## Assets and Sprites

### World and UI Sprite Assets

| Asset | Used by |
| --- | --- |
| `Sprites/Characters/Player/TacticalPlayer.png` | Player |
| `Sprites/Characters/Enemies/TacticalEnemy.png` | Four enemies |
| `Sprites/Collectibles/ObjectiveToken.png` | Five cash objectives |
| `Sprites/Environment/IndustrialFloor.png` | Floor |
| `Sprites/Environment/IndustrialWall.png` | Boundaries, walls, and maze blocks |
| `Sprites/Environment/CargoCrate.png` | Three crates |
| `Sprites/Environment/MetalVent.png` | Two vents |
| `Sprites/Environment/WarningFloorMarker.png` | Two warning markers |
| `Sprites/UI/IndustrialPanel.png` | UI panel Images |
| `Sprites/UI/HealthIcon.png` | HealthIcon |
| `Sprites/UI/ObjectiveIcon.png` | ObjectiveIcon |
| `Sprites/UI/EnemyIcon.png` | EnemyIcon |

### Weapon Sprite Connection

| Asset | Intended renderer | Current serialized state |
| --- | --- | --- |
| `Sprites/Weapons/Dagger.png` | Player/Dagger | Sprite field is `None`. |
| `Sprites/Weapons/Gun.png` | Four enemy Gun children | All four Sprite fields are `None`. |

Both weapon PNG importers currently use Sprite Mode `Multiple`. If Unity shows no selectable sub-sprite, open Sprite Editor and create/apply a slice, or change Sprite Mode to `Single`. This affects only decoration; combat uses scripts on the parent characters.

### Other Assets

| Asset | Type and connection |
| --- | --- |
| `Prefabs/Effects/PlayerDeathParticles.prefab` | Particle prefab assigned to Player's DeathEffectSpawner. |
| `Prefabs/Effects/EnemyDeathParticles.prefab` | Particle prefab assigned to every enemy's DeathEffectSpawner. |
| `InputSystem_Actions.inputactions` | Input Actions asset; gameplay scripts currently read `Keyboard.current` directly. |
| `Settings/*RPAsset.asset` and renderer assets | Universal Render Pipeline project configuration. |
| Files under `Assets/Scripts` | MonoScript assets that become components when attached to GameObjects. |

## Connection Flow

```text
Keyboard -> PlayerController -> Player Rigidbody2D

Player touches Enemy
  -> PlayerContactKill -> EnemyHealth
  -> DeathEffectSpawner + GameManager
  -> UIManager enemy counter

Enemy sees Player through Wall/Player LayerMask
  -> EnemyShooter -> PlayerHealth
  -> UIManager health -> GameManager lose state at zero

Player enters Cash trigger
  -> CashCollectible -> GameManager -> UIManager cash counter

All 4 enemies + all 5 objectives
  -> GameManager.CheckWinCondition -> UIManager.ShowWin
```

## How to Read Any Inspector

1. Read the object name, active checkbox, Tag, and Layer.
2. Read Transform/RectTransform to understand position and hierarchy.
3. Read Renderer or UI graphic to find the connected visual asset.
4. Read Collider and Rigidbody2D to understand physical interaction.
5. Read custom script tooltips and follow reference fields to their targets.
6. Check each component's enabled checkbox; PlayerAttack and EnemyTouchDamage are intentionally disabled.
7. Distinguish scene references from assets: Player Transform is a scene reference; a PNG Sprite or particle prefab is an asset reference.
