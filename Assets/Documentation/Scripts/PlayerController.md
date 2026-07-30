# PlayerController

## Purpose

This script reads keyboard input and moves the player in four directions. It uses the player's `Rigidbody2D` so movement works correctly with wall colliders.

**Status:** Active.

## Attached Scene Object

- `Player`

## Main Responsibility

- Player input
- Player movement
- Stop movement after game end

## Important Variables / Inspector Fields

| Field | Meaning | Example value |
| --- | --- | --- |
| `moveSpeed` | Controls player movement speed | `5` |
| `rb` | Stores the player's `Rigidbody2D` | Player component |
| `movement` | Stores the current input direction | `(x, y)` |

## Main Functions

| Function | What it does | When it runs |
| --- | --- | --- |
| `Awake()` | Gets the attached `Rigidbody2D` | When the object initializes |
| `Update()` | Reads WASD and arrow-key input | Every rendered frame |
| `FixedUpdate()` | Moves the rigidbody using the stored direction | Every physics step |

## Function Signatures and Parameters

| Signature | Parameters | Return value |
| --- | --- | --- |
| `private void Awake()` | None | None |
| `private void Update()` | None | None |
| `private void FixedUpdate()` | None | None |

Unity calls these functions automatically. They are `private` because no other script needs to call them directly.

## Important Code Lines

| Source lines | Code | Explanation |
| --- | --- | --- |
| `4` | `[RequireComponent(typeof(Rigidbody2D))]` | Unity must keep a `Rigidbody2D` on the same object. |
| `14` | `rb = GetComponent<Rigidbody2D>();` | Saves the player rigidbody reference once during initialization. |
| `19–23` | `gameEnded` check | Clears movement and stops input after win or lose. |
| `25–29` | `Keyboard.current == null` check | Prevents errors if no keyboard device is available. |
| `34–52` | Key checks | Converts A/D or Left/Right into horizontal movement and W/S or Up/Down into vertical movement. |
| `54` | `new Vector2(horizontal, vertical).normalized` | Prevents diagonal movement from being faster than straight movement. |
| `64–65` | Position calculation | Uses direction, speed, and fixed physics time to calculate the next position. |
| `67` | `rb.MovePosition(nextPosition);` | Moves through the 2D physics system instead of directly changing the transform. |

## Execution Sequence

1. `Awake()` stores the `Rigidbody2D`.
2. `Update()` checks whether the game has ended.
3. `Update()` reads keyboard directions.
4. The direction is normalized and stored in `movement`.
5. `FixedUpdate()` calculates `nextPosition`.
6. `Rigidbody2D.MovePosition()` moves the player.

## How It Works In Game

`Update()` checks the keyboard and creates a normalized direction. `FixedUpdate()` adds movement based on `moveSpeed` and calls `Rigidbody2D.MovePosition()`. If `GameManager.gameEnded` is true, movement stops.

## Linked Scripts

- Reads `GameManager.Instance.gameEnded`.
- Is disabled by `GameManager.DisablePlayerGameplay()` after win or lose.

## Linked Scene Objects

- `Player`
- `GameManager`
- Wall collider objects

## Why This Approach Is Used

Physics-safe movement

## Notes For Presentation

“This script controls player movement using WASD or arrow keys. It moves the Rigidbody2D in FixedUpdate so collision with maze walls remains reliable.”
