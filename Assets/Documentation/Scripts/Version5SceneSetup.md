# Version5SceneSetup

## Purpose

This editor-only script created and configured Version 5 scene content. It sets layers, player/enemy components, weapon visuals, waypoints, maze walls, and particle prefabs inside the Unity Editor.

**Status:** Editor setup script. Not attached to any scene object and not runtime gameplay logic.

## Attached Scene Object

- Not attached to a GameObject
- Located in `Assets/Scripts/Editor`

## Main Responsibility

- Editor scene configuration
- Layer setup
- Component assignment
- Waypoint creation
- Wall creation
- Generated visual assets

## Important Variables / Inspector Fields

This is a static editor class and has no Inspector fields.

| Field | Meaning | Example value |
| --- | --- | --- |
| `ScenePath` | Scene modified by the setup | `Assets/Scenes/MainScene.unity` |
| `GeneratedSpriteFolder` | Weapon sprite output folder | `Assets/Sprites/Weapons` |
| `EffectsFolder` | Particle prefab output folder | `Assets/Prefabs/Effects` |
| `AutoApplyKey` | Prevents repeated automatic application | Editor preference key |

## Main Functions

| Function | What it does | When it runs |
| --- | --- | --- |
| `QueueAutomaticApply()` | Queues setup after editor load | Unity Editor loads scripts |
| `ApplyAutomatically()` | Waits for a safe editor state and applies once | Editor delay callback |
| `ApplyVersion5()` | Runs the complete Version 5 setup | Menu command or automatic call |
| `ApplyFromCommandLine()` | Applies setup and exits the editor | Command-line editor execution |
| `ConfigurePlayer()` | Sets player layer, health, combat, dagger, and particles | Version 5 setup |
| `ConfigureEnemy()` | Sets patrol, shooter, gun, layers, and particles | Version 5 setup |
| `RebuildPatrolPoints()` | Creates twelve fixed waypoint objects | Version 5 setup |
| `CreateWaypoint()` | Creates one named route point | Patrol setup |
| `ConfigureWalls()` | Assigns the Wall layer | Version 5 setup |
| `AddMazeWalls()` | Creates the additional maze blocks | Version 5 setup |
| `CreateMazeWall()` | Configures one wall object | Maze setup |
| `CreateParticlePrefab()` | Creates a reusable particle prefab | Missing prefab setup |
| `CreateDaggerSprite()` | Generates the dagger image | Weapon setup |
| `CreateGunSprite()` | Generates the gun image | Weapon setup |
| `SaveSprite()` | Saves and imports generated sprite data | Weapon setup |
| `GetOrCreateChild()` | Reuses or creates a child object | Scene setup |
| `GetOrAdd<T>()` | Reuses or adds a component | Scene setup |
| `RequireObject()` | Finds a required scene object | Scene validation |
| `EnsureFolder()` | Creates a required asset folder | Asset setup |
| `EnsureLayer()` | Creates or updates a Unity layer | Layer setup |

## Function Signatures and Parameters

