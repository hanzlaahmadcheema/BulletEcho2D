# EnemyTouchDamage

## Purpose

This script file is retained for compatibility with the older enemy contact-damage design. Its current class contains no damage behavior, and its components are disabled.

**Status:** Disabled but kept for legacy support.

## Attached Scene Object

- `Enemy_01` — disabled
- `Enemy_02` — disabled
- `Enemy_03` — disabled

## Main Responsibility

- Legacy enemy contact damage placeholder
- Preserve older scene component references

## Important Variables / Inspector Fields

The current script has no fields.

| Field | Meaning | Example value |
| --- | --- | --- |
| None | Contact damage was intentionally removed | Not applicable |

## Main Functions

The current script has no runtime functions.

| Function | What it does | When it runs |
| --- | --- | --- |
| None | Enemy body contact is harmless | Not applicable |

## Function Signatures and Parameters

The class currently declares no functions, parameters, fields, or return values.

```csharp
public class EnemyTouchDamage : MonoBehaviour
{
}
```

## Important Code Lines

| Source lines | Code | Explanation |
| --- | --- | --- |
| `3` | `public class EnemyTouchDamage : MonoBehaviour` | Keeps the old component type valid for scene compatibility. |
| `5–6` | Documentation comments | Clearly state that enemy body contact is intentionally harmless. |

The important behavior is the absence of collision damage code. The components are also disabled on all four enemies.

## Execution Sequence

There is no runtime execution sequence:

1. The component remains attached but disabled.
2. No collision callback exists.
3. No player damage method is called.
4. Enemy damage is handled only by `EnemyShooter`.

## How It Works In Game

It does not perform any action. Touching an enemy does not damage the player. Instead, `PlayerContactKill` detects the contact and eliminates the enemy.

## Linked Scripts

- Replaced by `EnemyShooter` for enemy damage.
- Works with the design of `PlayerContactKill`.

## Linked Scene Objects

- `Enemy_01`
- `Enemy_02`
- `Enemy_03`
- `Player`

## Why This Approach Is Used

Legacy scene support

## Notes For Presentation

“This is an old support component. It is disabled because enemies no longer damage the player by body contact; enemy damage now comes only from the shooting script.”
