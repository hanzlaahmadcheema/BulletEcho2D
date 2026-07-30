# CashCollectible

## Purpose

This script controls each collectible objective. It detects the player entering the trigger, updates the objective count, and removes the collected object.

**Status:** Active.

## Attached Scene Object

- `Cash_01`
- `Cash_02`
- `Cash_03`
- `Cash_04`
- `Cash_05`

## Main Responsibility

- Objective detection
- Objective collection
- Objective counter update

## Important Variables / Inspector Fields

| Field | Meaning | Example value |
| --- | --- | --- |
| `cashAmount` | Progress added when collected | `1` |
| `collected` | Prevents repeated collection | `false` initially |

## Main Functions

| Function | What it does | When it runs |
| --- | --- | --- |
| `OnTriggerEnter2D()` | Checks for player health, updates count, and destroys the object | Player enters collectible trigger |

## Function Signatures and Parameters

| Signature | Parameters | Return value |
| --- | --- | --- |
| `private void OnTriggerEnter2D(Collider2D other)` | `other`: collider entering the collectible trigger | None |

Unity supplies the `other` parameter automatically. The script uses it to verify that the entering object is the player.

## Important Code Lines

| Source lines | Code | Explanation |
| --- | --- | --- |
| `5` | `public int cashAmount = 1;` | Defines how much objective progress this object gives. |
| `7` | `private bool collected;` | Prevents the same collectible from being counted twice. |
| `11–14` | Collection guard | Rejects repeated entry and objects without `PlayerHealth`. |
| `16` | `collected = true;` | Locks collection before calling other systems. |
| `18–21` | Manager call | Sends `cashAmount` to `GameManager` if the manager exists. |
| `23` | `Destroy(gameObject);` | Removes the collected token from the scene. |

## Parameter Details

| Parameter | Type | Meaning |
| --- | --- | --- |
| `other` | `Collider2D` | The collider that entered this object's trigger |

The player is identified by checking `other.GetComponent<PlayerHealth>()`.

## Execution Sequence

1. Player enters the collectible's trigger collider.
2. Unity calls `OnTriggerEnter2D(other)`.
3. The script checks `collected`.
4. It confirms that `other` has `PlayerHealth`.
5. It locks the collectible.
6. `GameManager.CollectCash(cashAmount)` updates progress.
7. The collectible destroys itself.

## How It Works In Game

Each collectible has a trigger collider. The script confirms that the entering object has `PlayerHealth`, marks the collectible as used, calls `GameManager.CollectCash(1)`, and destroys the collectible.

## Linked Scripts

- Uses `PlayerHealth` to identify the player.
- Calls `GameManager.CollectCash(int)`.
- `GameManager` calls `UIManager.UpdateCash()`.

## Linked Scene Objects

- `Cash`
- `Cash_01` to `Cash_05`
- `Player`
- `GameManager`
- `Canvas/CashText`

## Why This Approach Is Used

Automatic trigger pickup

## Notes For Presentation

“This script is attached to every objective token. When the player enters its trigger, it adds one objective to the manager and removes the collected token.”