| Signature | Parameters | Return value |
| --- | --- | --- |
| `private static void QueueAutomaticApply()` | None | None |
| `private static void ApplyAutomatically()` | None | None |
| `public static void ApplyVersion5()` | None | None |
| `public static void ApplyFromCommandLine()` | None | None |
| `private static void ConfigurePlayer(GameObject player, Sprite daggerSprite, ParticleSystem playerParticles)` | Player object and generated assets | None |
| `private static void ConfigureEnemy(GameObject enemy, Sprite gunSprite, ParticleSystem enemyParticles, Transform player, Transform patrolPoints, string waypointPrefix, float moveSpeed, float shootingRange, int damage, float shootCooldown)` | Enemy object plus all patrol/shooting settings | None |
| `private static Transform RebuildPatrolPoints(Transform enemiesRoot)` | `enemiesRoot`: parent for `PatrolPoints` | Returns `PatrolPoints` transform |
| `private static void CreateWaypoint(Transform parent, string name, float x, float y)` | Parent, object name, and coordinates | None |
| `private static void ConfigureWalls(GameObject wallsRoot)` | `wallsRoot`: Walls container | None |
| `private static void AddMazeWalls(Transform wallsRoot)` | `wallsRoot`: parent transform | None |
| `private static void CreateMazeWall(Transform parent, string name, Vector2 position, Vector2 scale, Sprite wallSprite)` | Parent, name, transform values, and sprite | None |
| `private static ParticleSystem CreateParticlePrefab(string name, Color startColor, Color endColor)` | Prefab name and two colors | Returns prefab Particle System |
| `private static Sprite CreateDaggerSprite()` | None | Returns imported dagger sprite |
| `private static Sprite CreateGunSprite()` | None | Returns imported gun sprite |
| `private static Sprite SaveSprite(string path, int width, int height, Color32[] pixels, float pixelsPerUnit)` | File and texture import values | Returns imported sprite |
| `private static Color32[] NewTransparentPixels(int width, int height)` | Texture dimensions | Returns empty pixel array |
| `private static void FillRect(Color32[] pixels, int width, int height, int minX, int minY, int maxX, int maxY, Color32 color)` | Pixel array, bounds, and color | None |
| `private static GameObject GetOrCreateChild(Transform parent, string name)` | Parent and child name | Existing or new child |
| `private static T GetOrAdd<T>(GameObject target) where T : Component` | Target object and component type `T` | Existing or new component |
| `private static GameObject RequireObject(string name)` | Required scene object name | Found object or exception |
| `private static void EnsureFolder(string parent, string child)` | Parent folder and child folder name | None |
| `private static void EnsureLayer(int index, string layerName)` | Layer index and layer name | None |

## Parameter Details

### `ConfigurePlayer(...)`

| Parameter | Type | Meaning |
| --- | --- | --- |
| `player` | `GameObject` | Player object being configured |
| `daggerSprite` | `Sprite` | Generated dagger visual |
| `playerParticles` | `ParticleSystem` | Player death prefab component |

### `ConfigureEnemy(...)`

| Parameter | Type | Meaning |
| --- | --- | --- |
| `enemy` | `GameObject` | Enemy being configured |
| `gunSprite` | `Sprite` | Generated gun visual |
| `enemyParticles` | `ParticleSystem` | Enemy death effect |
| `player` | `Transform` | Shooting target |
| `patrolPoints` | `Transform` | Parent containing waypoint objects |
| `waypointPrefix` | `string` | Prefix such as `Enemy01` |
| `moveSpeed` | `float` | Patrol speed |
| `shootingRange` | `float` | Maximum gun range |
| `damage` | `int` | Damage per successful shot |
| `shootCooldown` | `float` | Time between shots |

### `CreateMazeWall(...)`

| Parameter | Type | Meaning |
| --- | --- | --- |
| `parent` | `Transform` | Walls container |
| `name` | `string` | Exact wall object name |
| `position` | `Vector2` | Local X and Y position |
| `scale` | `Vector2` | Local X and Y scale |
| `wallSprite` | `Sprite` | Existing industrial wall sprite |

### Sprite Helper Parameters

| Parameter | Type | Meaning |
| --- | --- | --- |
| `path` | `string` | Output PNG path |
| `width`, `height` | `int` | Texture dimensions |
| `pixels` | `Color32[]` | Raw pixel colors |
| `pixelsPerUnit` | `float` | Unity sprite scale setting |
| `minX`, `minY`, `maxX`, `maxY` | `int` | Rectangle drawing bounds |
| `color` | `Color32` | Rectangle pixel color |

## Important Code Lines

