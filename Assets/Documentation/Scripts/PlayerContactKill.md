# PlayerContactKill

## Purpose

This script implements the current player attack. When the player collider touches an enemy collider, it finds the enemy health component and eliminates that enemy.

**Status:** Active.

## Attached Scene Object

- `Player`

## Main Responsibility

- Contact combat
- Enemy detection
- Instant enemy elimination

## Important Variables / Inspector Fields

This script has no public Inspector fields.

| Field | Meaning | Example value |
| --- | --- | --- |
| None | Uses collision callback data directly | Not applicable |

## Main Functions

| Function | What it does | When it runs |
| --- | --- | --- |
| `OnCollisionEnter2D()` | Sends a normal collision to the kill check | Player collides with another collider |
| `OnTriggerEnter2D()` | Sends a trigger contact to the kill check | Player enters a trigger |
| `TryKillEnemy()` | Finds `EnemyHealth` and calls `KillInstantly()` | After a contact callback |

## Function Signatures and Parameters

| Signature | Parameters | Return value |
| --- | --- | --- |
| `private void OnCollisionEnter2D(Collision2D collision)` | `collision`: Unity collision information | None |
| `private void OnTriggerEnter2D(Collider2D other)` | `other`: collider entering the trigger | None |
| `private void TryKillEnemy(Collider2D other)` | `other`: collider that may belong to an enemy | None |

Both Unity contact callbacks pass a `Collider2D` into the same helper. This supports normal colliders and trigger-based enemy layouts.

## Important Code Lines

| Source lines | Code | Explanation |
| --- | --- | --- |
| `5–8` | `OnCollisionEnter2D()` | Handles normal physical collision. |
| `10–13` | `OnTriggerEnter2D()` | Handles trigger contact. |
| `17–20` | Contact guard | Rejects null colliders and prevents kills after game end. |
| `22` | `other.GetComponent<EnemyHealth>()` | First checks the exact contacted object. |
| `24–27` | `GetComponentInParent<EnemyHealth>()` | Supports colliders placed on an enemy child object. |
| `29` | Enemy validity check | Requires a found and living enemy. |
| `31` | `enemyHealth.KillInstantly();` | Starts the enemy death flow immediately. |

## Execution Sequence

1. Player touches another collider.
2. Unity calls a collision or trigger callback.
3. The callback passes the collider to `TryKillEnemy()`.
4. The helper checks the game state.
5. It searches the object and parent for `EnemyHealth`.
6. A living enemy receives `KillInstantly()`.

## How It Works In Game

The script checks both the contacted object and its parent for `EnemyHealth`. If an alive enemy is found and the game has not ended, the enemy is killed immediately. No keyboard attack button is required.

## Linked Scripts

- Calls `EnemyHealth.KillInstantly()`.
- Reads `GameManager.Instance.gameEnded`.

## Linked Scene Objects

- `Player`
- `Enemy_01`
- `Enemy_02`
- `Enemy_03`

## Why This Approach Is Used

Simple melee combat

## Notes For Presentation

“The player attacks by touching an enemy. This script detects the contact and calls the enemy health script to eliminate that enemy instantly.”
