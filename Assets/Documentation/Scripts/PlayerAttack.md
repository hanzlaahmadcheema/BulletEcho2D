# PlayerAttack

## Purpose

This is the older Space-key attack script. It searches for enemies inside a circular range and damages them, but it is not used by the current contact-kill gameplay.

**Status:** Disabled but kept for old support.

## Attached Scene Object

- `Player` — component is disabled in `MainScene`

## Main Responsibility

- Legacy ranged-area melee check
- Space-key attack input

## Important Variables / Inspector Fields

| Field | Meaning | Example value |
| --- | --- | --- |
| `attackRange` | Radius used to search for enemies | `2` in `MainScene` |
| `damage` | Damage sent to enemy health | `1` |
| `attackCooldown` | Delay between attacks | `0.4` |
| `enemyLayer` | Limits overlap checks to enemies | `Enemy` layer |
| `nextAttackTime` | Stores the next allowed attack time | Runtime value |

## Main Functions

| Function | What it does | When it runs |
| --- | --- | --- |
| `Update()` | Checks Space key and cooldown | Every frame if enabled |
| `Attack()` | Finds nearby enemy colliders and damages them | Space attack is accepted |
| `OnDrawGizmosSelected()` | Draws the attack range in the editor | Player is selected |

## Function Signatures and Parameters

| Signature | Parameters | Return value |
| --- | --- | --- |
| `private void Update()` | None | None |
| `private void Attack()` | None | None |
| `private void OnDrawGizmosSelected()` | None | None |

There are no method parameters because the script reads the player's transform, keyboard, and Inspector fields directly. The component is disabled in the current scene.

## Important Code Lines

| Source lines | Code | Explanation |
| --- | --- | --- |
| `15–18` | `gameEnded` guard | Would prevent old attacks after the game ends. |
| `20–23` | Keyboard guard | Avoids accessing a missing keyboard device. |
| `25` | Space key and cooldown check | Would start an attack only when both conditions pass. |
| `28` | `Attack();` | Calls the area-search method. |
| `29` | Cooldown assignment | Delays the next allowed attack. |
| `35–37` | `Physics2D.OverlapCircleAll()` | Finds colliders inside the attack radius, optionally filtered by Enemy layer. |
| `39–53` | Hit loop | Checks every detected collider. |
| `41–46` | Health lookup | Searches the collider and its parent for `EnemyHealth`. |
| `51` | `enemyHealth.TakeDamage(damage);` | Would apply configured damage. |
| `58–59` | Gizmo drawing | Shows the attack radius in the Scene view. |

## Execution Sequence If Enabled

1. `Update()` checks game state and keyboard availability.
2. Space press and cooldown are checked.
3. `Attack()` collects colliders in a circle.
4. Each collider is checked for `EnemyHealth`.
5. Found enemies receive damage.
6. The next attack time is stored.

Current execution stops before this sequence because the component is disabled.

## How It Works In Game

The script does not currently run because its component is disabled. Current combat uses `PlayerContactKill`, so the player eliminates enemies through physical contact instead of pressing Space.

## Linked Scripts

- Would call `EnemyHealth.TakeDamage(int)` if enabled.
- Reads `GameManager.Instance.gameEnded`.
- Replaced in current gameplay by `PlayerContactKill`.

## Linked Scene Objects

- `Player`
- Enemy objects
- `Enemy` layer

## Why This Approach Is Used

Legacy attack support

## Notes For Presentation

“This script is the old Space-key attack system. It remains in the project for support, but it is disabled because the final design uses player contact to eliminate enemies.”