| Source lines | Code | Explanation |
| --- | --- | --- |
| `14–15` | `[InitializeOnLoadMethod]` | Registers automatic editor initialization. |
| `17–22` | Editor preference check | Avoids repeatedly applying the setup. |
| `27–30` | Compile/Play Mode guard | Delays scene editing until Unity is safe. |
| `35–40` | Apply and exception handling | Runs setup and reports failures clearly. |
| `44–45` | `[MenuItem(...)]` | Adds a manual Unity menu command. |
| `47` | `OpenScene(..., Single)` | Opens only `MainScene` for configuration. |
| `49–52` | Folder and layer setup | Ensures Weapons, Effects, Player, and Wall configuration. |
| `54–65` | Asset generation calls | Creates dagger, gun, and both death effects. |
| `67–73` | Required object lookup | Gets Player, Enemies, Walls, and rebuilds patrol points. |
| `75–110` | Three enemy configurations | Supplies exact speed, range, damage, and cooldown values. |
| `114–117` | Save operations | Marks scene dirty, saves scene/assets, and refreshes imports. |
| `139–142` | Player core setup | Assigns Player layer, start position, and health 5. |
| `144–149` | Old attack disable | Keeps `PlayerAttack` but disables it. |
| `151–153` | Contact/effect setup | Adds contact kill and assigns player particles. |
| `155–164` | Dagger setup | Creates visual child and sets transform/render values. |
| `180` | Enemy layer assignment | Places enemies on the Enemy layer. |
| `182–187` | Touch-damage disable | Makes body contact harmless. |
| `189–198` | Patrol assignment | Creates a four-item waypoint array and resolves exact names. |
| `200–206` | Shooter assignment | Applies player target, balance values, and masks. |
| `208–220` | Enemy particles and gun | Assigns death effects and creates visual Gun child. |
| `228–231` | Old waypoint cleanup | Removes existing patrol children before rebuilding. |
| `233–246` | Exact waypoint definitions | Creates all twelve route transforms. |
| `260–265` | Wall layer loop | Applies Wall layer to every direct wall child. |
| `278–285` | Maze wall definitions | Creates the eight Version 5 maze blocks. |
| `298–309` | Individual wall setup | Reuses/creates object, sets transform, renderer, and collider. |
| `318–324` | Existing prefab reuse | Avoids recreating a particle prefab that already exists. |
| `326–352` | Particle creation | Configures a short, non-looping burst and saves it as a prefab. |
| `355–394` | Weapon drawing | Generates dagger and gun sprites from colored pixel rectangles. |
| `404–419` | Sprite import | Writes PNG data and configures Unity's sprite importer. |
| `438–444` | Pixel rectangle loops | Fills a bounded section of the generated texture. |
| `447–459` | Child reuse helper | Prevents duplicate child objects. |
| `461–465` | Generic component helper | Prevents duplicate components. |
| `467–477` | Required object helper | Throws a clear error if a required object is missing. |
| `479–487` | Folder helper | Creates an asset folder only when absent. |
| `489–501` | Layer helper | Edits the Unity layer list only when needed. |

## Execution Sequence — Editor

1. Unity loads editor scripts.
2. `QueueAutomaticApply()` checks the Editor preference.
3. `ApplyAutomatically()` waits until Unity is not compiling or entering Play Mode.
4. `ApplyVersion5()` opens `MainScene`.
5. Required folders and layers are confirmed.
6. Weapon sprites and particle prefabs are generated or reused.
7. Player configuration is applied.
8. Patrol points are rebuilt.
9. All three enemies are configured.
10. Maze walls are created or updated.
11. Scene and assets are saved.
12. A setup summary is logged.

## How It Works In Game

This script does not run as part of the built game. It prepared `MainScene` in the Unity Editor. The runtime behavior is handled by the gameplay scripts that it attached and configured.

## Linked Scripts

- Configures `PlayerHealth`.
- Disables `PlayerAttack`.
- Adds or enables `PlayerContactKill`.
- Configures `EnemyHealth`, `EnemyPatrol`, and `EnemyShooter`.
- Disables `EnemyTouchDamage`.
- Assigns `DeathEffectSpawner`.

## Linked Scene Objects

- `MainScene`
- `Player`
- `Dagger`
- `Enemies`
- `Enemy_01`
- `Enemy_02`
- `Enemy_03`
- Enemy `Gun` children
- `PatrolPoints`
- Wall objects

## Why This Approach Is Used

Repeatable editor setup

## Notes For Presentation

“This is an editor-only setup script, not gameplay code. It prepared the scene by assigning layers, components, patrol points, weapon visuals, maze walls, and particle prefabs.”
