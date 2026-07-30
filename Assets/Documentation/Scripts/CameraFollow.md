# CameraFollow

## Purpose

This script makes the main camera follow the player smoothly. It also clamps the camera position so it remains inside the Level 1 map limits.

**Status:** Active.

## Attached Scene Object

- `Main Camera`

## Main Responsibility

- Player tracking
- Camera smoothing
- Camera boundary control

## Important Variables / Inspector Fields

| Field | Meaning | Example value |
| --- | --- | --- |
| `target` | Transform followed by the camera | `Player` |
| `smoothSpeed` | Camera interpolation speed | `8` |
| `minBounds` | Minimum allowed camera position | `(-9, -10)` |
| `maxBounds` | Maximum allowed camera position | `(9, 16)` |

## Main Functions

| Function | What it does | When it runs |
| --- | --- | --- |
| `LateUpdate()` | Clamps the target position and smoothly moves the camera | After normal frame updates |

## Function Signatures and Parameters

| Signature | Parameters | Return value |
| --- | --- | --- |
| `private void LateUpdate()` | None | None |

Unity automatically calls `LateUpdate()` after regular `Update()` calls. This lets the player move first and the camera follow the latest position.

## Important Code Lines

| Source lines | Code | Explanation |
| --- | --- | --- |
| `12–15` | `target == null` guard | Prevents a missing-reference error. |
| `17–21` | `new Vector3(...)` | Builds the camera's desired position. |
| `18` | X-axis `Mathf.Clamp()` | Keeps horizontal movement inside bounds. |
| `19` | Y-axis `Mathf.Clamp()` | Keeps vertical movement inside bounds. |
| `20` | `-10f` | Keeps the 2D camera at the required Z distance. |
| `23–27` | `Vector3.Lerp()` | Smoothly moves from the current position toward the target. |
| `26` | `smoothSpeed * Time.deltaTime` | Makes smoothing frame-rate independent. |

## Execution Sequence

1. Player movement updates its transform.
2. Unity calls `LateUpdate()`.
3. The script checks that `target` exists.
4. Player X and Y positions are clamped.
5. A target camera position is created.
6. `Vector3.Lerp()` smoothly updates the camera.

## How It Works In Game

The script reads the player's position, limits it to the configured map bounds, and uses `Vector3.Lerp()` to move the camera. The camera keeps its Z position at `-10`.

## Linked Scripts

- Follows the transform of the object controlled by `PlayerController`.

## Linked Scene Objects

- `Main Camera`
- `Player`
- Level 1 map bounds

## Why This Approach Is Used

Smooth bounded tracking

## Notes For Presentation

“This script follows the player after movement has been calculated. It uses smooth interpolation and map limits so the camera does not move outside the playable level.”
