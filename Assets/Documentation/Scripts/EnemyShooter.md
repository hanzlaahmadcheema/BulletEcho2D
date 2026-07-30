# EnemyShooter

## Purpose

This script controls enemy gun attacks without creating bullets. It checks player distance, shooting cooldown, and wall-blocked line-of-sight before applying damage.

**Status:** Active.

## Attached Scene Object

- `Enemy_01`
- `Enemy_02`
- `Enemy_03`

## Main Responsibility

- Enemy shooting range
- Shooting cooldown
- Line-of-sight check
- Player damage

## Important Variables / Inspector Fields

| Field | Meaning | Example value |
| --- | --- | --- |
| `player` | Player transform target | `Player` |
| `shootingRange` | Maximum shooting distance | `2.5`, `2.8`, `3.0` |
| `damage` | Health removed per shot | `1` |
| `shootCooldown` | Delay between shots | `1.6`, `1.5`, `1.4` |
| `wallLayer` | Layer used for cover detection | `Wall` |
| `playerLayer` | Layer used for target detection | `Player` |
| `nextShootTime` | Next allowed shooting time | Runtime value |
| `enemyHealth` | Attached health reference | Enemy component |

## Main Functions

| Function | What it does | When it runs |
| --- | --- | --- |
| `Awake()` | Gets `EnemyHealth` | Enemy initialization |
| `Update()` | Checks state, range, cooldown, visibility, and damage | Every frame |
| `HasClearLineOfSight()` | Linecasts using Wall and Player layers | Before each shot |
| `OnDrawGizmosSelected()` | Draws the shooting range in the editor | Enemy is selected |

## Function Signatures and Parameters

| Signature | Parameters | Return value |
| --- | --- | --- |
| `private void Awake()` | None | None |
| `private void Update()` | None | None |
| `private bool HasClearLineOfSight()` | None | `true` only when the first valid hit is the player |
| `private void OnDrawGizmosSelected()` | None | None |

`HasClearLineOfSight()` returns a Boolean result to `Update()`. It takes no parameter because it uses the stored `player`, `wallLayer`, and `playerLayer` fields.

## Important Code Lines

| Source lines | Code | Explanation |
| --- | --- | --- |
| `5–10` | Public shooter fields | Configure target, range, damage, cooldown, and linecast layers. |
| `17` | `GetComponent<EnemyHealth>()` | Stores health so dead enemies cannot shoot. |
| `22–25` | Game-end guard | Stops shooting after win or lose. |
| `27–30` | Enemy-death guard | Stops a dead enemy from attacking. |
| `32–36` | Player fallback search | Finds `PlayerHealth` if the Inspector reference is missing. |
| `38–41` | Range check | Rejects missing or out-of-range targets. |
| `43–46` | Cooldown and visibility check | Requires both shooting time and clear line-of-sight. |
| `48–53` | Health lookup | Finds `PlayerHealth` on the target or parent. |
| `57` | `targetHealth.TakeDamage(damage);` | Applies one point with current scene values. |
| `58` | Next shot time | Starts the enemy-specific cooldown. |
| `64` | Combined layer mask | Uses bitwise OR to include Wall and Player layers. |
| `65` | `Physics2D.Linecast(...)` | Finds the first collider between enemy and player. |
| `67–70` | No-hit result | Treats no collider as no valid shot. |
| `72–77` | Hit identity check | Searches the first hit collider or parent for `PlayerHealth`. |
| `79` | `return hitPlayer != null;` | Allows shooting only when the player was hit first. |
| `84–85` | Range gizmo | Draws the configured radius for editor inspection. |

## Function Parameter Details

Although the methods have no explicit parameters, the shooting operation uses these configured inputs:

| Input | Type | Use |
| --- | --- | --- |
| `player` | `Transform` | Start-to-end target for distance and linecast |
| `shootingRange` | `float` | Maximum allowed distance |
| `damage` | `int` | Amount passed to `TakeDamage()` |
| `shootCooldown` | `float` | Seconds added to `nextShootTime` |
| `wallLayer` | `LayerMask` | Allows walls to block the cast |
| `playerLayer` | `LayerMask` | Allows the cast to detect the player |

## Execution Sequence

1. `Update()` checks game and enemy state.
2. Missing player reference is recovered if possible.
3. Distance is compared with `shootingRange`.
4. `Time.time` is compared with `nextShootTime`.
5. `HasClearLineOfSight()` performs the linecast.
6. A first wall hit returns `false`.
7. A first player hit returns `true`.
8. `PlayerHealth.TakeDamage(damage)` runs.
9. The next allowed shooting time is stored.

## How It Works In Game

The enemy first checks whether the player is inside range. It then performs `Physics2D.Linecast()` using a combined Wall and Player mask. The enemy damages the player only if the first detected collider belongs to the player. A wall hit blocks the shot.

No bullet, projectile, or beam object is created.

## Linked Scripts

- Calls `PlayerHealth.TakeDamage(damage)`.
- Reads `EnemyHealth.IsDead`.
- Reads `GameManager.Instance.gameEnded`.

## Linked Scene Objects

- `Enemy_01`
- `Enemy_02`
- `Enemy_03`
- `Player`
- Wall objects
- Enemy `Gun` child objects

## Why This Approach Is Used

Wall-blocked shooting

## Notes For Presentation

“This script simulates enemy gunfire without bullets. It checks distance and uses a 2D linecast, so walls block the attack and the player loses one health only when visible.”
